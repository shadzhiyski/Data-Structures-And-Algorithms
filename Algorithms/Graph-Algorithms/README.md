## These are the problems solved with graph algorithms, described in increasing level of difficulty.

## 1.	Distance between Vertices
We are given a directed graph consisting of N vertices and M edges. We are given also a set of pairs of vertices. Find the shortest distance between each pair of vertices or -1 if there is no path connecting them. There are no specified requirements for the input and output, so you may hardcode the input and output values.

Input | Output | 
--- | ---
Graph:<br />1 -> 2<br />2 -><br />Distances to find:<br />1-2<br />2-1 | {1, 2} -> 1<br />{2, 1} -> -1
Graph:<br />1 -> 4<br />2 -> 4<br />3 -> 4, 5<br />4 -> 6<br />5 -> 3, 7, 8<br />6 -><br />7 -> 8<br />8 -> | {1, 6} -> 2<br />{1, 5} -> -1<br />{5, 6} -> 3<br />{5, 8} -> 1

<b>Hint:</b> for each pair use BFS to find all paths from the source to the destination vertex.

## 2.	Areas in Matrix
We are given a matrix of letters of size N * M. Two cells are neighbor if they share a common wall. Write a program to find the connected areas of neighbor cells holding the same letter. Display the total number of areas and the number of areas for each alphabetical letter. 

Input | Output
--- | ---
Number of rows: 6<br />aacccaac<br />baaaaccc<br />baabaccc<br />bbdaaccc<br />ccdccccc<br />ccdccccc | Areas: 8<br />Letter 'a' -> 2<br />Letter 'b' -> 2<br />Letter 'c' -> 3<br />Letter 'd' -> 1

<b>Hint:</b> Initially mark all cells as unvisited. Start a recursive DFS traversal (or BFS) from each unvisited cell and mark all reached cells as visited. Each DFS traversal will find one of the connected areas.

## 3.	Cycles in a Graph
Write a program to check whether an undirected graph is acyclic or holds any cycles.

Input | Output
--- | ---
C – G | Acyclic: Yes
A – F<br />F – D<br />D – A | Acyclic: No
E – Q<br />Q – P<br />P – B | Acyclic: Yes

## 4.	Salaries
We have a hierarchy between the employees in a company. Employees can have one or several direct managers. People who manage nobody are called regular employees and their salaries are 1. People who manage at least one employee are called managers. Each manager takes a salary which is equal to the sum of the salaries of their directly managed employees. Managers cannot manage directly or indirectly (transitively) themselves. Some employees might have no manager (like the big boss)

If we have N employees, they will be indexed from 0 to N – 1. For each employee, you’ll be given a string with N symbols. The symbol at a given index i, either 'Y' or 'N', shows whether the current employee is a direct manager of employee i.

Input | Output | Comments
--- | --- | ---
1<br />N | 1 | Only 1 employee with salary 1.
4<br />NNYN<br />NNYN<br />NNNN<br />NYYN | 5 | We have 4 employees. 0, 1, and 3 are managers of 2. 3 is also a manager of 1. Therefore: <br />salary(2) = 1 <br />salary(0) = salary(2) = 1<br />salary(1) = salary(2) = 1 <br />salary(3) = salary(2) + salary(1) = 2


