using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float Speed = 3;
    [SerializeField] float AirFriction = 1.5f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(h,0,v).normalized * Speed);

        var flattenedVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(-flattenedVelocity*AirFriction);      
    }
}
