using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public Vector3 currentTarget;
    public float xOffset, yOffset, zOffset;
    public float targetAngle, currentAngle;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.transform.position + new Vector3(xOffset, yOffset, zOffset);
        transform.LookAt(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        currentTarget = target.transform.position;
        if (Mathf.Abs(currentAngle - targetAngle) < Time.deltaTime)
        {
            currentAngle = targetAngle;
        }
        else if (currentAngle > targetAngle)
        {
            currentAngle -= Mathf.Min(Time.deltaTime * 180, currentAngle-targetAngle);
        }
        else if (currentAngle < targetAngle)
        {
            currentAngle += Mathf.Min(Time.deltaTime * 180, targetAngle- currentAngle);
        }
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
        transform.position = new Vector3(currentTarget.x + xOffset, currentTarget.y > -10 ? currentTarget.y + yOffset : -Mathf.Sqrt(Mathf.Abs(currentTarget.y+10))-10 + yOffset, currentTarget.z + zOffset);
        transform.RotateAround(target.transform.position, Vector3.up, currentAngle);
        transform.LookAt(target.transform.position);
    }
}
