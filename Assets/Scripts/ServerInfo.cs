using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using System.Collections;

public class ServerInfo : MonoBehaviour {

	[SerializeField] private MatchDesc matchInfo;

	// Use this for initialization
	public void UpdateInfo (string name, bool hasPassword, int currentPlayers, int maxPlayers) {
		transform.Find("MatchName").GetComponent<Text>().text = name;
		transform.Find("HasPassword").GetComponent<Image>().enabled = hasPassword;
		transform.Find("ConnectedPlayers").GetComponent<Text>().text = currentPlayers + "/" + maxPlayers;
	}

	public void SetMatchInfo(MatchDesc m)
	{
		matchInfo = m;
	}

	public MatchDesc GetMatchInfo()
	{
		return matchInfo;
	}
}
