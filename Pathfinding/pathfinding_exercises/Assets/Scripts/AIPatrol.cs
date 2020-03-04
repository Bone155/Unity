using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    Agent agent;
    public Decision root;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<Agent>();
        root = new Waypoint(agent,
                new newWayPoint(agent),
                new seekWayPoint(agent) );
    }

    // Update is called once per frame
    void Update()
    {
        Decision currentDecision = root;
        while(currentDecision != null)
        {
            currentDecision = currentDecision.makeDecision();
        }
        //if (agent.dj.atTarget(agent.transform.position, agent.target.position) == true)
        //{
        //    move = new Waypoint(agent, true); // get new waypoint
        //    move.makeDecision();
        //}
        //else
        //{
        //    move = new Waypoint(agent, false); // move to waypoint
        //    move.makeDecision();
        //}
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
    Decision closeEnough;
    Decision tooFar;

    public Waypoint() { }

    public Waypoint(Agent agent, Decision closeEnoughDecision, Decision tooFarDecision)
    {
        this.agent = agent;
        closeEnough = closeEnoughDecision;
        tooFar = tooFarDecision;
    }

    public Decision makeDecision()
    {
        if(agent.dj.atTarget(agent.transform.position, agent.target.position) == true)
        {
            return closeEnough;
        }
        else
        {
            return tooFar;
        }
        // if the agent has reached its waypoint
            // return the closeEnoughDecision
        // otherwise
            // return the tooFarDecision

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
        agent.transform.position += agent.path[agent.idx];
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
        agent.idx++;
        return null;
    }

}