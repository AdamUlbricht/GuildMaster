/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
using System.Collections.Generic;

public class GameManager :MonoBehaviour {
	#region Inspector
	[SerializeField] private List<GameObject> m_CharClassPrefabList;
	public List<GameObject> CharClassPrefabList { get { return m_CharClassPrefabList; } }

	[SerializeField] private List<GameObject> m_RoomPrefabList;
	public List<GameObject> RoomPrefabList { get { return m_RoomPrefabList; } }

	[SerializeField] private List<GameObject> m_QuestPrefabList;
	public List<GameObject> QuestPrefabList { get { return m_QuestPrefabList; } }

	[SerializeField] private GameObject m_GuildObject;
	public GuildManager GuildScript { get { return m_GuildObject.GetComponent<GuildManager>(); } }

	[SerializeField] private GameObject m_QuestBoardObject;
	public QuestManager QuestManager { get { return m_QuestBoardObject.GetComponent<QuestManager>(); } }

	[SerializeField] private GameObject m_MemberList;
	public GameObject MemberList { get { return m_MemberList; } }

	[SerializeField] private GameObject m_QuestList;
	public GameObject QuestList { get { return m_QuestList; } }

	[SerializeField] private GameObject m_RoomList;
	public GameObject RoomList { get { return m_RoomList; } }

	[SerializeField] private int m_InitialPopCap;
	public int InitialPopCap { get { return m_InitialPopCap; } }
	[SerializeField] private int m_InitialQuestCap;
	public int InitialQuestCap { get { return m_InitialQuestCap; } }
	[SerializeField] private int m_InitialGoldCap;
	public int InitialGoldCap { get { return m_InitialGoldCap; } }
	[SerializeField] private int m_DaysInMonth;
	public int DaysInMonth { get { return m_DaysInMonth; } }
	[SerializeField] private int m_DayLength;
	public int DayLength { get { return m_DayLength; } }
	[SerializeField] private int m_InitialGold;
	public int InitialGold { get { return m_InitialGold; } }
	#endregion

	#region Variables
	private bool m_GameIsRunning;
	private float m_CurrentTime;
	public int Month { get; private set; }
	public int Day { get; private set; }
	#endregion

	#region Custom Functions
	// Countdown to the end of the month
	private void Countdown() {
		if(m_CurrentTime > DayLength) {
			NextDay();
			QuestManager.QuestProgression();
		}
		if(Day > DaysInMonth) {
			NextMonth();
		}
		m_CurrentTime += Time.deltaTime;
	}
	private void NextMonth() {
		Month++;
		ResetTheDay();
		QuestManager.UpdateQuests();
		GuildScript.PayUpkeep();
	}
	private void NextDay() {
		Day++;
		m_CurrentTime = 0;
	}
	public void StartGame() {
		InitialiseGame();
		m_GameIsRunning = true;
	}
	public void EndGame() {
		m_GameIsRunning = false;
	}
	private void InitialiseGame() {
		GuildScript.ClearGuild();
		ResetTheMonth();
	}
	private void ResetTheMonth() {
		ResetTheDay();
		Month = 1;
	}
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
			if(GuildScript.Gold < 0) {
				EndGame();
			}
		}
	}
	#endregion
}
