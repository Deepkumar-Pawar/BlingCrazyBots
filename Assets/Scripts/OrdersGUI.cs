using UnityEngine;
using System.Collections;
using System.Linq;

public class OrdersGUI : MonoBehaviour {

	OrdersBook orderBook;

	GameManager gameManager;

	public GameObject orderEntry;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		orderBook = GameObject.FindGameObjectWithTag ("GO_OrdersBook").GetComponent<OrdersBook> ();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject ordEnt;

		if (orderBook.adding) {
			ClearEntries ();
		}

		if (orderBook.adding && gameManager.gameOn == true) {
			orderBook.ordersList.Sort ((a, b) => {
				if (a.orderStatus == Order.OrderStatus.Executed)
					return -1;
				else
					return 1;
			});

			var x = (from c in orderBook.ordersList
				select c)
				.OrderBy (c => c.number)
				.OrderBy (c => c.rate)
				.OrderByDescending(c=>c.orderType)
				.OrderByDescending(c=>c.orderStatus);
			for (int i = 0; i <x.Count() ; i++) {
				ordEnt = (GameObject) Instantiate(orderEntry);
				ordEnt.gameObject.transform.SetParent (gameObject.transform);

				if ((x.ElementAt(i)).orderStatus == Order.OrderStatus.Executed) {
					ordEnt.GetComponent<OrderEntry> ().BG.color = Color.grey;
				}else {
					ordEnt.GetComponent<OrderEntry> ().BG.color = Color.green;
				}
				ordEnt.GetComponent<OrderEntry> ().typeText = x.ElementAt(i).orderType.ToString();
				ordEnt.GetComponent<OrderEntry> ().numText = x.ElementAt(i).number.ToString();
				ordEnt.GetComponent<OrderEntry> ().rateText = x.ElementAt(i).rate.ToString();
				ordEnt.GetComponent<OrderEntry> ().playerIDText = x.ElementAt(i).player.name;
				ordEnt.GetComponent<OrderEntry> ().statusText = x.ElementAt(i).orderStatus.ToString();

				orderBook.adding = false;

				if (i > 25) {
					break;
				}
			}
		}
	}

	void ClearEntries ()
	{
		for (int i = 0; i < transform.childCount; i++) {
			Destroy(transform.GetChild(i).gameObject);
		}
	}
}
