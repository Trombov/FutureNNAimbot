# FutureNNAimbot
Universal neural network aimbot for all games with custom training mode

## NVIDIA ONLY

This cheat uses machine learning, which recognizes objects in a certain range, then aims at the object and shoots. It does not interfere in any way with the memory of the game, it simply takes screenshots and recognizes objects.
Of course, this is not the final version, in the future we will have to wait for improvements of a different type (amd video card support (if I have one for the test :)), smooth aim, AdvancedRCS for any games, improvements in object recognition, etc.).

**Training dependencies must be downloaded from the Release version and copied to the Debug folder!**

**Upload trained files of games in the "Trained files for games" folder via pull requests!**

**If you cannot upload the files, then write to me in Telegram: CowDrow, or Discord: ZxCL#1220!**

## Requirements:
* Install the latest Nvidia driver for your graphics card
* Install the [Nvidia CUDA Toolkit 10.0](https://developer.nvidia.com/cuda-toolkit-archive) (**NOT 10.1!!!**) (for cuda driver support when training)

### Menu in Training Mode:
* Insert to enable/disable Training Mode
* Arrow keys to change size of the blue box
* PageUp and PageDown to change the target object for training
* Home to take pictures of the object (you will hear a beep)
* End to exit training mode and start training the neural network to detect objects
* Backspace to take screenshots of objects that should not be recognized.

### Normal Menu:
* Insert to enable/disable Training Mode
* Arrow keys to change SmoothAim value
* PageUp and PageDown to change target object
* Home to enable/disable SimpleRCS
* Delete to aim at the Head or Body

#### Default settings (optional):
* 320 for search range X and Y values
* MiddleMouse to shoot
* True for SimpleRCS
* Insert to enable/disable Training Mode
* Home to take a screenshot in Training Mode
* Numpad9 to change screeshot mode (centered/following)
* 0.1 for SmoothAim value
* True to show real-time Info on your settings
* False to aim at the head

**In the config.json file, you can change the resolution range for object recognition, enable/disable SimpleRCS, specify which game you will use, and specify the button to attack and enable/disable Training Mode.**

#### Training the NN:
When you start the game for the first time, you will be asked to start training, this step is not optional (unless you already have a pre-trained NN for the game).

During training, you must select the target object using PageUp and PageDown, take multiple screenshots of the target objects in the blue frame by pressing **HOME**(files should be created at darknet/data/img/), and once you're done, press the **END** button.

The training process for the neural network begins.

After you are tired of waiting or you have reached the peak on the chart (description below), then press CTRL + C and confirm the action by writing Y and pressing enter in the window form(with a lot of text).

After that, you will need to open the game again. When the game is started, write to the console `done`.
Files should be copied into the trainfiles folder automatically (if not, then check in the darknet folder and subfolders; such as data or backup, for files like: GAME.names(data folder) GAME.cfg(darknet folder) GAME_last.weights(backup folder, file should be renamed to GAME.weights in order to start the program)

#### Training a pre-trained NN:
Press Insert and you will enter training mode. If you press Insert again you will leave training mode.

After this, repeat the steps as shown in the **Training the NN** section.

**New video on how to train step by step:** https://youtu.be/NhTlDnXeLC8

**Video on how to train:** https://youtu.be/B2G-3stYPPI

**Video demonstration of a "good" trained neural network:** https://youtu.be/2DCMulOaMVg

#### Important information:
**Train with different distances, lights, and angles for the best possible recognition.**

**Take screenshots of objects that should not be recognized by pressing Backspace**

**When you finished training, write in cfg file: `batch=1` and `subdivision=1` for testing(recognizing)**

**New width and height must be divisible by 32**

**If you want to make a point for enemys on the picture then run the yolo_mark.bat file(change it first for correct values)**

**The lower the Average Loss, the better the detection of objects.**

**You should get weights from the Early Stopping Point:**

**When it's at the Early Stopping Point, press Ctrl+C to stop and make more pictures to train on!**
![screenshot of sample](https://camo.githubusercontent.com/51af5be5cfa94b6d741c90d10a163b168bf9170e/68747470733a2f2f6873746f2e6f72672f66696c65732f3564632f3761652f3766612f35646337616537666164396434653365623361343834633538626663316666352e706e67)

If there are any problems or suggestions, please write me about it.

**Also, pull requesting this github is welcome!**

### FAMOUS ISSUES:
1.Unable to load DLL 'x64\yolo_cpp_dll_gpu.dll'

**Solution**: Uninstall CUDA 10.1 and make sure that you only installed CUDA 10.0

#### Change log:

Version 0.6-alpha:

1. Added funtion "Empty object on screenshots for better detection" check README.md on how to use it
2. Added Smooth aim
3. Better aiming for when two enemies are in the same frame
4. Train again mode correcting

**TODO:**
1. Advanced RCS settings
2. Config testing for better recognizion
