using UnityEngine;
using System.Collections;

public class zombieAi : MonoBehaviour {

	NavMeshAgent agent;
	public Transform target;
	public Transform player;
	public GameObject playerShadow;
	
	GameObject pS = null;
	bool seenPlayer = false;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Ray ray = new Ray(this.transform.position,
				player.position - this.transform.position);
		Debug.DrawRay(ray.origin,player.position - this.transform.position);
		RaycastHit hit;
		Physics.Raycast(ray,out hit,100);
		
		if(hit.transform.tag == "Player")
		{	
			if(seenPlayer)
			{
				pS.transform.position = player.position;
			}
			else
			{
				seenPlayer = true;
				pS = (GameObject)Instantiate(playerShadow,hit.point,Quaternion.identity);					
			}
			
			agent.SetDestination(pS.transform.position);
		}
		
		GameObject[] gunshots = GameObject.FindGameObjectsWithTag("gunShot");
		
		foreach(GameObject g in gunshots)
		{
			if(Vector3.Distance(g.transform.position,this.gameObject.transform.position) < g.GetComponent<AudioSource>().maxDistance
					&& !seenPlayer)
			{			
				if(pS!=null)
				{
					Destroy(pS);
				}
				pS = (GameObject)Instantiate(playerShadow,g.transform.position,Quaternion.identity);
			}
			
			agent.SetDestination(pS.transform.position);
		}
		
	}
}
