// Set up communication speed same as client
const int baud_rate = 115200;

char grid[9] = {
  '0', '0', '0',
  '0', '0', '0',
  '0', '0', '0'
};

char win = 'n';

bool checkForWin(){
  char C = 'o';
  for (int k = 0; k < 2; k++){
    // check horizontal lines
    for(int i = 0; i < 3; i++){
      if(grid[i*3] == grid[i*3 + 1] == grid[i*3 + 2] == C){
        win = C;
        return true;
      }
    }
    // check vertical lines
    for(int i = 0; i < 3; i++){
      if(grid[i] == grid[i + 3] == grid[i + 6] == C){
        win = C;
        return true;
      }
    }
    // check diagonals
    if((grid[0] == grid[4] == grid[8] == C) || (grid[2] == grid[4] == grid[6] == C)){
      win = C;
      return true;
    }
    C = 'x'
  }
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
    String received_message = Serial.readStringUntil('\n');
 
    // Modify recieved message and send it back to client
    String modified_message = received_message + " - Hello from ESP32!";
    Serial.println(modified_message);
  }
}