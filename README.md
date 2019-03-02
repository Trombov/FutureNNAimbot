# FutureNNAimbot
Universal neural network aimbot for all games with custom training mode

## NVIDIA ONLY

This cheat on machine learning, which recognizes objects in a certain range, then leads to an object and makes a shot. He does not interfere in any way with the memory of the game, he simply takes screenshots and recognizes objects.
Of course, this is not the final version, in the future we will have to wait for improvements of a different type (amd video card support (if I have one for the test :)), smooth aim, AdvancedRCS for any games, improvements in object recognition, etc.).

**To launch sources download release version and copy file to DEBUG folder!**

**Upload trained files of games in the "Trained files for games" folder via pull requests!**

**Or if u can't then write me in telegram: CowDrow or Discord: ZxCL#1220!**


Okay, let's get down to the detailed description:
---
Menu in training mode:
1. Button Insert for enable training mode or disable
2. Arrows for change size of the blue box (training mode)
3. Button PageUp and PageDown for change object for  train
4. Button Home for make picture of object (training mode) (you will hear the sound of Beep)
5. Button End for exit training mode and start train neural network for detect objects
6. Button BACKSPACE for taking screenshots of objects that should not be recognized. **Take screenshots of objects that should not be recognized by pressing BACKSPACE button**.

Menu in playing mode:
1. Button Insert for enable training mode or disable
2. Arrows for change SmoothAim value
3. Button PageUp and PageDown for change object for shoot
4. Button Home for enable or disable SimpleRCS
5. Button Delete for change aim to Head or Body

Default settings:
1. MouseWheel for shoot (u can change it)
2. Insert for enter in training mode (u can change it)

**In the config.json file, you can change the range resolution for object recognition, enable or disable SimpleRCS, specify which game we will use and specify the buttons for attack and training.**

Requirements:
---
1. Install the latest Nvidia driver for your graphic device
2. Install Nvidia CUDA Toolkit 10.0(**!!!NOT 10.1!!!**) (must be installed add a hardware driver for cuda support)

If you have something missing, the program will tell you about it.

How to train NN
---
When you start the game for the first time, you will be immediately asked to start training. You can not refuse :)

During a workout, you must select an object using special buttons(PageUP and PageDOWN), take screenshots of objects in the frame by pressing **HOME**(files should be created at darknet/data/img/), and after the procedure done, click on the **END** button.

The training process for neural network begins. You can quietly close the game for best performance.

After you are tired of waiting or you have reached the peak on the chart (description below), then press CTRL + C and confirm the action by writing Y and pressing enter in the window form(with a lot of text).

After that, you definitely need to open the game again. Then the game is started, write to the console "done".
Files should be copied into the trainfiles folder(automatically, but if it not then chekc files in the darknet(folder and subfolders) like a GAME.names(data folder) GAME.cfg(darknet folder) GAME_last.weights(backup folder, file shood be renamed to GAME.weights for program starts)

How to train NN if you already trained it
---
Press Insert button and u will come in training mode. If u press Insert again u will leave training mode.

After this, repeat the steps as shown above.


**Video on how to training:** https://youtu.be/B2G-3stYPPI

**Video demonstration of a "good" trained neural network:** https://youtu.be/2DCMulOaMVg



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



**FAMOUS ISSUES:**
---
1.Unable to load DLL 'x64\yolo_cpp_dll_gpu.dll'

**Solution**: Unistall CUDA 10.1 and make sure that you install CUDA 10.0





Change log:

Version 0.6-alpha:

1. Added funtion "Empty object on screenshots for better detection" check README.md to how to use it
2. Added Smooth aim
3. Better aiming on two enemys on the frame
4. Train again mode correcting

**TODO:**
1. Advanced RCS settings
2. Config testing for better recognizion
