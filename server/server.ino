// Set up communication speed same as client
const int baud_rate = 115200;

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