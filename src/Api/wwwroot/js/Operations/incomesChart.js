const incomesChart = document.getElementById('incomesChart').getContext('2d');

async function updateIncomesChart() {
    const chartData = await incomesAndExpensesFetchData(1);
    const filteredChartData = chartData.filter(e => e.value !== 0);

    const backgroundColor = filteredChartData.map(() => randomColors('incomesChart'));
    const borderColor = backgroundColor.map(color => color.replace("0.5", "1"));

    const dataLabels = filteredChartData.map(item => item.name);
    const dataValues = filteredChartData.map(item => item.value);

    const chartConfig = {
        type: 'pie',
        data: {
            labels: dataLabels,
            datasets: [{
                data: dataValues,
                backgroundColor: backgroundColor,
                borderColor: borderColor,
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    display: false,
                    position: 'right',
                    align: 'center',
                }
            }
        }
    };

    if (filteredChartData.length === 0) {
        chartConfig.data.datasets[0].data = [100];
        chartConfig.data.datasets[0].backgroundColor = "#2d8bba";
        chartConfig.data.datasets[0].borderColor = "#2d8bba";
        chartConfig.data.labels = ['Brak danych'];
    }

    if (chartInstances['incomesChart']) {
        chartInstances['incomesChart'].destroy();
    }

    const newChart = new Chart(incomesChart, chartConfig);
    chartInstances['incomesChart'] = newChart;
}

updateIncomesChart();