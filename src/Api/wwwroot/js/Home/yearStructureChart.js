var ctx = document.getElementById('yearStructureChart').getContext('2d');

async function updateChart() {
    try {

        const labels = ['I', 'II', 'III', 'IV', 'V', 'VI', 'VII', 'VIII', 'IX', 'X', 'XI', 'XII'];
        const initialData = {
            income: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            expenses: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
        };

        const data = {
            labels: labels,
            datasets: [
                {
                    label: 'Dochody',
                    data: initialData.income,
                    fill: false,
                    borderColor: '#5cad5c',
                    tension: 0.1
                },
                {
                    label: 'Wydatki',
                    data: initialData.expenses,
                    fill: false,
                    borderColor: '#ff5757',
                    tension: 0.1
                }
            ]
        };

        const options = {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                x: {
                    display: true,
                    title: {
                        display: true,
                    }
                },
                y: {
                    display: true,
                    title: {
                        display: true,
                    }
                }
            },
            plugins: {
                legend: {
                    display: true,
                    position: 'bottom',
                    padding: 1,
                    margin: 1,
                }
            },
        };

        var structureChart = new Chart(ctx, {
            type: 'line',
            data: data,
            options: options
        });

        const incomeData = await yearStructureChartFetchData(1);
        const expensesData = await yearStructureChartFetchData(0);

        const incomeValues = Array(12).fill(0);
        const expensesValues = Array(12).fill(0);

        incomeData.forEach(item => {
            const monthIndex = parseInt(item.month) - 1;
            incomeValues[monthIndex] += item.value;
        });

        expensesData.forEach(item => {
            const monthIndex = parseInt(item.month) - 1;
            expensesValues[monthIndex] += item.value;
        });

        structureChart.data.labels = labels;
        structureChart.data.datasets[0].data = incomeValues;
        structureChart.data.datasets[1].data = expensesValues;

        structureChart.update();
    } catch (error) {
        console.error('Error fetching and updating chart data:', error);
    }
}

updateChart();