using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRigidbody = collision.rigidbody;

        if (otherRigidbody != null)
        {
            // Set the other object's velocity to zero
            otherRigidbody.velocity = Vector3.zero;

            // Set the other object's angular velocity to zero
            otherRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
