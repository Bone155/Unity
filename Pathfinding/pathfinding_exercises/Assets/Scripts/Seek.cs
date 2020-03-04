using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    public Agent agent;
    Vector3 force;

    public Vector3 desiredVelocity
    {
        get
        {
            return (agent.target.position - transform.position).normalized * agent.maxSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        force = desiredVelocity - agent.velocity;
        agent.velocity += force * Time.deltaTime;
        transform.position += agent.velocity * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(desiredVelocity);
    }

}
