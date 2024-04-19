using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Player player;
    private PlayerHp playerHp;

    private Camera maincam;

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

        maincam = GetComponent<Camera>();
    }

    public Player GetPlayer()
    {
        return player;
    }


    public void SetPlayer(Player _value)
    {
        player = _value;
    }

    public PlayerHp GetPlayerHp()
    {
        return playerHp;
    }

    public void SetPlayerHp(PlayerHp _value)
    {
        playerHp = _value;
    }
}
