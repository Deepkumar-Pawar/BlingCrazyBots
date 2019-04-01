using UnityEngine;
using System.Collections;

public class RandomBot : BasePlayer {

	int orderCount = 0;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		playerBussinessManager = GameObject.FindGameObjectWithTag("PlayerBussinessManager").GetComponent<PlayerBussinessManager> ();
		playerBussinessManager.players.Add (this);
	}
	
	// Update is called once per frame
	void Update () {
		if (CanOrder ()) {
			if (playerBussinessManager.book.ordersList.Count == 0) {
				PlaceOrder ();
				orderCount = playerBussinessManager.book.ordersList.Count;
			} else if (orderCount != playerBussinessManager.book.ordersList.Count) {
				PlaceOrder ();
			}
			orderCount = playerBussinessManager.book.ordersList.Count;

			profit = (balance - startingBalance) + stockBalance * playerBussinessManager.latestExecutedRate;
		}
	}

	public override void PlaceOrder ()
	{
		int qty;
		int rate;
		Order.OrderType orderType;
		if (playerBussinessManager.latestExecutedRate > 0) {
			rate = Random.Range (1, playerBussinessManager.latestExecutedRate +10);
		} else {
			rate = Random.Range (1, 50);
		}
		qty = Random.Range (1, 51);
		

		orderType = (Order.OrderType) Random.Range (1, 3);

		PlaceOrder (this, rate, qty, orderType);
	}
}
