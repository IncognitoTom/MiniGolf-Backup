using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfClubSwing : MonoBehaviour
{
    [Header("Mini-Golf Force Settings")]
    public float forceMultiplier = 0.5f; 
    public float maxForce = 5f; 
    public float groundDampening = 0.8f; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GolfClub"))
        {
            Rigidbody ballRb = GetComponent<Rigidbody>();
            Vector3 hitDirection = collision.contacts[0].normal;
            
            
            float clubSpeed = collision.relativeVelocity.magnitude;
            float forceMagnitude = Mathf.Min(clubSpeed * forceMultiplier, maxForce);

            
            hitDirection.y *= 0.2f;
            hitDirection.Normalize();

           
            ballRb.AddForce(hitDirection * forceMagnitude, ForceMode.Impulse);

        }
    }
}