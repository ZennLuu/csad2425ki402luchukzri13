// Set up communication speed same as client
const int baud_rate = 115200;

char grid[] = {
  'n', 'n', 'n',
  'n', 'n', 'n',
  'n', 'n', 'n'
};

bool checkForWin(char C){
  // check horizontal lines
  for(int i = 0; i < 3; i++)
    if(grid[i*3] == C && grid[i*3 + 1] == C && grid[i*3 + 2] == C)
      return true;

  // check vertical lines
  for(int i = 0; i < 3; i++)
    if(grid[i] == C && grid[i + 3] == C && grid[i + 6] == C)
      return true;

  // check diagonals
  if((grid[0] == C && grid[4] == C && grid[8] == C) || (grid[2] == C && grid[4] == C && grid[6] == C))
    return true;

  return false;
}

void setup() {
  // Start the UART communication with same speed as client
  Serial.begin(baud_rate);
}

void loop() {
  // Check if data is available to read
  if (Serial.available()) {
    // Read incoming message from client
    String command = Serial.readStringUntil('\n');
    String response;

    if(command[0] == 'M' && command[1] == 'W'){
      int cell = command[3] - '0';
      if(grid[cell] == 'n'){
        response = "MA_";
        response += cell;
        Serial.println(response);
        grid[cell] = 'e';
      } else {
        Serial.println("Cell is already occupied!");
      }
    } else {
        Serial.println("Unidentified command!");
    }
  }
}