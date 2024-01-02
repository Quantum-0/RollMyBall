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

public class MaterialWasChangedEventArgs : EventArgs
{
    public BallMaterialType newMaterial { get; private set; }
    public BallMaterialType? oldMaterial { get; private set; }

    public MaterialWasChangedEventArgs(BallMaterialType newMat, BallMaterialType? oldMat = null)
    {
        newMaterial = newMat;
        oldMaterial = oldMat;
    }
}

/// <summary>
/// Controls visual representation and physical properties of the ball depends on it's material type
/// </summary>
public class BallMaterialController : MonoBehaviour
{
    public Material materialRock;
    public Material materialWood;
    public Material materialPaper;

    // TODO: Rework!
    [SerializeField] GameObject paperForm;

    [SerializeField] private BallMaterialType _materialType;
    public BallMaterialType materialType
    {
        get => _materialType;
        set
        {
            Debug.Log("Switch material type to " + value.ToString());
            transform.TransformVector(Vector3.up);

            var oldMat = _materialType;
            _materialType = value;
            switch (value)
            {
                case BallMaterialType.Rock:
                    rb.mass = 6.667f; // 3, 2.75f;
                    GetComponent<BallPhysics>().AirFriction = 1.3f;
                    paperForm.SetActive(false);
                    GetComponent<Renderer>().material = materialRock;
                    GetComponent<Renderer>().enabled = true;
                    GetComponent<SphereCollider>().enabled = true;
                    break;
                case BallMaterialType.Wood:
                    rb.mass = 1.55f; // 1.7f;
                    GetComponent<BallPhysics>().AirFriction = 1.3f;
                    paperForm.SetActive(false);
                    GetComponent<Renderer>().material = materialWood;
                    GetComponent<Renderer>().enabled = true;
                    GetComponent<SphereCollider>().enabled = true;
                    break;
                case BallMaterialType.Paper:
                    rb.mass = 0.04f; // 0.3f;
                    GetComponent<BallPhysics>().AirFriction = 0.25f;
                    paperForm.SetActive(true);
                    // GetComponent<Renderer>().material = materialPaper;
                    GetComponent<Renderer>().enabled = false;
                    GetComponent<SphereCollider>().enabled = false;
                    break;
                default:
                    break;
            }
            materialWasChanged?.Invoke(this, new MaterialWasChangedEventArgs(value, oldMat));
        }
    }

    Rigidbody rb;

    public event EventHandler<MaterialWasChangedEventArgs> materialWasChanged;

    void Start() {
        rb = GetComponent<Rigidbody>();
        materialType = BallMaterialType.Wood;
    }

    // TODO: Remove later! Added for debug
    void Update()
    {
        if (Input.GetKeyDown("space"))
            materialType = materialType.Next();
    }
}

/// <summary>
/// Adds Next() method to BallMaterialType enum
/// </summary>
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
                throw new Exception("Unknown material type");
        }
    }
}