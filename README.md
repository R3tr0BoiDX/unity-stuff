# SimpleAnimator and SimpleAnimatorEditor
Simple lightwight Animator
- GUI Mode: Change between SpriteRenderer or Image (Unity GUI element)
- Start At Spawn: Start animation at start (NOT AWAKE!)
- Loop Animation: Repeat animation endless, jumping from the last to the first sprite
- Destroy After Finish: When animation is played, destroy the gameobject. If "Loop Animation" is active, this gets not triggered
- Destroy Delay: Time till the gameobject gets destroyed after the animation is done.
- Time Between Frames: How long it takes to jump from sprite to sprite. Also described as playback speed
- Frames: All the frames of the animation. Playback starts at "Element 0"
- Reset Delay: Only Editor relevant. Time, till the GUI script resets the animation. Could be "Time Between Frames" or any other desired number for testing. (Right now, this seems not to work right, sorry)

![SimpleAnimator in Unity](https://github.com/R3tr0BoiDX/github-stuff/blob/master/SimpleAnimator.png)


# Multiline.cs
Just a script with a very large multiline string which you can use as notepad

![Some notes](https://raw.githubusercontent.com/mircojanisch/github-stuff/master/Multiline.png)
