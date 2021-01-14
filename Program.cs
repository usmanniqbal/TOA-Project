using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TOAPr1
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> charactersList = new List<string>();
			List<string> initialStateList = new List<string>();
			List<string> finalStateList = new List<string>();
			List<string> stateList = new List<string>();

			//Reading Transition Table
			string[] transTable = File.ReadAllLines(@"transTable.txt");


			//Adding characters in List
			string[] eachCharacter = transTable[0].Split(' ');
			foreach (string item in eachCharacter)
			{
				charactersList.Add(item);
			}

			//Seraching for Initial States and adding in List
			for (int j = 1; j < transTable.Length; j++)
			{
				if (transTable[j].Contains("-"))
				{
					string initialState = transTable[j].Split(' ')[0];
					initialStateList.Add(initialState);
					break;
				}
			}
			//Searching for Final States and adding in list
			for (int j = 1; j < transTable.Length; j++)
			{
				if (transTable[j].Contains("+"))
				{
					string finalState = transTable[j].Split(' ')[0];
					finalStateList.Add(finalState);
				}
			}

			//After --- symbols real transitions start for DFA
			int i;
			for (i = 1; i < transTable.Length; i++)
			{
				if (transTable[i].Contains("---"))
				{
					i++;
					break;
				}
				string states = transTable[i].Split(' ')[0];

				// Adding all states in to list
				stateList.Add(states);
			}

			// 
			string[,] transTable_table = new string[stateList.Count, charactersList.Count];
			for (i = i; i < transTable.Length; i++)
			{
				string[] tableLine = transTable[i].Split(' ');
				int x = stateList.FindIndex(a => a == tableLine[0]);
				int y = charactersList.FindIndex(a => a == tableLine[1]);
				transTable_table[x, y] = tableLine[2];
			}

			string curState = "";
			bool check = false;
			while (true)
			{
				Console.Write("Write your token:");
				var token = Console.ReadLine();
				Console.Write("Testing {0} ...", token);
				curState = initialStateList[0];
				check = false;
				for (int k = 0; k <= token.Length - 1; k++)
				{
					int x = stateList.FindIndex(a => a == curState);
					int y = charactersList.FindIndex(a => a == token[k].ToString());
					if (x < 0 || y < 0)
					{
						check = true;
						break;
					}
					curState = transTable_table[x, y];
				}
				if (check)
				{

					Console.WriteLine("rejected");
				}
				else
				{
					if (finalStateList.Contains(curState))
					{
						Console.WriteLine("accepted");
					}
					else
					{
						Console.WriteLine("rejected");
					}
				}
			}
		}

		private  void verifyToken(string token) { 
		}
	}
}