/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
using System.Collections.Generic;

public class GuildManager :MonoBehaviour {
	#region Inspector
	// The GameManager.
	[SerializeField] private GameManager m_GameManager;

	// The Members in the Guild.
	[SerializeField] private List<GameObject> m_Members;
	public List<GameObject> Members { get { return m_Members; } }

	// The Rooms in the Guild.
	[SerializeField] private List<GameObject> m_Rooms;
	public List<GameObject> Rooms { get { return m_Rooms; } }

	// The Members out on Quests.
	[SerializeField] private List<GameObject> m_MembersOnQuest;
	public List<GameObject> MembersOnQuest { get { return m_MembersOnQuest; } }

	#endregion

	#region Variables
	// The limit of the number of Quests available to the Guild at one time.
	public int QuestCap { get; private set; }

	// The limit of the number of Members which can live in the Guild at one time.
	public int PopLimit { get; private set; }

	// The amount of Gold in the Guild's Bank.
	public int Gold { get; private set; }

	// The limit of the amount of Gold which can be stored in the Guild's Bank at one time.
	public int GoldCap { get; private set; }

	// The name parts for the random name generator
	private string[] SyllableFirst = {
		"A",        "Ada",      "Aki",      "Al",       "Ali",      "Alo",      "Ana",      "Ani",      "Ba",       "Be",
		"Bo",       "Bra",      "Bro",      "Cha",      "Chi",      "Da",       "De",       "Do",       "Dra",      "Dro",
		"Eki",      "Eko",      "Ele",      "Eli",      "Elo",      "Er",       "Ere",      "Eri",      "Ero",      "Fa",
		"Fe",       "Fi",       "Fo",       "Fra",      "Gla",      "Gro",      "Ha",       "He",       "Hi",       "Illia",
		"Ira",      "Ja",       "Jo",       "Ka",       "Ki",       "Kra",      "La",       "Le",       "Lo",       "Ma",
		"Me",       "Mi",       "Mo",       "Na",       "Ne",       "No",       "O",        "Ol",       "Or",       "Pa",
		"Pe",       "Pi",       "Po",       "Pra",      "Qua",      "Qui",      "Quo",      "Ra",       "Re",       "Ro",
		"Sa",       "Sca",      "Sco",      "Se",       "Sha",      "She",      "Sho",      "So",       "Sta",      "Ste",
		"Sti",      "Stu",      "Ta",       "Tha",      "The",      "Tho",      "Ti",       "To",       "Tra",      "Tri",
		"Tru",      "Ul",       "Ur",       "Va",       "Vo",       "Wra",      "Xa",       "Xi",       "Zha",      "Zho"
	};
	private string[] SyllableSecond = {
		"bb",       "bl",       "bold",     "br",       "bran",     "can",      "carr",     "ch",       "cinn",     "ck",
		"ckl",      "ckr",      "cks",      "dd",       "dell",     "dr",       "ds",       "fadd",     "fall",     "farr",
		"ff",       "fill",     "fl",       "fr",       "genn",     "gg",       "gl",       "gord",     "gr",       "gs",
		"h",        "hall",     "hark",     "hill",     "hork",     "jenn",     "kell",     "kill",     "kk",       "kl",
		"klip",     "kr",       "krack",    "ladd",     "land",     "lark",     "ld",       "ldr",      "lind",     "ll",
		"ln",       "lord",     "ls",       "matt",     "mend",     "mm",       "ms",       "nd",       "nett",     "ng",
		"nk",       "nn",       "nodd",     "ns",       "nt",       "part",     "pelt",     "pl",       "pp",       "ppr",
		"pps",      "rand",     "rd",       "resh",     "rn",       "rp",       "rr",       "rush",     "salk",     "sass",
		"sc",       "sh",       "sp",       "ss",       "st",       "tall",     "tend",     "told",     "v",        "vall",
		"w",        "wall",     "wild",     "will",     "x",        "y",        "yang",     "yell",     "z",        "zenn"
		};
	private string[] SyllableThird = {
		"a",        "ab",       "ac",       "ace",      "ach",      "ad",       "adle",     "af",       "ag",       "ah",
		"ai",       "ak",       "aker",     "al",       "ale",      "am",       "an",       "and",      "ane",      "ar",
		"ard",      "ark",      "art",      "ash",      "at",       "ath",      "ave",      "ea",       "eb",       "ec",
		"ech",      "ed",       "ef",       "eh",       "ek",       "el",       "elle",     "elton",    "em",       "en",
		"end",      "ent",      "enton",    "ep",       "er",       "esh",      "ess",      "ett",      "ic",       "ich",
		"id",       "if",       "ik",       "il",       "im",       "in",       "ion",      "ir",       "is",       "ish",
		"it",       "ith",      "ive",      "ob",       "och",      "od",       "odin",     "oe",       "of",       "oh",
		"ol",       "olen",     "omir",     "or",       "orb",      "org",      "ort",      "os",       "osh",      "ot",
		"oth",      "ottle",    "ove",      "ow",       "ox",       "ud",       "ule",      "umber",    "un",       "under",
		"undle",    "unt",      "ur",       "us",       "ust",      "ut",       "",         "",         "",         ""
	};
	#endregion

	#region Custom Functions
	// Returns a name from the random name generator
	private string GenerateName() {
		int rnd1 = Random.Range(0, 99);
		int rnd2 = Random.Range(0, 99);
		int rnd3 = Random.Range(0, 99);
		string n = SyllableFirst[rnd1] + SyllableSecond[rnd2] + SyllableThird[rnd3];
		return n;
	}

	// Returns the current population of the Guild.
	public int Population {
		get {
			if(m_Members != null) {
				return m_Members.Count;
			}
			else { return 0; }
		}
	}

	// Subtracts the Upkeep Cost from the Guild Bank.
	public void PayUpkeep() {
		Gold -= UpkeepCost;
		Debug.Log("Upkeep Paid! $-" + UpkeepCost + ".");
	}

	// Returns the upkeep cost based on the guild at the time of call.
	private int UpkeepCost {
		get {
			int u = 0;
			foreach(GameObject m in m_Members) {
				u = u + m.GetComponent<Member>().Upkeep;
			}
			foreach(GameObject r in m_Rooms) {
				u = u + r.GetComponent<Room>().Upkeep;
			}
			return u;
		}
	}

	// Increases the Population Limit of the Guild.
	private void IncreasePopLimit(int NewBeds) {
		Debug.Log(NewBeds + " new beds added to the guild!");
		PopLimit += NewBeds;
	}

	// Adds gold to the Guild's Bank.
	public void AddGold(int GoldToAdd) {
		Gold += GoldToAdd;
		Debug.Log(GoldToAdd + " Gold added to the bank!");
		if(Gold > GoldCap) {
			Gold = GoldCap;
		}
	}

	// Remove all Members from the guild
	private void ClearMembers() {
		if(m_Members != null) {
			m_Members.Clear();
		}
		else { m_Members = new List<GameObject>(); }
		if (m_MembersOnQuest != null) {
			m_MembersOnQuest.Clear();
		}
		else { m_MembersOnQuest = new List<GameObject>(); }
	}

	// Removes all Rooms from the Guild.
	private void ClearRooms() {
		if(m_Rooms != null) {
			m_Rooms.Clear();
		}
		else { m_Rooms = new List<GameObject>(); }
	}

	// Increases the limit of the amount of Gold which can be stored in the Guild's Bank at one time.
	private void IncreaseGoldCap(int Amount) {
		GoldCap += Amount;
	}

	// Clears the Guild in preperation for a new game
	public void ClearGuild() {
		ClearRooms();

		Gold = m_GameManager.InitialGold;
		GoldCap = m_GameManager.InitialGoldCap;

		ClearMembers();
		PopLimit = m_GameManager.InitialPopCap;
		AddCharacterToGuild(m_GameManager.CharClassPrefabList[Random.Range(0,m_GameManager.CharClassPrefabList.Count)]);
		AddCharacterToGuild(m_GameManager.CharClassPrefabList[Random.Range(0, m_GameManager.CharClassPrefabList.Count)]);
		AddCharacterToGuild(m_GameManager.CharClassPrefabList[Random.Range(0, m_GameManager.CharClassPrefabList.Count)]);
		AddCharacterToGuild(m_GameManager.CharClassPrefabList[Random.Range(0, m_GameManager.CharClassPrefabList.Count)]);
		// TODO: Access list using keyword instead of index
		// AddCharacterToGiuld(g_GameManager.gm_AllCharacters.Adventurer);

		QuestCap = m_GameManager.InitialQuestCap;
		m_GameManager.QuestManager.UpdateQuests();

	}

	// Add a new Character to the Guild
	private void AddCharacterToGuild(GameObject Character) {
		// Instantiate the new character gameObject
		GameObject NewChar = Instantiate(Character, m_GameManager.MemberList.transform);
		// Get a reference to the Member script
		Member newMember = NewChar.GetComponent<Member>();
		// Initialise the Character
		newMember.Initialise(m_GameManager.QuestManager, GenerateName());
		// Add the member to the Guild
		Members.Add(NewChar);
		Debug.Log(newMember.name + " has been added to the guild!");
	}

	#endregion

	#region Unity Functions

	#endregion

	// TODO: Purchase a Room or Character
	//public void Purchase(GameObject PurchaseItem) {
	//	int cost = 0;
	//	if(Gold - cost >= 0) {
	//		Gold -= cost;
	//		if(PurchaseItem.GetComponent<Member>() != null) {
	//			AddCharacterToGiuld(PurchaseItem);
	//		}
	//		if(PurchaseItem.GetComponent<Room>() != null) {
	//			AddRoomToGuild(PurchaseItem);
	//		}
	//	}
	//}

	// TODO: Add a New Room to the Guild
	//private void AddRoomToGuild(GameObject Building) {
	//	GameObject NewBuilding = Instantiate(g_GameManager.RoomPrefabList[0], g_GameManager.RoomList.transform);
	//	//Instantiate the Room and make is a child of the RoomList GameObject
	//	Room newRoom = NewBuilding.GetComponent<Room>();
	//	//Get the room script from the building

	//   NewBuilding.name = newRoom.name + g_Rooms.Count;
	//	newRoom.AddEffectToGuild(this);
	//	g_Rooms.Add(NewBuilding);
	//}






}
