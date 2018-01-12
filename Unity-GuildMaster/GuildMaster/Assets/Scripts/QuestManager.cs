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
	private GameObject m_SelectedQuest;
	private GameObject m_SelectedCharacter;
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
		newQ.GetComponent<Quest>().QuestBoard = this.GetComponent<QuestManager>();
		m_AvailableQuests.Add(newQ);
	}
	public void PickQuest(GameObject quest) {
		m_SelectedQuest = quest;
		m_GameManager.QuestList.SetActive(false);
		m_GameManager.MemberList.SetActive(true);
	}
	public void PickCharacter(GameObject character) {
		m_SelectedCharacter = character;
		m_GameManager.MemberList.SetActive(false);
		CompleteQuest();
	}
	private void CompleteQuest() {
		m_SelectedQuest.GetComponent<Quest>().Completion = 0;
		m_ActiveQuests.Add(m_SelectedQuest);
		m_AvailableQuests.Remove(m_SelectedQuest);

	}
	public void QuestProgression() {
		foreach(GameObject q in ActiveQuests) {
			Quest quest = q.GetComponent<Quest>();
			quest.Completion++;
			if(quest.Completion >= quest.Duration) {
				quest.Finished();
				Debug.Log("Quest Complete! - " + quest.name);
				// TODO: calculate whether or not the chacter succeeded
				m_GameManager.GuildScript.AddGold(quest.Reward);
				m_ActiveQuests.Remove(q);
				Destroy(q);
			}
		}
	}
	#endregion

	#region Unity Functions
	private void Start() {
		ClearActive();
		ClearAvailable();
	}
	#endregion

}