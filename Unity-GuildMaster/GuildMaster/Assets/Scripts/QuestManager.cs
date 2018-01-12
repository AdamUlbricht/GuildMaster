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
		for(int i = 0; i < m_GameManager.GuildScript.QuestCap-1; i++) {
			AddNewQuest(i);
		}
	}
	public void AddNewQuest(int i) {
		//GameObject newQ = Instantiate(m_GameManager.QuestPrefabList[Random.Range(0, m_GameManager.QuestPrefabList.Count)], m_GameManager.QuestList.transform);
		GameObject newQ = Instantiate(m_GameManager.QuestPrefabList[i], m_GameManager.QuestList.transform); 
		newQ.GetComponent<Quest>().QuestBoard = this.GetComponent<QuestManager>();
		m_AvailableQuests.Add(newQ);
	}
	public void PickQuest(GameObject quest) {
		m_AvailableQuests.Remove(quest);
		m_ActiveQuests.Add(quest);
		m_SelectedQuest = quest;
		m_GameManager.QuestList.SetActive(false);
	}

	public void PickCharacter(GameObject character) {
		// The quest is given to the character.
		character.GetComponent<Member>().MyQuest = m_SelectedQuest;
		// Move the character out of the guild.
		m_GameManager.GuildScript.MembersOnQuest.Add(character);
		m_GameManager.GuildScript.Members.Remove(character);
		
	}
	public void QuestProgression() {
		foreach(GameObject c in m_GameManager.GuildScript.MembersOnQuest) {

			Quest q = c.GetComponent<Member>().MyQuest.GetComponent<Quest>();

			q.Completion++;

			if(q.Completion >= q.Duration) {

				q.Finished();

				Debug.Log("Quest Complete! - " + q.name);

				// TODO: calculate whether or not the chacter succeeded

				m_GameManager.GuildScript.AddGold(q.Reward);

				// Move the charcter back to the guild
				m_GameManager.GuildScript.MembersOnQuest.Remove(c);
				m_GameManager.GuildScript.Members.Add(c);
				
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