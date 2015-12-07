using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TracerScript : NetworkBehaviour {

	[SerializeField] private float speed = 1.0f;
	
	[SyncVar] private Vector3 direction;
	[SyncVar] private float startTime;
	public float maxLifetime = 5.0f;

	void Start()
	{
		startTime = Time.time;
	}

	// Update is called once per frame
	void Update () {

		Vector3 newPos = transform.position;

		newPos += speed * direction * Time.deltaTime;

		transform.position = newPos;

		if (Time.time - startTime >= maxLifetime) {
			Destroy(gameObject);
		}
	}

	public void InitDirection(Vector3 endPos)
	{
		direction = (endPos - transform.position).normalized;
	}

	void OnCollisionEnter(Collision collision) 
	{
		Debug.Log ("COLLIDE");
		Destroy (gameObject);
	}

}
