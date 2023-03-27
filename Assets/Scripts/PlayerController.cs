using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [SerializeField] public float ControllingForce = 4.5f;
    public float Direction;

    Rigidbody rb;

    // Change acceleration depends on material
    private float ControllingForceFromMaterialType(BallMaterialType materialType)
    {
        if (materialType == BallMaterialType.Paper)
            return 0.67f;
        return 4.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<BallMaterialController>().materialWasChanged +=
            (s, e) => ControllingForce = ControllingForceFromMaterialType(e.newMaterial);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Disable control when Left Shift is pressed
        if (Input.GetKey(KeyCode.LeftShift))
            return;

        // Get original axis values
        float hRaw = Input.GetAxis("Horizontal");
        float vRaw = Input.GetAxis("Vertical");

        // Converts them applying camera rotation
        float v = Mathf.Cos(Direction) * vRaw - Mathf.Sin(Direction) * hRaw;
        float h = Mathf.Sin(Direction) * vRaw + Mathf.Cos(Direction) * hRaw;

        // rb.AddTorque(new Vector3(v, 0, -h).normalized * Speed);
        rb.AddForce(new Vector3(h, 0, v).normalized * ControllingForce * 2);    
    }
}
