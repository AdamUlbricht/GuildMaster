/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
public class Quest :MonoBehaviour {
	#region Inspector
	// TODO: The same field name is serialized miltiple times in hte class or its parent class
	//			This is not supported: Base(MonoBehaviour) m_Name
	[SerializeField] private string m_Name;
	public string Name { get { return m_Name; } }
	[SerializeField] private string m_Description;
	public string Description { get { return m_Description; } }
	    // The time it will take to complete this quest
	[SerializeField] private int m_Duration;
	public int Duration {
		get { return m_Duration; }
	}
	    // The reward for completing this quest
	[SerializeField] private int m_Reward;
	public int Reward {
		get { return m_Reward; }
	}
	[SerializeField] private int m_STR;
	public int STR { get { return m_STR; } }
	[SerializeField] private int m_MAG;
	public int MAG { get { return m_MAG; } }
	[SerializeField] private int m_DEX;
	public int DEX { get { return m_DEX; } }
	#endregion

	public QuestManager QuestBoard { get; set; }
	public float Completion { get; set; }
	public void Finished() {

	}
	private void Start() {
		Completion = 0;
	}
	public void SelectThisQuest() {
		QuestBoard.PickQuest(gameObject);
	}
}
