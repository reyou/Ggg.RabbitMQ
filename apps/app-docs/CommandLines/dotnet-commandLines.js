//=============================================================================
// https://code.visualstudio.com/docs/other/dotnet
// https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html
var help = "dotnet --help";
// Now let's generate two projects, one for the publisher and one for the consumer:
var send = "dotnet new console --name Send";
var move = "mv Send/Program.cs Send/Send.cs";
var receive = "dotnet new console --name Receive";
var move_receive = "mv Receive/Program.cs Receive/Receive.cs";
// Then we add the client dependency.
var send_cd = "cd Send";
// https://www.nuget.org/packages/RabbitMQ.Client
// https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html
var add = "dotnet add package RabbitMQ.Client --version 5.0.1";
var restore = "dotnet restore";
var receive_up = "cd ../Receive";
var add_package = "dotnet add package RabbitMQ.Client --version 5.0.1";
var restore_receive = "dotnet restore";
var build = "dotnet build";
var run = "dotnet run";
//=============================================================================


