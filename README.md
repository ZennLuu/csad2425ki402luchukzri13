# Repository of practical tasks for "Computer systems automated design"

## Task
Develop the game that has HW(server) and SW(client) sides. Ensures different game modes(Man vs Man, Man vs AI etc.). 
Have game parameters to play with. Game should save, load and create new game whenever needed. 

## Task number
I have 13 number in Table 1. Tasks for students.
Which corresponds to Tic-tac-toe 3x3 game, that uses XML as config file.

## Details 
I'll use C#(client) and C++(server) programing languages. Hardware server will be ESP32-WROOM32. 
And technology to communicate between client and server will be UART(Simulated serial port).
Also i'll use Arduino IDE to flash bytecode into ESP32.


# how to build and run
## ESP32WROOM32
Flash code from server/server.ino file with Arduino IDE using ESP32WROOM32-DA Module as selected board.

## TTTClient
Open client/TTTClient/TTTClient.sln file and build in MS Visual Studio. All custom code contained within Form1.cs.
For now requires manual lookup for COM-port number and changing in Form1.cs file to desired. (In plans to automate this step).
In case of COM-port disconnected program will tell user about it.