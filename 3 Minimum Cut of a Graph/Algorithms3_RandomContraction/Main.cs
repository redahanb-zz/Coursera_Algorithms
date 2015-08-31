using System;
using System.IO;
using System.Collections.Generic;

namespace Algorithms3_RandomContraction
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string[] importedRows = File.ReadAllLines("/users/redahanb/projects/Algorithms3_RandomContraction/kargermincut.txt");
			List<int> vertices = new List<int>();
			List<List<int>> edges = new List<List<int>>();
			List<List<int>> edges2 = new List<List<int>> ();

			Random random = new Random();

//			// creating edge and vertex lists
			for (int i = 0; i < importedRows.Length; i++) {
				// take each row from imported string array, split it into a string array using tab delimiter
				string[] split = importedRows[i].Split('\t');

				// create list of edges, 'v_edges', for this particular vertex (first element of the row)
				List<int> v_edges = new List<int>();

				// populate list of edges and add to list of lists 'edges'
				for(int j = 0; j < split.Length-1; j++){
					int entry = int.Parse(split[j]); // read string as integer
					v_edges.Add(entry);
				}
				edges.Add(v_edges);
				edges2.Add (v_edges);

			} // end creating lists



			// randomly select an edge (edges[u][v] with j > 0


//			// SAMPLE FOR TESTING
//			List<int> list1 = new List<int>{1,2,3,5};
//			List<int> list2 = new List<int>{2,1,3};
//			List<int> list3 = new List<int>{3,1,2,4};
//			List<int> list4 = new List<int>{4,3,5};
//			List<int> list5 = new List<int>{5,1,4};
//
//			List<List<int>> sampleList = new List<List<int>>{list1, list2, list3, list4, list5};

//			List<int> list6 = new List<int>{1, 2, 3, 4, 7};
//			List<int> list7 = new List<int>{2, 1, 3, 4};
//			List<int> list8 = new List<int>{3, 1, 2, 4};
//			List<int> list9 = new List<int>{4, 1, 2, 3, 5};
//			List<int> list10 = new List<int>{5, 4, 6, 7, 8};
//			List<int> list11 = new List<int>{6, 5, 7, 8};
//			List<int> list12 = new List<int>{7, 1, 5, 6, 8};
//			List<int> list13 = new List<int>{8, 5, 6, 7};
//
//			List<List<int>> sampleList2 = new List<List<int>>{list6,list7,list8,list9,list10,list11,list12,list13};
////			// END SAMPLE TESTING
//
//			int rU = random.Next(0,sampleList2.Count);
//			int rV = random.Next(1, sampleList2[rU].Count);
//
//			while(sampleList2.Count > 2){
//				rU = random.Next(0,sampleList2.Count);
//				rV = random.Next(1, sampleList2[rU].Count);
//				contract (rU, rV, sampleList2);
//				Console.WriteLine(sampleList2.Count);
//			}
//
//			foreach (List<int> mega in sampleList2) {
//				Console.WriteLine("Vertex: " + mega[0] + ", " + (mega.Count - 1) + " edges.");
//				Console.WriteLine();
//				foreach(int y in mega){
//					Console.Write(y + ", ");
//				}
//			}

			///
			///

			int rU = random.Next(0,edges.Count);
			int rV = random.Next(1, edges[rU].Count);

			int mininmumCut = 100000;
			int runs = 0;

			for (int z = 0; z < 2; z++) {
				edges = new List<List<int>>();
				foreach(List<int> sublist in edges2){
					edges.Add(sublist);
				}


				runs++;
				Console.WriteLine("Run: " + runs);
				Console.WriteLine("AdjList size: " + edges.Count);

				while(edges.Count > 2){
					rU = random.Next(0,edges.Count);
					rV = random.Next(1, edges[rU].Count);
					contract (rU, rV, edges);
					Console.WriteLine(edges.Count);
				}
				
				foreach (List<int> mega in edges) {
					Console.WriteLine("Vertex: " + mega[0] + ", " + (mega.Count - 1) + " edges.");
				}

				if(edges[0].Count < mininmumCut){
					mininmumCut = edges[0].Count - 1;
				}
			}

			Console.WriteLine ();
			Console.WriteLine ("Minimun cut found was: " + mininmumCut);

			///
			///

//			Console.WriteLine ("Random u = " + rU + ", Random v = " + rV);
//			printAll (sampleList);
//			Console.WriteLine ();
//			contract (rU, rV, sampleList);
//			Console.WriteLine ();
//			printAll (sampleList);
//
//			rU = random.Next(0,sampleList.Count);
//			rV = random.Next(1, sampleList[rU].Count);
//			Console.WriteLine ("Random u = " + rU + ", Random v = " + rV);
//			contract (rU, rV, sampleList);
//			printAll (sampleList);
//
//			rU = random.Next(0,sampleList.Count);
//			rV = random.Next(1, sampleList[rU].Count);
//			Console.WriteLine ("Random u = " + rU + ", Random v = " + rV);
//			contract (rU, rV, sampleList);
//			printAll (sampleList);
//
//			rU = random.Next(0,sampleList.Count);
//			rV = random.Next(1, sampleList[rU].Count);
//			Console.WriteLine ("Random u = " + rU + ", Random v = " + rV);
//			contract (rU, rV, sampleList);
//			printAll (sampleList);
		
		} // end Main

		// function to remove an edge, (u,v) and merge all the loose edges remaining
		public static void contract(int u, int v, List<List<int>> list){

			Console.WriteLine ("Stage 1: Initialising...");

			// 1: merge/contract the vertices u and v: move v into u, bring all of v's edges into u/v, delete self-loops
			List<int> vertA = list [u];
			int A = vertA [0];
			int B = vertA [v];
			int savedIndex =  searchList(B, list);
			List<int> vertB = list [searchList(B, list)]; // find adjacancy list for the value in the vth position of the uth array
			//Console.WriteLine("Vertex A: " + vertA [0] + ", Vertex B: " + vertB [0]);

			Console.WriteLine ("Stage 2: Scouring...");

			// remove edge between vertices u and v
			scour (vertA,B);
			scour (vertB,A);

			Console.WriteLine ("Stage 3: Replacing references...");

			for (int x = 1; x < vertB.Count; x++) {
				//Console.WriteLine("Loop: " + x);
				vertA.Add(vertB[x]);
				///REMOVE ALL TRACES OF VERTB FROM THESE VERTICES ADJACANCY LISTS
				scour(list[searchList(vertB[x], list)],B);
				// REPLACE WITH REFERENCE TO VERTA
				list[searchList(vertB[x], list)].Add(A);
			}

			// remove first element of list B (vertex v)
			vertB.RemoveAt (0);

			Console.WriteLine ("Stage 4: Applying Changes...");

			list [u] = vertA;
			list.RemoveAt(savedIndex);

			Console.WriteLine ("Stage 5: End Run");
			
		} // end contract

		public static void scour(List<int> edges, int vertex){
			for(int j = edges.Count-1; j >= 0; j--){
//				Console.WriteLine("Do we reach this far?");
				if(edges[j] == vertex){
					edges.RemoveAt(j);
				}
			}
		} // end scour

		public static void printAll(List<List<int>> list){
			Console.WriteLine("Array contents: ");
			//Console.WriteLine();
			foreach(List<int> i in list){
				foreach(int j in i){
					Console.Write(j + ",");
				}
				Console.WriteLine();
			}
		}

		public static int searchList(int vertex, List<List<int>> adjList){
			// take a number and search the list for the index of the list that begins with the given int
			for(int i = 0; i < adjList.Count; i++) {
				if(adjList[i][0] == vertex){
					return i;
//				} else {
//					Console.WriteLine("MAJOR ERROR 1: searchList" + ", i = " + i);
//					return 99;
				}
			} 
			Console.WriteLine ("MAJOR ERROR 2: searchList");
			return 99;
		}

	} // end Class
}
