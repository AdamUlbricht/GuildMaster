/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
[System.Serializable]
public class Job : MonoBehaviour
{
	#region Variables and Properties
	[SerializeField] public string name; 	// The name shown in the inspector
	[SerializeField] public int j_upkeep;    // The upkeep cost for each member in this job
	#endregion
	
	public Job()
	{

	}
}
