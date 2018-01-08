/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
using System.Collections.Generic;

public class GuildManager : MonoBehaviour
{
	#region Private Variables
	[SerializeField] private GameManager g_GameManager;         // The GameManager
	[SerializeField] private List<GameObject> g_Members;        // The list of members currently in the guild
	[SerializeField] private List<GameObject> g_Rooms;          // The list of rooms in the guild

	private QuestManager g_QuestManager;    // The QuestManager, Gotten from the GameManager in Start()
	private int g_QuestCap;                 // The initial number of quests available
	private int g_PopLimit;                 // The maximum number of members the guild can have
	private int g_Gold;                     // The amount of gold available in the guild
	private int g_GoldCap;                  // The maximum gold the guild can hold
	private int UpkeepCost                  // The upkeep cost for running the guild
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
	private int CurrentPopulation           // The current number of members in the guild
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
	#endregion

	#region Public Propeties
	public int Population
	{
		get { return CurrentPopulation; }
	}            // Public read access to the current population
	public int QuestCap
	{
		get { return g_QuestCap; }
	}              // Public read access to the current quest cap
	public int PopulationLimit
	{
		get { return g_PopLimit; }
	}       // Public read access to the current population cap
	public int GoldCap
	{
		get { return g_GoldCap; }
	}               // Public read access to the current Gold cap
	public int Gold
	{
		get { return g_Gold; }
	}                  // Public read access to the current gold
	public List<GameObject> Rooms
	{
		get { return g_Rooms; }
	}    // Public read access to the current list of Room GameObjects
	public List<GameObject> Members
	{
		get { return g_Members; }
	}  // public read access tot he current list of Member Gameobjects
	#endregion

	private void Start()
	{
		g_QuestManager = g_GameManager.QuestManager;    // Get the QuestManager from the GameManager
	}

	void Update()
	{
		if (g_GameManager.IsRunning)
		{
			if (g_GameManager.Countdown())    // Countdown to end of month
			{
				g_QuestManager.UpdateQuests();

				g_Gold -= UpkeepCost; // Remove the upkeep cost from the treasury

			} // Countdown to end of month
			if (g_Gold < 0)
			{
				g_GameManager.EndGame();
			}
		}
	}
	public void Purchase(GameObject PurchaseItem)
	{
		int cost = 0;       // TODO: Get the cost of the room or character

		if (g_Gold - cost >= 0) // If there is enough gold to make the purchase
		{
			g_Gold -= cost; // The cost is subtracted from the wallet
			if (PurchaseItem.GetComponent<Member>() != null) // If the item is a Character
			{
				AddCharacterToGiuld(PurchaseItem);
			}
			else if (PurchaseItem.GetComponent<Room>() != null) // If the item is a Room
			{
				AddRoomToGuild(PurchaseItem);
			}
		}
	}           // Makes a purchase
	private void AddRoomToGuild(GameObject Building)
	{
		GameObject NewBuilding = Instantiate(g_GameManager.gm_AllRooms[0], g_GameManager.RoomList.transform);   // Instantiate the Room and make is a child of the RoomList GameObject
		Room newRoom = NewBuilding.GetComponent<Room>(); // Get the room script from the building
		NewBuilding.name = newRoom.name + g_Rooms.Count;
		newRoom.AddEffectToGuild(this);
		g_Rooms.Add(NewBuilding);
	}        // Adds the Building to the guild
	private void AddCharacterToGiuld(GameObject Character)
	{
		GameObject NewChar = Instantiate(Character, g_GameManager.MemberList.transform); // Instantiate the character and make it a child of the member list gameobject
		Member newMember = NewChar.GetComponent<Member>();  // Get the member script from the character
		string charName = newMember.m_Job.ToString() + g_Members.Count; // TODO: Generate the name of the new character
		newMember.name = charName;      // Assign the characters name to the member script name variable
		NewChar.name = charName;        // Assign the charactes name to the name of the GameObject
		NewChar.GetComponent<Member>().SetBaseStats();  // set the base stats of the character
		NewChar.GetComponent<Member>().SetJobStats();   // set the job stats of the character
		Members.Add(NewChar);
	}  // Adds the Character to the guild
	private void IncreasePopLimit(int NewBeds)
	{
		g_PopLimit += NewBeds;
	}              // Adds new beds to the guild
	private void AddGold(int GoldToAdd)
	{
		g_Gold += GoldToAdd;    // Add the gold to the wallet
		if (g_Gold > g_GoldCap)     // If the wallet exceeds the gold cap
		{
			g_Gold = g_GoldCap; // remove excess gold from the universe
		}
	}                     // Adds gold to the guild wallet
	private void ClearMembers()
	{
		if (g_Members != null)  // if there is a list of members
		{
			g_Members.Clear();  // clear the list of members
		}
		else g_Members = new List<GameObject>();    // generate a new list of members
	}                             // Clear the Members list
	private void ClearRooms()
	{
		if (g_Rooms != null)    // If there is a list of rooms
		{
			g_Rooms.Clear();    // clear the list of rooms
		}
		else g_Rooms = new List<GameObject>();  // genertate a new list of rooms
	}                               // Clear the Rooms List
	private void IncreaseGoldCap(int Amount)
	{
		g_GoldCap += Amount;
	}
	#region Public Methods
	public void ClearGuild()
	{
		g_GoldCap = g_GameManager.InitialGoldCap;   // reset the gold cap
		g_PopLimit = g_GameManager.InitialPopCap;   // reset the population cap
		ClearRooms();   // Clear all rooms from the guild
		ClearMembers(); // Clear all members from the guild

		AddCharacterToGiuld(g_GameManager.gm_AllCharacters[0]); // Add an adventurer to the guild

		// TODO: Access list using keyword instead of index
		// AddCharacterToGiuld(g_GameManager.gm_AllCharacters.Adventurer);

		g_Gold = g_GameManager.InitialGold; // Reset the gold to the initial value
		g_QuestCap = g_GameManager.InitialQuestCap; // Reset the quest cap to the initial value
		g_QuestManager.UpdateQuests();  // Update the Quest List

	}                               // Wipe the guild info ready for a new game
	#endregion
}
