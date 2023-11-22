using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [Space]

    [SerializeField] private Tile _topLeftTile;
    [SerializeField] private Tile _topMiddleTile;
    [SerializeField] private Tile _topRightTile;
    [Space]

    [SerializeField] private Tile _middleLeftTile;
    [SerializeField] private Tile _centerTile;
    [SerializeField] private Tile _middleRightTile;
    [Space]

    [SerializeField] private Tile _bottomLeftTile;
    [SerializeField] private Tile _bottomMiddleTile;
    [SerializeField] private Tile _bottomRightTile;
    [Space]

    [SerializeField] private Vector2Int _mapSize;

    Tile[,] _tiles;

    void InitList()
    {
        _tiles = new Tile[3, 3]
        {
            //[0, 0][0, 1][0, 2]
            {
                _bottomLeftTile,
                _middleLeftTile,
                _topLeftTile,
            },

            //[1, 0][1, 1][1, 2]
            {
                _bottomMiddleTile,
                _centerTile,
                _topMiddleTile,
            },

            //[2, 0][2, 1][2, 2]
            {
                _bottomRightTile,
                _middleRightTile,
                _topRightTile,
            }
        };

        /*
         
        _tiles[2, 0] = _topRightTile;
         
        */
    }

    private void Start()
    {
        InitList();

        _tilemap.ClearAllTiles();
        // epic generating animation >:D
        //StartCoroutine(GenerateTiles());
        PaintTiles();
    }



    public void PaintTiles()
    {
        for (int y = 0; y <= _mapSize.y - 1; y++)
        {
            for (int x = 0; x <= _mapSize.x - 1; x++)
            {

                Vector3Int tilePosition = new(x, y);

                //we can NOT index past (2, 2) in either dimension. To fix this, we need a method to 
                _tilemap.SetTile(tilePosition, SelectTile(x, y)); //what if x || y > 3
            }
        }
    }

    private Tile SelectTile(int x, int y)
    {
        //if x == 0, LEFT
        //if x == mapSize.x, RIGHT
        //if y == 0, BOTTOM
        //if y == mapSize.y, TOP

        int xIndex;
        int yIndex;

        if (x == 0)
            xIndex = 0;
        else if (x == _mapSize.x - 1)
            xIndex = 2;
        else
            xIndex = 1;

        if (y == 0)
            yIndex = 0;
        else if (y == _mapSize.y - 1)
            yIndex = 2;
        else
            yIndex = 1;

        return _tiles[xIndex, yIndex];
    }

    IEnumerator GenerateTiles()
    {
        for (int y = 0; y <= _mapSize.y - 1; y++)
        {
            for (int x = 0; x <= _mapSize.x - 1; x++)
            {

                Vector3Int tilePosition = new(x, y);
                //we can NOT index past (2, 2) in either dimension. To fix this, we need a method to 
                _tilemap.SetTile(tilePosition, SelectTile(x, y)); //what if x || y > 3
                yield return new WaitForSeconds(0.1f);
            }
        }
    }


    /*
    
    FOR PROCEDURAL MAP:
    - check adjacent tiles to see which one connects (WFC: Wave Function Collapse)
    - Noise Values to dictate whether or not a tile will actually spawn: if (noiseValue > planetDensity) GenerateTile();

    */
}