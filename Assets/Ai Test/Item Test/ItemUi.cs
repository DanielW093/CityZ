using UnityEngine;
using System.Collections;

public class ItemUi : MonoBehaviour {

	public static Texture itemSlot;
	public static Texture itemDefault;
	public static Texture2D grey;
	
	
	// Use this for initialization
	void Start () {
		itemSlot = Resources.Load("itemTile") as Texture;
		itemDefault = Resources.Load("ItemDefault") as Texture;
		
		grey = new Texture2D(1,1);
		grey.SetPixel(0,0,Color.grey);
		grey.Apply();
		
		Items.initItems();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI()
	{
		
	}
		
	public Texture getItemSlot()
	{
		return itemSlot;
	}
}
