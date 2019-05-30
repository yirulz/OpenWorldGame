using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : SteeringBehaviour
{
    private float targetTime;
    public Transform targetLocation;

    public void Awake()
    {
        targetTime = Time.time;
    }
    public override Vector3 GetForce(AI owner)
    {
        // Create a value to return later
        Vector3 force = Vector3.zero;

        if (owner.hasTarget) // target != null
        {
            // Get direction from AI agent to Target
            force += owner.transform.position - owner.target.position;
        }

        // Return normalized value 
        return force.normalized;
    }
}
