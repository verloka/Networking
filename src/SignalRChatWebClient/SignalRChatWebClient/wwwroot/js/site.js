$(document).ready(() => {
    let hubConnection = null;
    let notifyConnectionStatus = false;

    function InitConnection() {
        hubConnection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:3000/chat", { accessTokenFactory: () => $('#Token').val() })
            .build();

        //take a new message
        hubConnection.on("Send", function (data) {
            $('#MessagesSection').append(MakeMessage(data.username, FormatDate(data.date), data.text, hubConnection.connection.connectionId == data.connectionID));
            new bootstrap.Toast($('#MessagesSection').children().last()[0], { autohide: false, animation: true }).show();
            $('#MessagesSection').animate({ scrollTop: $('#MessagesSection').prop("scrollHeight") }, 100);
        });

        //take a new private message
        hubConnection.on("PM", function (data) {
            $('#MessagesSection').append(MakeMessage(data.username, FormatDate(data.date), data.text, hubConnection.connection.connectionId == data.connectionID, true));
            new bootstrap.Toast($('#MessagesSection').children().last()[0], { autohide: false, animation: true }).show();
            $('#MessagesSection').animate({ scrollTop: $('#MessagesSection').prop("scrollHeight") }, 100);
        });

        if (notifyConnectionStatus) {
            //new user connected
            hubConnection.on("Connected", function (data) {
                $('#MessagesSection').append(Notification(`${data.username} entered the chat at ${FormatDate(data.date)}`));
                new bootstrap.Toast($('#MessagesSection').children().last()[0], { autohide: false, animation: true }).show();
                $('#MessagesSection').animate({ scrollTop: $('#MessagesSection').prop("scrollHeight") }, 100);
            });

            //user disconnected
            hubConnection.on("Disconnected", function (data) {
                $('#MessagesSection').append(Notification(`${data.username} left the chat at ${FormatDate(data.date)}`));
                new bootstrap.Toast($('#MessagesSection').children().last()[0], { autohide: false, animation: true }).show();
                $('#MessagesSection').animate({ scrollTop: $('#MessagesSection').prop("scrollHeight") }, 100);
            });
        }

        //take hisotry of messages
        hubConnection.on("SendBulk", function (data) {
            let username = $('#Username').text();
            for (var i = 0; i < data.length; i++) {
                $('#MessagesSection').append(MakeMessage(data[i].username, FormatDate(data[i].date), data[i].text, data[i].username == username, data[i].to == username));
                new bootstrap.Toast($('#MessagesSection').children().last()[0], { autohide: false, animation: true }).show();
            }

            $('#MessagesSection').animate({ scrollTop: $('#MessagesSection').prop("scrollHeight") }, 100);
        });
    }

    function UpdateSectionSize() {
        windowHeight = window.innerHeight -
            $('header').outerHeight() -
            $('.align-items-start').outerHeight() -
            $('.align-items-end').outerHeight() -
            $('footer').outerHeight() -
            35;

        $('#MessagesSection').css('max-height', windowHeight + 'px');
        $('#MessagesSection').css('min-height', windowHeight + 'px');
    }

    function FormatDate(date) {
        date = new Date(date);

        var dd = date.getDate();

        var mm = date.getMonth() + 1;
        var yyyy = date.getFullYear();

        var hh = date.getHours();
        var MM = date.getMinutes();
        var ss = date.getSeconds();

        if (mm < 10) {
            mm = '0' + mm;
        }

        if (hh < 10) {
            hh = '0' + hh;
        }

        if (MM < 10) {
            MM = '0' + MM;
        }

        if (ss < 10) {
            ss = '0' + ss;
        }

        return `${dd}.${mm}.${yyyy} ${hh}:${MM}:${ss}`;
    }

    function Notification(text) {
        return `<div class="toast bg-light" role="alert" aria-live="assertive" aria-atomic="true" data-bs-autohide="false">
                        <div class="toast-body">
                            ${text}
                        </div>
                    </div>`;
    }

    window.PMTo = function (e) {
        $('#pMessageText').val('');
        $('#PrivateMessageUsername').text($(e).data('username'));

        $('#PublicMessages').hide();
        $('#PrivateMessages').show();
    }

    function MakeMessage(username, date, text, ismy = false, ispm = false) {
        return `<div class="toast ${ismy ? "text-white bg-primary" : ispm ? "bg-info" : ""}" role="alert" aria-live="assertive" aria-atomic="true" data-bs-autohide="false">
                        <div class="toast-header row">
                            <strong class="me-auto text-left col-8">${username}</strong>
                            <small class="text-muted text-right col-4">${date}</small>
                        </div>
                        <div class="toast-body">
                            ${text}
                            ${(ismy ? "" : `<div class="text-right"><a href="javascript:void(0);" onclick="window.PMTo(this)" data-username="${username}">pm</a></div>`)}
                        </div>
                    </div>`;
    }

    //EVENTS

    //send message
    $('#sendBtn').on('click', () => {
        if ($('#MessageText').val() == undefined || $('#MessageText').val() == '')
            return;

        hubConnection.invoke("Send", $('#MessageText').val());
        $('#MessageText').val('');
    });
    $('#MessageText').bind("enterKey", function (e) {
        $('#sendBtn').click();
    });
    $('#MessageText').keyup(function (e) {
        if (e.keyCode == 13) {
            $(this).trigger("enterKey");
        }
    });

    //send private message
    $('#psendBtn').on('click', () => {
        if ($('#pMessageText').val() == undefined || $('#pMessageText').val() == '')
            return;

        hubConnection.invoke("PM", $('#pMessageText').val(), $('#PrivateMessageUsername').text());
        $('#pMessageText').val('');
    });
    $('#pMessageText').bind("enterKey", function (e) {
        $('#psendBtn').click();
    });
    $('#pMessageText').keyup(function (e) {
        if (e.keyCode == 13) {
            $(this).trigger("enterKey");
        }
    });

    //resize window
    $(window).resize(UpdateSectionSize);

    //cancel private message
    $('#cancelBtn').on('click', () => {
        $('#pMessageText').val('');
        $('#PrivateMessageUsername').text('');

        $('#PrivateMessages').hide();
        $('#PublicMessages').show();
    });

    UpdateSectionSize();
    InitConnection();

    hubConnection.start();
});