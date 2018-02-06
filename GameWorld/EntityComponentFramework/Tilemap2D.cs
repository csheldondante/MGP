public class Tilemap2D : Viewable {
	public enum TileType{DIRT, WATER};
	public readonly TileType[,] tiles;

	public Tilemap2D(int sizeX, int sizeY, DataEntity entity) : base (entity) {
		tiles= new TileType[sizeX,sizeY];
	}
}
