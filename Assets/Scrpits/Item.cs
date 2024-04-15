using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] BoxCollider2D checkUser;

    public enum itemType
    {
        Hp,
        WeaponBox,
        Damage,
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == GameTag.Player.ToString())
        {
            
        }
    }

}
