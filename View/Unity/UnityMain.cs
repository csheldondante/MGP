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

	public List<Transform> _tileTypes = new List<Transform>();
	public Transform terrain2D;

	private List<UnityViewComponent> _viewables = new List<UnityViewComponent> ();

	// Use this for initialization
	void Start () {
		InitializeComponents ();
		InitializeStartupPrefs ();
		InitializeUserPrefs ();
		_gameLogicUpdater = new Main (this);
		_gameLogicUpdater.Start ();

		//MultiLevelVoxelMap2D map = VoxelMapGenerator.GenerateMap(256, 256, 1, UnityEngine.Random.Range(int.MinValue, int.MaxValue));
		//_renderer.InstantiateMap (map);



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
		if (viewable is Tilemap2D)
			CreateRepresentation ((Tilemap2D) viewable);
		//TODO
		//get the type of viewable
		//get the corrisponding game object for this viewable
		//set the data on the corresponding game objects view component
	}

	public Transform CreateRepresentation(Tilemap2D map){
		Transform terrain = GameObject.Instantiate (terrain2D);
		int tileNum = 0;
		foreach (Tilemap2D.TileType type in map.tiles) {
			int x = tileNum / map.tiles.GetLength (0);
			int y = tileNum % map.tiles.GetLength (0);
			int tileIndex = (int)type;
			Transform newTile = GameObject.Instantiate (_tileTypes [tileIndex], new Vector3(x-map.tiles.GetLength(0)/2, y-map.tiles.GetLength(1)/2,0), Quaternion.identity);
			newTile.SetParent (terrain);
			tileNum++;
		}
		_viewables.Add (terrain.GetComponent<UnityViewComponent> ());
		return terrain;
	
	}
}
