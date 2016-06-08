## These are the most common sorting and searching algorithms, described in increasing level of difficulty.

## 1.	Insertion Sort
Insertion sort is very simple: iterate the elements and move them to the left so that they end up with a smaller (or equal) element to the left and a larger one to the right. Check out Visualgo for a visual representation.

Since all classes implementing the ISorter interface in the skeleton work with a List<T>, we can use the very convenient RemoveAt and Insert methods to implement this sorting algorithm in just a few lines of code.

We can start at index 1 and check to the left until we find an element which is at least equal to the current element or until we reach index 0. Keeping track of where the last larger element was found, we can remove the element and insert it there.

## 2.	Interpolation Search
Information about [Interpolation Search](https://en.wikipedia.org/wiki/Interpolation_search).

## 3.	Merge Sort
Merge sort is an algorithm which takes two sorted halves of an array and merges them into one sorted array.

The idea is that an array is continuously split until the halves are of size 1 – by definition, an array with one element is sorted. Then, all the actual sorting is done by the merge procedure. We keep two pointers, one pointing to the smallest element of the left sub-array and one pointing to the smallest element of the right sub-array.

Merging requires a temporary array. In this problem, we’ll initialize a single array and use it throughout the entire process. This will eliminate the constant creation and destruction of temporary arrays at each step and will improve performance.

## 4.	Heap Sort
It works by building a min-heap from an unsorted array and then taking the smallest element repeatedly until the heap is empty. You can learn more about the Heap structure and get a complete implementation from the Advanced Tree Structures lecture ([Data Structures](https://softuni.bg/trainings/1147/Data-Structures-June-2015) course at SoftUni) or you may implement it yourself.

## 5.	The Fisher-Yates Shuffle 
Information about [Fisher-Yates Shuffle](https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle).

## 6. In-place Merge Sort
Merge Sort without the use of an auxiliary array.

## 7. Quicksort
Information about [Quicksort](https://en.wikipedia.org/wiki/Quicksort).
