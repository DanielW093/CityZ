using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	public GameObject[] items;
	
	// Use this for initialization
	void Start ()
	{
		int its = Random.Range(0,items.Length);
		Instantiate(items[its],this.gameObject.transform.position,Quaternion.identity);
	}
}
