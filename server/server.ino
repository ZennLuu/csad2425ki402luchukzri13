// Set up communication speed same as client
const int baud_rate = 115200;

char currentMove = 'x';
int gameMode = 0;
bool gameOver = false;
char grid[] = {
  'n', 'n', 'n',
  'n', 'n', 'n',
  'n', 'n', 'n'
};

char checkForWin(){
  const int winPatterns[8][3] = {
      {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
      {0, 3, 6}, {1, 4, 7}, {2, 5, 8},
      {0, 4, 8}, {2, 4, 6}
  };
    
  for (auto pattern : winPatterns) {
    if (grid[pattern[0]] != 'n' && grid[pattern[0]] == grid[pattern[1]] && grid[pattern[1]] == grid[pattern[2]]) {
      return grid[pattern[0]];
    }
  }
  return 'n';
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
        if(gameMode == 0){ 
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
        } else if (gameMode == 1){
          if(currentMove == 'x'){
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
          } else {
            int cell;
            do {
              cell = random(0,9);
            } while (grid[cell] != 'n');
            response = "MA_";
            response += cell;
            response += currentMove;
            grid[cell] = currentMove;
            currentMove = currentMove == 'x' ? 'o' : 'x';
          }
        } else if (gameMode == 2) {
            int cell;
            do {
              cell = random(0,9);
            } while (grid[cell] != 'n');
            response = "MA_";
            response += cell;
            response += currentMove;
            grid[cell] = currentMove;
            currentMove = currentMove == 'x' ? 'o' : 'x';
        }
      } else {
        response = "Game already over!";
      }
    } else if(command[0] == 'R' && command[1] == 'W') {
        resetGrid();
        currentMove = 'x';
        gameOver = false;
        response = "RA";
    } else if(command[0] == 'W' && command[1] == 'W'){
        if(checkForWin() == 'x'){
          response = "WA_x";
          gameOver = true;  
        }
        else if(checkForWin() == 'o'){
          response = "WA_o";
          gameOver = true;
        }
        else if(checkForDraw()){
          response = "WA_d";
          gameOver = true;
        }
        else
          response = "WD";
    } else if(command[0] == 'G' && command[1] == 'W') {
      int mode = command[3] - '0';
      if(0 <= mode <= 2){
        gameMode = mode;
        response = "GA";
      } else {
        response = "Invalid game mode!";
      }
    } else {
        response = "Unidentified command!";
    }
    // Send response to client
    Serial.println(response);

    // Blink at command execution
    delay(100);
    digitalWrite(led, LOW);
  }
}