//=============================================================================
// https://www.rabbitmq.com/install-windows.html
// https://www.rabbitmq.com/manpages.html
// https://www.rabbitmq.com/rabbitmq-server.8.html
// cd "C:\Program Files\RabbitMQ Server\rabbitmq_server-3.7.3\sbin"
//=============================================================================
var server = {
    "url": "http://localhost:15672/#/",
    "username": "guest",
    "password": "guest",
    "start": "rabbitmq-server —start",
    "detached": function detached() {
        // Start the server process in the background. Note that this will cause the 
        // pid not to be written to the pid file.
        return "rabbitmq-server -detached";
    }
}
//=============================================================================
// Managing the Broker
// https://www.rabbitmq.com/install-windows.html#managing-windows
var broker = {
    "stop": "rabbitmqctl stop",
    "status": "rabbitmqctl status"
}
//=============================================================================
// https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html
var debug = {
    "start cmd":"start cmd",
    "unacknowledged": "rabbitmqctl list_queues name messages_ready messages_unacknowledged",
    "list_exchanges": "rabbitmqctl list_exchanges",
    "list_bindings": "rabbitmqctl list_bindings",
    "ggg-ggg":"ggg"
}
//=============================================================================


