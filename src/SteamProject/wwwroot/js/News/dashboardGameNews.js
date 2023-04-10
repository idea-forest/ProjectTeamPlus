﻿// Function to animate typing effect on the provided element
function typeWriter(element, text, i, callback) {
    // Clear the element's inner HTML when starting the typing animation
    if (i === 0) {
        element.innerHTML = "";
    }

    // If there is still text to type, type the next character and set a timeout to continue
    if (i < text.length) {
        element.innerHTML += text.charAt(i);
        setTimeout(() => {
            typeWriter(element, text, i + 1, callback);
        }, 5); // Adjust the speed of typing here
    } else {
        // Once typing is finished, call the callback if provided
        if (callback) {
            callback();
        }
    }
}

// Function to save fetched game news and its timestamp to local storage
function saveGameNewsToLocalStorage(appId, summarizedNews, timestamp) {
    localStorage.setItem(`gameNews-${appId}`, summarizedNews);
    localStorage.setItem(`gameNewsTimestamp-${appId}`, timestamp);
}

// Function to check if the stored game news is expired or not
function isGameNewsExpired(storedNewsTimestamp) {
    const now = new Date().getTime();
    const expirationTime = 60 * 60 * 1000; // 1 hour in milliseconds
    return !storedNewsTimestamp || now - storedNewsTimestamp > expirationTime;
}

// Function to fetch game news from the API
function fetchGameNewsFromAPI(appId, gameType) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: "/api/News/GetRecentGameNews",
            data: { appId: appId },
            success: function (response) {
                console.log(response); // Check the received data
                const appId = response.appId;
                const summarizedNews = response.summarizedNews;
                const now = new Date().getTime();

                saveGameNewsToLocalStorage(appId, summarizedNews, now);
                resolve({ appId, summarizedNews, gameType, isFromAPI: true });
            },
            error: function (error) {
                reject(error);
            }
        });
    });
}

// Function to get recent game news either from local storage or the API
function getRecentGameNews(appId, gameType) {
    const storedNews = localStorage.getItem(`gameNews-${appId}`);
    const storedNewsTimestamp = localStorage.getItem(`gameNewsTimestamp-${appId}`);

    if (storedNews && !isGameNewsExpired(storedNewsTimestamp)) {
        return Promise.resolve({ appId, summarizedNews: storedNews, gameType, isFromAPI: false });
    } else {
        return fetchGameNewsFromAPI(appId, gameType);
    }
}

// Function to update the game news element with the fetched news
function updateGameNews({ appId, summarizedNews, gameType, isFromAPI }) {
    const gameElement = document.querySelector(`.card .card-body .${gameType}-game-news[data-gameid="${appId}"]`);

    // If the news is fetched from the API, use the typing animation
    if (isFromAPI) {
        typeWriter(gameElement, summarizedNews, 0);
    } else {
        // If the news is from local storage, simply display the text
        gameElement.innerHTML = summarizedNews;
    }
}


// Add an event listener to run the code once the DOM content is fully loaded
document.addEventListener("DOMContentLoaded", function () {
    // Get all recent and followed game news elements
    const recentGames = document.querySelectorAll(".recent-game-news");
    const followedGames = document.querySelectorAll(".followed-game-news");

    // Iterate through the recent game news elements
    recentGames.forEach((game) => {
        // Get the appId from the game element's data attribute
        const appId = game.getAttribute("data-gameid");

        // Fetch the recent game news and update the game news element
        getRecentGameNews(appId, "recent")
            .then(updateGameNews)
            .catch((error) => console.error(error));
    });

    // Iterate through the followed game news elements
    followedGames.forEach((game) => {
        // Get the appId from the game element's data attribute
        const appId = game.getAttribute("data-gameid");

        // Fetch the followed game news and update the game news element
        getRecentGameNews(appId, "followed")
            .then(updateGameNews)
            .catch((error) => console.error(error));
    });
});
