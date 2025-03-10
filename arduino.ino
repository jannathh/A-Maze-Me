#define xAxis A0
#define yAxis A1

void setup() {
  Serial.begin(9600);
  pinMode(xAxis, INPUT);
   
  pinMode(yAxis, INPUT);
}

void loop() {
  int xVal = analogRead(xAxis);
  int yVal = analogRead(yAxis);

  float xMapped = (xVal - 512) / 512.0;
  float yMapped = (yVal - 512) / 512.0;

  if (xMapped <= -0.6) {
    Serial.println("LEFT");
  } 
  else if (xMapped >= 0.6) {
    Serial.println("RIGHT");
  } 
  else if (yMapped <= -0.6) {
    Serial.println("UP");
  } 
  else if (yMapped >= 0.6) {
    Serial.println("DOWN");
  }
  delay(160);
}
