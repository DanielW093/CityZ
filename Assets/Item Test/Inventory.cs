using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	bool drawGui = false;
	public int size = 5;
	ItemContainer inv;
	
	void Start () {
		inv = new ItemContainer(0,size);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E))
		{
			drawGui = !drawGui;
		}
		
		if(Input.GetKeyDown(KeyCode.F))
		{
			inv.setItemAt(0,Items.ammo,50);
		}	
	}
	
	void OnGUI()
	{		
		if(drawGui){inv.drawGui();}
	}
}
