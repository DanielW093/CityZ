using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WeaponScript : NetworkBehaviour {

	[SyncVar] private bool hasGun; //Does the player have a gun?

	[SerializeField] private GameObject gunShot; //Gunshot Prefab
	
	[SerializeField] private bool isAutomatic = true;
	[SerializeField] private float fireDelay;
	[SerializeField] private int shotDamage = 2;
	[SerializeField] private float shotVariation = 0.01f;
		
	private float lastFireTime;
	private float projectileRange = 1000f;

	private Transform firePoint;
	private Transform playerCam;
	private GameObject currentWeapon;
	private DecalManager decalManager;
	[SerializeField] private GameObject tracer;
	
	
	private HitMarkerScript hitMarker;

	// Use this for initialization
	void Start () {
		lastFireTime = Time.time;

		playerCam = transform.GetComponentInChildren<Camera>().transform;
		hitMarker = GameObject.FindGameObjectWithTag("ClientUI").transform.Find ("HitMarker").GetComponent<HitMarkerScript>();

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

		if(decalManager == null)
		{
			if(GameObject.Find("DecalManager") != null)
				decalManager = GameObject.Find("DecalManager").GetComponent<DecalManager> ();
		}

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
						Fire();
					}
				}
				else
				{
					//Automatic Guns
					if(Input.GetMouseButtonDown (0) && Time.time - lastFireTime > fireDelay)
					{
						Fire();
					}
				}
			}
		}
	}

	void Fire()
	{
		float spreadX = 0; //Random.Range (-shotVariation, shotVariation);
		float spreadY = 0; //Random.Range (-shotVariation, shotVariation);

		//TODO: Calculate a hit circle based on spread and distance between player and target?

		Vector3 origin = playerCam.position;
		Vector3 direction = playerCam.forward;

		direction.x += spreadX; direction.y += spreadY;

		RaycastHit hit;

		if (Physics.Raycast (origin,direction, out hit, projectileRange)) {
			if (hit.collider != null)
			{
				if(hit.collider.gameObject.transform.root.CompareTag ("Player"))
				{
					Debug.Log ("HIT PLAYER - CLIENT SIDE");
					if(hit.collider.gameObject.transform.root.GetComponent<PlayerMovement>().isAlive)
						hitMarker.ActivateMarker();
				}
			}
		}
		
		CmdFire (origin, direction);

		//Important for fire delay
		lastFireTime = Time.time;
	}

	[Command] void CmdFire(Vector3 origin, Vector3 direction)
	{
		Ray ray = new Ray (origin, direction);
		RaycastHit hit;

		Vector3 hitPoint = Vector3.zero;

		if (Physics.Raycast (ray, out hit, projectileRange)) {
			if (hit.collider != null) {
				hitPoint = hit.point;
				GameObject hitObject = hit.collider.gameObject;
				if (hitObject.transform.root != null && hitObject.transform.root.CompareTag ("Player")) {
					if(hit.collider.gameObject.transform.root.GetComponent<PlayerMovement>().isAlive)
					{
						hitObject.transform.root.GetComponent<PlayerMovement> ().TakeDamage (-shotDamage);
						Debug.Log ("HIT PLAYER - SERVER SIDE");
					}
				} else {
					Quaternion hitRotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
					Vector3 holePoint = hit.point + hit.normal * 0.01f;
					decalManager.CreateBullethole(holePoint, hitRotation);
					Debug.Log ("Shot hit " + hitObject.name);
				}
			}
		} else {
			hitPoint = ray.GetPoint (projectileRange);
			Debug.Log ("SHOT HIT NOTHING");
		}
		
		GameObject tempShot = Instantiate(gunShot,firePoint.position,Quaternion.identity) as GameObject;
		tempShot.GetComponent<AudioSource>().Play();

		//GameObject thisTrace = (GameObject)Instantiate (tracer, firePoint.position, Quaternion.identity);
		//thisTrace.GetComponent<TracerScript> ().InitDirection (hitPoint);
		//NetworkServer.Spawn (thisTrace);

		//DEBUG ONLY
		Vector3 rayLength = direction * projectileRange;
		Debug.DrawRay (origin, rayLength, Color.green, fireDelay);
		////////////
	}
}
