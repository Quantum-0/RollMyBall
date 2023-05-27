using UnityEngine;
using System.Collections;

public class LivesDeathAndCheckpointController : MonoBehaviour
{
    public GameObject LastCheckpoint;

    // Use this for initialization
    void Start()
    {
        LastCheckpoint = GameObject.Find("Start Point");
        PutOnCheckpoint();
    }

    int CheckpointsLeft => Checkpoint.CheckpointsLeft.Count;

    void PutOnCheckpoint()
    {
        var rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        this.transform.position = LastCheckpoint.transform.position + Vector3.up;
    }

    void Death()
    {
        // TODO: Какие-нибудь красивые эффектики
        // TODO: -1 жизнь
        PutOnCheckpoint();
    }

    // Update is called once per frame
    void Update()
    {
        // Death
        if (this.transform.position.y < -40)
            Death();
    }
}
