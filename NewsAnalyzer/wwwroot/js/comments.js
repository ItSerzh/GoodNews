let btnToggle = $("#comments-toggle");
let divComments = $("#comments-container");
let newsId = $('#newsId').val();
let isCommentsDisplayed = false;

let toggleComments = function (newsId) {

    divComments.toggle();
    if (isCommentsDisplayed) {
        btnToggle.html('Display comments');
        
    } else {
        btnToggle.html('Hide comments');
        loadComments(newsId, divComments)
    }
    isCommentsDisplayed = !isCommentsDisplayed;
}

let loadComments = function (newsId, container) {
    let request = new XMLHttpRequest();
    request.open('GET', `/Comments/List/?newsId=${newsId}`, true);
    request.onload = function () {
        if (request.status >= 200 && request.status < 400) {
            let resp = request.responseText;
            container.html(resp);
        }
    };
    request.send();
}

function validateCommentData() {

}

function addComment() {

    let commentText = document.getElementById('commentText').value;

    validateCommentData();

    var postRequest = new XMLHttpRequest();
    postRequest.open("POST", '/Comments/Create', true);
    postRequest.setRequestHeader('Content-Type', 'application/json');

    let newsId = $('#newsId').val();
    postRequest.send(JSON.stringify({
        commentText: commentText,
        newsId: newsId
    }));

    postRequest.onload = function () {
        if (postRequest.status >= 200 && postRequest.status < 400) {
            document.getElementById('commentText').value = '';

            loadComments(newsId, divComments);
        }
    }
}

var getCommentsIntervalId = setInterval(function () {
    let divComments = $("#comments-container");
    let newsId = $('#newsId').val();
    loadComments(newsId, divComments);
}, 15000);