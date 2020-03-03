using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : Agent
{
    int i = 0;
    public Vector3 desiredVelocity
    {
        get
        {
            return (target.position - transform.position).normalized * maxSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 force = desiredVelocity - velocity;
        //velocity += force * Time.deltaTime;
        //transform.position += velocity * Time.deltaTime;
        transform.position += path[i] * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(desiredVelocity);
        i++;
    }

}
