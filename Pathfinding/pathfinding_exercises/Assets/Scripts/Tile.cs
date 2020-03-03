using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public Transform tile;
    public List<Tile> Connections;
    public Tile previous;
    public int gScore;

    public Tile()
    {
        Connections = new List<Tile>();
        previous = null;
        gScore = int.MaxValue;
    }

}
