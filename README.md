# A-Maze-Me 🌀

Welcome to **A-Maze-Me**, an interactive maze game that combines both hardware and software to deliver a fun and competitive experience! The game is built using **Unity**, while an **Arduino-based joystick** is used to control an avatar navigating through a maze.

Players start from the center of the maze and race to a predetermined endpoint as fast as possible. Their speed and efficiency determine their ranking on the leaderboard. The project also incorporates **machine learning** to analyze gameplay data and enhance the experience.

## 📜 Table of Contents
- [Features](#features)
- [System Architecture](#system-architecture)
- [Installation](#installation)
- [Usage](#usage)
- [Arduino Joystick Module](#arduino-joystick-module)
- [Contributing](#contributing)


## 🚀 Features
✅ **Random Maze Generation** - Unique mazes generated using an algorithm  
✅ **Arduino-based Joystick Control** - Navigate the maze with a custom controller  
✅ **Real-time Leaderboard** - Tracks player rankings based on speed and efficiency  
✅ **Machine Learning Analysis** - Game data is analyzed for insights  
✅ **Engaging Sound & Visual Feedback** - Immersive gaming experience  

## 🏗 System Architecture
### Unity Application Components
- **MazeGenerator**: Creates mazes by initializing a grid filled with walls and carving paths using a recursive algorithm.  
- **JoystickManager**: Manages serial communication between the Arduino and Unity, reading input data and passing it to the movement script.  
- **MazePlayerMovement**: Translates joystick inputs into smooth movement using acceleration and deceleration.  
- **GoalCollision**: Triggers sound feedback, resets the maze, and updates the score when the player reaches the goal.  

## 🛠 Installation
1. Clone this repository:
   ```bash
   git clone https://github.com/jannathh/A-Maze-Me.git
   ```
2. Navigate to the project directory:
   ```bash
   cd A-Maze-Me
   ```
3. Install dependencies:
   ```bash
   pip install -r requirements.txt  # For Python projects
   ```
   OR
   ```bash
   npm install  # For JavaScript projects
   ```
4. Open the Unity project in **Unity Editor**.
5. Connect the **Arduino-based joystick** to your system.
6. Run the project:
   ```bash
   python main.py  # For Python
   ```
   OR
   ```bash
   npm start  # For JavaScript
   ```

## 🎮 Usage
1. Start the **A-Maze-Me** application in Unity.
2. Use the Arduino-based joystick to control the player’s movement.
3. Navigate through the maze and reach the goal as fast as possible!
4. Check your ranking on the leaderboard.

## 🎛️ Arduino Joystick Module
The **Arduino-based joystick module** allows for smooth and responsive player movement within the maze. The joystick data is read by an Arduino script and sent to Unity via serial communication, ensuring real-time interaction between hardware and software. 

To use the Arduino joystick code:
1. Navigate to the `ArduinoJoystick` folder in the repository.
2. Open the `.ino` file in the **Arduino IDE**.
3. Connect your Arduino to your computer via USB.
4. Upload the script to the Arduino board.
5. Ensure the correct COM port is selected in Unity for communication.

## 🤝 Contributing
We welcome contributions! Follow these steps:
1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature-branch
   ```
3. Make your changes and commit:
   ```bash
   git commit -m "Add new feature"
   ```
4. Push to your fork and create a pull request.


Feel free to reach out if you have any questions or suggestions!
