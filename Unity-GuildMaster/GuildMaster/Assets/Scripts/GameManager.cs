/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	#region Private Variables
	public List<GameObject> gm_AllCharacters;     // All Available Characters
	public List<GameObject> gm_AllRooms;          // All Available Rooms
	public List<GameObject> gm_AllQuests;         // All Available Quests

	[SerializeField] private GameObject gm_GuildObject;             // The Guild GameObject
	[SerializeField] private GameObject gm_QuestManager;            // The QuestManager GameObject

	[SerializeField] private GameObject gm_MemberList;               // The MemberList GameObject
	[SerializeField] private GameObject gm_QuestList;				// The QuestList GameObject
	[SerializeField] private GameObject gm_RoomList;					// The RoomList GameObject

	[SerializeField] private Toggle gm_ToggleIsRunning;             // A bool value to show if the game is running
	[SerializeField] private float gm_DaysInMonth;                  // The time between each upkeep is paid
	[SerializeField] private int gm_InitialPopCap;                  // The initial population cap of the guild
	[SerializeField] private int gm_InitialQuestCap;                // The initial quest cap of the guild
	[SerializeField] private int gm_InitialGoldCap;                 // The initial gold cap of the guild
	[SerializeField] private int gm_InitialGold;

	private int gm_month;           // The Current Month
	private int gm_PreviousDay;     // The Previous Day
	private int gm_CurrentDay;      // The Current Day
	private float gm_CurrentTime;   // The current time
	#endregion
	#region Properties
	public GameObject QuestList
	{
		get { return gm_QuestList; }
	}
	public GuildManager GM_Guild
	{
		get { return gm_GuildObject.GetComponent<GuildManager>(); }
	}        // The GuildManager Script attached to the Guild GameObject
	public QuestManager QuestManager
	{
		get { return gm_QuestManager.GetComponent<QuestManager>(); }
	}    // Public read access to the QuestManager script
	public GameObject MemberList
	{
		get { return gm_MemberList; }
	}        // Public read access to the Member List GameObject
	public GameObject RoomList
	{
		get { return gm_RoomList; }
	}				// Public read access to the RoomList GameObject
	public int InitialQuestCap
	{
		get { return gm_InitialQuestCap; }
	}          // Public read access to initial Quest Cap value
	public int InitialPopCap
	{
		get { return gm_InitialPopCap; }
	}            // Public read access to initial Population Cap value
	public int InitialGoldCap
	{
		get { return gm_InitialGoldCap; }
	}           // Public read access to initial Gold Cap value
	public int InitialGold
	{
		get { return gm_InitialGold; }
	}				// Public read access to initial gold value
	public int Month
	{
		get { return gm_month; }
	}                    // Public read access to the current month
	public bool IsRunning
	{
		get { return gm_ToggleIsRunning.isOn; }
	}               // Public read access to the running state of the game
	public int CurrentDay
	{
		get { return gm_CurrentDay; }
	}               // Public Read access to the current day
	public bool Countdown()
	{
		if (gm_CurrentDay < (int)gm_CurrentTime) // If the current time is greater than the current day
		{
			gm_PreviousDay++;  // Increment the day
			gm_CurrentDay++;   //
		}
		if (gm_CurrentTime > gm_DaysInMonth)    // If the current time is greater than the length of the month
		{

			gm_month++; // Increment the month
			return true;    // Countdown is complete
		}
		gm_CurrentTime += Time.deltaTime; // Add the time it took to complete the last frame to the current time
		return false;  // Countdown is incomplete
	}             // Countdown to the end of the month, returns true when month is finished
	#endregion
	void Start()
	{
		gm_ToggleIsRunning.isOn = false;    // The game is not running when the application starts

	}
	public void StartGame()
	{
		InitialiseGame();   // Initialise all the guild values
		gm_ToggleIsRunning.isOn = true; // The game starts running
	}          // Start the game
	public void EndGame()
	{
		gm_ToggleIsRunning.isOn = false;    // The game is no longer running
	}            // End the game
	public void InitialiseGame()
	{
		GM_Guild.ClearGuild();
		ResetTheMonth();
		// TODO: The guild begins with one guildmember
	}     // Resest the guild to start a new game
	private void ResetTheMonth()
	{
		ResetTheDays();
		gm_month = 1;
	}     // Resets the Month to the beggining
	private void ResetTheDays()
	{
		gm_CurrentTime = 0;   // Reset the current time
		gm_CurrentDay = 1;  // Reset the current day
		gm_PreviousDay = 0; // Reset the previous day
	}      // Resets the days to the beggining
}
