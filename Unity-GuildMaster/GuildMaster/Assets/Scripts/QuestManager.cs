using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField] private GameObject GameManagerObject;  // The GameManager GameObject
	#endregion
	#region Hidden Variables
	private GuildManager q_GuildManager;
	private GameObject q_QuestList;
	private List<GameObject> q_ActiveQuests;
	private List<GameObject> q_AvailableQuests;
	#endregion
	#region Private Methods
	private void Start()
	{
		ActiveQuests.Clear();
		AvailableQuests.Clear();
	}
	#endregion
	#region Public Methods
	public List<GameObject> ActiveQuests
	{
		get { return q_ActiveQuests; }
	}
	public List<GameObject> AvailableQuests
	{
		get { return q_AvailableQuests; }
	}
	public void UpdateQuests()
	{
		foreach (GameObject Q in AvailableQuests)
		{
			Destroy(Q);
		}
		AvailableQuests.Clear();
		for (int i = 0; i < q_GuildManager.QuestCap; i++)
		{
			AddNewQuest();
		}

	}
	public void AddNewQuest()
	{
		//GameObject newQ = Instantiate(AllQuests[Random.Range(0, AllQuests.Count)],q_QuestList.transform,false);
		//AvailableQuests.Add(newQ);
	}
	public void GoOnQuest(Quest quest)
	{
		//AvailableQuests.Remove(Q.gameObject);
		//ActiveQuests.Add(Q.gameObject);

		//bool strWin = (M.STR > Q.STR_Req);
		//bool magWin = (M.MAG > Q.MAG_Req);
		//bool dexWin = (M.DEX > Q.DEX_Req);

	}
	#endregion
}
