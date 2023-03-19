using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // [SerializeField] public AudioClip rollingWood;
    [SerializeField] public AudioSource rollingWood;
    [SerializeField] public AudioSource rollingSteel;
    [SerializeField] public GameObject ball;

    Rigidbody rb;
    PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        rb = ball.GetComponent<Rigidbody>();
        pc = ball.GetComponent<PlayerController>();
        rollingWood.loop = true;
        rollingWood.Play();
        rollingSteel.loop = true;
        rollingSteel.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(pc.collidingObjects.Count);

        rollingWood.volume = 0;
        rollingSteel.volume = 0;

        if (pc.collidingObjects.Count > 0)
        { 
            var currentSurface = pc.collidingObjects[0].tag;
            if (currentSurface == "Wood")
            {
                rollingWood.pitch = 0.5f + (rb.velocity.magnitude / 5f);
                rollingWood.volume = Mathf.Sqrt(rb.velocity.magnitude / 5f);
            }
            else if (currentSurface == "Steel")
            {
                rollingSteel.pitch = 0.5f + (rb.velocity.magnitude / 5f);
                rollingSteel.volume = Mathf.Sqrt(rb.velocity.magnitude / 5f);
            }
        }
        else
        {
            rollingWood.volume = 0;
        }
    }
}
