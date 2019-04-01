using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OrderEntry : MonoBehaviour {

	public string typeText;
	public string numText;
	public string rateText;
	public string playerIDText;
	public string statusText;

	public Image BG;
	public Text type;
	public Text num;
	public Text rate;
	public Text playerID;
	public Text status;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		type.text = typeText;
		num.text = numText;
		rate.text = rateText;
		playerID.text = playerIDText;
		status.text = statusText;
	}
}
