﻿// Function to animate typing effect on the provided element
function typeWriter(element, text, i, callback) {
    if (i === 0) {
        element.innerHTML = "";
    }

    if (i < text.length) {
        element.innerHTML += text.charAt(i);
        setTimeout(() => {
            typeWriter(element, text, i + 1, callback);
        }, 15); //adjust speed of typing here
    } else {
        element.insertAdjacentHTML("afterend", "</div>");
        if (callback) {
            callback();
        }
    }
}

// Function to update the game news element with the fetched news
function updateGameNews(appId, summarizedNews, gameType, isTypingEnabled) {
    const gameElement = document.querySelector(`.card .card-body .${gameType}-game-news[data-gameid="${appId}"]`);

    // Save the fetched news in local storage
    localStorage.setItem(`gameNews-${appId}`, summarizedNews);

    // Update the news elements with the fetched news using the typing animation
    if (isTypingEnabled) {
        if (gameElement) {
            typeWriter(gameElement, summarizedNews, 0);
        }
    }
    else {
        gameElement.innerHTML = summarizedNews;
    }
}

// Function to fetch game news either from local storage or the API
function getGameNews(appId, gameType) {
    const storedNews = localStorage.getItem(`gameNews-${appId}`);
    const storedNewsTimestamp = localStorage.getItem(`gameNewsTimestamp-${appId}`);
    const now = new Date().getTime();
    const expirationTime = 60 * 60 * 1000; // 1 hour in milliseconds

    // Check if the stored news is expired or not
    const isExpired = !storedNewsTimestamp || now - storedNewsTimestamp > expirationTime;

    // If the news is stored and not expired, use it, otherwise fetch from the API
    if (storedNews && !isExpired) {
        updateGameNews(appId, storedNews, gameType, false);
    } else {
        $.ajax({
            url: "https://localhost:7123/api/News/GetGameNews",
            data: { appId: appId },
            success: function (response) {
                console.log(response); // Check the received data

                const appId = response.appId;
                const summarizedNews = response.summarizedNews;

                // Save the fetched news and its timestamp in local storage
                localStorage.setItem(`gameNews-${appId}`, summarizedNews);
                localStorage.setItem(`gameNewsTimestamp-${appId}`, now);

                // Update the news elements with the fetched news
                updateGameNews(appId, summarizedNews, gameType, true);
            }
        });
    }
}


// Add event listener for DOMContentLoaded event to ensure the DOM is ready
document.addEventListener("DOMContentLoaded", function () {
    // Get all recent and followed game news elements
    const recentGames = document.querySelectorAll(".recent-game-news");
    const followedGames = document.querySelectorAll(".followed-game-news");

    // Iterate through the elements and fetch game news for each game
    recentGames.forEach((game) => {
        const appId = game.getAttribute("data-gameid");
        getGameNews(appId, "recent");
    });

    followedGames.forEach((game) => {
        const appId = game.getAttribute("data-gameid");
        getGameNews(appId, "followed");
    });
});
