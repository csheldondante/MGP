using System;
using System.Collections;
using System.Collections.Generic;


public class Main{
	public IViewManager _viewManager;
	public AIManager _AIManager = new AIManager();
	public List<DataEntity> _entities = new List<DataEntity>();
	public List<Viewable> _viewables = new List<Viewable>();

	public Main(IViewManager viewManager){
		_viewManager = viewManager;
	}

	public void Start(){
		DataEntity terrain = new DataEntity ();
		Placeable placeable = new Placeable (terrain);
		Tilemap2D terrainViewable = new Tilemap2D (terrain);
		_viewManager.AddViewable (terrainViewable);
		CreateEntities ();
	}

	public void update(double deltaTime){
		foreach (DataEntity entity in _entities) {
			if (entity.IsIdle ())
				_AIManager.GiveOrder (entity);
			entity.UpdateActions (deltaTime);
		}
	}

	public List<Viewable> GetViewables(){
		List<Viewable> viewables = new List<Viewable> ();
		foreach (DataEntity entity in _entities) {
			Viewable viewable = entity.GetComponent<Viewable> ();
			if (viewable != null)
				viewables.Add (viewable);
		}
		return viewables;
	}

	private void CreateEntities(){
		for (int i = 0; i < 10; ++i) {
			DataEntity entity = CreateEntity ();
			_entities.Add (entity);
			_viewManager.AddViewable (entity.GetComponent<Viewable> ());
		}
	}

	private DataEntity CreateEntity(){
		DataEntity entity = new DataEntity ();
		Placeable placeable = new Placeable (entity);
		UnityViewData view = new UnityViewData (entity);
		return entity;
	}
}
