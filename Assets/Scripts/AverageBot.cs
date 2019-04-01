using UnityEngine;
using System.Collections;

public class AverageBot : BasePlayer {

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

				startTimer = Random.Range (10f, 20f);
				timer = startTimer;
			}
			timer -= Time.deltaTime;

			profit = (balance - startingBalance) + stockBalance * playerBussinessManager.latestExecutedRate;
		}
	}

	public override void PlaceOrder ()
	{
		int qtySum = 0;
		int rateSum = 0;

		foreach (Order order in playerBussinessManager.book.ordersList) {
			qtySum += order.number;
			rateSum += order.rate;
		}

		int qty = qtySum / playerBussinessManager.book.ordersList.Count;
		int rate = rateSum / playerBussinessManager.book.ordersList.Count;
		Order.OrderType orderType;

		orderType = (Order.OrderType) Random.Range (1, 3);

		PlaceOrder (this, rate, qty, orderType);
	}
}
