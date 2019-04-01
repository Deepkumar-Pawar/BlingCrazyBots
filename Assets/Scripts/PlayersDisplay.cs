using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayersDisplay : MonoBehaviour {

	PlayerBussinessManager playerBussinessManager;

	public GameObject playerEntryObj;
	List<PlayerEntry> entries  = new List<PlayerEntry>();
	GameObject playerEntry;

	// Use this for initialization
	void Start () {
		playerBussinessManager = GameObject.FindGameObjectWithTag ("PlayerBussinessManager").GetComponent<PlayerBussinessManager> ();

		for (int i = 0; i < playerBussinessManager.players.Count; i++) {
			playerEntry = (GameObject)Instantiate (playerEntryObj);
			playerEntry.gameObject.transform.SetParent (gameObject.transform);
			entries.Add (playerEntry.GetComponent<PlayerEntry>());
		}
	}
	
	// Update is called once per frame
	void Update () {
//		GameObject playerEntry = null;
//		int j = 0;


		if (entries.Count == 0) {
			for (int i = 0; i < playerBussinessManager.players.Count; i++) {
				playerBussinessManager.players.Sort ((a, b) => {
					if (a.ID <= b.ID)
						return -1;
					else
						return 1;
				});

				playerEntry = (GameObject)Instantiate (playerEntryObj);
				playerEntry.gameObject.transform.SetParent (gameObject.transform);
				entries.Add (playerEntry.GetComponent<PlayerEntry>());
			}
		}else{
			for (int i = 0; i < playerBussinessManager.players.Count; i++) {

				//			if (j == 0) {
				//				playerEntry = (GameObject)Instantiate (playerEntryObj);
				//				playerEntry.gameObject.transform.SetParent (gameObject.transform);
				//				j++;
				//			}

//				if (((BasePlayer)playerBussinessManager.players.ToArray()[i]) == playerBussinessManager.playerInLead) {
//					entries.ToArray () [i].GetComponent<PlayerEntry> ().playerColorImg.color = Color.red;
//				}

				entries.ToArray()[i].GetComponent<PlayerEntry> ().playerText = ((BasePlayer)playerBussinessManager.players.ToArray()[i]).name;
				entries.ToArray()[i].GetComponent<PlayerEntry> ().balanceText = ((BasePlayer)playerBussinessManager.players.ToArray()[i]).balance.ToString();
				entries.ToArray()[i].GetComponent<PlayerEntry> ().stockBalanceText = ((BasePlayer)playerBussinessManager.players.ToArray()[i]).stockBalance.ToString();
				entries.ToArray()[i].GetComponent<PlayerEntry> ().profitText = ((BasePlayer)playerBussinessManager.players.ToArray()[i]).profit.ToString();

			}
		}
	}
}