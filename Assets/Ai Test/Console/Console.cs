using UnityEngine;
using System.Collections;

public class Console : MonoBehaviour {

	char[] breakChars = {'(',')',','};
	bool shouldShow = false;
	string textField= "";
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.BackQuote))
		{
			shouldShow = !shouldShow;
		}			
	}
	
	void OnGUI()
	{
		if(shouldShow)
		{
			if (Event.current.isKey) 
			{
				if (Event.current.keyCode == KeyCode.Return)
				{
					Debug.Log("Command Entered: " + textField);
					findCommand(textField);
					shouldShow = false;
					textField = "";
				}
			}
			textField = GUI.TextField(new Rect(0,0,Screen.width,20),textField,99);
		}
	}
	
	void findCommand(string s)
	{
		string[] sin = s.Split(breakChars);
		if(sin[0] == "additem")
		{
			if(sin[1] != null && sin[2] != null){ cmAddItem(int.Parse(sin[1]),int.Parse(sin[2]));}
		}
	}
	
	
	void cmAddItem(int id,int q)
	{
		foreach(Item i in Items.items)
		{
			if(i.getId() == id)
			{
				Debug.Log("Adding Item: " + i.getName() + "x" + q);
			}
		}
	}
}
