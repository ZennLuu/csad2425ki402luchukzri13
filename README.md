# Task 2
Create basic communication scheme between server and client via UART(Serial port).

## Details 
### ESP32WROOM32
Flash code from `server/server.ino` file with Arduino IDE using ESP32WROOM32-DA Module as selected board.
More detailed installation guide for esp32 support in Arduino IDE is [here](https://randomnerdtutorials.com/installing-the-esp32-board-in-arduino-ide-windows-instructions/ "ESP32 in Arduine IDE").

### TTTClient
Open `client/TTTClient/TTTClient.sln` file and build in MS Visual Studio. All necessary code is contained within `Form1.cs`.
If `COM-port "does not exist"` - manually change it to available COM-port that associated with the ESP32 in **line 23** of `Form1.cs` file (In plans to automate this step).