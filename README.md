# FutureNNAimbot
Universal neural network aimbot for all games with custom training mode

This is a modified version specifically for Apex Legends.

Download Train data from https://github.com/vadash/a_pepe_x_legends_training_data/tree/master/img and follow the instruccions on how to use nvidia profiler.

Sample config:
[{"Game":"r5apex",
"Head":false,
"Information":true,
"DrawAreaRectangle":false,
"DrawText":true,
"ScreenshotKey":36,
"ScreenshotModeKey":105,
"ShootKey":2,
"SimpleRCS":true,
"SizeX":320,
"SizeY":320,
"SmoothAim":0.80,
"TrainModeKey":45
}]

Features:
* Optimized and refactored the code to run faster and reduce frame drops
* Option to hide or show text/rectangles
* Change colors and opacity for the player frames
* Better activation of the aim bot, will scan when L/R mouse buttons are pressed nd activate auto lock on target
* Fixed bug that triggered the mouse button or simulated a mouse up event causing the player to not shoot to target
* other freebies
