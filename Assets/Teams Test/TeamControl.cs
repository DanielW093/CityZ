using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamControl {

	public int _currentId = 0;
	List<Team> _teams = new List<Team>();
		
	public void addTeam(string hc)
	{		
		//_currentId++;
		_teams.Add(new Team(_currentId,hc));
	}
	
	public void removeTeam(Team t)
	{
		_teams.Remove(t);
	}
	
	public Team getTeamAt(int i){ return _teams[i];}
}
