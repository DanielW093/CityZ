using UnityEngine;
using System.Collections;

public class ZomibeLOS : MonoBehaviour {


	public float fieldOfViewAngle = 110f;
	public bool playerInSight;
	public Vector3 lastSighting;
	
	private NavMeshAgent nav;                       
	private SphereCollider col;
	private GameObject player;                     
	private Vector3 previousSighting;               
	
	
	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent>();
		col = GetComponent<SphereCollider>();
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{		
		nav.SetDestination(lastSighting);
	}
	
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject == player)
		{
			playerInSight = false;
			
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			
			if(angle < fieldOfViewAngle * 0.5f)
			{
				RaycastHit hit;
				if(Physics.Raycast(transform.position,direction.normalized,out hit, col.radius))
				{
					if(hit.collider.gameObject == player)
					{
						playerInSight = true;
						lastSighting = player.transform.position;
					}
				}
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject == player)
		{
			playerInSight = false;
		}
	}
}
