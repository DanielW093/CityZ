using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {
	
	public float speed = 20f;
	int direction;
	public Vector3 dir = new Vector3(0,0,1);
	
	// Use this for initialization
	void Start () 
	{
		direction = Random.Range(0,2);
		if(direction == 0)
		{
			speed = -speed;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(this.gameObject.transform.parent.position, dir, speed * Time.deltaTime);
		
	}
	
	public void setDir(Vector3 d)
	{
		dir = d;
	}
	
	public void setSpeed(float i)
	{
		speed = i;
	}
}
