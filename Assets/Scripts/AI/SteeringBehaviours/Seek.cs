using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Seek", menuName = "SteeringBehavious/Seek", order = 1)]
public class Seek : SteeringBehaviour
{
    public float stoppingDistance = 1f; //Requires that you set "NavMeshAgent.stoppingDistance" to zero
    public override void OnDrawGizmosSelected(AI owner)
    {
        Gizmos.color = Color.blue;
        float distance = Vector3.Distance(owner.target.position, owner.transform.position);
        Gizmos.DrawWireSphere(owner.transform.position, distance - stoppingDistance);
    }
    public override Vector3 GetForce(AI owner)
    {
        //Create a vlaue to return
        Vector3 force = Vector3.zero;

        //Get distance between owner and target
        float distance = Vector3.Distance(owner.transform.position, owner.target.position);
        //If Ai is further away
        if(distance > stoppingDistance)
        {
            //modify value here...
            if (owner.hasTarget)
            {
                //Get direction from AI agent to target
                force += owner.target.position - owner.transform.position;
            }
        }
       
        //return normalized value
        return force.normalized;
    }



}
