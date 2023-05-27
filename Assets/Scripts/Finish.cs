using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != player)
            return;
        if (Checkpoint.CheckpointsLeft.Count > 0)
            return;

        // TODO: Звук победы
        player.GetComponent<PlayerController>().enabled = false;
        // TODO: Какие-нибудь красивые эффектики, победа, следующий уровень
    }
}
