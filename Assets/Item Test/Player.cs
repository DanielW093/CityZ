using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	ItemContainer inventory;
	
	bool drawGui = false;
	public Player ()
	{
		inventory = new ItemContainer(0,15);
		//inventory.setItemAt(0,Items.medKit,1);
	}	
	
	void Start()
	{		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E))
		{
			drawGui = !drawGui;
		}
		
		if(Input.GetKeyDown(KeyCode.F))
		{
			inventory.setItemAt(0,Items.ammo,50);
		}	
	}
	
	void OnGUI()
	{		
		if(drawGui){inventory.drawGui();}
	}
}

