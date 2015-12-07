using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Camera))]
[RequireComponent(typeof (AudioListener))]

public class CameraManagement : MonoBehaviour {

	Camera cam;
	AudioListener aud;
	
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
		aud = GetComponent<AudioListener>();
	}

	void MakeActive()
	{
		cam.enabled = true;
		aud.enabled = true;
	}

	void MakeInactive()
	{
		cam.enabled = false;
		aud.enabled = false;
	}
}
