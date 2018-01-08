/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
[System.Serializable]
public class Member : MonoBehaviour
{
	#region Variables and Properties
	public new string name; // The name shown in the inspector 
	public int m_Upkeep;	// The upkeep Cost for this Character Prefab
	public int m_Cost;		// The purchase cosr for this Character Prefab
	public enum Jobs { Adventurer, Soldier, Mage, Ranger}
	public Jobs m_Job;	

	// Personality
	public int STR;
	public int MAG;
	public int DEX;

	int RND() { return (int)Random.Range(1, 4); }
	// Job

	#endregion

	public void SetBaseStats()
	{
		STR = RND();
		MAG = RND();
		DEX = RND();
	}
	public void SetJobStats()
	{
		switch (m_Job)
		{
			case Jobs.Adventurer:
				break;
			case Jobs.Soldier:
				STR += RND();
				break;
			case Jobs.Mage:
				MAG += RND();
				break;
			case Jobs.Ranger:
				DEX += RND();
				break;
			default:
				break;
		}

	}
}
