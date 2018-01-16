/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
using UnityEngine.UI;
public class Member :MonoBehaviour {
	#region Inspector
	[SerializeField] private Text m_TextName;
	public string TextName { get { return m_TextName.text; } set { m_TextName.text = value; } }

	[SerializeField] private GameObject m_Job;
	public Job CharacterJob { get { return m_Job.GetComponent<Job>(); } }
	
	public int Upkeep { get { return CharacterJob.Upkeep; } }

	[SerializeField] private int m_STR;
	public int STR { get { return m_STR; } }

	[SerializeField] private int m_MAG;
	public int MAG { get { return m_MAG; } }

	[SerializeField] private int m_DEX;
	public int DEX { get { return m_DEX; } }

	#endregion

	public void Initialise( QuestManager q, string newName) {
		SetBaseStats();
		SetJobStats();
		gameObject.name = newName;
		TextName = newName;
		questManager = q;
	}

	public GameObject MyQuest { get; set; }

	private QuestManager questManager;

	public void SelectThisCharacter() {
		if(questManager.QuestSelected) {
			questManager.PickCharacter(this.gameObject);
		}
	}

	private int Rand() {
		return Random.Range(0, 2);
	}

	private void SetBaseStats() {
		m_DEX += Rand();
		m_STR += Rand();
		m_MAG += Rand();
	}
	private void SetJobStats() {
		m_DEX += CharacterJob.DEXBonus;
		m_STR += CharacterJob.STRBonus;
		m_MAG += CharacterJob.MAGBonus;
	}
}
