using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerEntry : MonoBehaviour {

	public string playerText;
	public string balanceText;
	public string stockBalanceText;
	public string profitText;

	public Text player;
	public Text balance;
	public Text stockBalance;
	public Text profit;
	public Image playerColorImg;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		player.text = playerText;
		balance.text = balanceText;
		stockBalance.text = stockBalanceText;
		profit.text = profitText;
	}
}
