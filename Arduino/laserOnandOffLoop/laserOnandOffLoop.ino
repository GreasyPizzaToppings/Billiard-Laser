const int laserPin = 13;      // Define the pin connected to the laser module

void setup() {
  pinMode(laserPin, OUTPUT);  // Set the laser pin as an output
  Serial.begin(9600);         // Initialize serial communication at 9600 baud rate
  Serial.setTimeout(10);
}

void loop() {
  if (Serial.available() > 0) {

    char command = Serial.read();  // Read the incoming command from serial

    // Turn on laser for '1'
    if (command == '1') {
      digitalWrite(laserPin, HIGH);
    }

    // Turn off laser for '0'
    else if (command == '0') {
      digitalWrite(laserPin, LOW);
    }
  }
}
