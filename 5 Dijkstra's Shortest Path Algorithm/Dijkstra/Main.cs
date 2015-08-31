using System;
using System.Collections.Generic;
using System.IO;

namespace Dijkstra
{
	class MainClass
	{
		public static void Main (string[] args){

			// Main dictionary to hold the Adjacency list
			Dictionary<int,Dictionary<int,int>> AdjList = new Dictionary<int, Dictionary<int,int>>();

			// Import and prepare data
			string[] importArray = File.ReadAllLines ("/users/redahanb/projects/dijkstra/dijkstradata.txt");
			foreach (string line in importArray) {
				string[] tempLine = line.Split('\t');

				Dictionary<int,int> subList = new Dictionary<int, int>();
				for(int i = 1; i < tempLine.Length; i++){

					if(tempLine[i].Length != 0){
						string[] tempPair = tempLine[i].Split(',');

						int key = int.Parse(tempPair[0]);
						int value = int.Parse(tempPair[1]);

						subList.Add (key, value);
					}
				}
				int vertex = int.Parse( tempLine[0] );
				AdjList.Add(vertex, subList);

			} // end import

			/// TASK: Run Dijkstra's shortest path algorithm from node 1 to the other 199 nodes. 
			/// Report back the shortest path from 1 to the following ten nodes:
			/// 7,37,59,82,99,115,133,165,188,197
			/// (if no path exists, lenght is defined as 1,000,000)

			Dictionary<int,int> QList = new Dictionary<int, int> ();
			Dictionary<int,int> VList = new Dictionary<int, int> ();
			Dictionary<int,bool> WList = new Dictionary<int, bool> ();
			foreach (int key in AdjList.Keys) {
				int length = 1000000;
				if(key == 1){
					length = 0;
				}
				QList.Add(key, length); // list of distances from source
				VList.Add(key, 1); // list of nodes remaining in loop
				WList.Add(key, false); // list of nodes checked/unchecked
			}

			// current node is X, starting at node 1
			int X = 1;
			int counter = 0;

			// Dijkstra Loop
			while(VList.Count > 0){
				Dictionary<int,int> currentEdges = AdjList[X];

				foreach(int node in currentEdges.Keys){
					if(WList[node] == false){
						int tentDist = QList[X] + currentEdges[node];
						if(tentDist < QList[node]){
							QList[node] = tentDist;
						} // end distance check
				}
				} // end foreach

				VList.Remove(X);
				WList[X] = true;

				// update current node X to unchecked node with min length
				int minNode = 201;
				int minDist = 1000000;

				foreach(int node in VList.Keys){
					if(QList[node] < minDist){
						minDist = QList[node];
						minNode = node;
					}
				}

				X = minNode;
				Console.WriteLine("Min node is: " + X);
				counter++;
			}

			Console.WriteLine ("End of loop reached");
			Console.WriteLine ("Counter = " + counter);
			Console.WriteLine ("7,37,59,82,99,115,133,165,188,197");
			Console.WriteLine (QList[7] + "," + QList[37] + "," + QList[59] + "," + QList[82] + "," + QList[99] + "," + QList[115] + "," + QList[133] + "," + QList[165] + "," + QList[188] + "," + QList[197]);

		}
	}
}
