using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallImpact : MonoBehaviour
{
    [Header("Force Settings")]
    public float forceMultiplier = 1.2f; 
    public float upwardForceFactor = 0.1f; 
    public float maxForce = 10f; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GolfClub"))
        {
            Rigidbody ballRb = GetComponent<Rigidbody>();
            Rigidbody clubRb = collision.rigidbody;

            
            Vector3 forceDirection = collision.contacts[0].normal;
            
            
            forceDirection.y += upwardForceFactor;
            forceDirection.Normalize();

            
            float clubSpeed = collision.relativeVelocity.magnitude;
            float forceMagnitude = Mathf.Min(clubSpeed * forceMultiplier, maxForce);

            
            ballRb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
        }
    }
}
