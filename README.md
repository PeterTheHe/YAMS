# YAMS
Yet Another Maze Solver

So I got bored and decided to speedily write up a solution to this age-old interview problem. Using the A* algorithm, navigate through pointlessly complex mazes in less than a second!*

![maze](http://i.imgur.com/hBsJsLm.png)


*assuming your maze isn't pointlessly huge

## Maze File Format

The input is a maze description file in plain text.  

- 1 - Wall
- 0 - Passage

### Input
 
WIDTH HEIGHT

START_X START_Y

END_X END_Y

HEIGHT rows where each row has WIDTH {0,1} integers space delimited

### Output
- The maze with a path from start to end
- Walls marked by '#', passages marked by ' ', path marked by 'X', start/end marked by 'S'/'E'
 
### Example file
10 10

1 1

8 8

1 1 1 1 1 1 1 1 1 1

1 0 0 0 0 0 0 0 0 1

1 0 1 0 1 1 1 1 1 1

1 0 1 0 0 0 0 0 0 1

1 0 1 1 0 1 0 1 1 1

1 0 1 0 0 1 0 1 0 1

1 0 1 0 0 0 0 0 0 1

1 0 1 1 1 0 1 1 1 1

1 0 1 0 0 0 0 0 0 1

1 1 1 1 1 1 1 1 1 1




