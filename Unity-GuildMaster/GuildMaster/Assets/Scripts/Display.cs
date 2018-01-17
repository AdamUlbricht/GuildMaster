/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class Display :MonoBehaviour {
	#region Inspector
	[SerializeField] private GameManager m_GameManager;
	[SerializeField] private Text m_GoldText;
	[SerializeField] private Text m_PopulationText;
	[SerializeField] private Text m_Date;
	#endregion

	#region Variables
	#endregion

	#region Custom Functions
	private void DisplayQuests() {
		if(m_GameManager.QuestList.activeInHierarchy) {
			DisplayQuestList();
			DisplayActiveQuestList();
		}
	}
	private void DisplayQuestList() {
		if(m_GameManager.QuestManager.AvailableQuests != null) {
			if(m_GameManager.QuestManager.AvailableQuests.Count > 0) {
				int n = 0;
				foreach(GameObject m in m_GameManager.QuestManager.AvailableQuests) { 
					PlaceButton(m, n, 1);
					m.GetComponent<Quest>().UpdateText();
					n++;
				}
			}
		}
	}
	private void DisplayActiveQuestList() {
		if(m_GameManager.QuestManager.ActiveQuests != null) {
			int n = 0;
			foreach(GameObject m in m_GameManager.QuestManager.ActiveQuests) {
				PlaceButton(m, n, 2);
				n++;
			}
		}
		else { Debug.Log("No MemberList found!"); }
	}
	private void PlaceButton(GameObject Button, int n, int col) {
		int yPos = (-70) - (n * 70);
		int xPos = (50 + ((col - 1) * 160));
		Button.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
	}
	private void DisplayMembers() {
		if(m_GameManager.MemberList.activeSelf) {
			DisplayMemberList();
			DisplayMembersOnQuestList();
		}
	}
	private void DisplayMemberList() {
		if(m_GameManager.GuildScript.Members != null) {
			int n = 0;
			foreach(GameObject m in m_GameManager.GuildScript.Members) {
				PlaceButton(m, n,1 );
				m.GetComponent<Member>().UpdateText();
				n++;
			}
		}
	}
	private void DisplayMembersOnQuestList() {
		if(m_GameManager.GuildScript.MembersOnQuest != null) {
			int n = 0;
			foreach(GameObject m in m_GameManager.GuildScript.MembersOnQuest) {
				PlaceButton(m, n, 2);
				n++;
			}
		}
		else { Debug.Log("No MemberList found!"); }
	}
	private void DisplayRooms() {
		if(m_GameManager.RoomList.activeSelf) {
			DisplayRoomsList();
		}
	}
	private void DisplayDate() {
		string date = m_GameManager.Day + "/" + m_GameManager.Month;
		m_Date.text = date;
	}
	private void DisplayGold() {
		string Gold = m_GameManager.GuildScript.Gold.ToString() + "/" + m_GameManager.GuildScript.GoldCap.ToString();
		m_GoldText.text = Gold;
	}
	private void DisplayPopulation() {
		string Population = m_GameManager.GuildScript.Population.ToString() + "/" + m_GameManager.GuildScript.PopLimit.ToString();
		m_PopulationText.text = Population;
	}
	private void DisplayRoomsList() {
		if(m_GameManager.GuildScript.Rooms != null) {
			int n = 0;
			foreach(GameObject r in m_GameManager.GuildScript.Rooms) {
				PlaceButton(r, n, 1);
				n++;
			}
		}
		else { Debug.Log("No RoomList found!"); }
	}
	private string ConcatList(string list, string newLine) {
		string t = list + newLine + "\r\n";
		return t;
	}
	#endregion

	#region Unity Functions
	private void Update() {
		DisplayQuests();
		DisplayMembers();
		DisplayRooms();
		DisplayDate();
		DisplayGold();
		DisplayPopulation();
	}
	#endregion
}
