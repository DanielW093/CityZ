using UnityEngine;
using System.Collections;

public class ApplicationSetup : MonoBehaviour {

	[SerializeField] private GameObject NetworkManager;

	// Use this for initialization
	void Start () {
		if (GameObject.FindGameObjectWithTag ("NetworkManager") == null)
			Instantiate (NetworkManager);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
