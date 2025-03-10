var combinedChart = document.getElementById('combinedChart').getContext('2d');

async function updateCombinedChart() {
    const incomeData = await combinedChartFetchData(1);
    const expenseData = await combinedChartFetchData(0);

    var totalIncome = incomeData.reduce((sum, item) => sum + item.value, 0);
    var totalExpense = expenseData.reduce((sum, item) => sum + item.value, 0);

    var incomePercentage = ((totalIncome / (totalIncome + totalExpense)) * 100).toFixed(2);
    var expensePercentage = ((totalExpense / (totalIncome + totalExpense)) * 100).toFixed(2);

    if ((!incomeData || incomeData.length === 0) && (!expenseData || expenseData.length === 0)) {
        totalIncome = 50;
        incomePercentage = 50;
        totalExpense = 50;
        expensePercentage = 50;
    }

    const labels = ["Wydatki", "Wpływy"];

    const combinedData = [
        { description: "Wydatki", value: totalExpense, percentage: expensePercentage, category: 'Expense' },
        { description: "Wp³ywy", value: totalIncome, percentage: incomePercentage, category: 'Income' },
    ];

    if ((!incomeData || incomeData.length === 0) && (!expenseData || expenseData.length === 0)) {
        var newChart = new Chart(combinedChart, {
            type: 'doughnut',
            data: {
                datasets: [{
                    data: combinedData.map(item => item.percentage),
                    backgroundColor: ['#2d8bba', '#2d8bba'],
                    borderColor: ['#2d8bba', '#2d8bba'],
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
                                return `${labels[context.dataIndex]}: 0 %`;
                            }
                        }
                    }
                }
            },
            legend: {
                display: false
            }
        });
    }
    else {
        var newChart = new Chart(combinedChart, {
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
                }
            },
            legend: {
                display: false
            }
        });
    }

    chartInstances['combinedChart'] = newChart;
}

updateCombinedChart();