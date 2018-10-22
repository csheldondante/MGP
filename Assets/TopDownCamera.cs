using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour {
	public float minZoom=2f;
	public float maxZoom=100f;
	public float zoomSpeed=50f;
	public float desiredDistance = 20f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		desiredDistance -= Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed * transform.localPosition.magnitude * Time.deltaTime;
		desiredDistance = Mathf.Min (desiredDistance, maxZoom);
		desiredDistance = Mathf.Max (desiredDistance, minZoom);
		transform.localPosition = transform.localPosition.normalized * desiredDistance;
	}
}
