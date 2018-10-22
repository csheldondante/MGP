using System.Collections.Generic;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine;

public class UnitComponent : IComponent {
	private IEntity _entity;

	public IEntity GetEntity(){
		return _entity;	
	}

	public bool SetEntity(IEntity entity){
		if (_entity == default(IEntity))
			return false;
		_entity = entity;
		return true;
	}
}

public class PositionComponent : UnitComponent {
}

public class TileComponent : UnitComponent {
	public string tileType;
	public TileBase tile;
}

public class PathfindingComponent : UnitComponent {
	public bool waterwalking=false;
	public bool isBusy=false;
}

public class SpriteComponent : UnitComponent {
	public GameObject spriteObject;
}
