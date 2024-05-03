using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool checkEnemy = false;
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == GameTag.Player.ToString())
        {
            checkEnemy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == GameTag.Player.ToString())
        {
            checkEnemy = false;
        }
    }

}

