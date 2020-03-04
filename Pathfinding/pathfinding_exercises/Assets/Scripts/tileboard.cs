using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileboard : MonoBehaviour
{
    public List<Tile> tiles;
    public Transform tileObject;
    public int row;
    public int col;
    Vector3 pos;
    // Start is called before the first frame update
    void Awake()
    {
        tiles = new List<Tile>();
        pos = new Vector3(0,1,0);
        for(int i = 0; i < row; i++)
        {
            for(int j = 0; j < col; j++)
            {
                Tile tmp = new Tile();
                tmp.tile = Instantiate(tileObject, new Vector3(pos.x + i, pos.y, pos.z + j), Quaternion.LookRotation(tileObject.forward));
                tiles.Add(tmp);
            }

        }
        connectingTiles();
    }

    void connectingTiles()
    {
        int width = col;
        for (int i = 0; i < row * col; i++)
        {
            if (!(i < width))//north
            {
                tiles[i].Connections.Add(tiles[i - width]);
            }
            if (i + width < col * row)//south
            {
                tiles[i].Connections.Add(tiles[i + width]);
            }
            if (i + 1 < col * row)//east
            {
                tiles[i].Connections.Add(tiles[i + 1]);
            }
            if (!(i % width == 0))//west
            {
                tiles[i].Connections.Add(tiles[i - 1]);
            }

        }

    }


}
