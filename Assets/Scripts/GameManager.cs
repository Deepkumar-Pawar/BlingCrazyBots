using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	PlayerBussinessManager playerBussinessManager;

	public Text timerText;

	public float gameTimer;

	public bool gameOn = true;

	public GameObject gameOverButtonBG;

	public Color gameOverButtonBGColor;

	public GameObject gameBG;
	public Material gameBGMat;

	// Use this for initialization
	void Start () {
		gameOverButtonBG.GetComponent<Image> ().color = Color.clear;
		playerBussinessManager = GameObject.FindGameObjectWithTag ("PlayerBussinessManager").GetComponent<PlayerBussinessManager> ();
	}
	
	// Update is called once per frame
	void Update () {

		gameBG.GetComponent<MeshRenderer> ().material = gameBGMat;

		if (gameTimer > 0f) {
			gameTimer -= Time.deltaTime;
		}

		if (gameTimer <= 0f) {
			gameOn = false;
			timerText.color = Color.red;
		}

		int minutes =(int) Mathf.Floor((int) gameTimer / 60);
		int seconds = (int) gameTimer % 60;

		timerText.text = minutes.ToString() + " : " + seconds.ToString();

	}

	void EndGame()
	{
		
	}

	void OnGUI()
	{
		if (!gameOn) {
			gameOverButtonBG.GetComponent<Image> ().color = gameOverButtonBGColor;

			if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 + 200, 100, 50), "Play Again")) {
				Application.LoadLevel (1);
			}
		}
	}

//	BasePlayer DetermineWinner()
//	{
//		BasePlayer winner;
//
//		for (int i = 1; i < playerBussinessManager.players.Count; i++) {
//			BasePlayer P2 = playerBussinessManager.players.ToArray () [i];
//			BasePlayer P1 = playerBussinessManager.players.ToArray () [i - 1];
//
//			if (true) {
//				
//			}
//		}
//	}
}
