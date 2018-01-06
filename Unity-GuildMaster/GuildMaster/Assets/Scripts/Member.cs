/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
[System.Serializable]
public class Member
{
	#region Variables and Properties
	[SerializeField] public string name;	// The name shown in the inspector
	[SerializeField] public Job m_job;   // The members current job
	#endregion

	public Member(Job job, string newName)	// Constructor
	{
		m_job = job;    // The job for this member
		name = newName;
	}
}
