const ctx = document.getElementById('myChart');
const ligaDaJustica = document.getElementById('Liga da Justiça');
const vingadores = document.getElementById('Os Vingadores');


const data = {
    labels: [
        'Liga da Justiça',
        'Os Vingadores'
    ],
    datasets: [
    {
        label: 'Codinomes Cadastrados',
        data: [(ligaDaJustica) ? ligaDaJustica.value : 0, (vingadores) ? vingadores.value : 0],
        options: {
            responsive: true
        },
        backgroundColor: [
            'rgb(245, 188, 58)',
            'rgb(174, 1, 7)'
        ],
        hoverOffset: 4
    }
    ]
};

new Chart(ctx, {
    type: 'pie',
    data: data
}
);