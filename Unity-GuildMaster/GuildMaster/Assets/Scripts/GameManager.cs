/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	#region Variables
	[SerializeField] private GuildManager	gm_GuildManager;
	[SerializeField] private float			gm_DaysInMonth;  // The time between each upkeep is paid
	[SerializeField] private Toggle			gm_ToggleIsRunning;
	[SerializeField] private int gm_InitialPopCap;
	public int PopCap { get { return gm_InitialPopCap; } }
	public int GoldCap { get { return gm_InitialGoldCap; } }
	[SerializeField] private int gm_InitialGoldCap;
	private bool	gm_isRunning;
	public bool		IsRunning { get { return gm_isRunning; } }
	private int		gm_NewDay;
	public int		NewDay { get { return gm_NewDay; } }
	private int		gm_prevDay;
	private int		gm_month;
	public int		Month { get { return gm_month; } }
	private float	gm_CurrentTime;  // The current time
	public bool		Countdown()
	{
		if (gm_NewDay > (int)gm_CurrentTime) // If the next day is reached
		{
			gm_prevDay--;  // increment the days
			gm_NewDay--;   //
			foreach (GameObject m in gm_GuildManager.Members)
			{
				if (Random.Range(0, 100) <= 25) // 25% chance for each member to bring in some money
				{
					gm_GuildManager.AddMoney(20);
				}
			} // 50% chance for each member to earn 20 Gold
		}
		if (gm_CurrentTime <= 0)    // If the current time is less than or equal to 0
		{
			gm_CurrentTime = gm_DaysInMonth;   // Reset the current time
			gm_NewDay = (int)gm_DaysInMonth;
			gm_prevDay = (int)gm_DaysInMonth + 1;

			if (gm_GuildManager.Population <gm_GuildManager.PopulationLimit) // If there is room in the guild
			{
				if (Random.Range(0, 100) >= 50) // 50% chance to add new member
				{
					gm_GuildManager.AddNewAdventurer();
				}
			}
			return true;    // Countdown is complete
		}
		gm_CurrentTime -= Time.deltaTime; // Subtract the time it took to complete last frame to the current time
		return false;  // Otherwise countdown is incomplete
	}   // Countdown to the end of the upkeep period
	#endregion
	void Start()
	{
		gm_isRunning = false;

	}
	void Update()
	{
		gm_ToggleIsRunning.isOn = gm_isRunning;
	}
	public void StartGame()
	{
		InitialiseGame();
		gm_isRunning = true;
	}
	public void EndGame()
	{
		gm_isRunning = false;
	}
	public void InitialiseGame()
	{
		gm_GuildManager.ClearGuild();
		gm_CurrentTime = gm_DaysInMonth;
		gm_NewDay = gm_prevDay = (int)gm_DaysInMonth;
		gm_GuildManager.AddNewAdventurer(); // The guild begins with one guildmember
	}   // Resest the guild to start a new game
}
