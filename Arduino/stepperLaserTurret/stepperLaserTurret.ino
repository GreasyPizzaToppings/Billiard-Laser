// CNC Shield pin configuration
const int StepX = 2;
const int DirX = 5;
const int StepY = 3;
const int DirY = 6;
const int StepZ = 4;
const int DirZ = 7;

// Custom pin mappings
const int LaserPin = DirZ;
const int StepPanPin = StepX;
const int DirPanPin = DirX;
const int StepTiltPin = StepY;
const int DirTiltPin = DirY;

// Parameters
const int TotalSteps = 9000; // Total steps for full range of movement
const int MiddlePoint = TotalSteps / 2; // Middle point of the motor
const int PulseTime = 850; //<1200 pps
int StepAmount = 100; // Default step amount

void setup() {
  pinMode(StepX, OUTPUT);
  pinMode(DirX, OUTPUT);
  pinMode(StepY, OUTPUT);
  pinMode(DirY, OUTPUT);
  pinMode(StepZ, OUTPUT);
  pinMode(DirZ, OUTPUT);
  pinMode(LaserPin, OUTPUT);

  Serial.begin(9600);
  Serial.setTimeout(10);
}

// Parse the step amount from the received command
void setStepAmount(String command) {
  if (command.startsWith("s")) {
    int newStepAmount = command.substring(1).toInt(); // Extract the number after 's'
    if (newStepAmount >= 1 && newStepAmount <= 500) { // Validate range
      StepAmount = newStepAmount;
      Serial.print("Step amount set to: ");
      Serial.println(StepAmount);
    } else {
      Serial.println("Invalid step amount. Must be between 1 and 500.");
    }
  }
}

// Main loop to handle serial commands
void loop() {
  if (Serial.available() > 0) {
    String command = Serial.readStringUntil('\n'); // Read full command until newline

    // Turn on laser for '1'
    if (command == "1") {
      digitalWrite(LaserPin, HIGH);
    }

    // Turn off laser for '0'
    else if (command == "0") {
      digitalWrite(LaserPin, LOW);
    }

    // Pan left for 'l'
    else if (command == "l") {
      digitalWrite(DirPanPin, LOW); // Anticlockwise
      for (int steps = 0; steps < StepAmount; steps++) {
        digitalWrite(StepPanPin, HIGH);
        delayMicroseconds(PulseTime);
        digitalWrite(StepPanPin, LOW);
        delayMicroseconds(PulseTime);
      }
    }

    // Pan right for 'r'
    else if (command == "r") {
      digitalWrite(DirPanPin, HIGH); // Clockwise
      for (int steps = 0; steps < StepAmount; steps++) {
        digitalWrite(StepPanPin, HIGH);
        delayMicroseconds(PulseTime);
        digitalWrite(StepPanPin, LOW);
        delayMicroseconds(PulseTime);
      }
    }

    // Tilt up for 'u'
    else if (command == "u") {
      digitalWrite(DirTiltPin, LOW); // Anticlockwise
      for (int steps = 0; steps < StepAmount; steps++) {
        digitalWrite(StepTiltPin, HIGH);
        delayMicroseconds(PulseTime);
        digitalWrite(StepTiltPin, LOW);
        delayMicroseconds(PulseTime);
      }
    }

    // Tilt down for 'd'
    else if (command == "d") {
      digitalWrite(DirTiltPin, HIGH); // Clockwise
      for (int steps = 0; steps < StepAmount; steps++) {
        digitalWrite(StepTiltPin, HIGH);
        delayMicroseconds(PulseTime);
        digitalWrite(StepTiltPin, LOW);
        delayMicroseconds(PulseTime);
      }
    }

    // Adjust step amount for 'sXXX' command
    else if (command.startsWith("s")) {
      setStepAmount(command);
    }
  }
}
