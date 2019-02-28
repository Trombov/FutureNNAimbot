# FutureNNAimbot
Universal neural network aimbot for all games with custom training mode

This cheat on machine learning, which recognizes objects in a certain range, then leads to an object and makes a shot. He does not interfere in any way with the memory of the game, he simply takes screenshots and recognizes objects.
Of course, this is not the final version, in the future we will have to wait for improvements of a different type (amd video card support (if I have one for the test :)), smooth aim, AdvancedRCS for any games, improvements in object recognition, etc.).

Okay, let's get down to the detailed description:
---
Menu:
1. Button Insert for enable training mode or disable
2. Arrows for change size of the blue box (training mode)
3. Button PageUp and PageDown for shoot object for shoot or train
4. Button Home for make picture of object (training mode) (you will hear the sound of Beep)
5. Button

Default settings:
1. RButton for shoot (u can change it)
2. Insert for enter in training mode (u can change it)

**In the config.json file, you can change the range resolution for object recognition, enable or disable SimpleRCS, specify which game we will use and specify the buttons for attack and training.

Requirements:
---
1. Install the latest Nvidia driver for your graphic device
2. Install Nvidia CUDA Toolkit 10.0 (must be installed add a hardware driver for cuda support)

If you have something missing, the program will tell you about it.

**Video on how to training: https://youtu.be/B2G-3stYPPI

**Video demonstration of a "good" trained neural network: https://youtu.be/2DCMulOaMVg



Important information
---
**Take screenshots of objects that should not be recognized by pressing BACKSPACE button**

**Then u finished training write in cfg file batch=1 and subdivision=1 for testing(recognizing)**

**New width and height must be divisible by 32**

**If u want make point for enemys on the picture then just run yolo_mark.bat file(change it first for correct values)**

**The lower the AVG, the better the detection of objects.**

**You should get weights from Early Stopping Point:**

**Then it's on Early Stopping Point then press Ctrl+C to stop and make more pictures for train!**
![screenshot of sample](https://camo.githubusercontent.com/51af5be5cfa94b6d741c90d10a163b168bf9170e/68747470733a2f2f6873746f2e6f72672f66696c65732f3564632f3761652f3766612f35646337616537666164396434653365623361343834633538626663316666352e706e67)

If there are any problems or suggestions, please write me about it.

**Also pull request at github is welcome!**

**TODO:**
1. Smooth aim
2. Advanced RCS settings
3. Config testing for better recognizion
