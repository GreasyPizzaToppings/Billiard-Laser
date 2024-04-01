#include <Servo.h>

Servo pan, tilt;

// pin mappings
const int laserPin = 13;      // Define the pin connected to the laser module
const int panPin = 11;
const int tiltPin = 12;

int panAngle = 90;  // Initial pan angle (0-180 degrees)
int tiltAngle = 90; // Initial tilt angle (0-180 degrees)

void setup() {
  pinMode(laserPin, OUTPUT);  // Set the laser pin as an output
  Serial.begin(9600);         // Initialize serial communication at 9600 baud rate
  Serial.setTimeout(10);

  pan.attach(panPin);
  tilt.attach(tiltPin);

  pan.write(panAngle);  // Set initial pan angle
  tilt.write(tiltAngle); // Set initial tilt angle
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

    // Pan left for 'l'
    else if (command == 'l') {
      panAngle -= 10; // Decrease pan angle by 10 degrees
      panAngle = constrain(panAngle, 0, 180); // Constrain pan angle to 0-180 degrees
      pan.write(panAngle); // Update pan servo position
    }

    // Pan right for 'r'
    else if (command == 'r') {
      panAngle += 10; // Increase pan angle by 10 degrees
      panAngle = constrain(panAngle, 0, 180); // Constrain pan angle to 0-180 degrees
      pan.write(panAngle); // Update pan servo position
    }

    // Tilt up for 'u'
    else if (command == 'u') {
      tiltAngle -= 10; // Decrease tilt angle by 10 degrees
      tiltAngle = constrain(tiltAngle, 0, 180); // Constrain tilt angle to 0-180 degrees
      tilt.write(tiltAngle); // Update tilt servo position
    }

    // Tilt down for 'd'
    else if (command == 'd') {
      tiltAngle += 10; // Increase tilt angle by 10 degrees
      tiltAngle = constrain(tiltAngle, 0, 180); // Constrain tilt angle to 0-180 degrees
      tilt.write(tiltAngle); // Update tilt servo position
    }
    
  }
}
