﻿/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
using System.Collections.Generic;

public class GuildManager :MonoBehaviour {
	#region Inspector
	[SerializeField] private GameManager m_GameManager;
	[SerializeField] private List<GameObject> m_Members;
	public List<GameObject> Members { get { return m_Members; } }
	[SerializeField] private List<GameObject> m_Rooms;
	public List<GameObject> Rooms { get { return m_Rooms; } }
	#endregion

	#region Variables
	public int QuestCap { get; private set; }
	public int PopLimit { get; private set; }
	public int Gold { get; private set; }
	public int GoldCap { get; private set; }
	#endregion

	#region Custom Functions
	public int Population {
		get {
			if(m_Members != null) {
				return m_Members.Count;
			}
			else { return 0; }
		}
	}
	public void PayUpkeep() {
		Gold -= UpkeepCost;
		Debug.Log("Upkeep Paid! $-" + UpkeepCost + ".");
	}
	private int UpkeepCost {
		get {
			int u = 0;
			foreach(GameObject m in m_Members) {
				u = u + m.GetComponent<Member>().Upkeep;
			}
			foreach(GameObject r in m_Rooms) {
				u = u + r.GetComponent<Room>().Upkeep;
			}
			return u;
		}
	}
	private void IncreasePopLimit(int NewBeds) {
		Debug.Log(NewBeds + " new beds added to the guild!");
		PopLimit += NewBeds;
	}
	public void AddGold(int GoldToAdd) {
		Gold += GoldToAdd;
		Debug.Log(GoldToAdd + " Gold added to the bank!");
		if(Gold > GoldCap) {
			Gold = GoldCap;
		}
	}
	private void ClearMembers() {
		if(m_Members != null) {
			m_Members.Clear();
		}
		else { m_Members = new List<GameObject>(); }
	}
	private void ClearRooms() {
		if(m_Rooms != null) {
			m_Rooms.Clear();
		}
		else { m_Rooms = new List<GameObject>(); }
	}
	private void IncreaseGoldCap(int Amount) {
		GoldCap += Amount;
	}
	public void ClearGuild() {
		ClearRooms();

		Gold = m_GameManager.InitialGold;
		GoldCap = m_GameManager.InitialGoldCap;

		ClearMembers();
		PopLimit = m_GameManager.InitialPopCap;
		AddCharacterToGiuld(m_GameManager.CharClassPrefabList[0]);
		// TODO: Access list using keyword instead of index
		// AddCharacterToGiuld(g_GameManager.gm_AllCharacters.Adventurer);

		QuestCap = m_GameManager.InitialQuestCap;
		m_GameManager.QuestManager.UpdateQuests();

	}
	#endregion

	#region Unity Functions

	#endregion


	public void Purchase(GameObject PurchaseItem) {
		// TODO: Get the cost of the room or character
		int cost = 0;
		// If there is enough gold to make the purchase
		if(Gold - cost >= 0) {
			// The cost is subtracted from the bank
			Gold -= cost;
			// If the item is a Character
			if(PurchaseItem.GetComponent<Member>() != null) {
				AddCharacterToGiuld(PurchaseItem);
			}
			// If the item is a Room
			if(PurchaseItem.GetComponent<Room>() != null) {
				AddRoomToGuild(PurchaseItem);
			}
		}
	}
	private void AddRoomToGuild(GameObject Building) {
		//GameObject NewBuilding = Instantiate(g_GameManager.RoomPrefabList[0], g_GameManager.RoomList.transform);  
		// Instantiate the Room and make is a child of the RoomList GameObject
		//Room newRoom = NewBuilding.GetComponent<Room>(); 
		// Get the room script from the building
		//NewBuilding.name = newRoom.name + g_Rooms.Count;
		//newRoom.AddEffectToGuild(this);
		//g_Rooms.Add(NewBuilding);
	}
	private void AddCharacterToGiuld(GameObject Character) {
		GameObject NewChar = Instantiate(Character, m_GameManager.MemberList.transform); 
		Member newMember = NewChar.GetComponent<Member>();  
		// TODO: Generate the name of the new character
		NewChar.GetComponent<Member>().SetBaseStats();	
		NewChar.GetComponent<Member>().SetJobStats();
		newMember.QuestBoard = m_GameManager.QuestManager;
		Members.Add(NewChar);
	}

}
