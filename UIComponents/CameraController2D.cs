using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2D : MonoBehaviour {
	public Camera _camera;
	private Vector3 _velocity = new Vector3 ();
	private float _panAccelerationSpeed=12f;
	private float _zoomAccelerationSpeed=4f;
	private float _dampening=.005f;


	public void Reset(){
		_camera = gameObject.GetComponent<Camera> ();
		if (_camera == null)
			gameObject.SetActive (false);
	}

	public void Update(){	
		Vector3 acceleration = HandleInput ();
		acceleration *= Time.deltaTime;
		Accelerate (acceleration);
		MoveInDirection (_velocity*Time.deltaTime);
	}

	public Vector3 HandleInput(){
		Vector3 deltaV = new Vector3 ();
		if (Input.GetKey (KeyCode.W))
			deltaV.y += _panAccelerationSpeed;
		if (Input.GetKey (KeyCode.S))
			deltaV.y -= _panAccelerationSpeed;
		if (Input.GetKey (KeyCode.D))
			deltaV.x += _panAccelerationSpeed;
		if (Input.GetKey (KeyCode.A))
			deltaV.x -= _panAccelerationSpeed;		
		if (Input.GetKey (KeyCode.E))
			deltaV.z -= _zoomAccelerationSpeed;
		if (Input.GetKey (KeyCode.Q))
			deltaV.z += _zoomAccelerationSpeed;
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
			deltaV *= 5f;
		return deltaV;
	}

	public void Accelerate(Vector3 acceleration){
		_velocity *= (float) Math.Pow(_dampening, Time.deltaTime);
		_velocity += acceleration;
	}

	public void MoveInDirection(Vector3 direction){
		_camera.orthographicSize += direction.z*_camera.orthographicSize;
		var pan = new Vector3 (direction.x, direction.y, 0);
		transform.position += pan*_camera.orthographicSize;

	}
}
