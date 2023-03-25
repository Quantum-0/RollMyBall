using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the Main Camera which follows the Player object
/// </summary>
public class CameraController : MonoBehaviour
{
    private GameObject target;
    private Vector3 currentTarget;
    public Vector3 offset;
    public float targetAngle;
    private float currentAngle;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate camera smooth rotation
        if (Mathf.Abs(currentAngle - targetAngle) < Time.deltaTime)
            currentAngle = targetAngle;
        else if (currentAngle > targetAngle)
            currentAngle -= Mathf.Min(Time.deltaTime * 180, currentAngle-targetAngle);
        else if (currentAngle < targetAngle)
            currentAngle += Mathf.Min(Time.deltaTime * 180, targetAngle- currentAngle);

        // Handle user rotate camera
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                targetAngle -= 90;
                target.GetComponent<PlayerController>().Direction = Mathf.Deg2Rad * targetAngle;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                targetAngle += 90;
                target.GetComponent<PlayerController>().Direction = Mathf.Deg2Rad * targetAngle;
            }
        }

        // Move camera towards to player's ball
        currentTarget = target.transform.position;
        transform.position = new Vector3(currentTarget.x + offset.x, currentTarget.y > -10 ? currentTarget.y + offset.y : -Mathf.Sqrt(Mathf.Abs(currentTarget.y+10))-10 + offset.y, currentTarget.z + offset.z);
        transform.RotateAround(target.transform.position, Vector3.up, currentAngle);
        transform.LookAt(target.transform.position);
    }
}
