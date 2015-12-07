using UnityEngine;
using System.Collections;

public class Team {
	
	int _id;
	string _hexColor = "#000000";
	Color _color;
	
	public Team(int id, string color)
	{
		_id = id;
		_hexColor = color;
		_color = HexToColor(color);
	}
	
	
	//Functions to change the hex code to color
		
	Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		Debug.Log(r + " " + g + " " + b);
		return new Color32(r,g,b, 255);
	}
		
	//Getters and setters
	public int getId() { return _id; }
	public string getHexCode() { return _hexColor;}
	public Color getTeamColor(){ return _color;}
}
