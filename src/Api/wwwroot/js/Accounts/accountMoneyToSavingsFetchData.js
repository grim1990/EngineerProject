async function accountMoneyToSavingsFetchData() {
    try {
        var response = await fetch(`Account/AccountMoneyToSavings`);
        return await response.json();

    } catch (error) {
        console.error('Error fetching operation data:', error);
        return [];
    }
}