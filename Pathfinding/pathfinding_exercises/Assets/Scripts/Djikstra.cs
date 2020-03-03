using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Djikstra
{
    tileboard tile_board;
    List<Tile> open;
    List<Tile> close;
    List<Tile> tiles;
    Tile cur;
    public Tile tile_target;
    public Tile startTile;

    // Start is called before the first frame update
    void Start()
    {
        tile_board = new tileboard();
        open = new List<Tile>();
        close = new List<Tile>();
        tiles = tile_board.tiles;
    }

    Tile getStartTile(Vector3 startPos)
    {
        Tile startTile = new Tile();
        for (int i = 0; i < tiles.Count; i++)
        {
            if (startPos.x < tiles[i].tile.position.x + 0.5 && startPos.z < tiles[i].tile.position.z + 0.5 && startPos.x > tiles[i].tile.position.x - 0.5 && startPos.z > tiles[i].tile.position.z - 0.5)
            {
                startTile = tiles[i];
            }
        }
        return startTile;
    }

    Tile getTargetTile(Vector3 targetPos)
    {
        Tile targetTile = new Tile();
        for (int i = 0; i < tiles.Count; i++)
        {
            if (targetPos.x < tiles[i].tile.position.x + 0.5 && targetPos.z < tiles[i].tile.position.z + 0.5 && targetPos.x > tiles[i].tile.position.x - 0.5 && targetPos.z > tiles[i].tile.position.z - 0.5)
            {
                targetTile = tiles[i];
            }
        }
        return targetTile;
    }

    public List<Vector3> calculatePath(Vector3 start, Vector3 target)
    {
        startTile = getStartTile(start);
        tile_target = getTargetTile(target);
        open.Add(startTile);
        while (open.Count > 0)
        {
            cur = open[0];
            open.RemoveAt(0);
            close.Add(cur);

            // TODO: iterate through connections by using cur.Connections

            for (int i = 0; i < cur.Connections.Count; i++)
            {
                if (cur.gScore + 1 < cur.Connections[i].gScore)
                {
                    cur.Connections[i].gScore = cur.gScore + 1;
                    cur.Connections[i].previous = cur;
                    open.Add(cur.Connections[i]);
                }
                if (cur.Connections[i] == tile_target)
                {
                    if (cur.gScore < tile_target.gScore)
                    {
                        tile_target.gScore = cur.gScore;
                        tile_target.previous = cur;
                    }
                }
            }

            gScoreSort(open);
        }

        List<Vector3> targetPath = new List<Vector3>();
        while (cur.previous != null && cur.previous != startTile)
        {
            Tile node = cur.previous;
            targetPath.Add(cur.tile.position);
            cur = node;
        }
        targetPath.Reverse();
        return targetPath;
    }

    void gScoreSort(List<Tile> list)
    {
        for (int j = 0; j < list.Count; j++)
        {
            if (j - 1 >= 0)
            {
                if(list[j].gScore > list[j - 1].gScore)
                {
                    Tile temp = list[j];
                    list[j] = list[j - 1];
                    list[j - 1] = temp;
                }
            }
        }
    }


    void displayPath(List<Tile> path)
    {
        float height = 0.0f;
        for (int i = 0; i < path.Count; i++)
        {
            height = path[i].tile.position.y;
            height += 1;
            Debug.DrawLine(new Vector3(path[i].tile.position.x, height, path[i].tile.position.z), new Vector3(path[i].previous.tile.position.x, height, path[i].previous.tile.position.z), Color.red);
        }
    }

}
