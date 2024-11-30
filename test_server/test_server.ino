/**
 * @file test_server.ino
 * @brief tests for game logic 
 */

#include <ArduinoUnit.h>

 /**
  * @brief Communication speed for server (must match client's).
  */
const int baud_rate = 115200;

/**
 * @brief Indicates to who current move up to.
 */
char currentMove = 'x';

/**
 * @brief Game mode (0 - Man vs Man, 1 - Man vs AI, 2 - AI vs AI).
 */
int gameMode = 0;

/**
 * @brief Game over flag.
 */
bool gameOver = false;

/**
 * @brief Grid for the game ('n' - empty, 'x' - X, 'o' - O).
 */
char grid[] = {
  'n', 'n', 'n',
  'n', 'n', 'n',
  'n', 'n', 'n'
};

/**
 * @brief Check for a win.
 * @return The character representing the winner ('x', 'o') or 'n' if no winner.
 */
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

/**
 * @brief Check if the game is a draw.
 * @return True if the game is a draw, false otherwise.
 */
bool checkForDraw() {
    for (int i = 0; i < 9; i++) {
        if (grid[i] == 'n') {
            return false;
        }
    }
    return true;
}

/**
 * @brief Reset the game grid to its initial state.
 */
void resetGrid() {
    for (int i = 0; i < 9; i++) {
        grid[i] = 'n';
    }
}

/**
 * @brief Processess input command.
 * @param command input command.
 * @return command result.
 */
String processCommand(String command) {
    String response;
    if (command[0] == 'M' && command[1] == 'W') {
        // if game is not over go over current mode move processing
        if (!gameOver) {
            if (gameMode == 0) {
                int cell = command[3] - '0';
                if (grid[cell] == 'n') {
                    response = "MA_";
                    response += cell;
                    response += currentMove;
                    grid[cell] = currentMove;
                    currentMove = currentMove == 'x' ? 'o' : 'x';
                }
                else {
                    response = "Cell is already occupied!";
                }
            }
            else if (gameMode == 1) {
                // See if current move up to Man or AI
                if (currentMove == 'x') {
                    int cell = command[3] - '0';
                    if (grid[cell] == 'n') {
                        response = "MA_";
                        response += cell;
                        response += currentMove;
                        grid[cell] = currentMove;
                        currentMove = currentMove == 'x' ? 'o' : 'x';
                    }
                    else {
                        response = "Cell is already occupied!";
                    }
                }
                else {
                    int cell;
                    do {
                        cell = random(0, 9);
                    } while (grid[cell] != 'n');
                    response = "MA_";
                    response += cell;
                    response += currentMove;
                    grid[cell] = currentMove;
                    currentMove = currentMove == 'x' ? 'o' : 'x';
                }
            }
            else if (gameMode == 2) {
                int cell;
                do {
                    cell = random(0, 9);
                } while (grid[cell] != 'n');
                response = "MA_";
                response += cell;
                response += currentMove;
                grid[cell] = currentMove;
                currentMove = currentMove == 'x' ? 'o' : 'x';
            }
        }
        else {
            response = "Game already over!";
        }
    }
    // Reset command
    else if (command[0] == 'R' && command[1] == 'W') {
        resetGrid();
        currentMove = 'x';
        gameOver = false;
        response = "RA";
    }
    // Win check command
    else if (command[0] == 'W' && command[1] == 'W') {
        if (checkForWin() == 'x') {
            response = "WA_x";
            gameOver = true;
        }
        else if (checkForWin() == 'o') {
            response = "WA_o";
            gameOver = true;
        }
        else if (checkForDraw()) {
            response = "WA_d";
            gameOver = true;
        }
        else
            response = "WD";
    }
    // Gamemode set request
    else if (command[0] == 'G' && command[1] == 'W') {
        int mode = command[3] - '0';
        if (0 <= mode <= 2) {
            gameMode = mode;
            response = "GA";
        }
        else {
            response = "Invalid game mode!";
        }
    }
    // Save game request
    else if (command[0] == 'S' && command[1] == 'W') {
        response = "SA_";
        response += gameMode;
        response += currentMove;
        for (int i = 0; i < 9; i++) {
            response += grid[i];
        }
    }
    // Load game request
    else if (command[0] == 'L' && command[1] == 'W') {
        response = "LA";
        int tempGameMode = command[3] - '0';
        if (0 <= tempGameMode <= 2) {
            gameMode = tempGameMode;
        }
        else {
            response = "Invalid gamemode!";
        }
        if (command[4] == 'x') {
            currentMove = 'x';
        }
        else if (command[4] == 'o') {
            currentMove = 'o';
        }
        else {
            response = "Invalid currentMove!";
        }
        for (int i = 5; i < 14; i++) {
            if (command[i] == 'x') {
                grid[i - 5] = 'x';
            }
            else if (command[i] == 'o') {
                grid[i - 5] = 'o';
            }
            else if (command[i] == 'n') {
                grid[i - 5] = 'n';
            }
            else {
                response = "Invalid grid cells";
            }
        }
    }
    else {
        response = "Unidentified command!";
    }
    return response;
}

/**
 * @brief Test case: Check for win in a row 1
 */
test(test_check_for_win_row1) {
  resetGrid();
  grid[0] = 'x'; grid[1] = 'x'; grid[2] = 'x'; // Winning row
  assertEqual(checkForWin(), 'x');
}
/**
 * @brief Test case: Check for win in a row 2
 */
test(test_check_for_win_row2) {
  resetGrid();
  grid[3] = 'x'; grid[4] = 'x'; grid[5] = 'x'; // Winning row
  assertEqual(checkForWin(), 'x');
}
/**
 * @brief Test case: Check for win in a row 3
 */
test(test_check_for_win_row3) {
  resetGrid();
  grid[6] = 'x'; grid[7] = 'x'; grid[8] = 'x'; // Winning row
  assertEqual(checkForWin(), 'x');
}

/**
 * @brief Test case: Check for win in a column 1
 */
test(test_check_for_win_column1) {
  resetGrid();
  grid[0] = 'o'; grid[3] = 'o'; grid[6] = 'o'; // Winning column
  assertEqual(checkForWin(), 'o');
}
/**
 * @brief Test case: Check for win in a column 2
 */
test(test_check_for_win_column2) {
  resetGrid();
  grid[1] = 'o'; grid[4] = 'o'; grid[7] = 'o'; // Winning column
  assertEqual(checkForWin(), 'o');
}
/**
 * @brief Test case: Check for win in a column 3
 */
test(test_check_for_win_column3) {
  resetGrid();
  grid[2] = 'o'; grid[5] = 'o'; grid[8] = 'o'; // Winning column
  assertEqual(checkForWin(), 'o');
}

/**
 * @brief Test case: Check for win in a diagonal 1
 */
test(test_check_for_win_diagonal1) {
  resetGrid();
  grid[0] = 'x'; grid[4] = 'x'; grid[8] = 'x'; // Winning diagonal
  assertEqual(checkForWin(), 'x');
}
/**
 * @brief Test case: Check for win in a diagonal 2
 */
test(test_check_for_win_diagonal2) {
  resetGrid();
  grid[2] = 'x'; grid[4] = 'x'; grid[6] = 'x'; // Winning diagonal
  assertEqual(checkForWin(), 'x');
}

/**
 * @brief Test case: Check for draw
 */
test(test_check_for_draw) {
  resetGrid();
  grid[0] = 'x'; grid[1] = 'o'; grid[2] = 'x';
  grid[3] = 'o'; grid[4] = 'x'; grid[5] = 'o';
  grid[6] = 'o'; grid[7] = 'x'; grid[8] = 'o'; // Full grid, no winner
  assertTrue(checkForDraw());
}

/**
 * @brief Test case: Reset the grid
 */
test(test_reset_grid) {
  grid[0] = 'x'; grid[4] = 'o'; grid[8] = 'x';
  resetGrid();
  for (int i = 0; i < 9; i++) {
    assertEqual(grid[i], 'n');
  }
}

/**
 * @brief Test case: Process a valid move command
 */
test(test_process_valid_move) {
  resetGrid();
  currentMove = 'x';
  String response = processCommand("MW_0");
  assertEqual(grid[0], 'x');
  assertEqual(response, "MA_0x");
}

/**
 * @brief Test case: Process an invalid move (occupied cell)
 */
test(test_process_invalid_move) {
  resetGrid();
  grid[0] = 'x'; // Cell already occupied
  currentMove = 'o';
  String response = processCommand("MW_0");
  assertEqual(response, "Cell is already occupied!");
}

/**
 * @brief Test case: Process reset command
 */
test(test_process_reset_command) {
  grid[0] = 'x'; gameOver = true;
  String response = processCommand("RW");
  assertEqual(response, "RA");
  assertFalse(gameOver);
  for (int i = 0; i < 9; i++) {
    assertEqual(grid[i], 'n');
  }
}

/**
 * @brief Test case: Process win check command 1
 */
test(test_process_win_check_command1) {
  resetGrid();
  grid[0] = 'x'; grid[1] = 'x'; grid[2] = 'x';
  String response = processCommand("WW");
  assertEqual(response, "WA_x");
}
/**
 * @brief Test case: Process win check command 2
 */
test(test_process_win_check_command2) {
  resetGrid();
  grid[3] = 'x'; grid[4] = 'x'; grid[5] = 'x';
  String response = processCommand("WW");
  assertEqual(response, "WA_x");
}
/**
 * @brief Test case: Process win check command 3
 */
test(test_process_win_check_command3) {
  resetGrid();
  grid[6] = 'x'; grid[7] = 'x'; grid[8] = 'x';
  String response = processCommand("WW");
  assertEqual(response, "WA_x");
}
/**
 * @brief Test case: Process win check command 4
 */
test(test_process_win_check_command4) {
  resetGrid();
  grid[0] = 'o'; grid[1] = 'o'; grid[2] = 'o';
  String response = processCommand("WW");
  assertEqual(response, "WA_o");
}
/**
 * @brief Test case: Process win check command 5
 */
test(test_process_win_check_command5) {
  resetGrid();
  grid[0] = 'o'; grid[1] = 'o'; grid[2] = 'o';
  String response = processCommand("WW");
  assertEqual(response, "WA_o");
}
/**
 * @brief Test case: Process win check command 6
 */
test(test_process_win_check_command6) {
  resetGrid();
  grid[0] = 'o'; grid[1] = 'o'; grid[2] = 'o';
  String response = processCommand("WW");
  assertEqual(response, "WA_o");
}
/**
 * @brief Test case: Process win check command 7
 */
test(test_process_win_check_command7) {
  resetGrid();
  grid[2] = 'o'; grid[4] = 'o'; grid[6] = 'o';
  String response = processCommand("WW");
  assertEqual(response, "WA_o");
}
/**
 * @brief Test case: Process win check command 8
 */
test(test_process_win_check_command8) {
  resetGrid();
  grid[0] = 'x'; grid[4] = 'x'; grid[8] = 'x';
  String response = processCommand("WW");
  assertEqual(response, "WA_x");
}
/**
 * @brief Test case: Process win check command 9
 */
test(test_process_win_check_command9) {
  resetGrid();
  grid[0] = 'x'; grid[4] = 'x'; grid[8] = 'x';
  String response = processCommand("WW");
  assertEqual(response, "WA_x");
}
/**
 * @brief Test case: Process win check command 10
 */
test(test_process_win_check_command10) {
  resetGrid();
  grid[0] = 'x'; grid[4] = 'x'; grid[8] = 'o';
  String response = processCommand("WW");
  assertEqual(response, "WD");
}
/**
 * @brief Test case: Process win check command 11
 */
test(test_process_win_check_command11) {
  resetGrid();
  grid[0] = 'x'; grid[4] = 'x'; grid[8] = 'o';
  String response = processCommand("WW");
  assertEqual(response, "WD");
}
/**
 * @brief Test case: Process win check command 12
 */
test(test_process_win_check_command12) {
  resetGrid();
  grid[0] = 'x'; grid[1] = 'x'; grid[2] = 'o';
  grid[3] = 'o'; grid[4] = 'x'; grid[5] = 'x';
  grid[6] = 'x'; grid[7] = 'o'; grid[8] = 'o';
  String response = processCommand("WW");
  assertEqual(response, "WA_d");
}

void setup() {
  Serial.begin(baud_rate);
  delay(1000);
  Serial.println("Starting tests...");
}

void loop() {
  Test::run(); // Run all tests
}
