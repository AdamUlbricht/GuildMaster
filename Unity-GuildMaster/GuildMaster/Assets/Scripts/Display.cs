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
	[SerializeField] private GuildManager d_GuildManager;
	[SerializeField] private GameManager d_GameManager;
	[SerializeField] private QuestManager d_QuestManager;
	[SerializeField] private Text d_GoldText;
	[SerializeField] private Text d_PopulationText;
	[SerializeField] private Text d_RoomsText;
	[SerializeField] private Text d_MembersText;
	[SerializeField] private Text d_DayText;

	[SerializeField] private GameObject d_QuestWindow;
	[SerializeField] private Text d_QuestNameList;
	[SerializeField] private Text d_QuestDescList;

	private string d_Treasury_Val;
	private string d_Population_Val;
	private string d_Rooms_Val;
	private string d_Members_Val;
	private string d_Quest_Val;
	private string newLine = "\r\n";    // Set the new line string
	private string t, list = "";
	#endregion
	void Update()
	{
		if (d_QuestWindow.activeSelf)	// If the quest window is active
		{
			d_QuestNameList.text = QuestNamesList();	// show the list of quest names
			d_QuestDescList.text = QuestDescList();	// show the list of quest descriptions
		}
		d_DayText.text = string.Concat(d_GameManager.NewDay.ToString(), "/", d_GameManager.Month.ToString());
		d_GoldText.text = string.Concat(d_GuildManager.Gold.ToString(), "/", d_GuildManager.GoldCap);
		d_PopulationText.text = string.Concat(d_GuildManager.Population, "/", d_GuildManager.PopulationLimit);
		d_RoomsText.text = RoomsList();
		d_MembersText.text = MembersList();
	}
	public string QuestNamesList()
	{
		if (d_QuestManager.ActiveQuests != null)
		{
			t = list = "";
			foreach (GameObject q in d_QuestManager.ActiveQuests)
			{
				t = string.Concat(list, q.GetComponent<Quest>().name, newLine);

				list = t;
			}
			return list;
		}
		else return "No List Found";
	}

	public string QuestDescList()
	{
		if (d_QuestManager.ActiveQuests != null)
		{
			t = list = "";
			foreach (GameObject q in d_QuestManager.ActiveQuests)
			{
				t = string.Concat(list, q.GetComponent<Quest>().description, newLine);
				list = t;
			}
			return list;
		}
		else return "No List Found";
	}

	public string RoomsList()
	{
		if (d_GuildManager.Rooms != null)
		{
			t = list = "";
			foreach (GameObject r in d_GuildManager.Rooms)    // for each room in the guild
			{
				t = string.Concat(list, r.GetComponent<Room>().name, newLine); // Add the next room to the end of the string list
				list = t;
			}
			return list;
		}
		else return "No list found";
	}
	public string MembersList()
	{
		if (d_GuildManager.Members != null)
		{
			t = list = "";
			foreach (GameObject m in d_GuildManager.Members)
			{
				t = string.Concat(list, m.GetComponent<Member>().name, " STR: ", m.GetComponent<Member>().STR, " MAG: ", m.GetComponent<Member>().MAG, " DEX:", m.GetComponent<Member>().DEX, newLine);
				list = t;
			}
			return list;
		}
		else return "No list found";
	}
}
