/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
[System.Serializable]
public class Room : MonoBehaviour
{
	#region Variables and Properties
	public new string name;	// The name shown in the inspector
	private string r_name;	// The name variable of the room
	public int r_buildCost;	// The cost to build the room
	public int r_upkeep;	// The upkeep cost of the room
	public RoomEffect r_effect; // The effect the room has on the guild
	#endregion

	public Room(string newName, int BuildCost, int Upkeep, RoomEffect Effect) // Constructor
	{
		name = r_name = newName;
		r_buildCost = BuildCost;
		r_upkeep = Upkeep;
		r_effect = Effect;
	}
}

[System.Serializable]
public class RoomEffect
{
	public string name;			// This name is shown in the inspector
	
	public RoomEffect()
	{			
		
	}

	// PopulationCap+
		// Increase population capacicty by 4

	// PopulationCap++
		// Increase population capacity by 6

	// PopulationCap+++
		// increase population capacity by 8



}