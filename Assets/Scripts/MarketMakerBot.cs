using UnityEngine;
using System.Collections;

public class MarketMakerBot : BasePlayer {

	float timer;
	float startTimer;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		playerBussinessManager = GameObject.FindGameObjectWithTag("PlayerBussinessManager").GetComponent<PlayerBussinessManager> ();
		playerBussinessManager.players.Add (this);
		startTimer = Random.Range (10f, 50f);
		timer = startTimer;
	}

	// Update is called once per frame
	void Update () {
		if (CanOrder ()) {
			if (timer <= 0f) {
				PlaceOrder ();

				startTimer = Random.Range (10f, 75f);
				timer = startTimer;
			}
			timer -= Time.deltaTime;

			profit = (balance - startingBalance) + stockBalance * playerBussinessManager.latestExecutedRate;
		}
	}

	public override void PlaceOrder ()
	{
		int qtyMax = 0;
		int rateMax = int.MaxValue;
		bool found = false;
		Order.OrderType orderType = Order.OrderType.NotSet;

		foreach (Order order in playerBussinessManager.book.ordersList) {
			if (order.rate < rateMax && order.orderStatus != Order.OrderStatus.Executed) {
				found = true;
				qtyMax = order.number;
				rateMax = order.rate;
				orderType = order.orderType == Order.OrderType.Buy ? Order.OrderType.Sell : Order.OrderType.Buy;
			}
		}
		if (found) {
			PlaceOrder (this, rateMax + 1 , qtyMax + 10, orderType);
		}

	}

}

