# Noughts and Crosses
## Brief

#### Part 1
- Single player mode
- Two player mode
- Winner screen
- Alternate which player starts first in every subsequent game
- Keep a tally of the scores
- Title screen

#### Part 2
- alternate game mode

# Tasks

TODO List:
- [X] screen sketch
- [X] git repo
- [X] system design first pass
- [X] gameplay UI bones
- [X] win detection
- [X] basic 2-player implementation
- [X] win screen
- [X] cpu player (min max alogrithm?)
- [X] restart (alternating)
- [X] title screen
- [X] plan alternate game mode
- [X] build alternate game mode

# Screen Sketch
![first pass doodle for the game screens](https://github.com/ludovino/TicTacToe/blob/master/UI-sketch.png?raw=true)

# System Design

## Game Controller
- Alternate between player turns
- trigger Win / Lose / Draw

## Player Controller
- Recieve input from UI
- Translate input into marking board

## CPU Player Controller
- Read board
- Evaluate next moves - avoid unforced 1 turn losses, always play 1 turn wins
- Play move based on evaluation

## Board
- hold play state
- prevent invalid moves
- raise event on play to change turn or win / lose / draw

# Alternate Game Mode
## Survival Mode

- The player has 5 lives, and each time they lose a game of tic tac toe they lose a life.
- Rather than taking turns to place marks, your ability to place a mark is on a cooldown. If you're quick, you might be able to place two marks in the time the computer takes to place one!
- the game gradually accelerates, increasing the difficulty as the player has to act and also think faster.
