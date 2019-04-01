using UnityEngine;
using System.Collections;

public class Order : MonoBehaviour {

	public enum OrderType {NotSet, Sell, Buy};
	public enum OrderStatus {NotSet, Executed, Open};

	public BasePlayer player;

	public OrderType orderType;
	public OrderStatus orderStatus = OrderStatus.Open;

	public int rate; //For 1 commodity
	public int number;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
