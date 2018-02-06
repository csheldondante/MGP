using System;

public class Viewable : DataComponent
{
	public enum ViewableType{TILEMAP_2D, SPRITE};
	public Viewable(DataEntity entity) : base(entity){
		
	}
}
