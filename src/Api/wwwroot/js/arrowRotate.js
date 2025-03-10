var isArrowDown = false;

function arrowRotate(button) {
    var container = button.parentElement;
    var arrow = document.getElementById('arrow__down');

    if (isArrowDown) {
        button.style.transform = 'rotate(0deg)';
        isArrowDown = false;
    }
    else {
        button.style.transform = 'rotate(180deg)';
        isArrowDown = true;

    }
}

