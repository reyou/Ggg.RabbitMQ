//=============================================================================
https://didyoureadme.azurewebsites.net/UserUrls/TagUrls?UserUrlTagId=270c50c6-0aed-4084-98ab-160419b8c466
https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html
//=============================================================================
C:\Program Files\RabbitMQ Server\rabbitmq_server-3.7.3\sbin
rabbitmq-server —start
http://localhost:15672/#/
//=============================================================================
https://www.rabbitmq.com/tutorials/tutorial-three-dotnet.html
A producer is a user application that sends messages.
A queue is a buffer that stores messages.
A consumer is a user application that receives messages.
//=============================================================================
The core idea in the messaging model in RabbitMQ is that the producer never sends 
any messages directly to a queue. Actually, quite often the producer doesn't even 
know if a message will be delivered to any queue at all.
Instead, the producer can only send messages to an exchange. 
An exchange is a very simple thing. On one side it receives messages from producers 
and the other side it pushes them to queues. The exchange must know exactly what 
to do with a message it receives. Should it be appended to a particular queue? 
Should it be appended to many queues? Or should it get discarded. The rules for 
that are defined by the exchange type.
//=============================================================================
The default exchange is implicitly bound to every queue, with a routing key 
equal to the queue name. It is not possible to explicitly bind to, or unbind 
from the default exchange. It also cannot be deleted.
//=============================================================================
https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html
Two things are required to make sure that messages aren't lost: we need to mark 
both the queue and messages as durable.
//=============================================================================

