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
        var fires = this.GetComponentsInChildren<ParticleSystem>();
        foreach (var ps in fires)
            ps.enableEmission = false;
        player = GameObject.FindGameObjectWithTag("Player");
        CheckpointsLeft.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Activated)
            return;
        if (other.gameObject != player)
            return;

        var ldcc = player.GetComponent<LivesDeathAndCheckpointController>();
        ldcc.LastCheckpoint = this.gameObject;
        CheckpointsLeft.Remove(this);
        Activated = true;

        var fires = this.GetComponentsInChildren<ParticleSystem>();
        foreach (var ps in fires)
            ps.enableEmission = true;

        // TODO: Звук сохранения на чекпоинте (записать с помощью глюкофона?)
        // TODO: Зажечь на нём огоньки (патикл системы заюзать)
    }
}
