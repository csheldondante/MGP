using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the Main unity component that sets up the game startup scene and intializes the startup routine.
/// </summary>
public class Main : MonoBehaviour {
	public GameObject _splashScreen;
	public LoadingBar _loadingBar;
	public WorldRenderer2D _renderer;

	// Use this for initialization
	void Start () {
		InitializeComponents ();
		InitializeStartupPrefs ();
		InitializeUserPrefs ();
		MultiLevelVoxelMap2D map = VoxelMapGenerator.GenerateMap(56, 56, 1, UnityEngine.Random.Range(int.MinValue, int.MaxValue));
		_renderer.InstantiateMap (map);



		//Load unity asset bundles for initial menu scene in asynch thread
		//Load unity asset bundles for main game in asynch thread
	}

	void Reset(){
		if (_loadingBar == null || _splashScreen == null){
			Debug.LogWarning("Main component has no loading bar or splash screen assigned");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void InitializeComponents(){
	}

	private void InitializeStartupPrefs(){
	}

	private void InitializeUserPrefs(){
	}

}
