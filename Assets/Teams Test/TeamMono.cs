using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TeamMono : MonoBehaviour {

	public Material torso;
	public Material topArm;
	public Material topLeg;
	
	public Text colorin;
	TeamControl tc;
	
	// Use this for initialization
	void Start () {
		tc = new TeamControl();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void createTeam()
	{
		tc.addTeam(colorin.text);		
		updateMats();
	}
	
	void updateMats()
	{
		torso.color = tc.getTeamAt(tc._currentId).getTeamColor();
		topArm.color = tc.getTeamAt(tc._currentId).getTeamColor();
		topLeg.color  = tc.getTeamAt(tc._currentId).getTeamColor();
	}
}
