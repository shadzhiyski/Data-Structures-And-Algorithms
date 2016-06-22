## These are the most common problems solved with greedy algorithms, described in increasing level of difficulty.

## 1. Fractional Knapsack Problem
A modification of the famous knapsack problem is the so-called [continuous (or fractional) knapsack problem](https://en.wikipedia.org/wiki/Continuous_knapsack_problem). 

We have N items, each with a certain weight and price. The knapsack has a maximum capacity, so we need to choose what to take in order to maximize the value (price) of the items in it. 

Unlike the classical version of the problem where an object should either be taken in its entirety or not at all, in this version we can take a fraction of each item. For example, such items may be liquids or powders; unlike solid objects which (presumably) we cannot split, we’ll assume that all items under consideration can be divided in any proportion. Therefore, the weight can be though as the maximum quantity Q of an item we are allowed to take – we can take any amount in the range [0 … Q]. Note that in this version of the problem the knapsack will always be filled completely (if the total quantity of items is greater than its capacity).

In this solution items are given on separate lines in format price -> weight.

## 2. Processor Scheduling
We are given a processor capable of performing only one task at a time and N tasks. Each task has two properties associated with it – value v of completing the task and a deadline d. Each task takes exactly one unit of time to complete and should be completed at or before its deadline. E.g., if we have a task with deadline 2, we can complete it either at step 1 or step 2, but not afterwards. Given a set of tasks numbered from 1 to N, find a schedule which will maximize the total value of the tasks performed.


Task | 1 | 2 | 3 | 4 | 5
---- | --- | --- | --- | --- | ---
Value | 40 | 30 | 15 | 20 | 50
Deadline | 1 | 2 | 1 | 1 | 2

With a maximum of 2 steps (largest deadline is 2) we can complete at most two tasks. Performing one of the tasks that has a deadline 2 on the first step leaves us with no choice for the second step, but to take the other task with deadline 2 (tasks with deadline 1 can no longer be performed). Therefore, 2 -> 5, or 5 -> 2 will produce a total value of 30 + 50 = 80.

It is obvious from the table above that the optimal solution is 1 -> 5 which produces total value of 40 + 50 = 90.

## 3. Knight’s Tour
Given a board of size NxN (a standard square matrix), a chess knight can perform a tour of the board, visiting each cell only once. The knight moves according to the rules of chess (in an L-shaped pattern) and starts from the upper-left corner. Write a program which finds and prints the path the knight needs to take in order to visit all cells. From the input you receive just the board size N (N > 4). 

On the output, print the board where each cell’s value is the number of the step which led to it, e.g. cell (0, 0) will be 1, the first step, the next visited cell (0, 2) will have value 2, etc. In order to format the output, assume NxN < 1000 – no cell’s value will be more than 3 digits (you can pad the values with spaces with the string.PadLeft() method).

 Input | Output
 --- | ---
 5 |  &nbsp;&nbsp;1  12  25  18 &nbsp;3<br />22  17 &nbsp;2  13  24<br />11 &nbsp;8  23 &nbsp;4  19<br />16  21 &nbsp;6 &nbsp;9  14<br />&nbsp;7  10  15  20  &nbsp;5

## 4. Best Lectures Schedule
We've got just one lecture hall. Let’s have a list of lectures, each having a start time <b>s</b> and a finish time <b>f</b> (<b>s</b> and <b>f</b> will be positive integers). Obviously, only one lecture can be presented at a time, they cannot overlap. 

Lectures are given in format name: start – finish.

Input | Output
--- | ---
Lectures: 4<br />Java: 1 – 7<br />OOP: 3 – 13<br />C_Programming: 5 – 9<br />Advanced_JavaScript: 10 – 14 | Lectures (2):<br />1-7 -> Java<br />10-14 -> Advanced_JavaScript

## 5. Egyptian Fractions
In mathematics, a fraction is the rational number p/q where p and q are integers. An Egyptian fraction is a sum of fractions, each with numerator 1 where all denominators are different, e.g. 1/2 + 1/3 + 1/16 is an Egyptian fraction, but 1/3 + 1/3 + 1/5 is not (repeated denominator 3). 

Every positive fraction (q != 0, p < q) can be represented by an Egyptian fraction, for instance, 43/48 = 1/2 + 1/3 + 1/16. Given p and q, write a program to represent the fraction p/q as an Egyptian fraction.

Input | Output
--- | ---
43/38 | 43/48 = 1/2 + 1/3 + 1/16
3/7 | 3/7 = 1/3 + 1/11 + 1/231
23/46 | 23/46 = 1/2
134/3151 | 134/3151 = 1/24 + 1/1164 + 1/2445176

<b>Note:</b> There may be more than one correct solution, e.g. 3/7 = 1/4 + 1/8 + 1/19 + 1/1064.

<b>Hint:</b> You can complete the expression by starting with the biggest fraction with numerator 1 which added to the expression keeps it smaller than or equal to the target fraction. The biggest fraction is the one with smallest denominator – 1/2. Increase the denominator until you’ve found a solution.
