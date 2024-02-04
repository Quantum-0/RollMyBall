using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] public AudioSource rollingRock;
    [SerializeField] public AudioSource rollingWood;
    [SerializeField] public AudioSource rollingSteel;
    [SerializeField] public AudioSource rollingPaper;

    Rigidbody rb;
    public List<GameObject> collidingObjects;
    public List<GameObject> triggerCollidingObjects;


    [SerializeField] AudioSource bonkRock;
    [SerializeField] AudioSource bonkWood;
    [SerializeField] AudioSource bonkSteel;
    [SerializeField] AudioSource bonkPaper;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rollingRock.loop = true;
        rollingRock.Play();
        rollingWood.loop = true;
        rollingWood.Play();
        rollingSteel.loop = true;
        rollingSteel.Play();
        rollingPaper.loop = true;
        rollingPaper.Play();
        GetComponent<BallMaterialController>().materialWasChanged += (s, e) => { collidingObjects.Clear(); triggerCollidingObjects.Clear(); };
    }

    // Update is called once per frame
    void Update()
    {
        rollingRock.volume = 0;
        rollingWood.volume = 0;
        rollingSteel.volume = 0;
        rollingPaper.volume = 0;

        var ballMaterial = GetComponent<BallMaterialController>().materialType;

        // TODO: Подумать как сделать адекватнее
        var collidingObjectsCollection = ballMaterial == BallMaterialType.Paper ? triggerCollidingObjects : collidingObjects;

        if (collidingObjectsCollection.Count > 0)
        { 
            var currentSurface = collidingObjectsCollection[0].tag;
            AudioSource rollingSound;

            if (ballMaterial == BallMaterialType.Paper)
                rollingSound = rollingPaper;
            else if (currentSurface == "Wood")
                rollingSound = rollingWood;
            else if (currentSurface == "Steel")
                rollingSound = rollingSteel;
            else if (currentSurface == "Rock")
                rollingSound = rollingRock;
            else
                return;

            rollingSound.pitch = 0.4f + (rb.velocity.magnitude / 7f);
            rollingSound.volume = Mathf.Sqrt(rb.velocity.magnitude / 7f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");
        triggerCollidingObjects.Add(other.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit");
        triggerCollidingObjects.Remove(other.gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject);

        // Add current collision to the list of currently colliding surfaces
        collidingObjects.Add(collision.gameObject);

        // Take first surface (it will be main/ground, probably)
        AudioSource bonkSound;
        if (GetComponent<BallMaterialController>().materialType == BallMaterialType.Paper)
            bonkSound = bonkPaper;
        else if (collision.gameObject.tag == "Rock")
            bonkSound = bonkRock;
        else if (collision.gameObject.tag == "Wood")
            bonkSound = bonkWood;
        else if (collision.gameObject.tag == "Steel")
            bonkSound = bonkSteel;
        else
            return;

        // Calculate the impulse of collision and play sound
        var impulse = Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity);
        bonkSound.volume = Mathf.Min(1, impulse * impulse / 25);
        bonkSound.Play();
    }

    public void OnCollisionExit(Collision collision)
    {
        //Debug.Log(collision.gameObject);
        collidingObjects.Remove(collision.gameObject);
    }
}
