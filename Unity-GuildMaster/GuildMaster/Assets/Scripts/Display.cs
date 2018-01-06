/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{

	#region Variables and Properties
	public GuildManager t_GuildManager;
	public Text t_Treasury;
	public Text t_Population;
	public Text t_Rooms;
	public Text t_Members;
	public Text t_Day;

	private string t_Treasury_Val;
	private string t_Population_Val;
	private string t_Rooms_Val;
	private string t_Members_Val;
	#endregion

	#region Unity Methods

	void Start()
	{
		

	}

	void Update()
	{
		t_Day.text =string.Concat(t_GuildManager.newDay.ToString(), "/", t_GuildManager.month.ToString());
		t_Treasury.text = t_GuildManager.treasury.ToString();
		t_Population.text = string.Concat(t_GuildManager.PopCurrent.ToString(), "/", t_GuildManager.popLimit);
		t_Rooms.text = RoomsList();
		t_Members.text = MembersList();
	}

	public string RoomsList()
	{
		string list = "";	// Initialise the string list
		string t = "";  // Initialise the temp variable
		string newLine = "\r\n";	// Set the new line string

		foreach(Room r in t_GuildManager.rooms)	// for each room in the guild
		{
			t = string.Concat(list, r.name, newLine); // Add the next room to the end of the string list
			list = t;
		}
		return list;
	}
	public string MembersList()
	{
		string list = ""; // Initialise the string list
		string t = ""; // Initialise the temp variable
		string newLine = "\r\n"; // Set the new line string

		foreach (Member m in t_GuildManager.members)
		{
			t = string.Concat(list, m.name, newLine);
			list = t;
		}
		return list;
	}

	#endregion
}
