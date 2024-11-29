#include <ArduinoUnit.h>

char currentMove = 'x';
int gameMode = 0;
bool gameOver = false;
char grid[] = {
  'n', 'n', 'n',
  'n', 'n', 'n',
  'n', 'n', 'n'
};

char checkForWin() {
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

bool checkForDraw() {
  for (int i = 0; i < 9; i++) {
    if (grid[i] == 'n') {
      return false;
    }
  }
  return true;
}

void resetGrid() {
  for (int i = 0; i < 9; i++) {
    grid[i] = 'n';
  }
}

String processCommand(String command){
  String response;
  if(command[0] == 'M' && command[1] == 'W') {
    // if game is not over go over current mode move processing
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
        // See if current move up to Man or AI
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
  } 
  // Reset command
  else if(command[0] == 'R' && command[1] == 'W') {
    resetGrid();
    currentMove = 'x';
    gameOver = false;
    response = "RA";
  } 
  // Win check command
  else if(command[0] == 'W' && command[1] == 'W'){
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
  } 
  // Gamemode set request
  else if(command[0] == 'G' && command[1] == 'W') {
    int mode = command[3] - '0';
    if(0 <= mode <= 2){
      gameMode = mode;
      response = "GA";
    } else {
      response = "Invalid game mode!";
    }
  } 
  // Save game request
  else if(command[0] == 'S' && command[1] == 'W') {
    response = "SA_";
    response += gameMode;
    response += currentMove;
    for(int i = 0; i < 9; i++){
      response += grid[i];
    }
  } 
  // Load game request
  else if(command[0] == 'L' && command[1] == 'W') {
    response = "LA";
    int tempGameMode = command[3] - '0';
    if (0 <= tempGameMode <= 2){
      gameMode = tempGameMode;
    } else {
      response = "Invalid gamemode!";
    }
    if(command[4] == 'x'){
      currentMove = 'x';
    } else if (command[4] == 'o'){
      currentMove ='o';
    } else {
      response = "Invalid currentMove!";
    }
    for(int i = 5; i < 14; i++){
      if(command[i] == 'x'){
        grid[i-5] = 'x';
      } else if(command[i] == 'o'){
        grid[i-5] = 'o';
      } else if(command[i] == 'n'){
        grid[i-5] = 'n';
      } else {
        response = "Invalid grid cells";
      }
    }
  } else {
      response = "Unidentified command!";
  }
  return response;
}

// Test case: Check for win in a row
test(test_check_for_win_row) {
  resetGrid();
  grid[0] = 'x'; grid[1] = 'x'; grid[2] = 'x'; // Winning row
  assertEqual(checkForWin(), 'x');
}

// Test case: Check for win in a column
test(test_check_for_win_column) {
  resetGrid();
  grid[0] = 'o'; grid[3] = 'o'; grid[6] = 'o'; // Winning column
  assertEqual(checkForWin(), 'o');
}

// Test case: Check for win in a diagonal
test(test_check_for_win_diagonal) {
  resetGrid();
  grid[0] = 'x'; grid[4] = 'x'; grid[8] = 'x'; // Winning diagonal
  assertEqual(checkForWin(), 'x');
}

// Test case: Check for draw
test(test_check_for_draw) {
  resetGrid();
  grid[0] = 'x'; grid[1] = 'o'; grid[2] = 'x';
  grid[3] = 'o'; grid[4] = 'x'; grid[5] = 'o';
  grid[6] = 'o'; grid[7] = 'x'; grid[8] = 'o'; // Full grid, no winner
  assertTrue(checkForDraw());
}

// Test case: Reset the grid
test(test_reset_grid) {
  grid[0] = 'x'; grid[4] = 'o'; grid[8] = 'x';
  resetGrid();
  for (int i = 0; i < 9; i++) {
    assertEqual(grid[i], 'n');
  }
}

// Test case: Process a valid move command
test(test_process_valid_move) {
  resetGrid();
  currentMove = 'x';
  String response = processCommand("MW_0");
  assertEqual(grid[0], 'x');
  assertEqual(response, "MA_0x");
}

// Test case: Process an invalid move (occupied cell)
test(test_process_invalid_move) {
  resetGrid();
  grid[0] = 'x'; // Cell already occupied
  currentMove = 'o';
  String response = processCommand("MW_0");
  assertEqual(response, "Cell is already occupied!");
}

// Test case: Process reset command
test(test_process_reset_command) {
  grid[0] = 'x'; gameOver = true;
  String response = processCommand("RW");
  assertEqual(response, "RA");
  assertFalse(gameOver);
  for (int i = 0; i < 9; i++) {
    assertEqual(grid[i], 'n');
  }
}

// Test case: Process win check command
test(test_process_win_check_command) {
  resetGrid();
  grid[0] = 'x'; grid[1] = 'x'; grid[2] = 'x'; // Winning row
  String response = processCommand("WW");
  assertEqual(response, "WA_x");
}

void setup() {
  Serial.begin(115200);
  Serial.println("Starting tests...");
}

void loop() {
  Test::run(); // Run all tests
}
