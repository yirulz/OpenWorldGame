using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

[CreateAssetMenu(fileName = "Flee", menuName = "SteeringBehavious/Flee", order = 1)]
public class Flee : SteeringBehaviour
{
    public float fleeTime = 3f;
    public float stoppingDistance = 1f; // Requires that you set "NavMeshAgent.stoppingDistance" to zero

    public override void OnDrawGizmosSelected(AI owner)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(owner.target.position, stoppingDistance);
    }

    public override Vector3 GetForce(AI owner)
    {
        // Create a value to return later
        Vector3 force = Vector3.zero;

        //Get distance between owner and target
        float distance = Vector3.Distance(owner.transform.position, owner.target.position);
        //If Ai is further away from target

        if (distance <= stoppingDistance)
        {
            if (owner.hasTarget) // target != null
            {
                fleeTime -= Time.deltaTime;
                // Get direction from AI agent to Target
                force += owner.transform.position - owner.target.position;
            }
        }
        // Return normalized value 
        return force.normalized;
    }
}
