using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : BasePlayer {

	public InputField buyNum;
	public InputField buyRate;

	public Text stockBalanceText;
	public Text balanceText;
	public Text lastTradedRateText;

	public InputField sellNum;
	public InputField sellRate;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		playerBussinessManager = GameObject.FindGameObjectWithTag("PlayerBussinessManager").GetComponent<PlayerBussinessManager> ();
		balance = startingBalance;
		playerBussinessManager.players.Add (this);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
		if (CanOrder ()) {
			if (GUI.Button (new Rect (buyRate.transform.position.x - 190
				, Screen.height - buyRate.transform.position.y - 10
				, 50, 60), "Buy")) {
				if (int.Parse (buyNum.textComponent.text) != 0 && int.Parse (buyRate.textComponent.text) != 0) {
					PlaceOrder (this, int.Parse (buyRate.textComponent.text), int.Parse (buyNum.textComponent.text), Order.OrderType.Buy);
				}
			}

			if (GUI.Button (new Rect (sellRate.transform.position.x - 190,
				Screen.height - sellRate.transform.position.y - 10,
				50, 60), "Sell")) {
				if (int.Parse (sellNum.textComponent.text) != 0 && int.Parse (sellRate.textComponent.text) != 0) {
					PlaceOrder (this, int.Parse (sellRate.textComponent.text), int.Parse (sellNum.textComponent.text), Order.OrderType.Sell);

				}
			}
			stockBalanceText.text = ("Stock Balance : " + stockBalance.ToString ());
			balanceText.text = ("Balance : " + balance.ToString ());
			lastTradedRateText.text = ("Last Traded Rate : " + playerBussinessManager.latestExecutedRate.ToString ());

			profit = (balance - startingBalance) + stockBalance * playerBussinessManager.latestExecutedRate;
		}
	}
}
