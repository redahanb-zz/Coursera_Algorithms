using System;
using System.IO;
using System.Collections.Generic;

namespace CourseraAlgorithms_Programming2
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// Input strings from file into string array (.txt file of numbers 1 to 10,000, unordered)
			string[] readFile = File.ReadAllLines ("/Users/redahanb/projects/courseraalgorithms_programming2/quicksort.txt");

			// for testing purposes
			//string[] readFile = File.ReadAllLines ("/Users/redahanb/projects/courseraalgorithms_programming2/1000.txt");

			// copy and parse strings into new integer array
			int[] convertedFile = new int[readFile.Length];
			for (int i = 0; i < convertedFile.Length; i++) {
				convertedFile[i] = int.Parse( readFile[i]); // this is the file we work with
			}

			// Testing findMedian function
			//Console.WriteLine (findMedian(new int[]{8,2,4,5,7,1}));

			// Perform QuickSort operation
			int[] sortedarray = QuickSort (convertedFile);

			// testing
			Console.WriteLine ("");
			for (int x = 0; x < 20; x++) {
				Console.WriteLine(sortedarray[x]);
			}

			// report back number of comparisons performed during QuickSort
			Console.WriteLine("Comparisons performed: " + ComparisonCounter.reportCount ().ToString() );

		} // main function/method

		public static int[] QuickSort(int[] input){
			// Randomly select a pivot, P
			int n = input.Length;

			if(n < 2){
				return input;
			} else {
				// Set pivot index // 3 options here for each of three different parts of the assignment
				//int pivot = 0; // question 1: pivot is first element
				//int pivot = n-1; // question 2: pivot is last element
				int pivot = findMedian(input); // question 3: pivot is median of 1st, last, and middle element

				/////////////////////////////
				///Partitioning the array///
				////////////////////////////
				int divide = 1; // integer to keep track of index between the 2 partitions

				// put pivot at index 0 temporarily
				swapEntry(ref input, 0, pivot);

				// Sort the array to have entries less than the pivot to the left, and greater than the pivot to the right
				for (int j = 1; j < n; j++) {
					if(input[j] < input[0]){
						// swap operation
						swapEntry(ref input, divide, j);
						divide++;
					} 
				}
				ComparisonCounter.incrementCount(n-1);

				// put the pivot element at the partition, dividing the array IN TWAIN
				swapEntry(ref input, 0, divide-1); // divide is the left-most element in the upper partition (> pivot value)

				////////////////////////
				///Recursive sorting ///
				////////////////////////

				// Split the array above and below the pivot
				int[] arrayA = new int[divide - 1];
				int[] arrayB = new int[n - divide];

				Array.Copy(input, 0, arrayA, 0, divide-1);
				Array.Copy (input, divide, arrayB, 0, n - divide);

				// Recursively call QuickSort() on the arrays on either side of the pivot
				arrayA = QuickSort (arrayA);
//				if(arrayA.Length > 1){
//					ComparisonCounter.incrementCount (arrayA.Length - 1);
//				}

				arrayB = QuickSort (arrayB);
//				if(arrayB.Length > 1){
//					ComparisonCounter.incrementCount (arrayB.Length - 1);
//				}

				// create a return array to return the sorted arrays from the recursive calls, adding in the pivot inbetween
				int[] returnArray = new int[n];
				arrayA.CopyTo (returnArray, 0);
				returnArray [divide-1] = input [divide-1];
				arrayB.CopyTo (returnArray, divide);

				return returnArray;
			}

		} // QuickSort function

		public static void swapEntry(ref int[] array, int indexA, int indexB){
			int temp = array[indexA];
			array[indexA] = array[indexB];
			array [indexB] = temp;
		}// swap entry

		public static int findMedian(int[] array){
			Console.WriteLine ("Finding Median...");
			if (array.Length > 2) {
				int first = array [0];
				int last = array [array.Length - 1];
				int middle;
				if (array.Length % 2 == 0) {
					middle = array [array.Length / 2 - 1];
				} else {
					middle = array [array.Length / 2];
				}

				Console.WriteLine(first + ", " + middle + ", " + last);

				if (first < middle && middle < last || last < middle && middle < first) {
					if (array.Length % 2 == 0) {
						return array.Length / 2 - 1;
					} else {
						return array.Length / 2;
					}
				} else if (middle < first && first < last || last < first && first < middle) {
					return 0;
				} else if (first < last && last < middle || middle < last && last < first) {
					return array.Length - 1;
				} else {
					Console.WriteLine ("SERIOUS ERROR WITH MEDIAN CALCULATION!!!");
					return 0;
				}
			} else if(array.Length == 2){
				Console.WriteLine("Array too small...");
				if(array[0] < array[1]){
					return 0;
				} else {
					return 1;
				}
			} else {
				return 0;
			}
		}

		public class ComparisonCounter {
				
			static int counter = 0;
				
			public static void incrementCount(int increase){
				if (increase <= 0) {
					counter += 0;
				} else {
					counter += increase;
				}
			}
				
			public static int reportCount(){
				return counter;
			}

		} // counter class


	} // main class
}// namespace
