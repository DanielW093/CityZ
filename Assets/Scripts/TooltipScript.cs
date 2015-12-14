using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TooltipScript : MonoBehaviour {

	Canvas tooltipCanvas;
	Toggle toggle;

	// Use this for initialization
	void Start () {
		tooltipCanvas = transform.Find("Canvas").GetComponent<Canvas>();
		tooltipCanvas.enabled = false;

		toggle = GetComponent<Toggle>();
	}

	void Update()
	{
		if(toggle.isOn != tooltipCanvas.enabled)
		{
			tooltipCanvas.enabled = toggle.isOn;
		}
	}
}