using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using System.Collections;
using System.Collections.Generic;

public class ApplicationSetup : MonoBehaviour {

	[SerializeField] private GameObject MyLobbyManager;
	private GameObject Manager;
	private NetworkLobbyManager LobbyNet;

	[SerializeField] private Canvas LobbyCreationMenu;
	[SerializeField] private Canvas ServerBrowser;
	[SerializeField] private Canvas DirectIPMenu;
	[SerializeField] private Canvas PasswordMenu;
	[SerializeField] private Button HostButton;
	[SerializeField] private Button ServerListButton;
	[SerializeField] private Button LanPlayButton;
	[SerializeField] private Button QuitButton;

	// Use this for initialization
	void Start () {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;

		if(GameObject.FindGameObjectWithTag("NetworkManager") == null)
			Instantiate (MyLobbyManager);
		
		if(Manager == null)
			Manager = GameObject.FindGameObjectWithTag("NetworkManager");

		if(LobbyNet == null)
			LobbyNet = Manager.GetComponent<NetworkLobbyManager>();

		//Hide all menus
		LobbyCreationMenu.enabled = false;
		ServerBrowser.enabled = false;
		DirectIPMenu.enabled = false;
		PasswordMenu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(LobbyCreationMenu.enabled)
		{
			if(GameObject.Find("LanToggle").GetComponent<Toggle>().isOn)
			{
				GameObject.Find("PasswordInput").GetComponent<InputField>().interactable = false;
			}
			else
			{
				GameObject.Find("PasswordInput").GetComponent<InputField>().interactable = true;
			}
		}
	}

	public void DisplayHost()
	{
		ServerBrowser.enabled = false; //Disable Server List

		DirectIPMenu.enabled = false; //Hide directIP menu

		LobbyCreationMenu.enabled = true; //Display Lobby Creation Menu
	}

	public void DisplayDirectIP()
	{
		LobbyCreationMenu.enabled = false; //Disable lobby creation screen

		ServerBrowser.enabled = false; //Disable Server List

		DirectIPMenu.enabled = true; //Enable direct connect menu
	}

	public void DisplayServerList()
	{
		LobbyCreationMenu.enabled = false; //Disable lobby creation screen

		DirectIPMenu.enabled = false; //Disable direct connect menu

		ServerBrowser.enabled = true; //Enable Server List

		ServerBrowser.transform.GetComponentInChildren<ServerBrowser>().LoadServerList();
	}

	public void DisplayPasswordMenu()
	{
		LobbyCreationMenu.enabled = false; //Disable lobby creation screen

		DirectIPMenu.enabled = false; //Disable direct connect menu

		ServerBrowser.enabled = false; //Enable Server List

		PasswordMenu.enabled = true;
	}
		
	public void DisableAllMenus()
	{
		LobbyCreationMenu.enabled = false; //Disable lobby creation screen

		DirectIPMenu.enabled = false; //Disable direct connect menu

		ServerBrowser.enabled = false; //Enable Server List

		PasswordMenu.enabled = false;

		HostButton.interactable = false;

		ServerListButton.interactable = false;

		LanPlayButton.interactable = false;

		QuitButton.interactable = false;
	}

	public void EnableMainMenu()
	{
		HostButton.interactable = true;

		ServerListButton.interactable = true;

		LanPlayButton.interactable = true;

		QuitButton.interactable = true;
	}

	public void HostGame()
	{
		if(!LobbyNet.isNetworkActive)
		{
			bool lan = GameObject.Find("LanToggle").GetComponent<Toggle>().isOn;

			string matchName = "Default";
			uint maxplayers = 4;
			int port = 7777;

			if(GameObject.Find("MatchNameInput").transform.Find("Text").GetComponent<Text>().text.Length != 0)
				matchName = GameObject.Find("MatchNameInput").transform.Find("Text").GetComponent<Text>().text;

			if(GameObject.Find("MaxPlayersInput").transform.Find("Text").GetComponent<Text>().text.Length != 0)
			{
				int input; //Max players variable
				int.TryParse(GameObject.Find("MaxPlayersInput").transform.Find("Text").GetComponent<Text>().text, out input); //Parse contents into integer

				if(input >= 2 && input <= 16) //If entered amount of players is within acceptable ranges
					maxplayers = (uint)input; //Set max players
				else{
					Debug.Log("INVALID PlAYER AMOUNT ENTERED");
					return;
				}
			}

			if(GameObject.Find("HostPortInput").transform.Find("Text").GetComponent<Text>().text.Length != 0)
			{
				int input; //Port variable
				int.TryParse(GameObject.Find("HostPortInput").transform.Find("Text").GetComponent<Text>().text, out input); //Parse contents into integer

				if(input > 0 && input <= 65535) //If entered port is within acceptable ranges
					port = input; //Set port
				else{
					Debug.Log("INVALID PORT ENTERED");
					return;
				}
			}
			LobbyNet.networkPort = port;

			if(!lan)
			{
				if(LobbyNet.matchMaker == null)
					LobbyNet.StartMatchMaker();
				
				NetworkMatch match = LobbyNet.matchMaker;
				CreateMatchRequest req = new CreateMatchRequest();
				req.name = matchName;
				req.size = maxplayers;
				req.advertise = true;

				if(GameObject.Find("PasswordInput").transform.Find("Text").GetComponent<Text>().text.Length != 0)
					req.password = GameObject.Find("PasswordInput").transform.Find("Text").GetComponent<Text>().text;
				else
					req.password = "";

				match.CreateMatch(req, LobbyNet.OnMatchCreate);
			}
			else
			{
				LobbyNet.name = matchName;
				LobbyNet.matchSize = maxplayers;

				LobbyNet.StartHost();
			}
		}
	}

	public void CloseLobby()
	{
		if(LobbyNet.isNetworkActive)
		{
			LobbyNet.StopHost();
		}

		if(LobbyNet.matchMaker != null)
		{
			LobbyNet.StopMatchMaker();
		}
	}

	public void DirectConnect()
	{
//		string ipInput = GameObject.Find("IPInput").transform.Find("Text").GetComponent<Text>().text;
//
//		if(ipInput.Length != 0)
//		{
//			if(!ipInput.Contains(" "))
//			{
//				ip = ipInput;
//			}
//			else{
//				Debug.Log("INVALID ADDRESS ENTERTED");
//				return;
//			}
//		}
//		
		string ip = "localhost";
		int port = 7777;

		if(GameObject.Find("LanPortInput").transform.Find("Text").GetComponent<Text>().text.Length != 0)
		{
			int portInput; //Port variable
			int.TryParse(GameObject.Find("LanPortInput").transform.Find("Text").GetComponent<Text>().text, out portInput); //Parse contents into integer

			if(portInput > 0 && portInput <= 65535) //If entered port is within acceptable ranges
				port = portInput; //Set port
			else{
				Debug.Log("INVALID PORT ENTERED");
				return;
			}
		}

		LobbyNet.networkAddress = ip;
		LobbyNet.networkPort = port;

		LobbyNet.StartClient();

		GameObject.Find("CancelLanButton").GetComponent<Button>().interactable = true;
		GameObject.Find("ConnectingText").GetComponent<Text>().enabled = true;
	}

	public void CancelLanConnect()
	{
		LobbyNet.StopClient();

		GameObject.Find("CancelLanButton").GetComponent<Button>().interactable = false;
		GameObject.Find("ConnectingText").GetComponent<Text>().enabled = false;
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
