using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGiver : MonoBehaviour {
	private delegate void RaycastHitHandler(RaycastHit hit);
	public OrderableAgent selectedAgent=null;
	public Camera camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			MousePick (SelectUnit);
		}
		if(Input.GetMouseButtonUp(1)&&selectedAgent!=null){
			MousePick (OrderMovement);
		}
	}

	private void MousePick(RaycastHitHandler handler){
		Ray ray = camera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit))
			handler(hit);
	}

	private void SelectUnit(RaycastHit hit){
		OrderableAgent newAgent = hit.transform.GetComponent<OrderableAgent> ();
		selectedAgent = newAgent;
	}

	private void OrderMovement(RaycastHit hit){
		if(selectedAgent!=null)
			selectedAgent.MoveTo (hit.point);
	}
}

