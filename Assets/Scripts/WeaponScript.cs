using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WeaponScript : NetworkBehaviour {

	[SyncVar] private bool hasGun; //Does the player have a gun?

	public float fireDelay = 0.2f;
	public bool isAutomatic;
	private float lastFireTime;
	private float projectileRange = 1000f;
	private int shotDamage = 10;

	private Transform firePoint;
	private Transform playerCam;
	private GameObject currentWeapon;
	[SerializeField] private GameObject tracer;

	// Use this for initialization
	void Start () {
		lastFireTime = Time.time;

		playerCam = transform.GetComponentInChildren<Camera>().transform;

		foreach(Transform tr in playerCam.transform)
		{
			if(tr.tag == "Weapon")
			{
				currentWeapon = tr.gameObject;
				firePoint = currentWeapon.transform.Find("FirePoint").transform;
				hasGun = true;
				return;
			}
		}
		Debug.Log ("No weapon found");
	}
	
	// Update is called once per frame
	void Update () {
		if(isLocalPlayer)
		{
			//INPUTS MUST GO HERE
			if(hasGun)
			{
				if(isAutomatic)
				{
					//Automatic Guns
					if(Input.GetMouseButton (0) && Time.time - lastFireTime > fireDelay)
					{
						CmdFire ();
					}
				}
				else
				{
					//Automatic Guns
					if(Input.GetMouseButtonDown (0) && Time.time - lastFireTime > fireDelay)
					{
						CmdFire ();
					}
				}
			}
		}
	}

	[Command] void CmdFire()
	{
		//Debug.Log ("FIRE");
		Vector3 origin = playerCam.position;
		Vector3 direction = playerCam.forward; //TODO: ADD SPREAD TO GIVE VARIANCE TO SHOOTING
		Ray ray = new Ray (origin, direction);
		RaycastHit hit;

		Vector3 hitPoint = Vector3.zero;

		if (Physics.Raycast (ray, out hit, projectileRange)) {
			if (hit.collider != null) {
				hitPoint = hit.point;
				GameObject hitObject = hit.collider.gameObject;
				if (hitObject.transform.parent != null && hitObject.transform.parent.CompareTag ("Player")) {
					hitObject.transform.parent.GetComponent<PlayerMovement> ().ChangeHealth (-shotDamage);
					Debug.Log ("HIT PLAYER");
				} else {
					Debug.Log ("Shot hit " + hitObject.name);
				}
			}
		} else {
			hitPoint = ray.GetPoint (projectileRange);
			Debug.Log ("SHOT HIT NOTHING");
		}

		GameObject thisTrace = (GameObject)Instantiate (tracer, firePoint.position, Quaternion.identity);
		thisTrace.GetComponent<TracerScript> ().InitDirection (hitPoint);
		NetworkServer.Spawn (thisTrace);

		//DEBUG ONLY
		Vector3 rayLength = direction * projectileRange;
		Debug.DrawRay (origin, rayLength, Color.green, fireDelay);
		////////////

		//Important for fire delay
		lastFireTime = Time.time;
	}
}
