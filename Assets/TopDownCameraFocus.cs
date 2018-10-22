using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraFocus : MonoBehaviour {
	public float panSpeed=2f;
	public Transform camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			transform.position -= (transform.right*panSpeed*Time.deltaTime)*camera.localPosition.magnitude;
		}
		if (Input.GetKey (KeyCode.W)) {
			transform.position += (transform.forward*panSpeed*Time.deltaTime)*camera.localPosition.magnitude;
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.position -= (transform.forward*panSpeed*Time.deltaTime)*camera.localPosition.magnitude;
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.position += (transform.right*panSpeed*Time.deltaTime)*camera.localPosition.magnitude;
		}
	}
}
