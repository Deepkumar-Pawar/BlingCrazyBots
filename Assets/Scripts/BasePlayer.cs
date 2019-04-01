using UnityEngine;
using System.Collections;

public class BasePlayer : MonoBehaviour {

	public int ID;
	public int startingBalance;
	public int balance;
	public int stockBalance;
	public int profit;
	public string name;

	protected PlayerBussinessManager playerBussinessManager;
	protected GameManager gameManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaceOrder(BasePlayer player, int rate, int qty, Order.OrderType orderType)
	{
		if (CanOrder ()) {
			if (orderType == Order.OrderType.Buy) {
				playerBussinessManager.IssueBuyOrder (player, rate, qty);
			} else {
				playerBussinessManager.IssueSellOrder (player, rate, qty);
			}
		}
	}

	public virtual void PlaceOrder()
	{
		
	}

	public bool CanOrder()
	{
		return gameManager.gameOn;
	}


}
