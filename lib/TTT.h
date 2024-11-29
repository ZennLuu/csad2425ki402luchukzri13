/**
 * @file TTT.h
 * @brief Game logic main functions
 */

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