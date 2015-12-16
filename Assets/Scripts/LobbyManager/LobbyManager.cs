using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LobbyManager : NetworkLobbyManager {

	List<NetworkConnection> ActiveConnections = new List<NetworkConnection>();
	public List<GameObject> PlayerBanners = new List<GameObject>();

	[SerializeField] private GameObject PlayerBanner;
		
	public override void OnLobbyStartServer ()
	{
		//THIS IS CALLED WHEN A SERVER IS STARTED (BOTH LAN AND MATCHMAKING)
		Debug.Log("SERVER STARTED"); 
		base.OnLobbyStartServer ();
	}

	public override void OnStopServer ()
	{
		//THIS IS CALLED WHEN A SERVER IS STOPPED
		if(GameObject.FindGameObjectWithTag("GameController") != null)
			GameObject.FindGameObjectWithTag("GameController").GetComponent<ApplicationSetup>().EnableMainMenu();
		if(GameObject.Find("LobbyMenu") != null)	
			GameObject.Find("LobbyMenu").GetComponent<Canvas>().enabled = false;
		
		ActiveConnections.Clear();
		PlayerBanners.Clear();
		base.OnStopServer ();
	}

	public override void OnLobbyServerConnect (NetworkConnection conn)
	{
		ActiveConnections.Add(conn);
		//THIS IS CALLED SERVERSIDE WHEN A CLIENT CONNECTS. INCLUDES THE SERVER HOST JOINING THEIR OWN GAME.
		Debug.Log("CLIENT CONNECTED");

		base.OnLobbyServerConnect (conn);
	}

	public override GameObject OnLobbyServerCreateLobbyPlayer (NetworkConnection conn, short playerControllerId)
	{
		//THIS IS CALLED SERVERSIDE WHEN THE LOBBY PLAYER OBJECT IS CREATED
		//TODO: USE TO CREATE ITEMS TO REPRESENT PLAYER IN LOBBY

		GameObject NewBanner = Instantiate(PlayerBanner) as GameObject;
		NewBanner.GetComponent<PlayerBannerScript>().UpdatePosition(PlayerBanners.Count);
		PlayerBanners.Add(NewBanner);

		string playername = "Player " + conn.connectionId;
		NewBanner.GetComponent<PlayerBannerScript>().SetConnectionInfo(playername, conn, playerControllerId);
		NetworkServer.Spawn(NewBanner);
		NewBanner.GetComponent<NetworkIdentity>().AssignClientAuthority(conn);

		Debug.Log(conn.connectionId);

		return base.OnLobbyServerCreateLobbyPlayer (conn, playerControllerId);
	}

	public override void OnLobbyServerPlayerRemoved (NetworkConnection conn, short playerControllerId)
	{

		base.OnLobbyServerPlayerRemoved (conn, playerControllerId);
	}

	public override void OnLobbyServerDisconnect (NetworkConnection conn)
	{
		//THIS IS CALLED SERVERSIDE WHEN A CLIENT DISCONNECTS
		Debug.Log("STEP ONE");
		for(int i = 0; i < ActiveConnections.Count; i++)
		{
			if (ActiveConnections[i] == conn)
			{
				ActiveConnections.RemoveAt(i);
				Debug.Log("ACTIVE CONNECTION TERMINATED");
				break;
			}
		}

		for (int i = 0; i < PlayerBanners.Count; i++)
		{
			if(PlayerBanners[i].GetComponent<PlayerBannerScript>().GetConnectionID() == conn.connectionId)
			{
				PlayerBanners.RemoveAt(i);
				Debug.Log("PLAYER BANNER REMOVED");
				break;
			}
		}

		foreach (GameObject g in PlayerBanners)
		{
			if(g.GetComponent<PlayerBannerScript>().GetConnectionID() == conn.connectionId)
			{
				PlayerBanners.Remove(g);
			}
		}

		for (int i = 0; i < PlayerBanners.Count; i++)
		{
			PlayerBanners[i].GetComponent<PlayerBannerScript>().UpdatePosition(i);
		}


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

	public override void OnLobbyServerSceneChanged (string sceneName)
	{
		foreach (GameObject g in PlayerBanners)
		{
			Destroy(g);
		}

		PlayerBanners.Clear();

		base.OnLobbyServerSceneChanged (sceneName);
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
		if(GameObject.FindGameObjectWithTag("GameController") != null)
			GameObject.FindGameObjectWithTag("GameController").GetComponent<ApplicationSetup>().EnableMainMenu();
		if(GameObject.Find("LobbyMenu") != null)
			GameObject.Find("LobbyMenu").GetComponent<Canvas>().enabled = false;
		base.OnLobbyClientExit ();
	}

	public override void OnClientError (NetworkConnection conn, int errorCode)
	{
		base.OnClientError (conn, errorCode);
	}


}
