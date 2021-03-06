﻿/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
public class Quest :MonoBehaviour {
	#region Inspector
	// TODO: The same field name is serialized miltiple times in hte class or its parent class
	//			This is not supported: Base(MonoBehaviour) m_Name
	[SerializeField] private string m_QuestName;
	public string QuestName { get { return m_QuestName; } }

	[SerializeField] private string m_Description;
	public string Description { get { return m_Description; } }

	// The time it will take to complete this quest
	[SerializeField] private int m_Duration;
	public int Duration { get { return m_Duration; } }

	// The reward for completing this quest
	[SerializeField] private int m_Reward;
	public int Reward { get { return m_Reward; } }

	[SerializeField] private int m_Difficulty;
	public int Difficulty { get { return m_Difficulty; } }

	[SerializeField] private int m_STR;
	public int STR { get { return m_STR; } }

	[SerializeField] private int m_MAG;
	public int MAG { get { return m_MAG; } }

	[SerializeField] private int m_DEX;
	public int DEX { get { return m_DEX; } }
	#endregion

	public QuestManager QuestBoard { get; set; }
	public int Completion { get; set; }

	public void Finished() {
		Destroy(gameObject);
	}
	private void Start() {
		Completion = 0;
		
	}
	public void UpdateText() {
		GetComponentInChildren<Text>().text = m_QuestName + "\n\r" + "STR: " + STR + " DEX: " + DEX + " MAG: " + MAG;
	}
	public void SelectThisQuest() {
		QuestBoard.PickQuest(gameObject);
	}
}
