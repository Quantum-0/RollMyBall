using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [SerializeField] public float ControllingForce = 4.5f;
    public float Direction;
    public JoystickController joystick = null;

    Rigidbody rb;

    // Change acceleration depends on material
    private float ControllingForceFromMaterialType(BallMaterialType materialType)
    {
        if (materialType == BallMaterialType.Paper)
            return 0.67f;
        if (materialType == BallMaterialType.Rock)
            return 10f;
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
        float hRaw, vRaw;
        if (joystick)
        {
            hRaw = joystick.InputHorizontal();
            vRaw = joystick.InputVertical();
        }
        else
        {
            hRaw = Input.GetAxis("Horizontal");
            vRaw = Input.GetAxis("Vertical");
        }

        // Converts them applying camera rotation
        float v = Mathf.Cos(Direction) * vRaw - Mathf.Sin(Direction) * hRaw;
        float h = Mathf.Sin(Direction) * vRaw + Mathf.Cos(Direction) * hRaw;

        // rb.AddTorque(new Vector3(v, 0, -h).normalized * Speed);
        rb.AddForce(new Vector3(h, 0, v).normalized * ControllingForce * 2);    
    }
}
