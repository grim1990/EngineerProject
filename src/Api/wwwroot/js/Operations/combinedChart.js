const combainedChart = document.getElementById('combainedChart').getContext('2d');

async function updateCombinedChart() {
    const incomeData = await incomesAndExpensesFetchData(1);
    const expenseData = await incomesAndExpensesFetchData(0);

    const totalIncome = incomeData.reduce((sum, item) => sum + item.value, 0);
    const totalExpense = expenseData.reduce((sum, item) => sum + item.value, 0);

    const totalOperations = totalIncome + totalExpense;

    const incomePercentage = totalOperations === 0 ? 50 : ((totalIncome / totalOperations) * 100).toFixed(2);
    const expensePercentage = totalOperations === 0 ? 50 : ((totalExpense / totalOperations) * 100).toFixed(2);

    const combinedData = [
        { description: "Wydatki", value: totalExpense, percentage: expensePercentage, category: 'Expense' },
        { description: "Wpływy", value: totalIncome, percentage: incomePercentage, category: 'Income' },
    ];

    const labels = ["Wydatki", "Wpływy"];

    const chartConfig = {
        type: 'doughnut',
        data: {
            datasets: [{
                data: combinedData.map(item => item.percentage),
                backgroundColor: ['#2d8bba', '#2f5f98'],
                borderColor: ['#1c7aa0', '#1e4e87'],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            const percentage = combinedData[context.dataIndex].percentage;
                            return `${labels[context.dataIndex]}: ${percentage} %`;
                        }
                    }
                }
            },
            legend: {
                display: false
            }
        }
    };

    if (totalOperations === 0) {
        chartConfig.data.datasets[0].data = [50, 50];
    }

    if (chartInstances['combainedChart']) {
        chartInstances['combainedChart'].destroy();
    }

    const newChart = new Chart(combainedChart, chartConfig);
    chartInstances['combainedChart'] = newChart;
}

updateCombinedChart();