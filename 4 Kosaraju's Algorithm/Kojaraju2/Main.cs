using System;
using System.IO;
using System.Collections.Generic;

namespace Kojaraju2u
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			/////////// Part 1.1: Data Prep ///////////////
			
			// import the data by line into string array
			String[] importArray = File.ReadAllLines ("/users/redahanb/projects/algorithms4_kosaraju/SCC.txt");
			Console.WriteLine ("Imported " + importArray.Length + " rows.");
			
			// initialise master list (adjacency list)
			List<List<int>> MasterList = new List<List<int>>();
			List<List<int>> MasterListInverse = new List<List<int>> ();
			
			// loop through entries in imported file (rows of data)
			for(int i = 0; i < importArray.Length; i++){
				
				if((i%500000)==0){Console.WriteLine("Converting... " + i + " done.");} // to know it hasn't frozen :/
				
				string[] split = importArray[i].Split(new char[0], StringSplitOptions.RemoveEmptyEntries); // break row into strings delineated by a ' '
				
				List<int> edges = new List<int>(); // initialise entry for master list
				List<int> edges_inverse = new List<int>(); // create inverse list for first pass of DFS
				
				// populate edges list by reading the delineated row and parsing the strings as ints
				for(int j = 0; j < split.Length; j++){
					int entry = int.Parse(split[j]);
					edges.Add(entry);
				}
				
				for(int y = split.Length-1; y >= 0; y--){
					int entry = int.Parse(split[y]);
					edges_inverse.Add(entry);
				}
				
				MasterList.Add(edges); // add the edges list to the MasterList
				MasterListInverse.Add(edges_inverse); // ditto for the reverse list
			}
			Console.WriteLine ();
			
//			/// Testing
//			Console.WriteLine ();
//			Console.WriteLine ("Results");
//			Console.WriteLine ("Size of data set: " + MasterList.Count + ", and inverse: " + MasterListInverse.Count); // Confirmed, importing the full set of rows
//			Console.WriteLine ("Entry 1 Size: " + MasterList[1].Count + ", and inverse: " + MasterListInverse[1].Count);
//			Console.WriteLine ("Elements: " + MasterList[1][0] + ", " + MasterList[1][1]);
//			Console.WriteLine ("Inverse Elements: " + MasterListInverse[1][0] + ", " + MasterListInverse[1][1]);
//			Console.WriteLine ();
			
			////////////// End Part 1.1 /////////////////
			
			
			///// Part 1.2 Generating Adjacency Lists /////
			
			// Convert MasterList and MasterList_Inverse to Adjacency Lists
			// Building Adjacency List
			Dictionary<int, List<int>> AdjList2 = new Dictionary<int, List<int>>();

			for(int i = 0; i < MasterList.Count; i++){
				// writeline statement to track progress
				if((i  % 500000) == 0){Console.WriteLine("Building...");}
				
				int vertex = MasterList[i][0];

				if(!AdjList2.ContainsKey(vertex)){
					List<int> temp = new List<int>();
					temp.Add(MasterList[i][1]);
					AdjList2.Add(vertex, temp);
				} else if (AdjList2.ContainsKey(vertex)){
					AdjList2[vertex].Add(MasterList[i][1]);
				}

			}

			for(int i = 1; i <= 875714; i++){
				if(!AdjList2.ContainsKey(i)){
					List<int> temp = new List<int>();
					AdjList2.Add(i, temp);
				}
			}
			
			Console.WriteLine ("AdjList complete: " + AdjList2.Count);
			Console.WriteLine ();

			
			// Building Reverse Adjacency List
			Dictionary<int, List<int>> RevList2 = new Dictionary<int, List<int>> ();
			
			for(int j = 0; j < MasterListInverse.Count; j++){
				// writeline statement to track progress
				if((j % 500000) == 0){Console.WriteLine("Building...");}

				int vertex = MasterListInverse[j][0];

				if(!RevList2.ContainsKey(vertex)){
					List<int> temp = new List<int>();
					temp.Add (MasterListInverse[j][1]);
					RevList2.Add(vertex, temp);
				} else if(RevList2.ContainsKey(vertex)){
					RevList2[vertex].Add(MasterListInverse[j][1]);
				}
			}

			for(int i = 1; i <= 875714; i++){
				if(!RevList2.ContainsKey(i)){
					List<int> temp = new List<int>();
					RevList2.Add(i, temp);
				}
			}

			
			Console.WriteLine ("RevList complete: " + RevList2.Count);

//			Console.WriteLine ();
//			Console.WriteLine ("List contains node 252: " + AdjList2.ContainsKey(252));
//			Console.WriteLine ("List contains node 253: " + AdjList2.ContainsKey(253));
//			Console.WriteLine ("List contains node 434: " + AdjList2.ContainsKey(434));
//			Console.WriteLine ("List contains node 435: " + AdjList2.ContainsKey(435));
//			Console.WriteLine ("List contains node 4: " + AdjList2.ContainsKey(4));
//			Console.WriteLine ();
//			Console.WriteLine ("RevList contains node 875714: " + RevList2.ContainsKey(875715));
//			Console.WriteLine ("RevList contains node 253: " + RevList2.ContainsKey(253));
//			Console.WriteLine ("RevList contains node 434: " + RevList2.ContainsKey(434));
//			Console.WriteLine ("RevList contains node 435: " + RevList2.ContainsKey(435));

			
			/// global int to track the number of nodes
			int nodes = RevList2.Count;
			Console.WriteLine ("Nodes: " + nodes);

			
			//////////////// End Part 1.2 /////////////////
			
			
			/// Part 2.1 Topgraphical Search - First Pass ///
			
			// In brief: loop through the reversed list, starting from the last entry
			
//			int[] finishTime = new int[RevList.Length]; // array to track the finishing times of each vertex
//			int[] leadingVertex = new int[RevList.Length]; // array to track the leading vertex 
//			bool[] vertexExplored = new bool[RevList.Length]; // array to track whether each vertex has been explored yet or not

			Dictionary<int, bool> explored = new Dictionary<int, bool>();
			Dictionary<int, int> finishTime = new Dictionary<int, int> ();
			Dictionary<int, int> leadingNode = new Dictionary<int, int> ();

			for(int i = 1; i <= nodes; i++){
				explored.Add(i, false);
				finishTime.Add(i, 0);
				leadingNode.Add(i, 0);
			}

			int time = 0;
			int leader = -1;
			
			// Loop backwards through Reversed array, marking each vertex as explored when you reach it, mark it's finishing time t as you leave the loop
			for (int i = 1; i <= nodes; i++) {
				if(!explored[i]){
					leader = i;
					DFS(RevList2, i, explored, ref time, finishTime, leadingNode, ref leader);
				}
			}

			Console.WriteLine ("Finishing time: " + nodes + " = " + finishTime[nodes]);
			Console.WriteLine ("Completed DFS Round 1");
			Console.WriteLine ("Last leading node: " + leader);
			Console.WriteLine ("Last value for time: " + time);

			////////////////// End Part 2.1 /////////////////

			/// Part 2.2 Topgraphical Search - Second Pass ///

			Dictionary<int, bool> explored2 = new Dictionary<int, bool>();
			Dictionary<int, int> finishTime2 = new Dictionary<int, int> ();
			Dictionary<int, int> leadingNode2 = new Dictionary<int, int> ();

			for(int i = 1; i <= nodes; i++){
				explored2.Add(i, false);
				finishTime2.Add(i, 0);
				leadingNode2.Add(i, 0);
			}

			int time2 = 0;
			int leader2 = -1;
			
			// converting finishing time list into order-of-play for the second pass, using dictionary to lookup by finishingtTime
			Console.WriteLine ("Preparing finishing time ranking...");
			Dictionary<int, int> pass2Order = new Dictionary<int, int> ();
			for (int x = 1; x <= finishTime.Count; x++) {
				pass2Order.Add (finishTime[x],x);
			}
//			Console.WriteLine ("Ranking completed. Dictionary count = " + pass2Order.Count);
//			for (int i = 1; i < 100; i++) {
//				Console.WriteLine("Rank " + i + " = " + pass2Order[i]);
//			}

			// loop through the array in *decreasing* order of finishing time from the first pass, call DFS again
			for(int i = pass2Order.Count; i >= 1; i--){
				int vertex = pass2Order[i];
				if(!explored2[vertex]){
					leader2 = vertex;
					DFS (AdjList2, vertex, explored2, ref time2, finishTime2, leadingNode2, ref leader2);
				}

			}

			Console.WriteLine ("Completed DFS Round 2");
			Console.WriteLine ("Last leading node: " + leader2);
			Console.WriteLine ("Last value for time: " + time2);


		////////////////// End Part 2.2 //////////////////
		
		
		//////////////////// Part 3 /////////////////////
			Console.WriteLine ("Preparing Results...");
			Dictionary<int, int> counter = new Dictionary<int, int>();
			
			foreach(int i in leadingNode2.Values){
				if(counter.ContainsKey(i)){
					counter[i]++;
				} else {
					counter[i] = 1;
				}
			}
			Console.WriteLine ("Counter # values: " + counter.Count);
			
			List<int> results = new List<int> ();
			
			foreach (int i in counter.Keys) {
				results.Add(counter[i]);
			}

			results.Sort ();
			
			Console.WriteLine ();
			Console.WriteLine ("Results:" + results.Count);
			for (int i = results.Count-1; i >= results.Count-10; i--) {
				Console.WriteLine("Number: " + i + " = " + results[i]);	
			}

		////////////////// End Part 3 ////////////////// 
		
		
		} // End Main

//		/// Declaring and defining function for Depth-First Search of the graph.
		
		public static void DFS
			(
				Dictionary<int,List<int>> graph, 
				int vertex, 
				Dictionary<int, bool> explored, 
				ref int t, 
				Dictionary<int,int> time, 
				Dictionary<int,int> leader, 
				ref int s
			)
		{
			explored [vertex] = true;
			leader [vertex] = s;
			
			List<int> temp = graph[vertex]; // temp list of connected vertices
			
			// loop through temp list, checking
			for(int z = 0; z < temp.Count; z++){ // we are looping through VALUES here, so decrement by 1 to find the INDEX
				if(!explored[temp[z]]){
					DFS (graph, temp[z], explored, ref t, time, leader, ref s);
//					if(graph[temp[z]].Count != 0){
//						DFS (graph, temp[z], explored, ref t, time, leader, s);
//					} 
//					else {
//						explored[temp[z]] = true;
//						leader[temp[z]] = s;
//						t++;
//						time[temp[z]] = t;
//					}
				}
			}

			// increment the finishing time counter
			t++;
			time [vertex] = t;
			
		} // end DFS
	}
}
