/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
using System.Collections.Generic;

public class GuildManager : MonoBehaviour
{
	#region Variables and Properties
	public GameManager g_GameManager;   // The GameManager
	private List<GameObject> g_Rooms;     // The list of rooms in the guild
	public List<GameObject> Rooms { get { return g_Rooms; } }
	private int g_PopLimit;     // The maximum number of members the guild can have
	public int PopulationLimit { get { return g_PopLimit; } }
	private int g_Gold;     // The amount of gold available in the guild
	public int Gold { get { return g_Gold; } }
	private int g_GoldCap;
	public int GoldCap { get { return g_GoldCap; } }
	private List<GameObject> g_Members;     // The list of members currently in the guild
	public List<GameObject> Members { get { return g_Members; } }
	private int UpkeepCost      // The upkeep cost for running the guild
	{
		get
		{
			int u = 0;  // Initialise the upkeep calculation
			foreach (GameObject mem in g_Members)
			{
				u = u + mem.GetComponent<Member>().m_Upkeep;
			}   // Add the upkeep cost of each member in the guild
			foreach (GameObject room in g_Rooms)
			{
				u = u + room.GetComponent<Room>().Upkeep;
			}
			return u;   // Return the final sum
		}
	}
	private int CurrentPopulation   // The current number of members in the guild
	{
		get
		{
			if (g_Members != null)
			{
				return g_Members.Count;   // PopCurrent is always equal to members list length
			}
			else return 0;
		}
	}
	public int Population { get { return CurrentPopulation; } }
	[SerializeField] private GameObject Adventurer;
	[SerializeField] private GameObject Cabin;
	[SerializeField] private GameObject Treasury;
	#endregion
	void Update()
	{
		if (g_GameManager.IsRunning)
		{
			if (g_GameManager.Countdown())    // Countdown to end of month
			{
				g_Gold -= UpkeepCost; // Remove the upkeep cost from the treasury
			} // Countdown to end of month
			if (g_Gold < 0)
			{
				g_GameManager.EndGame();
			}
		}
	}
	public void AddNewAdventurer()
	{
		Member adventurer = Adventurer.GetComponent<Member>();
		if (g_Gold - adventurer.m_Cost >= 0)
		{
			g_Gold -= adventurer.m_Cost;
			AddAdventurer();
		}
	}     // Add a new member to the guild
	public void IncreasePopLimit(int NewBeds)
	{
		g_PopLimit += NewBeds;
	}
	public void AddMoney(int GoldToAdd)
	{
		g_Gold += GoldToAdd;
		if (g_Gold > g_GoldCap)
		{
			g_Gold = g_GoldCap;
		}
	}
	private void AddAdventurer()
	{
		Member adventurer = Adventurer.GetComponent<Member>();
		Adventurer = Instantiate(Adventurer);
		adventurer.SetBaseStats();
		adventurer.SetJobStats();
		adventurer.name = Adventurer.GetComponent<Member>().m_Job.ToString() + g_Members.Count;
		g_Members.Add(Adventurer);
	}
	public void ClearGuild()
	{
		g_GoldCap = g_GameManager.GoldCap;
		g_PopLimit = g_GameManager.PopCap;
		if (g_Members != null)
		{
			g_Members.Clear();
		}
		else g_Members = new List<GameObject>();

		AddAdventurer();
	
		if (g_Rooms != null)
		{
			g_Rooms.Clear();
		}
		else g_Rooms = new List<GameObject>();
		g_Gold = 0;
	}
	public void IncreaseTreasuryLimit(int Amount)
	{
		g_GoldCap += Amount;
	}
	public void AddNewCabin()
	{
		Room cabin = Cabin.GetComponent<Room>(); // Get the Room script from the gameobject
		if (g_Gold - cabin.BuildCost >= 0) // If there is enough gold
		{
			g_Gold -= cabin.BuildCost;	// Pay the gold to build the cabin
			Cabin = Instantiate(Cabin); // Instantiate new cabin gameobject
			cabin = Cabin.GetComponent<Room>(); // Get the Room script from the cabin
			cabin.name = "Cabin " + g_Rooms.Count;   // Set the name of the room
			cabin.AddEffectToGuild(this);   //	 Activate the effect
			g_Rooms.Add(Cabin); // Add the room to the list
		}
	}
	public void AddNewTreasury()
	{
		Room treasury = Treasury.GetComponent<Room>();
		if (g_Gold - treasury.BuildCost >= 0)
		{
			g_Gold -= treasury.BuildCost;
			Treasury = Instantiate(Treasury);
			treasury = Treasury.GetComponent<Room>();
			treasury.name = "Treasury " + g_Rooms.Count;
			treasury.AddEffectToGuild(this);
			g_Rooms.Add(Treasury);
		}
	}
}
