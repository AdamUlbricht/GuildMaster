/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
using UnityEngine.UI;

public class Display :MonoBehaviour {
	#region Inspector
	[SerializeField] private GameManager m_GameManager;
	[SerializeField] private Text m_GoldText;
	[SerializeField] private Text m_PopulationText;
	[SerializeField] private Text m_RoomsText;
	[SerializeField] private Text m_MembersText;
	[SerializeField] private Text m_Date;
	#endregion

	#region Variables
	#endregion

	#region Custom Functions
	private void DisplayQuests() {
		if(m_GameManager.QuestList.activeInHierarchy) {
			DisplayQuestList();
		}
	}
	private void DisplayQuestList() {
		if(m_GameManager.QuestManager.AvailableQuests != null) {
			int n = 0;
			foreach(GameObject q in m_GameManager.QuestManager.AvailableQuests) {
				PlaceButton(q, n);
				n++;
			}
		}
		else { Debug.Log("No QuestList found!"); }
	}
	private void DisplayMembers() {
		if(m_GameManager.MemberList.activeSelf) {
			DisplayMemberList();
		}
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
	private void DisplayMemberList() {
		if(m_GameManager.GuildScript.Members != null) {
			int n = 0;
			foreach(GameObject m in m_GameManager.GuildScript.Members) {
				PlaceButton(m, n);
				n++;
			}
		}
		else { Debug.Log("No MemberList found!"); }
	}
	private void DisplayRoomsList() {
		if(m_GameManager.GuildScript.Rooms != null) {
			int n = 0;
			foreach(GameObject r in m_GameManager.GuildScript.Rooms) {
				PlaceButton(r, n);
				n++;
			}
		}
		else { Debug.Log("No RoomList found!"); }
	}
	private void PlaceButton(GameObject Button, int n) {
		int yPos = (-70) - (n * 40);
		Button.GetComponent<RectTransform>().anchoredPosition = new Vector2(50, yPos);
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
