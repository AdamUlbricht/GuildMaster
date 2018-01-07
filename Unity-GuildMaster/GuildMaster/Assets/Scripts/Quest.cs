using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {

	public new string name;
	public string description;
	public int STR_Req;
	public int MAG_Req;
	public int DEX_Req;

	private int odds;

	public bool AttemptQuest(Member member)
	{
		odds = 0;
		if (member.STR >= STR_Req) {
			odds++;
			Debug.Log("STR win");
		}
		if (member.MAG >= MAG_Req)
		{
			odds++;
			Debug.Log("MAG win");
		}
		if (member.DEX >= DEX_Req)
		{
			odds++;
			Debug.Log("DEX win");
		}
		if (odds >= Random.Range(0, 2))
		{
			Debug.Log("Quest Win");
			return true;
		}
		else
		{
			Debug.Log("Quest Fail");
			return false;
		}

	}
}
