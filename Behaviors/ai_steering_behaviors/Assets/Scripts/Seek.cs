using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    Agent agent = new Agent();
    public Transform target;

    public Vector3 desiredVelocity
    {
        get
        {
            return (target.position - transform.position).normalized * agent.maxSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 force = desiredVelocity - agent.velocity;
        agent.velocity += force * Time.deltaTime;
        transform.position += agent.velocity * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(desiredVelocity);
    }

}
