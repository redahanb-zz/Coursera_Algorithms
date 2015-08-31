using System;
using System.Collections.Generic;
using System.IO;

namespace SUM_MedianMaintenance
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			/// Q1 - 2SUM

			// 1.2 Import
			string[] import = File.ReadAllLines ("/Users/redahanb/projects/6 2SUM & MedianMaintenance/2SUM.txt");
			//string[] import = File.ReadAllLines ("/Users/redahanb/projects/6 2SUM & MedianMaintenance/TestCase1.txt"); // test case: answer is 73
			//string[] import = File.ReadAllLines ("/Users/redahanb/projects/6 2SUM & MedianMaintenance/2sum_test1M.txt"); // test case: answer is 471
			//string[] import = File.ReadAllLines ("/Users/redahanb/projects/6 2SUM & MedianMaintenance/Tests/10000.txt"); // test case: answer is 496
			//string[] import = File.ReadAllLines ("/Users/redahanb/projects/6 2SUM & MedianMaintenance/Tests/100000.txt"); // test case: answer is 519

			// created the hash table
			Dictionary<long, List<long>> importHash = new Dictionary<long, List<long>> ();

			for(int i = 0; i < import.Length; i++){
				// progress ticker
				if((i%100000)==0){Console.WriteLine(".");}

				// hash function is the value of the entry divided by 10,000
				long hash = long.Parse(import[i])/10000;
				//Console.WriteLine(hash);

				// enter value into the hash table
				if(!importHash.ContainsKey(hash) ){
					List<long> t = new List<long>();
					t.Add(long.Parse(import[i]));
					importHash.Add(hash, t);
					//Console.WriteLine("New Bucket");
				} else{
					importHash[hash].Add(long.Parse(import[i]));
				}
			}

			// prepare dictionary of target values from -10k to +10k inclusive
			Dictionary<long, bool> targets = new Dictionary<long, bool> ();
			for (long x = -10000; x <= 10000; x++) {
				targets.Add(x, false);
			}

			Console.WriteLine ("Complete.");
			Console.WriteLine ("After discounting duplicates, imported: " + importHash.Count);


			// 1.2 Scan
			List<long> temp = new List<long> ();


			foreach(long q in importHash.Keys){
				// bucket that wold contain the possible numbers
				long bucket1 = -q;
				long bucket2 = -q-1;
				long bucket3 = -q-2;
				long bucket4 = -q+1;

				foreach(long x in importHash[q]){
					if(importHash.ContainsKey(bucket1)){
						foreach(long y in importHash[bucket1]){
							if(Math.Abs(x+y) <= 10000){
								if(x != y){
									if(!targets[x+y]){
										temp.Add(x+y);;
									}
								}
							}
						}
					}
				}

				foreach(long x in importHash[q]){
					if(importHash.ContainsKey(bucket2)){
						foreach(long y in importHash[bucket2]){
							if(Math.Abs(x+y) <= 10000){
								if(x != y){
									if(!targets[x+y]){
										temp.Add(x+y);;
									}
								}
							}
						}
					}
				}

				foreach(long x in importHash[q]){
					if(importHash.ContainsKey(bucket3)){
						foreach(long y in importHash[bucket3]){
							if(Math.Abs(x+y) <= 10000){
								if(x != y){
									if(!targets[x+y]){
										temp.Add(x+y);;
									}
								}
							}
						}
					}
				}

				foreach(long x in importHash[q]){
					if(importHash.ContainsKey(bucket4)){
						foreach(long y in importHash[bucket4]){
							if(Math.Abs(x+y) <= 10000){
								if(x != y){
									if(!targets[x+y]){
										temp.Add(x+y);;
									}
								}
							}
						}
					}
				}

			}

			foreach (long q in temp) {
				targets[q] = true;
			}

			// 1.3 Prepare Results

			int count = 0;
			foreach (bool b in targets.Values) {
				if(b == true){
					count++;
				}
			}
			Console.WriteLine ("Count is: " + count);
			Console.WriteLine ();


//			/// Q2 Median Mantenance
//
//			// 2.1 Import
//			string[] importMM = File.ReadAllLines ("Users/redahanb/projects/6 2SUM & MedianMaintenance/Median.txt");
//			int[] integerStream = new int[importMM.Length];
//
//			for(int i = 0; i < importMM.Length; i++){
//				integerStream[i] = importMM[i];
//			}
//
//			// 2.2 Analysis
//
//
//
//			// 2.3 Reporting Results
//
//

		}// end Main

	}
}
