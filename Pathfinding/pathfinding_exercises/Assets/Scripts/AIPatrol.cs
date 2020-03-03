using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    Agent agent;
    public Decision move;
    // Start is called before the first frame update
    void Start()
    {
        agent = new Agent();
        move = new Waypoint(agent);
        move.makeDecision();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public interface Decision
{
    Decision makeDecision();
}

public class Waypoint : Decision //question node
{
    Agent agent;
    public bool branch = false;
    Decision decision;
    public Waypoint() { }

    public Waypoint(bool branch)
    {
        this.branch = branch;
    }

    public Waypoint(Agent agent)
    {
        this.agent = agent;
    }

    public Decision makeDecision()
    {
        if (branch == true)
        {
            decision = new newWayPoint(agent);
        }
        else
        {
            decision = new seekWayPoint(agent);
        }
        return decision;
    }

}

public class seekWayPoint : Decision //answer node
{
    Agent agent;

    public seekWayPoint() { }

    public seekWayPoint(Agent agent)
    {
        this.agent = agent;
    }

    public Decision makeDecision()
    {
        agent = new Seek();
        return new Waypoint();
    }
}

public class newWayPoint : Decision //answer node
{
    Agent agent;

    public newWayPoint() { }

    public newWayPoint(Agent agent)
    {
        this.agent = agent;
    }

    public Decision makeDecision()
    {
        agent = new Wander();
        return new Waypoint();
    }
}