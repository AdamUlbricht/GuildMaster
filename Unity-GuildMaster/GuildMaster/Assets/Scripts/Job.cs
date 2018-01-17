using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Job :MonoBehaviour {
	[SerializeField] private string m_JobName;
	public string JobName { get { return m_JobName; }}

	[SerializeField] private int m_Upkeep;
	public int Upkeep { get { return m_Upkeep; } }

	[SerializeField] private int m_DEXBonus;
	public int DEXBonus { get { return m_DEXBonus; } }

	[SerializeField] private int m_STRBonus;
	public int STRBonus { get { return m_STRBonus; } }

	[SerializeField] private int m_MAGBonus;
	public int MAGBonus { get { return m_MAGBonus; } }

}
