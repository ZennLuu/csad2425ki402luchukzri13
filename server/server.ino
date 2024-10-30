// Set up communication speed same as client
const int baud_rate = 115200;

char currentMove = 'x';
bool gameOver = false;
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

bool checkForDraw(){
  for(int i = 0; i < 9; i++){
    if(grid[i] == 'n')
      return false;
  }
  return true;
}

void resetGrid(){
  for(int i = 0; i < 9; i++)
    grid[i] = 'n';
}

const int led = 2;

void setup() {
  // Start the UART communication with same speed as client
  Serial.begin(baud_rate);
  pinMode(led, OUTPUT);
  digitalWrite(led, HIGH);
  delay(1000);
  digitalWrite(led, LOW);
}

void loop() {
  // Check if data is available to read
  if (Serial.available()) {
    // Read command from client
    String command = Serial.readStringUntil('\n');
    digitalWrite(led, HIGH);

    String response;

    if(command[0] == 'M' && command[1] == 'W') {
      if(!gameOver){
        int cell = command[3] - '0';
        if(grid[cell] == 'n'){
          response = "MA_";
          response += cell;
          response += currentMove;
          grid[cell] = currentMove;
          currentMove = currentMove == 'x' ? 'o' : 'x';
        } else {
          response = "Cell is already occupied!";
        }
      }
    } else if(command[0] == 'R' && command[1] == 'W') {
        resetGrid();
        currentMove = 'x';
        gameOver = false;
        response = "RA";
    } else if(command[0] == 'W' && command[1] == 'W'){
        if(checkForWin('x')){
          response = "WA_x";
          gameOver = true;  
        }
        else if(checkForWin('o')){
          response = "WA_o";
          gameOver = true;
        }
        else if(checkForDraw()){
          response = "WA_d";
          gameOver = true;
        }
        else
          response = "WD";
    } else {
        response = "Unidentified command!";
    }
    // Send response to client
    Serial.println(response);

    delay(100);
    digitalWrite(led, LOW);
  }
}