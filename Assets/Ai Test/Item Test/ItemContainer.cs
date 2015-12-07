using UnityEngine;
using System.Collections;

public class ItemContainer
{	
	Item[] inv;
	int containerId;
	
	int rows = 1;
		
	public ItemContainer(int id,int size = 5)
	{
		containerId = id;
		inv = new Item[size];
		
		int tSize = size;
		while(tSize > 10)
		{
			tSize -= 10;
			rows++;
		}
	}	
	
	public void drawGui()
	{
		int dy = 0;
		int dx = 0;
		
		GUI.DrawTexture(new Rect(30,30,335,(rows*33)+6),ItemUi.grey);
		for(int x = 0;x< inv.Length;x++)
		{
			if(dx/10 == 1)
			{
				dy++;
				dx = 0;
			}
			GUI.DrawTexture(new Rect(((dx+1)*33),(dy+1)*33,32,32),ItemUi.itemSlot);
			
			if(inv[x] != null)
			{
				GUI.DrawTexture(new Rect(((dx+1)*33),(dy+1)*33,32,32),inv[x].getTex());
				GUI.Label(new Rect(((dx+1)*33),(dy+1)*33,32,32),inv[x].getQuan().ToString());
			}
			dx++;
		}
	}
	
	public Item getItemStackAt(int i)
	{
		return inv[i];
	}
	
	//Only use for force setting else use addItem()
	public void setItemAt(int i,Item item,int q)
	{
		Item it = item;
		it.setAmount(q);
		inv[i] = it;
	}	
	
	public int containerSize()
	{
		return inv.Length;
	}
}
