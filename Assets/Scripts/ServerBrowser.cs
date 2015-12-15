using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using System.Collections;
using System.Collections.Generic;

public class ServerBrowser : MonoBehaviour {

	Button ConnectButton;

	ToggleGroup togglegroup;
	MatchDesc ConnectGame;

	List<MatchDesc> matchList = new List<MatchDesc>();
	List<GameObject> matchDisplays = new List<GameObject>();
	[SerializeField] private GameObject ServerDisplay;

	void Awake()
	{
		togglegroup = GetComponent<ToggleGroup>();
		ConnectButton = GameObject.Find("ServerConnectButton").GetComponent<Button>();
		ConnectButton.interactable = false;
	}

	void Update()
	{
		if(matchList.Count > 0)
		{
			GameObject.Find("Content").GetComponent<RectTransform>().sizeDelta = new Vector2(0, (30 * matchList.Count) + 15);

			for(int i = 0; i < matchDisplays.Count; i++)
			{
				string name = matchList[i].name;
				bool hasPassword = matchList[i].isPrivate;
				int currentPlayers = matchList[i].currentSize;
				int maxPlayers = matchList[i].maxSize;
				matchDisplays[i].GetComponent<ServerInfo>().UpdateInfo(name, hasPassword, currentPlayers, maxPlayers);
			}
		}

		if(togglegroup.AnyTogglesOn())
		{
			Toggle[] toggles = GetComponentsInChildren<Toggle>();

			foreach (Toggle t in toggles)
			{
				if(t.isOn)
				{
					ConnectGame = t.gameObject.GetComponent<ServerInfo>().GetMatchInfo();
				}
			}
		}
		else
		{
			ConnectGame = null;
		}

		if(ConnectGame == null)
			ConnectButton.interactable = false;
		else
			ConnectButton.interactable = true;
	}

	public void LoadServerList()
	{
		matchList.Clear();
		foreach(GameObject obj in matchDisplays)
		{
			Destroy(obj);
		}
		matchDisplays.Clear();

		NetworkLobbyManager manager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkLobbyManager>();
		if(manager.matchMaker == null)
			manager.StartMatchMaker();

		NetworkMatch match = manager.matchMaker;

		match.ListMatches(0, 20, "", OnMatchList);
	}

	public void OnMatchList(ListMatchResponse matchListResponse)
	{
		if(matchListResponse.success && matchListResponse.matches.Count > 0)
		{
			matchList = matchListResponse.matches;
			PopulateServerList();
			Debug.Log("MATCHES FOUND");
			GameObject.Find("NoMatchesText").GetComponent<Text>().enabled = false;
		}
		else
		{
			Debug.Log("NO MATCHES FOUND");
			GameObject.Find("NoMatchesText").GetComponent<Text>().enabled = true;
		}
	}

	private void PopulateServerList()
	{
		int counter = 0;
		foreach(MatchDesc match in matchList)
		{
			//Debug.Log("Match Name: " + match.name + "  Players: " + match.currentSize + "/" + match.maxSize);

			GameObject newServer = Instantiate(ServerDisplay);
			newServer.transform.SetParent(transform.Find("Viewport").transform.Find("Content"),false);

			Vector3 newPos = newServer.transform.localPosition;
			newPos.y = -15 + (-30 * (counter));
			newServer.transform.localPosition = newPos;

			newServer.GetComponent<ServerInfo>().SetMatchInfo(match);
			newServer.GetComponent<Toggle>().group = togglegroup;

			matchDisplays.Add(newServer);

			counter++;
		}
	}

	public void CheckForPassword()
	{
		if(ConnectGame.isPrivate)
		{
			GameObject.FindGameObjectWithTag("GameController").GetComponent<ApplicationSetup>().DisableAllMenus();
			GameObject.FindGameObjectWithTag("GameController").GetComponent<ApplicationSetup>().DisplayPasswordMenu();
		}
		else
		{
			string pass = "";
			Connect(pass);
		}
	}

	public void PasswordConnect()
	{
		string pass = GameObject.Find("PasswordMenuInput").transform.Find("Text").GetComponent<Text>().text;

		Connect(pass);
	}

	public void Connect(string pass)
	{
		NetworkLobbyManager manager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkLobbyManager>();;
		NetworkMatch match = manager.GetComponent<NetworkLobbyManager>().matchMaker;
		match.JoinMatch(ConnectGame.networkId, pass, manager.OnMatchJoined);
	}
}
