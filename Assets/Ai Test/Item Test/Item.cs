using UnityEngine;
using System.Collections;

public class Item
{
	int _id;
	string _name;
	public Texture _tex;
	int maxStack,quantity;
	GameObject obj;
	
	public Item(int id
	,string name = "Unkown Item",int ms = 10,int q = 1,
	GameObject gO = null,Texture tex = null)
	{
		_id = id;
		_name = name;
		maxStack = ms;
		quantity = q;
		
		if(tex != null)
		{
			_tex = tex;
		}
		else
		{
			_tex = ItemUi.itemDefault;
		}
		
		if(gO != null)
		{
			obj = gO;
		}
		else
		{
			obj = gO;
		}
		Debug.Log("Item: " + name + " Created with id of: "+ _id);
	}
	
	public Texture getTex()
	{
		return _tex;
	}
	
	public int getMaxStack()
	{
		return maxStack;
	}
	
	public int getQuan()
	{
		return quantity;
	}
	
	public void setAmount(int i)
	{
		quantity = i;	
	}

	public bool pickup()
	{
		if(quantity < maxStack)
		{
			quantity++;
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public int getId(){return _id;}
	public string getName(){ return _name;}
	
	public GameObject getGameObject(){return obj;}
	
	public void setGameObject(GameObject gO)
	{
		obj = gO;
	}
	
	public void useItem()
	{
		
	}
	
}
