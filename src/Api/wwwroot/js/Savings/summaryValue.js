async function summaryValueFetchData() {
    try {
        var response = await fetch(`Home/BudgetPlanData?year=${0}`);
        return await response.json();
    } catch (error) {
        console.error('Error fetching budget plan data:', error);
        return [];
    }
}

async function updateSummaryValue() {
    const data = await summaryValueFetchData();
    const freeAmount = data.plannedIncomes - data.plannedExpenses;

    var spanElement = document.querySelector('.savings');
    spanElement.textContent = `${freeAmount} zł`;
}

window.addEventListener('load', updateSummaryValue);