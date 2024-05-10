using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        GameManager.Instance.SetShop(this);
    }

    void Update()
    {
        
    }
}
