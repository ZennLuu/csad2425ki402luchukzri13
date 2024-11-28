#include <ArduinoUnit.h>

// Mock grid for tests
extern char grid[];
extern char currentMove;
extern int gameMode;
extern bool gameOver;

// Mock functions to test
extern char checkForWin();
extern bool checkForDraw();
extern void resetGrid();
extern String processCommand(String command);

// Test case: Check for initial game grid state
test(test_initial_grid_state) {
  for (int i = 0; i < 9; i++) {
    assertEqual(grid[i], 'n');
  }
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
  Test::run(); // Run all tests
}

void loop() {
  // Nothing here
}
