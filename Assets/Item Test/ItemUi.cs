using UnityEngine;
using System.Collections;

public class ItemUi : MonoBehaviour {

	public static Texture itemSlot;
	public static Texture itemDefault;
	public static Texture2D grey;
		
	public static GameObject ammoGo;
	public static GameObject medkitGo;
	public static GameObject keycardGo;
	public static GameObject bandageGo;
	
	// Use this for initialization
	void Start () {
		itemSlot = Resources.Load("itemTile") as Texture;
		itemDefault = Resources.Load("ItemDefault") as Texture;
		
		ammoGo = Resources.Load("ammo") as GameObject;
		
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
