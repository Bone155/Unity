using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Djikstra : MonoBehaviour
{
    public tileboard tile_board;
    List<Tile> open;
    List<Tile> close;
    Tile cur;
    Tile tile_target;
    Tile startTile;

    // Start is called before the first frame update
    void Start()
    {
        open = new List<Tile>();
        close = new List<Tile>();
    }

    public bool atTarget(Vector3 pos, Vector3 target)
    {
        if (pos.x <= getTargetTile(tile_board.tiles, target).tile.position.x + 0.5 && pos.z <= getTargetTile(tile_board.tiles, target).tile.position.z + 0.5 && pos.x >= getTargetTile(tile_board.tiles, target).tile.position.x - 0.5 && pos.z >= getTargetTile(tile_board.tiles, target).tile.position.z - 0.5)
            return true;
        else
            return false;
    }

    public Tile getStartTile(List<Tile> tiles, Vector3 startPos)
    {
        Tile startTile = new Tile();
        for (int i = 0; i < tiles.Count; i++)
        {
            if (startPos.x <= tiles[i].tile.position.x + 0.5 && startPos.z <= tiles[i].tile.position.z + 0.5 && startPos.x >= tiles[i].tile.position.x - 0.5 && startPos.z >= tiles[i].tile.position.z - 0.5)
            {
                startTile = tiles[i];
            }
        }
        return startTile;
    }

    public Tile getTargetTile(List<Tile> tiles, Vector3 targetPos)
    {
        Tile targetTile = new Tile();
        for (int i = 0; i < tiles.Count; i++)
        {
            if (targetPos.x <= tiles[i].tile.position.x + 0.5 && targetPos.z <= tiles[i].tile.position.z + 0.5 && targetPos.x >= tiles[i].tile.position.x - 0.5 && targetPos.z >= tiles[i].tile.position.z - 0.5)
            {
                targetTile = tiles[i];
            }
        }
        return targetTile;
    }

    public List<Vector3> calculatePath(Vector3 start, Vector3 target)
    {
        startTile = getStartTile(tile_board.tiles, start);
        startTile.gScore = 0;
        tile_target = getTargetTile(tile_board.tiles, target);
        open.Add(startTile);
        while (open.Count > 0)
        {
            cur = open[0];
            open.RemoveAt(0);
            close.Add(cur);

            // TODO: iterate through connections by using cur.Connections

            for (int i = 0; i < cur.Connections.Count; i++)
            {
                if (cur == tile_target)
                {
                    if (cur.gScore < tile_target.gScore)
                    {
                        tile_target.gScore = cur.gScore;
                        tile_target.previous = cur;
                    }
                }
                if (cur.gScore + 1 < cur.Connections[i].gScore)
                {
                    cur.Connections[i].gScore = cur.gScore + 1;
                    cur.Connections[i].previous = cur;
                    open.Add(cur.Connections[i]);
                }
            }

            gScoreSort(open);
        }

        List<Vector3> targetPath = new List<Vector3>();
        while (cur != null && cur != startTile)
        {
            Tile node = cur.previous;
            targetPath.Add(cur.tile.position);
            cur = node;
        }
        targetPath.Reverse();
        close.Clear();
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
