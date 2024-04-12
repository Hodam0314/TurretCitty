using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{


    [SerializeField] GameObject player;


    void Update()
    {
        checkPlayer();
    }
    private void checkPlayer()
    {
        if (player != null)
        {
            Vector3 pos = player.transform.position;
            pos.z = -15;
            transform.position = pos;


        }
    }
}
