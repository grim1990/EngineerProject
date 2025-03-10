function chartDisplay() {
    var chart = document.getElementById('summaryOperations');
    var container = document.getElementById('chartContainer');

    chart.style.width = container.offsetWidth + 'px';
};
chartDisplay();