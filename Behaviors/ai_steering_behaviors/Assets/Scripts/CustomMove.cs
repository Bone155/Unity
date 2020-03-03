using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomMove : MonoBehaviour
{
    NavMeshAgent agent;
    NavMeshPath path;
    Seek seek;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        seek = new Seek();
        seek.target.position = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NavMesh.CalculatePath(transform.position, Input.mousePosition, NavMesh.AllAreas, path);
            for (int i = 0; i < path.corners.Length - 1; i++)
                Gizmos.DrawSphere(path.corners[i], 2);
        }
    }
}
