using UnityEngine;
using System.Collections;

public class BaseBussinessManager : MonoBehaviour {

	Player thisPlayerProps;

	OrdersBook orderBook;


	public UnityEngine.UI.InputField buyNum;
	public UnityEngine.UI.InputField buyRate;

	public UnityEngine.UI.InputField sellNum;
	public UnityEngine.UI.InputField sellRate;

	// Use this for initialization
	void Start () {
		thisPlayerProps = gameObject.GetComponent<Player> ();

		orderBook = GameObject.FindGameObjectWithTag ("GO_OrdersBook").GetComponent<OrdersBook> ();
	}

	// Update is called once per frame
	void OnGUI () {
		if (GUI.Button(new Rect(20, 300, 50, 60), "Buy")) {
			IssueBuyOrder ();
			orderBook.adding = true;
		}

		if (GUI.Button(new Rect(20, 400, 50, 60), "Sell")) {
			IssueSellOrder ();
			orderBook.adding = true;
		}
	}

	void IssueBuyOrder()
	{
		Order newOrder = new Order();

		newOrder.orderType = Order.OrderType.Buy;
		newOrder.rate = int.Parse (buyRate.textComponent.text);
		newOrder.number = int.Parse (buyNum.textComponent.text);

		orderBook.ordersList.Add(newOrder);
	}

	void IssueSellOrder()
	{
		Order newOrder = new Order();

		newOrder.orderType = Order.OrderType.Sell;
		newOrder.rate = int.Parse(sellRate.textComponent.text);
		newOrder.number = int.Parse (sellNum.textComponent.text);

		orderBook.ordersList.Add(newOrder);
	}
}