$(document).ready(() => {
    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:3000/chat")
        .build();

    hubConnection.on("Send", function (data) {
        let msg = `<blockquote class="blockquote text-left"><p class="mb-0">${data}</p></blockquote>`
        $('#MessagesSection').append(msg);
    });

    //EVENTS
    $('#sendBtn').on('click', () => {
        hubConnection.invoke("Send", $('#MessageText').val());
    });

    hubConnection.start();
});