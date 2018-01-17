/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class QuestManager :MonoBehaviour {
	#region Inspector
	// The GameManager
	[SerializeField] private GameManager m_GameManager;

	// The list of quests which are available to the guild
	[SerializeField] private List<GameObject> m_AvailableQuests;
	public List<GameObject> AvailableQuests { get { return m_AvailableQuests; } }

	// The list of quests which are currently being completed
	[SerializeField] private List<GameObject> m_ActiveQuests;
	public List<GameObject> ActiveQuests { get { return m_ActiveQuests; } }

	#endregion

	#region Variables
	private GameObject m_SelectedQuest;

	private Quest Q;
	private GameObject C;
	private Member M;
	private int Wins;
	public bool QuestSelected { get; private set; }
	#endregion

	#region Custom Functions
	// Remove all quests from the available quest list
	private void ClearAvailable() {
		if(m_AvailableQuests != null) {
			m_AvailableQuests.Clear();
		}
		else { m_AvailableQuests = new List<GameObject>(); }
	}

	// Remove all quests from the active quest list
	private void ClearActive() {
		if(m_ActiveQuests != null) {
			m_ActiveQuests.Clear();
		}
		else { m_ActiveQuests = new List<GameObject>(); }
	}

	// update the quest list with  new quests
	public void UpdateQuests() {
		foreach(GameObject q in m_AvailableQuests) {
			Destroy(q);
		}
		ClearAvailable();
		for(int i = 0; i < m_GameManager.GuildScript.QuestCap - 1; i++) {
			AddNewQuest(Random.Range(0, m_GameManager.QuestPrefabList.Count));
		}
	}

	// Add a new quest from the prefabs to the available quest list
	public void AddNewQuest(int i) {
		//GameObject newQ = Instantiate(m_GameManager.QuestPrefabList[Random.Range(0, m_GameManager.QuestPrefabList.Count)], m_GameManager.QuestList.transform);
		GameObject newQ = Instantiate(m_GameManager.QuestPrefabList[i], m_GameManager.QuestList.transform);
		newQ.GetComponent<Quest>().QuestBoard = this.GetComponent<QuestManager>();
		m_AvailableQuests.Add(newQ);
	}

	// Select a quest
	public void PickQuest(GameObject quest) {
		Debug.Log("The quest '" + quest.name + "' has been selected!");
		QuestSelected = true;
		// Store the selected quest
		m_SelectedQuest = quest;
		// close the quest list window
		m_GameManager.QuestList.SetActive(false);
	}

	// select a character to go on the selected quest
	public void PickCharacter(GameObject character) {
		Debug.Log("The character '" + character.name + "' has been selected!");
		// The quest is given to the character.
		character.GetComponent<Member>().MyQuest = m_SelectedQuest;

		// Move the character out of the guild.
		m_GameManager.GuildScript.MembersOnQuest.Add(character);
		m_GameManager.GuildScript.Members.Remove(character);
		// Move the quest out of the available list to the active list
		m_AvailableQuests.Remove(m_SelectedQuest);
		m_ActiveQuests.Add(m_SelectedQuest);
		Debug.Log(character.name + " has left the guild to complete the quest '" + m_SelectedQuest.name + "'.");
		QuestSelected = false;

	}

	public void QuestProgression() {
		foreach(GameObject c in m_GameManager.GuildScript.MembersOnQuest.ToArray()) {
			C = c;
			// Get a referemce to the characters member script
			M = C.GetComponent<Member>();
			// Get a reference to the Characters current Quest script.
			Q = M.MyQuest.GetComponent<Quest>();
			CheckProgress();

		}
	}

	private void CheckProgress() {
		// Increase Quest progression.
		Q.Completion++;
		if(Q.Completion >= Q.Duration) {
			QuestComplete();
		}
		else {
			int days = Q.Duration - Q.Completion;
			Debug.Log(C.name + " will return in " + days + " days.");
		}
	}

	private void QuestComplete() {
		// The quest is no longer active
		ActiveQuests.Remove(Q.gameObject);
		// The Character is no longer on the quest
		m_GameManager.GuildScript.MembersOnQuest.Remove(C);
		// Finish the Quest
		Q.Finished();


		CheckSuccess();

	}

	private void CheckSuccess() {
		Wins = 0;
		CheckWin(M.DEX, Q.DEX);
		CheckWin(M.STR, Q.STR);
		CheckWin(M.MAG, Q.MAG);

		bool Success = false;
		// If the number of wins is eqaual to, or greater than, the quests difficulty level
		if(Wins >= Q.Difficulty) {
			// The quest is a success
			Success = true;
			m_GameManager.GuildScript.AddGold(Q.Reward);
			m_GameManager.GuildScript.Members.Add(C);
			Debug.Log(C.name + " has returned from the quest " + Q.name + "!");
			
		}
		else {
			Success = false;
			Debug.Log(C.name + " did not make it back from the quest " + Q.name + "...");
			// The character lost
			Destroy(C);
		}
		Analytics.CustomEvent("Quest", new Dictionary<string, object>
{
			{"Character Name", M.name },
			{"Character DEX", M.DEX },
			{"Character MAG", M.MAG },
			{"Character STR", M.STR },
			{"Quest Name", Q.name },
			{"Quest DEX",  Q.DEX},
			{"Quest MAG",  Q.MAG},
			{"Quest STR",  Q.STR},
			{"Success", Success }
		});
	}

	private void CheckWin(int c, int q) {
		if(c >= q) {
			Wins++;
		}
	}

	#endregion

	#region Unity Functions
	private void Start() {
		ClearActive();
		ClearAvailable();
		QuestSelected = false;
	}
	#endregion

}