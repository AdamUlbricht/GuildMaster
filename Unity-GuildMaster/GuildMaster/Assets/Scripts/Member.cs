/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
public class Member :MonoBehaviour {
	#region Inspector
	// TODO: The same field name is serialized miltiple times in hte class or its parent class
	//			This is not supported: Base(MonoBehaviour) m_Name
	[SerializeField] private string m_Name;
	public string Name { get { return m_Name; } }
	[SerializeField] private int m_Upkeep;
	public int Upkeep { get { return m_Upkeep; } }
	[SerializeField] private int m_STR;
	public int STR { get { return m_STR; } }
	[SerializeField] private int m_MAG;
	public int MAG { get { return m_MAG; } }
	[SerializeField] private int m_DEX;
	public int DEX { get { return m_DEX; } }
	#endregion

	public GameObject MyQuest { get; set; }

	public QuestManager QuestBoard { get; set; }
	public void SelectThisCharacter() {
		QuestBoard.PickCharacter(this.gameObject);
	}
	public void SetBaseStats() {

	}
	public void SetJobStats() {

	}
}
