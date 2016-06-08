## These are the problems solved with recursion, described in increasing level of difficulty.

## 1.	Tower of Hanoi
The task is to solve the famous Tower of Hanoi puzzle using recursion. 
In this problem, you have three rods (let’s call them source, destination and spare). Initially, there are n disks, all placed on the source rod like in the [picture](https://upload.wikimedia.org/wikipedia/commons/0/07/Tower_of_Hanoi.jpeg).

## 2.	Nested Loops To Recursion
Write a program that simulates the execution of n nested loops from 1 to n which prints the values of all its iteration variables at any given time on a single line. Use recursion.

## 3.	Combinations with Repetition
Write a recursive program for generating and printing all combinations with duplicates of k elements from a set of n elements (k <= n). In combinations, the order of elements doesn’t matter, therefore (1 2) and (2 1) are the same combination, meaning that once you print/obtain (1 2), (2 1) is no longer valid.

## 4.	Combinations without Repetition
Modify the previous program to skip duplicates, e.g. (1 1) is not valid.

## 5.	Paths between Cells in Matrix
We are given a matrix of passable and non-passable cells. Write a recursive program for finding all paths between two cells in the matrix. The matrix can be represented by a two-dimensional char array or a string array, passable cells are represented by a space (' '), non-passable cells are represented by asterisks ('*'), the start cell is represented by the symbol 's' and the exit cell is represented by 'e'. Movement is allowed in all four directions (up, down, left, right) and a cell can be passed only once in a given path.
Print on the console all paths and on the last line the count of paths found. You can represent the directions with symbols, e.g. 'D' for down, 'U' for up, etc. The ordering of the paths is not relevant.

## 6.	Connected Areas in a Matrix
Let’s define a connected area in a matrix as an area of cells in which there is a path between every two cells. Write a program to find all connected areas in a matrix. On the console, print the total number of areas found, and on a separate line some info about each of the areas – its position (top-left corner) and size. Order the areas by size (in descending order) so that the largest area is printed first. If several areas have the same size, order them by their position, first by the row, then by the column of the top-left corner. So, if there are two connected areas with the same size, the one which is above and/or to the left of the other will be printed first.

Hints:
-	Create a method to find the first traversable cell which hasn’t been visited. This would be the top-left corner of a connected area. If there is no such cell, this means all areas have been found.
-	You can create a class to hold info about a connected area (its position and size). Additionally, you can implement IComparable and store all areas found in a SortedSet.

<hr />
<i> Problems 2, 3 and 4 are solved in "Nested-Loops-To-Recursion" solution. </i>
