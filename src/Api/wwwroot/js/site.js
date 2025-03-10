// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var isMenuVisible = false;
var isMoneyMenuVisible = false;
var isNotificationPanelVisible = false;
var isOptionsVisible = false;

function showMenu(button) {
    var container = button.parentElement;
    var menu = container.querySelector('.dropdown__element');
    var statusTextElement = document.getElementById('manage');
    var statusTextWidth = statusTextElement.offsetWidth;
    var arrow = document.getElementById('arrow__down');
    var arrowWidh = arrow.offsetWidth;

    if (isMenuVisible) {
        menu.style.display = 'none';
        button.style.transform = 'rotate(0deg)';
        isMenuVisible = false;
    } else {
        menu.style.left = -statusTextWidth - arrowWidh + 'px';
        button.style.transform = 'rotate(180deg)';
        menu.style.width = statusTextWidth + 'px';
        menu.style.display = 'block';
        isMenuVisible = true;
    }
}

//Closing open dropdown menu
document.addEventListener('click', function (event) {
    var clickedElement = event.target;
    var container = clickedElement.closest('.dropdown__container');
    var button = document.getElementById('arrow__down'); // Find the closest button element
    var menu = document.querySelector('.dropdown__element');

    if (clickedElement !== button && !button.contains(clickedElement) && !container) {
        if (isMenuVisible) {
            menu.style.display = 'none';
            isMenuVisible = false;
            button.style.transform = 'rotate(0deg)';

        }
    }
});

const notifications = [];

//Notifications
function showNotifications(button) {

    var container = button.parentElement;
    var menu = document.getElementById('notificationsContainer');

    var statusTextElement = document.getElementById('manage');
    var statusTextWidth = statusTextElement.offsetWidth;
    var arrow = document.getElementById('arrow__down');
    var arrowWidh = arrow.offsetWidth;
    menu.style.left = -statusTextWidth - arrowWidh - 50 + 'px';
    notificationsSelect();

    if (isOptionsVisible) {
        menu.style.display = 'none';
        isOptionsVisible = false;
    }
    else {
        if (notifications.length === 0) {
            menu.style.display = 'none';
            isOptionsVisible = false;
        }
        else {
            menu.style.display = 'block';
            isOptionsVisible = true;
        }
    }
}

//Days left to the end of month calculator

function daysLeftInMonth() {
    const today = new Date();
    const currentMonth = today.getMonth();
    const nextMonth = (currentMonth + 1) % 12;
    const endOfMonth = new Date(today.getFullYear(), nextMonth, 0);
    const daysLeft = Math.ceil((endOfMonth - today) / (1000 * 60 * 60 * 24));

    return daysLeft;
}

//Notifications render

function notificationsSelect() {
    if (daysLeftInMonth() <= 7) {
        notifications.push(`Do końca miesiąca pozostało: ${daysLeftInMonth()} dni. Pamiętaj o uzupełnieniu spisu wydatków.`);
    }
};
notificationsSelect();

function dotDisplay() {
    var dot = document.getElementById('dot');

    if (notifications.length === 0) {
        dot.style.display = 'none';
    }
    else {
        dot.style.display = 'block';
    }
};
dotDisplay();

function renderNotifications() {
    const ulElement = document.createElement("ul");

    notifications.forEach(notification => {
        const liElement = document.createElement("li");
        liElement.classList.add("list-notification");
        liElement.textContent = notification;
        ulElement.appendChild(liElement);
    });

    const notificationsContainer = document.getElementById("notificationsContainer");
    notificationsContainer.appendChild(ulElement);
}

renderNotifications();

//Closing open additional options
document.addEventListener('click', function (event) {
    var clickedElement = event.target;
    var menu = clickedElement.closest('.dropdown__menu__options');
    var container = document.getElementById('dropdown__options');
    var button = document.querySelector('.options__button');

    if (clickedElement !== button && !button.contains(clickedElement) && !menu) {
        if (isOptionsVisible) {
            container.style.display = 'none';
            isOptionsVisible = false;
        }
    }
});

function moveMoneyMenu(button) {
    var dropdownSection = document.getElementById('move__money');

    if (isMoneyMenuVisible) {
        dropdownSection.style.display = 'none';
        isMoneyMenuVisible = false;
    } else {
        dropdownSection.style.display = 'flex';
        isMoneyMenuVisible = true;
    }
}

function calculateNav() {
    var nav = document.getElementById('right__nav');
    var leftNav = document.getElementById('left__nav');

    if (window.matchMedia('(min-width: 992px)').matches) {
        if (leftNav != null) {
            var navHeight = leftNav.offsetHeight;
            nav.style.height = navHeight + 'px';
        }
    }
}

calculateNav();

