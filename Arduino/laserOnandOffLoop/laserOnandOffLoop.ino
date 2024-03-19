// Arduino code to control a laser module turning on and off every second

const int laserPin = 13; // The pin connected to the laser module

void setup() {
  
  pinMode(laserPin, OUTPUT); // Set the laser pin as an output

  Serial.begin(9600);
  Serial.setTimeout(10); // Respond to serial commands faster

}

void loop() {

  digitalWrite(laserPin, HIGH); // Turn on the laser
  delay(1000);
  digitalWrite(laserPin, LOW); // Turn off the laser
  delay(1000);
}
