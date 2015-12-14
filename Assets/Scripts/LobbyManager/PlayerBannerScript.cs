using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class PlayerBannerScript : NetworkBehaviour {

	[SyncVar] private int connectionID;
	[SyncVar] private string PlayerName;

	void Start()
	{
		if(!isServer)
		{
			//Is it mine?
//			if(connectionID != connectionToClient.connectionId)
//			{
//				transform.Find("TeamSelect").GetComponent<Dropdown>().interactable = false;
//				transform.Find("IsReady").GetComponent<Toggle>().interactable = false;
//			}
//			else
//			{
//				transform.Find("TeamSelect").GetComponent<Dropdown>().interactable = true;
//				transform.Find("IsReady").GetComponent<Toggle>().interactable = true;
//			}
				
			Reposition();
		}
	}

	public void SetConnectionInfo(string playername, NetworkConnection conn)
	{
		connectionID = conn.connectionId;
		PlayerName = playername;
		transform.Find("PlayerName").GetComponent<Text>().text = PlayerName;
	}

	public void SetElements()
	{
		if(GetComponent<NetworkIdentity>().clientAuthorityOwner.connectionId != NetworkServer.localConnections[0].connectionId)
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

	public void Reposition()
	{
		Debug.Log("Connection ID: " + connectionID);

		transform.SetParent(GameObject.Find("LeftPanel").transform, false);

		Vector3 pos = transform.localPosition;
		pos.x = 0;
		pos.y = 72.5f-25f*connectionID;

		transform.localPosition = pos;
		transform.Find("PlayerName").GetComponent<Text>().text = PlayerName;
	}
}
