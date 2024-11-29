/**
 * @file server.ino
 * @brief Logic for Game and server side communication 
 */

#include "../lib/TTT.h"

/**
 * @brief Led pin for blinking between command processing.
 */
const int led = 2;

/**
 * @brief Initialize the Arduino system.
 */
void setup() {
    // Start the UART communication at the same speed as the client.
    Serial.begin(baud_rate);
    pinMode(led, OUTPUT);

    // Blink at startup.
    digitalWrite(led, HIGH);
    delay(1000);
    digitalWrite(led, LOW);
}

/**
 * @brief Main loop to process input commands from the Serial port.
 */
void loop() {
  // Check if data is available to read
  if (Serial.available()) {
    // Read command from client
    String command = Serial.readStringUntil('\n');
    digitalWrite(led, HIGH);

    // Process input command
    String response = processCommand(command);

    // Send response to client
    Serial.println(response);

    // Blink at command execution
    delay(100);
    digitalWrite(led, LOW);
  }
}