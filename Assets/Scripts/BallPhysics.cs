using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Overrides original MaxAngularVelocity of the Ball's rigidbody and applies air friction force
/// </summary>
public class BallPhysics : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] public readonly float MaxAngularVelocity = 30f;
    [SerializeField] public float AirFriction = 1.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = MaxAngularVelocity;
    }

    void FixedUpdate()
    {
        var flattenedVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(-flattenedVelocity * AirFriction);
    }
}
