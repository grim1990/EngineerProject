async function summaryOperationsChartFetchData(year) {
    try {
        var response = await fetch(`Home/BudgetPlanData?year=${year}`);
        return await response.json();

    } catch (error) {
        console.error('Error fetching operation data:', error);
        return [];
    }
}