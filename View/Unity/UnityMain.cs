using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the Main unity component that sets up the game startup scene and intializes the startup routine
/// </summary>
public class UnityMain : MonoBehaviour, IViewManager {
	private Main _gameLogicUpdater;
	private double _elapsedTime=0;
	public GameObject _splashScreen;
	public LoadingBar _loadingBar;
	public WorldRenderer2D _renderer;

	// Use this for initialization
	void Start () {
		InitializeComponents ();
		InitializeStartupPrefs ();
		InitializeUserPrefs ();
		_gameLogicUpdater = new Main (this);
		_gameLogicUpdater.Start ();

		MultiLevelVoxelMap2D map = VoxelMapGenerator.GenerateMap(256, 256, 1, UnityEngine.Random.Range(int.MinValue, int.MaxValue));
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
		_elapsedTime += Time.deltaTime;
		if(_elapsedTime > .1f){
			_gameLogicUpdater.update(_elapsedTime);
			_elapsedTime=0f;
		}
	}

	private void InitializeComponents(){
	}

	private void InitializeStartupPrefs(){
	}

	private void InitializeUserPrefs(){
	}

	public void AddViewable(Viewable viewable){
		//TODO implement
	}

}
