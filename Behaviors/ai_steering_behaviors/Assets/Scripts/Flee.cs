using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    Agent agent = new Agent();
    public Transform target;
    float desiredDistance = 2.0f;
    public AnimationCurve fleeCurve;
    public Vector3 desiredVelocity
    {
        get
        {
            return (transform.position - target.position).normalized * agent.maxSpeed;
        }
    }

    public float Weight
    {
        get {
            Vector3 vectorToMe = transform.position - target.position;
            float dist = vectorToMe.magnitude;
            return fleeCurve.Evaluate(Mathf.InverseLerp(0, desiredDistance, dist));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 force = desiredVelocity - agent.velocity;
        agent.velocity += force * Weight * Time.deltaTime;
        transform.position += agent.velocity * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(desiredVelocity);
        
    }

}
