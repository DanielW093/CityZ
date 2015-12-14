using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof (Image))]

public class HitMarkerScript : MonoBehaviour {
	
	private Image thisImage; //The hit marker image
	private float startTime; //The time the hit marker was activated
	[SerializeField] private float visibleTime = 0.2f; //The time the hit marker should be visible

	// Use this for initialization
	void Start () {
		thisImage = transform.GetComponent<Image>(); //Get the hit marker image
		thisImage.enabled = true; //Hide the hit marker image
	}
	
	// Update is called once per frame
	void Update () {
		if(thisImage.enabled) //If the hit marker is visible
		{
			if(Time.time - startTime >= visibleTime) //Has the marker been visible for long enough?
			{
				thisImage.enabled = false; //Hide again
			}
		}
	}

	public void ActivateMarker()
	{
		Debug.Log ("HIT MARKER SCRIPT - ACTIVATE HIT MARKER");
		startTime = Time.time; //Set activated time
		thisImage.enabled = true; //Show hit marker
	}
}
