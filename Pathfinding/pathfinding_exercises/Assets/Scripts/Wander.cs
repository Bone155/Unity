using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Agent
{
    Vector3 point;
    float sec = 0;

    void Start()
    {
        point = randomPoint(transform.forward);
    }

    public Vector3 desiredVelocity
    {
        get
        {
            return (point - transform.position).normalized * maxSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sec >= 3)
        {
            point = randomPoint(transform.forward);
            sec = 0;
        }
        Vector3 force = desiredVelocity - velocity;
        velocity += force * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(desiredVelocity);
        sec += Time.deltaTime;
    }

    Vector3 randomPoint(Vector3 forward)
    {
        return new Vector3(Random.Range(forward.x - 4, forward.x + 4), forward.y, Random.Range(forward.z + 10, forward.z + 20));
    }
}
