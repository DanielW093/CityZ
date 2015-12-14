using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class DecalManager : NetworkBehaviour {

	[SerializeField] private int maxBulletHoles = 100; //Max amount of decals that can exist
	[SerializeField] private GameObject bhSprite; //Sprite image for bullet holes

	private List<GameObject> bulletHoles = new List<GameObject>();

	public void CreateBullethole(Vector3 position, Quaternion rotation)
	{
		if(bulletHoles.Count >= maxBulletHoles) //Has the maximum been reached?
		{
			NetworkServer.Destroy (bulletHoles[0]); //Destroy decal on server
			Destroy (bulletHoles[0]); //Destroy object locally
			bulletHoles.RemoveAt (0); //Remove from list
		}

		GameObject newHole = (GameObject)Instantiate (bhSprite, position, rotation); //Create new decal

		float randRot = Random.Range (0, 360);
		float randScale = Random.Range (0, 1);

		newHole.transform.RotateAround(newHole.transform.position, newHole.transform.forward, randRot);

		//TODO: MAKE DECALS APPEAR WITH SIZE VARIATIONS

		newHole.transform.parent = transform; //Set parent
		NetworkServer.Spawn(newHole); //Spawn on server
		bulletHoles.Add (newHole); //Add to array
	}
}
