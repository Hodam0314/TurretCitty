using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public static ShopManager Instance;
    Player player;

    public void SetPlayer(Player _value)
    {
        player = _value;
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }


    void Start()
    {
        GameManager.Instance.SetShop(this);
    }

    void Update()
    {
        
    }

    public void SlotUpgrade(int _pay)
    {

    }

    public void Sell()
    {

    }
    
    public void Buy()
    {

    }

}
