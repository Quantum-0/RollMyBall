using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LivesDeathAndCheckpointController : MonoBehaviour
{
    public GameObject LastCheckpoint;
    public int Lives = 3;  // TODO: Где-то их отрисовывать

    // Use this for initialization
    void Start()
    {
        LastCheckpoint = GameObject.Find("Start Point");
        PutOnCheckpoint();
    }

    public int CheckpointsLeft => Checkpoint.CheckpointsLeft.Count;

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
        Lives -= 1;
        if (Lives > 0)
            PutOnCheckpoint();
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);            
    }

    // Update is called once per frame
    void Update()
    {
        // Death
        if (this.transform.position.y < -40)
            Death();
    }
}
