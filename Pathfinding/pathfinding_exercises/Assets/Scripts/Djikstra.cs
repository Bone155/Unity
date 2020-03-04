using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Djikstra : MonoBehaviour
{
    public tileboard tile_board;
    List<Tile> open;
    List<Tile> close;
    Tile cur;
    public Tile tile_target;
    public Tile startTile;

    // Start is called before the first frame update
    void Start()
    {
        open = new List<Tile>();
        close = new List<Tile>();
    }

    public bool atTarget(Vector3 pos, Vector3 target)
    {
        if (pos.x <= getTargetTile(target).tile.position.x + 0.5 && pos.z <= getTargetTile(target).tile.position.z + 0.5 && pos.x >= getTargetTile(target).tile.position.x - 0.5 && pos.z >= getTargetTile(target).tile.position.z - 0.5)
            return true;
        else
            return false;
    }

    public Tile getStartTile(Vector3 startPos)
    {
        Tile startTile = new Tile();
        for (int i = 0; i < tile_board.tiles.Count; i++)
        {
            if (startPos.x <= tile_board.tiles[i].tile.position.x + 0.5 && startPos.z <= tile_board.tiles[i].tile.position.z + 0.5 && startPos.x >= tile_board.tiles[i].tile.position.x - 0.5 && startPos.z >= tile_board.tiles[i].tile.position.z - 0.5)
            {
                startTile = tile_board.tiles[i];
            }
        }
        return startTile;
    }

    public Tile getTargetTile(Vector3 targetPos)
    {
        Tile targetTile = new Tile();
        for (int i = 0; i < tile_board.tiles.Count; i++)
        {
            if (targetPos.x <= tile_board.tiles[i].tile.position.x + 0.5 && targetPos.z <= tile_board.tiles[i].tile.position.z + 0.5 && targetPos.x >= tile_board.tiles[i].tile.position.x - 0.5 && targetPos.z >= tile_board.tiles[i].tile.position.z - 0.5)
            {
                targetTile = tile_board.tiles[i];
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
