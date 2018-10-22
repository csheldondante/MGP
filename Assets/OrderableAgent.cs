using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]
public class OrderableAgent : MonoBehaviour {
	public NavMeshAgent agent;
	public SphereCollider interactionCollider;
	public HashSet<GameObject> nearbyStuff = new HashSet<GameObject> ();
	public Unit unit;


	// Use this for initialization
	void Start () {
		unit = new Unit ();
	}

	void Reset(){
		agent = gameObject.GetComponent<NavMeshAgent> ();
		interactionCollider = gameObject.GetComponent<SphereCollider> ();
		MeshRenderer renderer = gameObject.GetComponent<MeshRenderer> ();
		renderer.sharedMaterial.color = Color.red;
	}


	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.L))
			foreach (var obj in nearbyStuff)
				Debug.Log (gameObject.name+":"+obj.name);
	}

	public void MoveTo(Vector3 position){
		agent.destination = position;
		agent.isStopped = false;	
	}

	private void OnTriggerEnter(Collider other){
		nearbyStuff.Add (other.gameObject);
	}

	private void OnTriggerExit(Collider other){
		nearbyStuff.Remove (other.gameObject);
	}
}
