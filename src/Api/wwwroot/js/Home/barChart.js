const chartData = [
    { label: 'Dochody', data: Incomes },
    { label: 'Wydatki', data: Spends },
];

var barChart = document.getElementById('barChart').getContext('2d');

var newChart = new Chart(barChart, {

    type: 'bar',
    data: {
        labels: chartData.map(item => item.label),
        datasets: [{
            data: chartData.map(item => item.data),
            backgroundColor: ['#17bf5f', '#d94545'],
            borderColor: ['#06ae4e', '#c83424'],
            borderWidth: 1
        }]
    },
    options: {
        responsive: true,
        scales: {
            x: {
                grid: {
                    display: false
                }
            },
            y: {
                display: false,
                grid: {
                    display: false
                }
            }
        },
        plugins: {
            legend: {
                display: false,
                align: 'center',
            }
        }
    }
});
