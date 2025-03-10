var ctx = document.getElementById('summaryOperations').getContext('2d');

async function updateSummaryChart() {
    try {
        var summaryChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['Zaplanowane dochody', 'Zaplanowane wydatki'],
                datasets: [{
                    label: 'Zaplanowane',
                    data: [0, 0],
                    backgroundColor: '#2d8bba',
                    borderColor: '#2f5f98',
                    borderWidth: 1
                },
                {
                    label: 'Realne',
                    data: [0, 0],
                    backgroundColor: '#1c7aa0',
                    borderColor: '#1e4e87',
                    borderWidth: 1
                }]
            },
            options: {
                indexAxis: 'y',
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    x: {
                        beginAtZero: true
                    }
                }
            },
            labels: {
                generateLabels: function (chart) {
                    const originalLabels = Chart.defaults.plugins.legend.labels.generateLabels(chart);
                    originalLabels.forEach(label => {
                        if (label.text) {
                            label.text = label.text.split(' ').join('\n');
                        }
                    });
                    return originalLabels;
                }
            }
        });

        const budgetPlan = await summaryOperationsChartFetchData(0);
        const realIncomes = await incomesAndExpensesFetchData(1);
        const realExpenses = await incomesAndExpensesFetchData(0);

        const plannedIncomes = budgetPlan.plannedIncomes;
        const plannedExpenses = budgetPlan.plannedExpenses;

        const realIncomesSum = realIncomes.reduce((total, item) => total + item.value, 0);
        const realExpensesSum = realExpenses.reduce((total, item) => total + item.value, 0);

        summaryChart.data.datasets[0].data = [plannedIncomes, plannedExpenses];
        summaryChart.data.datasets[1].data = [realIncomesSum, realExpensesSum];

        summaryChart.update();

    } catch (error) {
        console.error('Error fetching and updating chart data:', error);
    }
}

updateSummaryChart();