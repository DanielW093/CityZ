using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

	private GameObject ClientUI;
	private RectTransform HealthBar;
	private Text HealthText;

	private GameObject player;

	// Use this for initialization
	void Start () {
		ClientUI = GameObject.FindGameObjectWithTag("ClientUI"); //Get the client UI
		HealthBar = ClientUI.transform.Find ("HealthBackground").transform.Find ("HealthBar").GetComponent<RectTransform>(); //Get the health bar UI element
		HealthText = ClientUI.transform.Find ("HealthBackground").transform.Find ("HealthText").GetComponent<Text>(); //Get the health text UI element
	}
	
	// Update is called once per frame
	void Update () {
		if(player != null)
		{
			int health = player.GetComponent<PlayerMovement>().GetHealth(); //Get current health

			float scale = (float)health/100f; //Calulate scale health bar should have
			Vector3 healthScale = HealthBar.localScale; //Get current health scale
			healthScale.x = scale; //Set new health scale
			HealthBar.localScale = healthScale; //Apply changes

			Vector3 healthPos = HealthBar.localPosition; //Get current health position
			healthPos.x = -(100-health); //Set new health position
			HealthBar.localPosition = healthPos; //Apply changes

			HealthText.text = "Health: " + health; //Change healthbar text
		}
		else
		{
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); //Get array of players
		
			if(players.Length > 0)
			{
				for(int i = 0; i < players.GetLength (0); i++) //Cycle through players
				{
					PlayerMovement pm = players[i].GetComponent<PlayerMovement>();
					if(pm.isLocalPlayer) //Is current player local player?
					{
						player = players[i]; //Set player
					}
				}
			}
		}
	}
}
