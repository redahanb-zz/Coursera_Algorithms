using System;
using System.IO;
using System.Collections.Generic;
//using Microsoft;

namespace CourseraAlgorithms_Prgramming1
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string[] file = File.ReadAllLines("/Users/redahanb/IntegerArray.txt"); 
			int[] intArray = new int[file.Length];



			for(int i = 0; i < file.Length; i++){
				//Console.WriteLine(file[i]);
				intArray[i] = int.Parse( file[i] );
			}

			// testing merging
			int[] output = mergeSort (intArray);

			for(int i = 0; i < 10; i++){
				Console.WriteLine(output[i]);
			}

			Console.WriteLine ("Inversions counted: " + Inversions.printInv());

		}

		public class Inversions {
			static uint inversionTally = 0;

			public static void countInv(){
				inversionTally++;
			}

			public static uint printInv(){
				return inversionTally;
			}
		}

		public static int[] mergeSort(int[] entry) {
			Console.WriteLine ("Calculating...");
			if (entry.Length == 1) {
				return entry;
			} 
			else {
				// split array in half
				int[] arrayA = new int[entry.Length - entry.Length/2]; // in case the length is odd in number
				int[] arrayB = new int[entry.Length/2];
				Array.Copy(entry, 0, arrayA, 0, arrayA.Length);
				Array.Copy(entry, arrayA.Length, arrayB, 0, arrayB.Length);

				// merge sort the half-arrays (recursion until array is length 1, then merge sort them so inversions are counted while merging
				int[] arrayX = mergeSort(arrayA);
				int[] arrayY = mergeSort(arrayB);

				int[] returnArray = new int[entry.Length];
				int countA = 0, countB = 0;
				// merge the sliced arrays back together, sorted into the return array
				for(int n = 0; n < returnArray.Length; n++){
					if(countA == arrayX.Length){
						// arrayX is exhausted - end case
						returnArray[n] = arrayY[countB];
						countB++;
					}
					else if (countB == arrayY.Length){
						// arrayY is exhausted - end case
						returnArray[n] = arrayX[countA];
						countA++;
					}
					else if(arrayX[countA] < arrayY[countB]){ // compare first terms or each array, keep tally of which index is being examined
						returnArray[n] = arrayX[countA];
						if(countA < arrayX.Length){
							countA++;
						}
					} 
					else if(arrayX[countA] > arrayY[countB]){
						returnArray[n] = arrayY[countB];
						// Inversion is noted here
						int diff = arrayX.Length - countA; // depends upon the number of elements left in the first array
						for(int h = 0; h < diff; h++){
							Inversions.countInv();
						}
						if(countB < arrayY.Length){
							countB++;
						}
					}

				}

				return returnArray;
			}
		
		}// end mergesort


	}
}
