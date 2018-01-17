/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class GameManager :MonoBehaviour {
	#region Inspector
	// The list of Character Class Prefabs
	[SerializeField] private List<GameObject> m_CharClassPrefabList;
	public List<GameObject> CharClassPrefabList { get { return m_CharClassPrefabList; } }

	// The list of Room Prefabs
	[SerializeField] private List<GameObject> m_RoomPrefabList;
	public List<GameObject> RoomPrefabList { get { return m_RoomPrefabList; } }

	// The list of Quest Prefabs
	[SerializeField] private List<GameObject> m_QuestPrefabList;
	public List<GameObject> QuestPrefabList { get { return m_QuestPrefabList; } }

	// The Guild
	[SerializeField] private GameObject m_GuildObject;
	public GuildManager GuildScript { get { return m_GuildObject.GetComponent<GuildManager>(); } }

	// The QuestManager
	[SerializeField] private GameObject m_QuestBoardObject;
	public QuestManager QuestManager { get { return m_QuestBoardObject.GetComponent<QuestManager>(); } }

	// The GameObject which is the parent of Members displayed in the list
	[SerializeField] private GameObject m_MemberList;
	public GameObject MemberList { get { return m_MemberList; } }

	// The GameObject which is the parent of Quests displayed in the list
	[SerializeField] private GameObject m_QuestList;
	public GameObject QuestList { get { return m_QuestList; } }

	// The GameObject which is the parent of Rooms displayed in the list
	[SerializeField] private GameObject m_RoomList;
	public GameObject RoomList { get { return m_RoomList; } }

	// The Starting population limit
	[SerializeField] private int m_InitialPopCap;
	public int InitialPopCap { get { return m_InitialPopCap; } }

	// The starting Quest limit
	[SerializeField] private int m_InitialQuestCap;
	public int InitialQuestCap { get { return m_InitialQuestCap; } }

	// The starting gold limit
	[SerializeField] private int m_InitialGoldCap;
	public int InitialGoldCap { get { return m_InitialGoldCap; } }

	// The number of Days in the month
	[SerializeField] private int m_DaysInMonth;
	public int DaysInMonth { get { return m_DaysInMonth; } }

	// The number of Seconds in the day
	[SerializeField] private int m_DayLength;
	public int DayLength { get { return m_DayLength; } }

	// The Number of days between each questboard update
	[SerializeField] private int m_QuestPeriod;
	public int QuestPeriod {
		get { return m_QuestPeriod; }
	}

	// The amount of gold given to a new guild
	[SerializeField] private int m_InitialGold;
	public int InitialGold { get { return m_InitialGold; } }
	#endregion

	#region Variables
	private bool m_GameIsRunning;
	private float m_CurrentTime;
	public int Month { get; private set; }
	public int Day { get; private set; }
	public int QuestDay { get; set; }
	#endregion

	#region Custom Functions
	// Countdown to the end of the day and month
	private void Countdown() {
		if(m_CurrentTime > DayLength) {
			NextDay();
		}
		if(Day > DaysInMonth) {
			NextMonth();
		}
		m_CurrentTime += Time.deltaTime;
	}
	// The month has ended
	private void NextMonth() {
		Month++;
		ResetTheDay();
		GuildScript.PayUpkeep();
	}
	// The QuestPeriod has ended
	private void NextQuest() {
		QuestDay = 1;
		QuestManager.UpdateQuests();
	}
	// The day has ended
	private void NextDay() {
		Day++;
		QuestDay++;
		m_CurrentTime = 0;
		QuestManager.QuestProgression();
		if(QuestDay >= QuestPeriod) {
			NextQuest();
		}
	}
	// Start the game
	public void StartGame() {
		InitialiseGame();
		m_GameIsRunning = true;
		Debug.Log("The game has begun.");
	}
	// End the game
	public void EndGame() {
		m_GameIsRunning = false;
		Debug.Log("The game has ended.");
	}
	// Initialise a  new guild
	private void InitialiseGame() {
		GuildScript.ClearGuild();
		ResetTheMonth();
	}
	// reset month
	private void ResetTheMonth() {
		ResetTheDay();
		Month = 1;
	}
	// reset day
	private void ResetTheDay() {
		Day = 1;
	}
	#endregion

	#region Unity Functions
	private void Start() {
		m_GameIsRunning = false;
	}
	private void Update() {
		if(m_GameIsRunning) {
			Countdown();
			if(GuildScript.Gold < 0 || GuildScript.Members.Count + GuildScript.MembersOnQuest.Count <= 0) {
				EndGame();
			}
		}
	}
	#endregion
}
