const colorCount = 20;
const maxAlpha = 1;
const minAlpha = 0.1;

var colorCycleIncomesChart = 0;
var colorCycleExpensesChart = 0;

var entryCountIncomesChart = 0;
var entryCountExpensesChart = 0;

function randomColors(chartType) {
    const colorWheel = {
        incomesChart: [
            [47, 95, 152],
        ],
        expensesChart: [
            [47, 95, 152],
        ],
    };

    var colorCycle;
    var entryCount;

    if (chartType === 'incomesChart') {
        colorCycle = colorCycleIncomesChart;
        entryCount = entryCountIncomesChart;
        entryCountIncomesChart = (entryCountIncomesChart + 1) % colorCount;
        if (entryCountIncomesChart === 0) {
            colorCycleIncomesChart = (colorCycleIncomesChart + 1) % colorCount;
        }
    } else if (chartType === 'expensesChart') {
        colorCycle = colorCycleExpensesChart;
        entryCount = entryCountExpensesChart;
        entryCountExpensesChart = (entryCountExpensesChart + 1) % colorCount;
        if (entryCountExpensesChart === 0) {
            colorCycleExpensesChart = (colorCycleExpensesChart + 1) % colorCount;
        }
    }

    const categoryIndex = colorCycle % colorCount;
    const color = colorWheel[chartType][categoryIndex];

    const alphaStep = (maxAlpha - minAlpha) / (colorCount - 1);
    var alpha;

    alpha = maxAlpha - (entryCount * alphaStep);

    return `rgba(${color[0]}, ${color[1]}, ${color[2]}, ${alpha})`;
}
