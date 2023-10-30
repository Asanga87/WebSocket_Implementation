# WebSocket_Implementation
There are two separet soltions for this implementation.
WSAPP folder conatin Web Socket Server Implementation.
First Paragarpgh expalin the implementation of Websocket serevr.
"WebSocketController" controller under the Controllers folder is for /message websocket end point implementation. Within this controller there are two other methods for send messages to client come from HTTP Post end points and Ws Client.
/message ws endpont has implemented with get with with one parameter. Id parameter identifies the method that server should respond to. If Id is 1 then its a wellcome call . if Id is two then its ping call. if Id is 3 then it is work call.
"WorkController" class under the Controllers is for server/ping http post endpoint and work/start http Post implementation.
Message handler logic is implemented based on the repository pattern architecture. IMessageBuilderService is the interface and MessageHandlercs is the its implementation.

This second paragarpgh explain the web socket client implementation.
This is C# desktop application which has for buttons to call separate endpoints. 
in the form load method it establishes the connection with serevr web socket end point by sending request via "ws" protocall which is inteded to use websocket communication.
Using "Wellcome call" button click it get it receive the welcome message form the server and print in the output window.
Using "Ping Handelr " button click it send the http call to /serever/ping http post end point and /server/ping end point generate ws call to websocket server with parameter 2.
Then Client print the respond come from initially web socket nd then from http post.
"Worker Handler" button send the call to work/start http post endpoint and simillarly it generate ws call to /message web socket endpoint. Respond will follow the similar path from Web socket to http post and then to client. Then client will print the work/start respond in the out put windows. Then server will wait for 3000 ms and then send the final message work complete.

However drawback of this implemenataion is client cannot grab the second message come from the ws server when the call generate from the http prtocol. Hence in order to demonstrate the accurate implementation of the client I use the Work Handler Ws button which make a final call to web socket server. Based on that call again web socket server directly respond to the client first and then after 3000 ms wait time it again respond to the client with the work completed end message. Both work start message comes with random generated Id and same Id will be used to send work completed message. 

Development Enviornment
Server: Visual Studio 2022 , Framework:.Net 6.0 , Project Template: Asp.Net Core Empty Template
Client: Visaul Studio 2022 , Framework:.Net 6.0, Project Template : Windows Desktop Application

How to run 

Step 1: Open Serever application using visual studio and run it on localhost . URL has been setup as http://localhost:6767
Step 2: Open server application using visul studio and run it normally. All above funtions are implemented from button clicks. Try to click "Welcome call" button to "Work Handler Ws" and you will see the output from Output window located in the below part of the form. 

Special Note: Please wait 3000 ms until serever send the final message after you click the "Work Handler Ws" button.

Thank you for reading and send me your comments after executing the project. Cheers!!!
