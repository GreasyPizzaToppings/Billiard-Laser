#include<Servo.h>

Servo serX;
Servo serY;

String serialData;

void setup() {

  serX.attach(11);
  serY.attach(12);
  Serial.begin(9600);
  Serial.setTimeout(10);

  serX.write(90);
  serY.write(90);

  digitalWrite(9, HIGH);
}

void loop() {

  serialData = Serial.readString();

  if (serialData.length() > 2) {
    int x = parseDataX(serialData);
    int y = parseDataY(serialData);

    Serial.print("x");
    Serial.println(x);
    Serial.print("y");
    Serial.print(y);
    Serial.println();

    serX.write(x);
    serY.write(y);

    delay(15);
  }
}

int parseDataX(String data){
  data.remove(data.indexOf("Y"));
  data.remove(data.indexOf("X"), 1);
  return data.toInt();
}

int parseDataY(String data){
  data.remove(0,data.indexOf("Y") + 1);
  return data.toInt();
}

