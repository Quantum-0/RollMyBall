using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float vSpeed {get; set;} = 1;
    public float hSpeed {get; set;} = 1;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {
        float h = hSpeed * Input.GetAxis("Horizontal");
        float v = vSpeed * Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(h, 0, v));        
    }
}
