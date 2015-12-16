using UnityEngine;
using System.Collections;

public class ItemStack {

	public Item item;
	int quantity;
	
	public ItemStack(Item i, int q)
	{
		item = i;
		quantity = q;
	}
	
	public Item getItem()
	{
		if(item == null)
		{
			return null;
		}
		else
		{
			return item;
		}
	}
}
