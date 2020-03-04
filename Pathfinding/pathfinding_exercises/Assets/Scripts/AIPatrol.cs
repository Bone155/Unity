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
        agent = GetComponent<Agent>();
        move = new Waypoint(agent, false);
        move.makeDecision();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.dj.atTarget(agent.transform.position, agent.target.position) == true)
        {
            move = new Waypoint(agent, true); // get new waypoint
            move.makeDecision();
        }
        else
        {
            move = new Waypoint(agent, false); // move to waypoint
            move.makeDecision();
        }
    }
}

public interface Decision
{
    Decision makeDecision();
}

public class Waypoint : Decision //question node // reached waypoint?
{
    Agent agent;
    public bool branch;
    Decision decision;
    public Waypoint() { }

    public Waypoint(Agent agent, bool branch)
    {
        this.agent = agent;
        this.branch = branch;
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
        return decision.makeDecision();
    }

}

public class seekWayPoint : Decision //answer node // No // move towards waypoint
{
    Agent agent;

    public seekWayPoint() { }

    public seekWayPoint(Agent agent)
    {
        this.agent = agent;
    }

    public Decision makeDecision()
    {
        agent.GetComponent<Seek>();
        return null;
    }
}

public class newWayPoint : Decision //answer node // yes // get new waypoint
{
    Agent agent;

    public newWayPoint() { }

    public newWayPoint(Agent agent)
    {
        this.agent = agent;
    }

    public Decision makeDecision()
    {
        agent.target.position = new Vector3(Random.Range(0, 9), agent.target.position.y, Random.Range(0, 9));
        agent.path = agent.dj.calculatePath(agent.transform.position, agent.target.position);
        return null;
    }

}