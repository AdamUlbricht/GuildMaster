/*
* Copyright (c) N45 Games
* Author: Adam Ulbricht
* http://www.n45games.com
*/
using UnityEngine;
[System.Serializable]
public class Room : MonoBehaviour
{
	#region Variables and Properties
	public new string name; // The name shown in the inspector
	public int BuildCost;    // The cost to build the room
	public int Upkeep;	    // The upkeep cost of the room
	public enum Effects { PopCap1,  PopCap2, PopCap3, TreasuryCap1, TreasuryCap2, TreasuryCap3};
	public Effects Effect;

	#endregion
	public void AddEffectToGuild(GuildManager guild)
	{
		switch (Effect)
		{
			case Effects.PopCap1:
				//guild.IncreasePopLimit(4);
				break;
			case Effects.PopCap2:
				//guild.IncreasePopLimit(6);
				break;
			case Effects.PopCap3:
				//guild.IncreasePopLimit(10);
				break;
			case Effects.TreasuryCap1:
				//guild.IncreaseGoldCap(100);
				break;
			case Effects.TreasuryCap2:
				//guild.IncreaseGoldCap(250);
				break;
			case Effects.TreasuryCap3:
				//guild.IncreaseGoldCap(500);
				break;
			default:
				break;
		}
	}
}