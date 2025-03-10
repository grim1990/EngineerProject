var accountMoneyToSavingsChart = document.getElementById('accountMoneyToSavingsChart').getContext('2d');

async function updateAccountMoneyToSavingsChart() {
    const accountMoneyToSavingsData = await accountMoneyToSavingsFetchData();

    var sumAccountsMoney = accountMoneyToSavingsData[0];
    var sumBlockade = accountMoneyToSavingsData[1];

    var totalSum = sumAccountsMoney + sumBlockade;

    var accountsMoneyPercentage = ((sumAccountsMoney / totalSum) * 100).toFixed(2);
    var blockadePercentage = ((sumBlockade / totalSum) * 100).toFixed(2);

    const combinedData = [
        { description: "Bieżące środki", value: sumAccountsMoney, percentage: accountsMoneyPercentage },
        { description: "Oszczędności", value: sumBlockade, percentage: blockadePercentage },
    ];

    var chartColor = totalSum > 0 ? ['#2d8bba', '#2f5f98'] : ['#2f5f98', '#2d8bba'];

    var newChart = new Chart(accountMoneyToSavingsChart, {
        type: 'doughnut',
        data: {
            datasets: [{
                data: combinedData.map(item => totalSum > 0 ? item.percentage : 100),
                backgroundColor: chartColor,
                borderColor: chartColor,
                borderWidth: 1
            }],
        },
        options: {
            responsive: true,
            plugins: {
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            const percentage = combinedData[context.dataIndex].percentage;
                            if (percentage === "NaN") {
                                return ` 0 %`;
                            }
                            else {
                                return `${percentage} %`;
                            }
                        }
                    }
                }
            }
        },
        legend: {
            display: false
        }
    });

    chartInstances['accountMoneyToSavingsChart'] = newChart;
}

updateAccountMoneyToSavingsChart();