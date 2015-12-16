using UnityEngine;
using System.Collections;

public class navChar : MonoBehaviour {

	NavMeshAgent agent;
	public GameObject gunAudio;
	
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(1))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit,100))
			{
				agent.SetDestination(hit.point);
			} 
		}
		
		if(Input.GetKeyDown(KeyCode.Q))
		{
			GameObject ga = (GameObject)Instantiate(gunAudio,this.transform.position,Quaternion.identity);
			ga.GetComponent<AudioSource>().Play();
		}
	}
}
