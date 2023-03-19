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

    [SerializeField] public Material rockMat;
    [SerializeField] public Material woodMat;
    [SerializeField] public Material paperMat;

    Rigidbody rb;

    public List<GameObject> collidingObjects;

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

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);

        collidingObjects.Add(collision.gameObject);

        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "MyGameObjectName")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Do something here");
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "MyGameObjectTag")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        Debug.Log(collision.gameObject);
        collidingObjects.Remove(collision.gameObject);
    }

    public void ApplyMaterial()
    {
        Debug.Log("Switch material type to " + materialType.ToString());
        transform.Translate(Vector3.up, Space.World);
        collidingObjects.Clear();

        switch (materialType)
        {
            case BallMaterialType.Rock:
                rb.mass = 2.75f;
                Speed = 4.5f;
                AirFriction = 1.3f;
                paperForm.SetActive(false);
                GetComponent<Renderer>().material = rockMat;
                return;
            case BallMaterialType.Wood:
                rb.mass = 1.55f;
                Speed = 4.5f;
                AirFriction = 1.3f;
                paperForm.SetActive(false);
                GetComponent<Renderer>().material = woodMat;
                return;
            case BallMaterialType.Paper:
                rb.mass = 0.3f;
                Speed = 2f;
                AirFriction = 0.2f;
                paperForm.SetActive(true);
                GetComponent<Renderer>().material = paperMat;
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