using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour {
	public List<Transform> terrainPrefabs = new List<Transform>();
	private List<Transform> instantiatedObjects = new List<Transform>();
	public NavMeshSurface navigationSurface;

	private float elapsedTime = 0f;
	private int size=10;

	// Use this for initialization
	void Start () {
		InstantiateInactiveObjects (Mathf.CeilToInt(size*size*4*.05f));
		shufflePositions ();
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if (elapsedTime > 2f) {
			InstantiateInactiveObjects (5);
			//shufflePositions ();
			elapsedTime = 0f;
		}
	}

	private void InstantiateInactiveObjects(int number){
		for (int i = 0; i < number; ++i) {
			Transform prefab = terrainPrefabs [Random.Range (0, terrainPrefabs.Count)];
			instantiatedObjects.Add(GameObject.Instantiate (prefab,  new Vector3 (Random.Range (-size, size) + .5f, .5f, Random.Range (-size, size) + .5f), Quaternion.identity));
		}
		navigationSurface.BuildNavMesh ();
	}

	private void shufflePositions(){
		for (int i = 0; i < instantiatedObjects.Count; ++i) {
			//instantiatedObjects [i].gameObject.SetActive (false);
			instantiatedObjects [i].position = new Vector3 (Random.Range (-size, size) + .5f, .5f, Random.Range (-size, size) + .5f);
			//instantiatedObjects [i].gameObject.SetActive (true);
		}
		navigationSurface.BuildNavMesh ();
	}
}
