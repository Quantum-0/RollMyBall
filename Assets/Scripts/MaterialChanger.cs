using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    GameObject player;
    BallMaterialController bmc;
    [SerializeField] BallMaterialType material;
    Vector3 playerTransformPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bmc = player.GetComponent<BallMaterialController>();
        playerTransformPosition = this.transform.position + new Vector3(0, 0.7f, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bmc.materialType == material)
            return;

        var dist = Vector3.Distance(playerTransformPosition, player.transform.position);
        if (dist > 0.9)
            return;

        if (dist > 0.08f)
        {
            player.GetComponent<Rigidbody>().isKinematic = true;
            player.transform.position += (playerTransformPosition - player.transform.position).normalized * 0.07f;
            return;
        }

        if (dist > 0)
        {
            player.transform.position = playerTransformPosition;
            return;
        }

        if (dist == 0)
        {
            bmc.materialType = this.material;
            // Тут ещё какая-нибудь анимация, красивый эффектик и т.п.
            player.GetComponent<Rigidbody>().isKinematic = false;

        }
    }
}
