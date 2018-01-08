/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField] private GameObject GameManagerObject;  // The GameManager Object
	[SerializeField] private Text d_GoldText;           // The Gold textbox
	[SerializeField] private Text d_PopulationText;     // The Population Textbox
	[SerializeField] private Text d_RoomsText;          // The Rooms list textbox
	[SerializeField] private Text d_MembersText;        // The Members list textbox
	[SerializeField] private Text d_DayText;            // The Date textbox
	[SerializeField] private GameObject d_QuestWindow;  // The Quest Window
	[SerializeField] private GameObject d_MemberWindow; // The Member Window
	#endregion
	#region Hidden Variables
	private GameObject d_MemberList;        // The MemberList GameObject
	private GameObject d_QuestList;         // The QuestList GameObject
	private string d_LineReturn = "\r\n";   // The string used for a Line Return
	private GuildManager d_guild;           // The Guild
	private QuestManager d_quests;          // The QuestManager
	private GameManager d_GameManager;      // The GameManager
	#endregion
	#region Private Methods
	private void QuestWindow()
	{
		if (d_QuestWindow.activeSelf)   // If the quest window is active
		{
			ShowQuestNamesList();   // Show the list of quest names
		}
	}       // Show the quests in the Quest Window
	private void MemberWindow()
	{
		if (d_MemberWindow.activeSelf)  // If the Member Window is active
		{
			ShowMemberNamesList();  // Show the list of Member Names
		}
	}       // Show the members in the Member Window
	private void Day()
	{
		d_DayText.text = string.Concat(d_GameManager.CurrentDay, "/", d_GameManager.Month); // Update the Date display
	}               // Update the Date display
	private void Gold()
	{
		d_GoldText.text = string.Concat(d_guild.Gold, "/", d_guild.GoldCap);    // Update the Gold display
	}               // Update the Gold display
	private void Population()
	{
		d_PopulationText.text = string.Concat(d_guild.Population, "/", d_guild.PopulationLimit);    // Update the Population display
	}       // Update the Population display
	private void Rooms()
	{
		d_RoomsText.text = RoomsList(); // Update the Rooms display
	}   // Update the Rooms display
	private void Members()
	{
		d_MembersText.text = MembersList(); // Update the Members display
	}   // Update the Members display
	#endregion
	#region Public Methods
	public void PlaceButton(GameObject Button, int NumberInList)
	{
		int yPos = (-70) - (NumberInList * 40);  // Calculate the y position 
		Button.GetComponent<RectTransform>().anchoredPosition = new Vector2(50, yPos); // Place the RectTransform in position
	}   // Place the GameObject Button in position in the list
	public void ShowQuestNamesList()
	{
		if (d_quests.AvailableQuests != null)   // If there is a valid list of quests
		{
			int NumberInList = 0;   // Reset the number in list
			foreach (GameObject Q in d_quests.AvailableQuests)  // For each quest in the quest list
			{
				PlaceButton(Q, NumberInList);
				NumberInList++;
			}
		}
	}   // Show the list of Quest Names
	public void ShowMemberNamesList()
	{
		if (d_guild.Members != null)    // If there is a valid list of Members
		{
			int NumberInList = 0;   // Rest the number in list
			foreach (GameObject M in d_guild.Members)   // For each member in the member list
			{
				PlaceButton(M, NumberInList);
				NumberInList++;
			}
		}
	}    // Show the list of Member Names
	public string ConcatList(string currentList, string newLine)
	{
		return string.Concat(currentList, newLine, d_LineReturn); // Add the name to the list, followed by a line return
	}   // Add a new line to a given list
	public string RoomsList()
	{
		if (d_guild.Rooms != null)
		{
			string list = "";
			foreach (GameObject Room in d_guild.Rooms)    // for each room in the guild
			{
				ConcatList(list, Room.name);    // Add the rooms name to the list
			}
			return list;
		}
		else return "No list found";
	}    // Returns a list of rooms as a string
	public string MembersList()
	{
		if (d_guild.Members != null)
		{
			string list = "";
			foreach (GameObject Member in d_guild.Members)
			{
				ConcatList(list, Member.name);
			}
			return list;
		}
		else return "No list found";
	}   // Returns a list of members as a string
	#endregion
	#region Unity Methods
	private void Start()
	{
		d_GameManager = GameManagerObject.GetComponent<GameManager>();  // Get the GameManager Script from the GameManagerObject
		d_guild = d_GameManager.GM_Guild;   // Get the Guild from the GameManager
		d_MemberList = d_GameManager.MemberList;    // Get the MemberList GameObject from the GameManager
		d_QuestList = d_GameManager.QuestList;  // Get the QuestList GameObject from the GameManager
	}   // Application Start
	void Update()
	{
		QuestWindow(); // Display Quest Window contents
		MemberWindow(); // Display Member Window contents
		Day();  // Display the day
		Gold(); // Display the Gold
		Population();	// Display the Population
		Rooms();	// Display the rooms
		Members();	// Display the Members
	}    // Run Every Frame
	#endregion
}
