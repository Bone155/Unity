using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Djikstra dj;
    public Vector3 velocity;
    public float mass = 1;
    public float maxSpeed = 8;
    float maxForce = 0.5f;
    Vector3 steeringForces;
    public Transform target;
    public List<Vector3> path;

    // Update is called once per frame
    void Update()
    {
        Vector3 steering = Vector3.ClampMagnitude(steeringForces, maxForce);
        steeringForces = Vector3.zero;
        steering /= mass;
        if (velocity != Vector3.zero)
        {
            transform.forward = velocity.normalized;
        }
        steeringForces += steering;
        
    }

}
