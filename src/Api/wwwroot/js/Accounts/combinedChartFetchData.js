async function combinedChartFetchData(operationType) {
    try {
        var response = await fetch(`Operation/GetOperationData?operationType=${operationType}`);
        return await response.json();

    } catch (error) {
        console.error('Error fetching operation data:', error);
        return [];
    }
}