using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallMaterialType
{
    Rock,
    Wood,
    Paper,
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] float Speed = 4.5f;
    [SerializeField] float AirFriction = 1.3f;
    [SerializeField] float MaxAngularVelocity = 15f;
    [SerializeField] GameObject paperForm;

    Rigidbody rb;

    [SerializeField] BallMaterialType materialType;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = MaxAngularVelocity;
        ApplyMaterial();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        rb.AddTorque(new Vector3(v, 0, -h).normalized * Speed);

        var flattenedVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(-flattenedVelocity*AirFriction);      
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            materialType = materialType.Next();
            ApplyMaterial();
        }
    }

    public void ApplyMaterial()
    {
        Debug.Log("Switch material type to " + materialType.ToString());
        switch (materialType)
        {
            case BallMaterialType.Rock:
                rb.mass = 2.75f;
                Speed = 4.5f;
                AirFriction = 1.3f;
                paperForm.SetActive(false);
                return;
            case BallMaterialType.Wood:
                rb.mass = 1.55f;
                Speed = 4.5f;
                AirFriction = 1.3f;
                paperForm.SetActive(false);
                return;
            case BallMaterialType.Paper:
                rb.mass = 0.3f;
                Speed = 2f;
                AirFriction = 0.2f;
                paperForm.SetActive(true);
                return;
            default:
                break;
        }
    }
}


public static class ExtensionMethods
{
    public static BallMaterialType Next(this BallMaterialType myEnum)
    {
        switch (myEnum)
        {
            case BallMaterialType.Rock:
                return BallMaterialType.Wood;
            case BallMaterialType.Wood:
                return BallMaterialType.Paper;
            case BallMaterialType.Paper:
                return BallMaterialType.Rock;
            default:
                throw new Exception();
        }
    }
}