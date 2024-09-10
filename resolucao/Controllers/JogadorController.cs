using AutoMapper;
using Desafio_UOL.Data;
using Desafio_UOL.Exceptions;
using Desafio_UOL.Models;
using Desafio_UOL.Services;
using Desafio_UOL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace Desafio_UOL.Controllers
{
    public class JogadorController : Controller
    {
        private readonly IJogadorService _jogadorService;
        private readonly ICodinomeService _codinomeService;
        private readonly IGrupoService _grupoService;
        private readonly IMapper _mapper;

        public JogadorController(IMapper mapper, IJogadorService jogadorService, ICodinomeService codinomeService, IGrupoService grupoService)
        {
            _jogadorService = jogadorService;
            _codinomeService = codinomeService;
            _grupoService = grupoService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var grupos = _grupoService.GetAll();

                if (grupos.Count() == 0)
                {
                    TempData["success"] = "Codinomes carregados com sucesso.";
                    _codinomeService.LoadCodenames();
                }

                var jogadores = _jogadorService.GetAll();
                var jogadoresViewModel = new List<JogadorExibicaoViewModel>();

                foreach (JogadorModel jogador in jogadores)
                {
                    var jogadorViewModel = _mapper.Map<JogadorExibicaoViewModel>(jogador);
                    jogadoresViewModel.Add(jogadorViewModel);
                }

                return View(jogadoresViewModel);

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Filter(long groupId)
        {
            try
            {
                var grupo = _grupoService.GetById(groupId);

                if (grupo != null)
                {

                    var jogadores = _jogadorService.GetAllByGroupId(grupo.Id);
                    var jogadoresViewModel = new List<JogadorExibicaoViewModel>();

                    foreach (JogadorModel jogador in jogadores)
                    {
                        var jogadorViewModel = _mapper.Map<JogadorExibicaoViewModel>(jogador);
                        jogadoresViewModel.Add(jogadorViewModel);
                    }

                    return View(jogadoresViewModel);
                }
                else
                {
                    TempData["error"] = "Grupo não encontrado.";
                    return RedirectToAction(nameof(Index));

                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Chart()
        {
            try
            {
                var jogadores = _jogadorService.GetAll();
                var jogadoresViewModel = new List<JogadorExibicaoViewModel>();

                foreach (JogadorModel jogador in jogadores)
                {
                    var jogadorViewModel = _mapper.Map<JogadorExibicaoViewModel>(jogador);
                    jogadoresViewModel.Add(jogadorViewModel);
                }
                return View(jogadoresViewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                var cadastroViewModel = new JogadorCadastroViewModel
                {
                    Grupos = new SelectList(_grupoService.GetAll(), "Id", "Nome")
                };

                return View(cadastroViewModel);

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Create(JogadorCadastroViewModel jogadorCadastroViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jogadorPersistir = new JogadorModel();
                    _mapper.Map(jogadorCadastroViewModel, jogadorPersistir);
                    jogadorPersistir.CodinomeId = _codinomeService.GetCodenameAvailable(jogadorCadastroViewModel.GrupoId).Id;
                    jogadorPersistir.Email = jogadorPersistir.Email.ToLower();


                    _jogadorService.Add(jogadorPersistir);
                    TempData["success"] = "Jogador cadastrado com sucesso.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["error"] = ex.Message;

                    if (ex.InnerException.Message.StartsWith("ORA-00001: unique constraint (UOL.IX_T_UOL_JOGADOR_Email) violated"))
                    {
                        TempData["error"] = "Email já cadastrado.";
                    }

                    var cadastroViewModel = new JogadorCadastroViewModel
                    {
                        Grupos = new SelectList(_grupoService.GetAll(), "Id", "Nome")
                    };

                    return View(cadastroViewModel);
                }
                catch (Exception ex)
                {
                TempData["error"] = ex.Message;
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                try
                {
                    var cadastroViewModel = new JogadorCadastroViewModel
                    {
                        Grupos = new SelectList(_grupoService.GetAll(), "Id", "Nome")
                    };

                    return View(cadastroViewModel);
                }

                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        [HttpGet]
        public IActionResult Update(long id)
        {
            try
            {
                var jogador = _jogadorService.GetById(id);
                if (jogador != null)
                {
                    var updateViewModel = _mapper.Map<JogadorUpdateViewModel>(jogador);
                    updateViewModel.Grupos = new SelectList(_grupoService.GetAll(), "Id", "Nome");
                    var codinome = _codinomeService.GetById(jogador.CodinomeId);
                    updateViewModel.GroupIdAtual = codinome.GrupoId;
                    updateViewModel.GrupoId = codinome.GrupoId;
                    return View(updateViewModel);
                }
                else
                {
                    TempData["error"] = "Jogador não encontrado";
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Update(JogadorUpdateViewModel jogadorUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jogadorAtualizar = _jogadorService.GetById(jogadorUpdateViewModel.Id);
                    _mapper.Map(jogadorUpdateViewModel, jogadorAtualizar);

                    if (jogadorUpdateViewModel.GroupIdAtual != jogadorUpdateViewModel.GrupoId)
                    {
                        jogadorAtualizar.CodinomeId = _codinomeService.GetCodenameAvailable(jogadorUpdateViewModel.GrupoId).Id;
                    }
                    jogadorAtualizar.Email = jogadorAtualizar.Email.ToLower();

                    _jogadorService.Update(jogadorAtualizar);
                    TempData["success"] = "Jogador atualizado com sucesso.";
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateException ex)
                {
                    TempData["error"] = ex.Message;

                    if (ex.InnerException.Message.StartsWith("ORA-00001: unique constraint (UOL.IX_T_UOL_JOGADOR_Email) violated")) {
                        TempData["error"] = "Email já cadastrado.";
                    }

                    var jogador = _jogadorService.GetById(jogadorUpdateViewModel.Id);
                    var updateViewModel = _mapper.Map<JogadorUpdateViewModel>(jogador);
                    updateViewModel.Grupos = new SelectList(_grupoService.GetAll(), "Id", "Nome");
                    var codinome = _codinomeService.GetById(jogador.CodinomeId);
                    updateViewModel.GroupIdAtual = codinome.GrupoId;
                    updateViewModel.GrupoId = codinome.GrupoId;
                    return View(updateViewModel);
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                try
                {
                    jogadorUpdateViewModel.Grupos = new SelectList(_grupoService.GetAll(), "Id", "Nome");
                    return View(jogadorUpdateViewModel);

                }

                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToAction(nameof(Index));
                }
            }
        }


        [HttpGet]
        public IActionResult Delete(long id)
        {
            try
            {
                var jogador = _jogadorService.GetById(id);
                if (jogador != null)
                {
                    TempData["success"] = "Jogador excluído com sucesso";
                    _jogadorService.Delete(jogador.Id);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "Jogador não encontrado";
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Detail(long id)
        {
            try
            {
                var jogador = _jogadorService.GetById(id);
                if (jogador != null)
                {
                    var codinomeJogador = _codinomeService.GetById(jogador.CodinomeId);
                    var grupoCodinomeJogador = _grupoService.GetById(codinomeJogador.GrupoId);
                    codinomeJogador.Grupo = grupoCodinomeJogador;
                    var jogadorViewModel = _mapper.Map<JogadorExibicaoViewModel>(jogador);
                    jogadorViewModel.Codinome = codinomeJogador;
                    return View(jogadorViewModel);
                }
                else
                {
                    TempData["error"] = "Jogador não encontrado";
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
