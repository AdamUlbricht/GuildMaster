/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
public class Room :MonoBehaviour {
	#region Inspector
	// TODO: The same field name is serialized miltiple times in hte class or its parent class
	//			This is not supported: Base(MonoBehaviour) m_Name
	[SerializeField] private string m_Name;
	public string Name { get { return m_Name; } }
	[SerializeField] private int m_BuildCost;
	public int BuildCost { get { return m_BuildCost; } }
	[SerializeField] private int m_Upkeep;
	public int Upkeep { get { return m_Upkeep; } }
	#endregion

}