using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour{

	public static Item[] items = new Item[2];
	public static Item ammo;
	public static Item medKit;
	
	public static void initItems()
	{
		items[0] = ammo = new Item(0,"Ammunition",100);
		items[1] = medKit = new Item(1,"Med Kit");
	}
}
