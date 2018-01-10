/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using System.Collections.Generic;
using UnityEngine;

public class QuestManager :MonoBehaviour {
	#region Inspector
	[SerializeField] private GameManager m_GameManager;
	[SerializeField] private List<GameObject> m_AvailableQuests;
	public List<GameObject> AvailableQuests { get { return m_AvailableQuests; } }
	[SerializeField] private List<GameObject> m_ActiveQuests;
	public List<GameObject> ActiveQuests { get { return m_ActiveQuests; } }
	#endregion

	#region Variables
	#endregion

	#region Custom Functions
	private void ClearAvailable() {
		if(m_AvailableQuests != null) {
			m_AvailableQuests.Clear();
		}
		else { m_AvailableQuests = new List<GameObject>(); }
	}
	private void ClearActive() {
		if(m_ActiveQuests != null) {
			m_ActiveQuests.Clear();
		}
		else { m_ActiveQuests = new List<GameObject>(); }
	}
	public void UpdateQuests() {
		foreach(GameObject q in m_AvailableQuests) {
			Destroy(q);
		}
		ClearAvailable();
		for(int i = 0; i < m_GameManager.GuildScript.QuestCap; i++) {
			AddNewQuest();
		}
	}
	public void AddNewQuest() {
		GameObject newQ = Instantiate(m_GameManager.QuestPrefabList[Random.Range(0, m_GameManager.QuestPrefabList.Count)], m_GameManager.QuestList.transform);
		m_AvailableQuests.Add(newQ);
	}
	public void GoOnQuest(Quest quest) {
		Debug.Log("TODO: GoOnQuest(Quest quest) - sends a character on the selected quest.");
	}
	#endregion

	#region Unity Functions
	private void Start() {
		ClearActive();
		ClearAvailable();
	}
	#endregion
}