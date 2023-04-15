using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [SerializeField] public float ControllingForce = 4.5f;
    public float Direction;
    public float EnergyBall = 10.0f;

    Rigidbody rb;

    // Change acceleration depends on material
    private float ControllingForceFromMaterialType(BallMaterialType materialType)
    {
        Debug.LogError(materialType);
        if (materialType == BallMaterialType.Paper)
            return 0.5f;
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

        // Press LeftControl for acceleration ball speed
        if (Input.GetKey(KeyCode.LeftControl) & ControllingForce < 15.0f & EnergyBall > 0){
                ControllingForce += 0.5f;
                EnergyBall -= 0.1f;
        }
        else
        {
            if (ControllingForce > 4.5f){
                ControllingForce -= 2.0f;
            }
        }

        // Limit energy for acceleration ball speed
        if (ControllingForce < 4.7f & EnergyBall <= 10.0f){
            EnergyBall += 0.1f;
        }

        // UnityEngine.Debug.Log(EnergyBall);
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
