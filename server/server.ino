void setup() {
  // Start the UART communication
  Serial.begin(115200);
  Serial.println("ESP32 is ready to communicate.");
}

void loop() {
  // Check if data is available to read
  if (Serial.available()) {
    // Read incoming message from client
    String received_message = Serial.readStringUntil('\n');

    String modified_message = received_message + " - Hello from ESP32!";

    // Send the modified message back to the client
    Serial.println(modified_message);
    delay(1000); // Optional delay for stability
  }
}