/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GuildManager : MonoBehaviour
{
	#region Variables and Properties
	public bool isRunning;
	public Toggle toggleIsRunning;
	public List<Room> rooms;    // The list of rooms in the guild
	public int popLimit;    // The maximum number of members the guild can have
	public float time;   // The current time
	[SerializeField] public int treasury;    // The amount of gold available in the guild
	[SerializeField] private float DaysInMonth;  // The time between each upkeep is paid
	[SerializeField] public List<Member> members;    // The list of members currently in the guild
	[SerializeField] private Job[] JobsList;  // The jobs available to characters in this guild
	private RoomEffect[] roomEffectsList;    // The effects
	public int newDay;
	private int prevDay;
	public int month;
	private int Upkeep   // The upkeep cost for running the guild
	{
		get
		{
			int u = 0;  // Initialise the upkeep calculation
			foreach (Member mem in members)
			{ u = u + mem.m_job.j_upkeep; }   // Add the upkeep cost of each member in the guild
			return u;   // Return the final sum
		}
	}
	public int PopCurrent   // The current number of members in the guild
	{
		get
		{
			return members.Count;   // PopCurrent is always equal to members list length
		}
	}
	#endregion

	void Start()
	{
		isRunning = false;

	}

	void Update()
	{
		toggleIsRunning.isOn = isRunning;

		if (isRunning)
		{
			if (Countdown())    // Countdown to end of month
			{
				treasury -= Upkeep; // Remove the upkeep cost from the treasury
			} // Countdown to end of month
			if (treasury < 0)
			{
				EndGame();
			}
		}
	}

	public void InitialiseGame()
	{
		members.Clear();
		rooms.Clear();
		treasury = 0;
		time = DaysInMonth;
		newDay = prevDay = (int)DaysInMonth;
		AddNewMember(); // The guild begins with one guildmember

	}   // Resest the guild to start a new game
	public bool Countdown()
	{
		if (newDay > (int)time) // If the next day is reached
		{
			prevDay--;	// increment the days
			newDay--;	//
			foreach (Member m in members) 
			{
				if (Random.Range(0, 100) <= 25) // 25% chance for each member to bring in some money
				{
					AddMoney(20);
				}
			} // 50% chance for each member to earn 20 Gold
		}
		if (time <= 0)    // If the current time is less than or equal to 0
		{
			time = DaysInMonth;   // Reset the current time
			newDay = (int)DaysInMonth;
			prevDay = (int)DaysInMonth + 1;

			if (PopCurrent < popLimit) // If there is room in the guild
			{
				if (Random.Range(0, 100) >= 50) // 50% chance to add new member
				{
					AddNewMember();
				}
			}
			return true;    // Countdown is complete
		}
		time -= Time.deltaTime; // Subtract the time it took to complete last frame to the current time
		return false;  // Otherwise countdown is incomplete
	}   // Countdown to the end of the upkeep period
	public void AddNewMember()
	{
		members.Add(new Member(JobsList[0], ("Member_" + Time.time)));
	}     // Add a new member to the guild

	public void StartGame()
	{
		InitialiseGame();
		isRunning = true;
	}
	public void EndGame()
	{
		isRunning = false;
	}
	public void AddMoney(int profit)
	{
		treasury += profit;
	}
}
