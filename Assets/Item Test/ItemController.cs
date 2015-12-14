using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {

	public GameObject canvas;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void newInventory()
	{
		Instantiate(canvas);
	}
}
