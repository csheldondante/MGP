using System;


public interface IDataComponent
{
	DataEntity GetEntity();
}

public class DataComponent : IDataComponent
{
	private readonly DataEntity _entity;
	public DataComponent(DataEntity entity){
		_entity = entity;
		entity.AddDataComponent (this);
	}

	public DataEntity GetEntity(){
		return _entity;
	}
}
