using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class PlayerBannerScript : NetworkBehaviour {

	[SyncVar] private int connectionID;
	[SyncVar] private short playerControllerId;
	[SyncVar] private string PlayerName;
	[SyncVar] private int LobbyPos;

	[SyncVar] private GameObject player;
	private Toggle readyToggle;

	public int GetConnectionID()
	{
		return connectionID;
	}

	public short GetPlayerControllerId()
	{
		return playerControllerId;
	}

	void Start()
	{
		readyToggle = GetComponentInChildren<Toggle>();
	}

	void Update()
	{
		if(player == null && GetComponent<NetworkIdentity>().clientAuthorityOwner != null)
		{
			GameObject[] LobbyPlayers = GameObject.FindGameObjectsWithTag("LobbyPlayer");

			foreach(GameObject p in LobbyPlayers)
			{
				if(p.GetComponent<NetworkLobbyPlayer>().playerControllerId == playerControllerId)
				{
					player = p;
					Debug.Log(p);
				}
			}
		}

		if(player != null)
		{
			SetElements();

			if(player.GetComponent<NetworkLobbyPlayer>().isLocalPlayer)
			{
				if(readyToggle.isOn && !player.GetComponent<NetworkLobbyPlayer>().readyToBegin)
					player.GetComponent<NetworkLobbyPlayer>().SendReadyToBeginMessage();
				else if(!readyToggle.isOn && player.GetComponent<NetworkLobbyPlayer>().readyToBegin)
					player.GetComponent<NetworkLobbyPlayer>().SendNotReadyToBeginMessage();
			}

			if(!player.GetComponent<NetworkLobbyPlayer>().isLocalPlayer)
			{	
				if(player.GetComponent<NetworkLobbyPlayer>().readyToBegin)
					readyToggle.isOn = true;
				else
					readyToggle.isOn = false;
			}
		}

		float ypos = 0;
		if(LobbyPos < 8)
		{
			transform.SetParent(GameObject.Find("LeftPanel").transform, false);
			ypos = 87.5f-25f*LobbyPos;
		}
		else
		{
			transform.SetParent(GameObject.Find("RightPanel").transform, false);
			ypos = 87.5f-25f*(LobbyPos-8);
		}

		Vector3 pos = transform.localPosition;
		pos.x = 0;
		pos.y = ypos;
		transform.localPosition = pos;

		transform.Find("PlayerName").GetComponent<Text>().text = PlayerName;
	}

	public void SetConnectionInfo(string playername, NetworkConnection conn, short playerId)
	{
		connectionID = conn.connectionId;
		PlayerName = playername;
					playerControllerId = playerId;
	}

	public void UpdatePosition(int pos)
	{
		LobbyPos = pos;
		Debug.Log("LOBBY POSITION: " + LobbyPos);
	}

	public void SetElements()
	{
		if(!player.GetComponent<NetworkLobbyPlayer>().isLocalPlayer)
		{
			transform.Find("TeamSelect").GetComponent<Dropdown>().interactable = false;
			transform.Find("IsReady").GetComponent<Toggle>().interactable = false;
		}
		else
		{
			transform.Find("TeamSelect").GetComponent<Dropdown>().interactable = true;
			transform.Find("IsReady").GetComponent<Toggle>().interactable = true;
		}
	}
}
