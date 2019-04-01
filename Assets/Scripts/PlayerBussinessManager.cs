using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBussinessManager : MonoBehaviour
{

	public OrdersBook book;
	public List<BasePlayer> players = new List<BasePlayer> ();

	public int latestExecutedRate;

//	public BasePlayer playerInLead;

	// Use this for initialization
	void Start ()
	{
		book = GameObject.FindGameObjectWithTag ("GO_OrdersBook").GetComponent<OrdersBook> ();
	}
	
	// Update is called once per frame


	public void IssueBuyOrder (BasePlayer player, int rate, int qty)
	{
		Order newOrder = new Order ();
		newOrder.player = player;
		newOrder.orderType = Order.OrderType.Buy;
		newOrder.rate = rate;
		newOrder.number = qty;

//		playerInLead = FindWinningPlayer ();

		ProcessOrder (newOrder);

	}

	public void IssueSellOrder (BasePlayer player, int rate, int qty)
	{
		Order newOrder = new Order ();
		newOrder.player = player;
		newOrder.orderType = Order.OrderType.Sell;
		newOrder.rate = rate;
		newOrder.number = qty;

//		playerInLead = FindWinningPlayer ();

		ProcessOrder (newOrder);

	}
		public void ProcessOrder (Order playerOrder )
	{
		
		playerOrder.orderStatus = Order.OrderStatus.Open;
		book.adding = true;
		List<Order> matches = FindMatchingOrders (playerOrder);
		ProcessMatchingOrders (playerOrder, matches);
		book.ordersList.Add (playerOrder);
	}

	List<Order> FindMatchingOrders (Order playerOrder)
	{
		List<Order> result = new List<Order> ();

		foreach (Order bookedOrder in book.ordersList) {
			
			if (bookedOrder.orderStatus != Order.OrderStatus.Executed) {
				
				if (bookedOrder.orderType != playerOrder.orderType) {
			
					if (bookedOrder.orderType == Order.OrderType.Buy && playerOrder.rate <= bookedOrder.rate) {
					
						result.Add (bookedOrder);
					}

					if (bookedOrder.orderType == Order.OrderType.Sell && playerOrder.rate >= bookedOrder.rate) {

						result.Add (bookedOrder);
					}

				}
			}

		}
		if (playerOrder.orderType == Order.OrderType.Sell) {
			result.Sort ((a, b) => {if(a.rate >= b.rate) return 1; else return -1;});
		} else {
			result.Sort ((a, b) => {if(a.rate <= b.rate) return 1; else return -1;});
		}
		return result;
	}

	void ProcessMatchingOrders (Order playerOrder, List<Order> matchingOrders)
	{
		int transactedQty = 0;

		foreach (Order matchingOrder in matchingOrders) {
			if (transactedQty < playerOrder.number) {

				// 10 == 10 
				if (transactedQty + matchingOrder.number == playerOrder.number
					&& (matchingOrder.orderStatus == Order.OrderStatus.Open
						&& playerOrder.orderStatus == Order.OrderStatus.Open)
				) {

					transactedQty = transactedQty + matchingOrder.number;

					if (matchingOrder.orderType == Order.OrderType.Sell) { //SELL
						
						matchingOrder.player.stockBalance = matchingOrder.player.stockBalance - matchingOrder.number;
						matchingOrder.player.balance = matchingOrder.player.balance + (matchingOrder.rate * matchingOrder.number);

						playerOrder.player.stockBalance = playerOrder.player.stockBalance + matchingOrder.number;
						playerOrder.player.balance = playerOrder.player.balance - (matchingOrder.rate * matchingOrder.number);

						latestExecutedRate = matchingOrder.rate;


					} else { // BUY
						
						matchingOrder.player.stockBalance = matchingOrder.player.stockBalance + matchingOrder.number;
						matchingOrder.player.balance = matchingOrder.player.balance - (playerOrder.rate * matchingOrder.number);

						playerOrder.player.stockBalance = playerOrder.player.stockBalance - matchingOrder.number;
						playerOrder.player.balance = playerOrder.player.balance + (playerOrder.rate * matchingOrder.number);

						latestExecutedRate = playerOrder.rate;
					}

					matchingOrder.orderStatus = Order.OrderStatus.Executed;
					playerOrder.orderStatus = Order.OrderStatus.Executed;
					//break;
				}

				// 10 and 8

				if (transactedQty + matchingOrder.number < playerOrder.number
					&& (matchingOrder.orderStatus == Order.OrderStatus.Open
						&& playerOrder.orderStatus == Order.OrderStatus.Open))
				 {

					transactedQty = transactedQty + matchingOrder.number;

					if (matchingOrder.orderType == Order.OrderType.Sell) { //SELL

						matchingOrder.player.stockBalance = matchingOrder.player.stockBalance - matchingOrder.number;
						matchingOrder.player.balance = matchingOrder.player.balance + (matchingOrder.rate * matchingOrder.number);

						playerOrder.player.stockBalance = playerOrder.player.stockBalance + matchingOrder.number;
						playerOrder.player.balance = playerOrder.player.balance - (matchingOrder.rate * matchingOrder.number);

						latestExecutedRate = matchingOrder.rate;


					} else { // BUY

						matchingOrder.player.stockBalance = matchingOrder.player.stockBalance + matchingOrder.number;
						matchingOrder.player.balance = matchingOrder.player.balance - (playerOrder.rate * matchingOrder.number);

						playerOrder.player.stockBalance = playerOrder.player.stockBalance - matchingOrder.number;
						playerOrder.player.balance = playerOrder.player.balance + (playerOrder.rate * matchingOrder.number);

						latestExecutedRate = playerOrder.rate;
					}

					playerOrder.number = playerOrder.number - matchingOrder.number;
					transactedQty = 0;
					matchingOrder.orderStatus = Order.OrderStatus.Executed;

				}

				// 10 and 12

				if ((transactedQty + matchingOrder.number > playerOrder.number ) 
					&& (matchingOrder.orderStatus == Order.OrderStatus.Open
						&& playerOrder.orderStatus == Order.OrderStatus.Open)) {

					transactedQty = transactedQty + playerOrder.number;

					if (matchingOrder.orderType == Order.OrderType.Sell) { //SELL

						matchingOrder.player.stockBalance = matchingOrder.player.stockBalance - playerOrder.number;
						matchingOrder.player.balance = matchingOrder.player.balance + (matchingOrder.rate * playerOrder.number);

						playerOrder.player.stockBalance = playerOrder.player.stockBalance + matchingOrder.number;
						playerOrder.player.balance = playerOrder.player.balance - (matchingOrder.rate * playerOrder.number);

						matchingOrder.number = matchingOrder.number - playerOrder.number;
						playerOrder.orderStatus = Order.OrderStatus.Executed;

						latestExecutedRate = matchingOrder.rate;

					} else { // BUY

						matchingOrder.player.stockBalance = matchingOrder.player.stockBalance + playerOrder.number;
						matchingOrder.player.balance = matchingOrder.player.balance - (playerOrder.rate * playerOrder.number);

						playerOrder.player.stockBalance = playerOrder.player.stockBalance - playerOrder.number;
						playerOrder.player.balance = playerOrder.player.balance + (playerOrder.rate * playerOrder.number);

						matchingOrder.number = matchingOrder.number - playerOrder.number;
						playerOrder.orderStatus = Order.OrderStatus.Executed;

						latestExecutedRate = playerOrder.rate;
					}


					//break;

				}
				
			} else {

				break;

			}


		}
	}

//	BasePlayer FindWinningPlayer()
//	{
//		BasePlayer maxProfitPlayer = null;
//
//		foreach (BasePlayer player in players) {
//			BasePlayer nextPlayer = null;
//
//			if (!(players.IndexOf(player) == players.Count - 1)) {
//				nextPlayer = players.ToArray () [players.IndexOf (player) + 1];
//			}
//			else if (players.IndexOf(player) == players.Count - 1) {
//				nextPlayer = players.ToArray () [players.IndexOf (player)];
//			}
//
//			int maxProfit = Mathf.Max ((player.profit), (nextPlayer.profit));
//
//			if (maxProfit == player.profit) {
//				maxProfitPlayer = player;
//			} else {
//				maxProfitPlayer = nextPlayer;
//			}
//		}
//
//		return maxProfitPlayer;
//	}
}