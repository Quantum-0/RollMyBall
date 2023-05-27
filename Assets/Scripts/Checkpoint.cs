using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static List<Checkpoint> CheckpointsLeft = new List<Checkpoint>();
    public bool Activated = false;

    public GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        CheckpointsLeft.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLISION");
        if (Activated)
            return;
        if (other.gameObject != player)
            return;

        var ldcc = other.GetComponent<LivesDeathAndCheckpointController>();
        ldcc.LastCheckpoint = this.gameObject;
        CheckpointsLeft.Remove(this);
        Activated = true;

        // TODO: Звук сохранения на чекпоинте (записать с помощью глюкофона?)
        // TODO: Зажечь на нём огоньки (патикл системы заюзать)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
