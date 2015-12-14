using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour{

	public static Item[] items = new Item[4];
	public static Item ammo;
	public static Item medKit;
	public static Item bandage;
	public static Item keyCard;
	
	
	public static void initItems()
	{
		items[0] = ammo = new Item(0,"Ammunition",100,1,ItemUi.ammoGo,ItemUi.itemDefault);
		items[1] = medKit = new Item(1,"Med Kit",2,1,ItemUi.medkitGo,ItemUi.itemDefault);
		items[2] = keyCard = new Item(2,"Key Card",1,1,ItemUi.keycardGo,ItemUi.itemDefault);
		items[3] = bandage = new Item(3,"Bandage",4,1,ItemUi.bandageGo,ItemUi.itemDefault);
		
	}
}
