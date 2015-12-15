using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LobbyManager : NetworkLobbyManager {

	List<NetworkConnection> ActiveConnections = new List<NetworkConnection>();

	[SerializeField] private GameObject PlayerBanner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(ActiveConnections.Count > 0)
		{
			//Debug.Log(ActiveConnections);
		}
	}
		
	public override void OnLobbyStartServer ()
	{
		//THIS IS CALLED WHEN A SERVER IS STARTED (BOTH LAN AND MATCHMAKING)
		Debug.Log("SERVER STARTED"); 
		base.OnLobbyStartServer ();
	}

	public override void OnStopServer ()
	{
		//THIS IS CALLED WHEN A SERVER IS STOPPED
		GameObject.FindGameObjectWithTag("GameController").GetComponent<ApplicationSetup>().EnableMainMenu();
		GameObject.Find("LobbyMenu").GetComponent<Canvas>().enabled = false;
		ActiveConnections.Clear();
		base.OnStopServer ();
	}

	public override void OnLobbyServerConnect (NetworkConnection conn)
	{
		//THIS IS CALLED SERVERSIDE WHEN A CLIENT CONNECTS. INCLUDES THE SERVER HOST JOINING THEIR OWN GAME.
		ActiveConnections.Add(conn);
		Debug.Log("CLIENT CONNECTED");
		base.OnLobbyServerConnect (conn);
	}

	public override GameObject OnLobbyServerCreateLobbyPlayer (NetworkConnection conn, short playerControllerId)
	{
		//THIS IS CALLED SERVERSIDE WHEN THE LOBBY PLAYER OBJECT IS CREATED
		//TODO: USE TO CREATE ITEMS TO REPRESENT PLAYER IN LOBBY
		GameObject NewBanner = Instantiate(PlayerBanner) as GameObject;

		NewBanner.transform.SetParent(GameObject.Find("LeftPanel").transform, false);
		Vector3 pos = NewBanner.transform.localPosition;
		pos.y -= 25f*conn.connectionId;
		NewBanner.transform.localPosition = pos;

		string playername = "Player " + conn.connectionId;
		NewBanner.GetComponent<PlayerBannerScript>().SetConnectionInfo(playername, conn);
		NetworkServer.Spawn(NewBanner);
		NewBanner.GetComponent<NetworkIdentity>().AssignClientAuthority(conn);
		NewBanner.GetComponent<PlayerBannerScript>().SetElements();

		Debug.Log(conn.connectionId);

		return base.OnLobbyServerCreateLobbyPlayer (conn, playerControllerId);
	}

	public override void OnLobbyServerDisconnect (NetworkConnection conn)
	{
		//THIS IS CALLED SERVERSIDE WHEN A CLIENT DISCONNECTS

//		for(int i = 0; i < ActiveConnections.Count; i++)
//		{
//			if (ActiveConnections[i] == conn)
//				ActiveConnections.RemoveAt(i);
//		}

		Debug.Log("CLIENT DISCONNECTED");
		base.OnLobbyServerDisconnect (conn);
	}

	public override void OnLobbyServerPlayersReady ()
	{
		//THIS IS CALLED SERVERSIDE WHEN ALL PLAYERS ARE READY
		//TODO: MATCH COUNTDOWN
		base.OnLobbyServerPlayersReady ();
	}

	public override void OnServerError (NetworkConnection conn, int errorCode)
	{
		//THIS IS CALLED SERVERSIDE WHEN A CLIENT HAS A CONNECTION ERROR
		//TODO: SERVERSIDE CONNECTION ERRORS
		base.OnServerError (conn, errorCode);
	}

	public override void OnLobbyClientConnect (NetworkConnection conn)
	{


		//THIS IS CALLED CLIENTSIDE WHEN SUCCESSFULLY CONNECTING TO A LOBBY
		base.OnLobbyClientConnect (conn);
	}

	public override void OnLobbyClientDisconnect (NetworkConnection conn)
	{
		//THIS IS CALLED CLIENTSIDE WHEN DISCONNECTING FROM A LOBBY
		base.OnLobbyClientDisconnect (conn);
	}

	public override void OnLobbyClientEnter ()
	{
		//THIS IS CALLED CLIENTSIDE WHEN ENTERING A LOBBY
		//TODO: CUSTOM BEHAVIOUR CAN GO HERE
		Debug.Log("ENTERING LOBBY");

		GameObject.FindGameObjectWithTag("GameController").GetComponent<ApplicationSetup>().DisableAllMenus();
		GameObject.Find("LobbyMenu").GetComponent<Canvas>().enabled = true;

		base.OnLobbyClientEnter ();
	}

	public override void OnLobbyClientExit ()
	{
		//THIS IS CALLED CLIENTSIDE WHEN EXITING A LOBBY
		//TODO: CUSTOM BEHAVIOUR CAN GO HERE
		GameObject.FindGameObjectWithTag("GameController").GetComponent<ApplicationSetup>().EnableMainMenu();
		GameObject.Find("LobbyMenu").GetComponent<Canvas>().enabled = false;
		base.OnLobbyClientExit ();
	}

	public override void OnClientError (NetworkConnection conn, int errorCode)
	{
		base.OnClientError (conn, errorCode);
	}
}
