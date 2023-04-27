# Noughts and Crosses
## Fish in a Bottle Brief

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
- [ ] plan alternate game mode
- [ ] build alternate game mode

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
