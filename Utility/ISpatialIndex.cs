using System;
using System.Collections.Generic;

public interface ISpatialIndex
{
	void Index(IPlaceable obj, Position position);
	List<IPlaceable> GetContents(Position position);
}
