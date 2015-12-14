using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class zombieAi : NetworkBehaviour {

	NavMeshAgent agent;
	[SerializeField] const float maxSightDist = 100f;
	[SerializeField] const float maxHeardDist = 1000f;
	[SerializeField] const float maxWanderDist = 5f;
	
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination (transform.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");		
		float seenDist = maxSightDist;
		GameObject seenPlayer = null;
		
		foreach(GameObject p in Players)
		{
			Ray ray = new Ray(this.transform.position,
			                  p.transform.position - this.transform.position);
			Debug.DrawRay(ray.origin,p.transform.position - this.transform.position);
			RaycastHit hit;
			Physics.Raycast(ray,out hit,maxSightDist);
			
			if(hit.transform != null)
			{
				if(hit.transform.root.tag == "Player")
				{	
					if(Vector3.Distance(p.transform.position, this.transform.position) < seenDist)
					{
						seenPlayer = p;
						seenDist = Vector3.Distance(p.transform.position, this.transform.position);
					}
				}
			}
		}
		
		GameObject[] shots = GameObject.FindGameObjectsWithTag("gunShot");	
		float heardDist = maxHeardDist;
		GameObject heardShot = null;
		
		foreach(GameObject s in shots)
		{
			if(Vector3.Distance(s.transform.position, this.transform.position) < heardDist)
			{
				heardShot = s;
				heardDist = Vector3.Distance(s.transform.position, this.transform.position);
			}
		}
		
		if(heardShot != null && seenPlayer == null)
		{
			Ray ray = new Ray(this.transform.position,
			                  heardShot.transform.position - this.transform.position);
			Debug.DrawRay(ray.origin,heardShot.transform.position - this.transform.position);
			RaycastHit hit;
			Physics.Raycast(ray,out hit,maxHeardDist);	
		
			agent.SetDestination(hit.transform.position);
		}
		
		if(seenPlayer != null)
		{
			agent.SetDestination(seenPlayer.transform.position);
		}
		
		if(heardShot == null && seenPlayer == null && !agent.hasPath)
		{
			Debug.Log ("WANDER");
			float angle = Random.Range (-45, 46);
			
			transform.Rotate(Vector3.up, angle);
			
			Vector3 dir = transform.forward;
			
			Ray ray = new Ray(this.transform.position, dir);
			Debug.DrawRay (ray.origin,dir, Color.black);
			RaycastHit hit;
			Physics.Raycast (ray,out hit,maxWanderDist);
			
			if(hit.collider != null)
				agent.SetDestination (hit.point);
			else
				agent.SetDestination (ray.GetPoint(maxWanderDist));
		}
		
	}
}
