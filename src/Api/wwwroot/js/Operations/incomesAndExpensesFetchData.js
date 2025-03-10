const chartInstances = {};

async function incomesAndExpensesFetchData(operationType) {
    try {
        const urlParams = new URLSearchParams(window.location.search);
        const categoryId = urlParams.get('categoryId') || '';
        const month = urlParams.get('month') || '';
        const year = urlParams.get('year') || '';

        const queryParams = new URLSearchParams({
            operationType,
            CategoryId: categoryId,
            Month: month,
            Year: year
        });

        const response = await fetch(`/Category/GetFilteredCategoriesByOperations?${queryParams}`);
        return await response.json();
    } catch (error) {
        console.error('Error fetching operation data:', error);
        return [];
    }
}