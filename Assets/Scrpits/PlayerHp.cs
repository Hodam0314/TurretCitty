using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHp : MonoBehaviour
{
    [SerializeField] Transform trsPlayer;
    [SerializeField] private Slider playerHp;

    private float maxHp = 100;
    private float curHp = 100;

    void Start()
    {
        playerHp.value = (float)curHp / (float)maxHp;

    }


    void Update()
    {
        checkPlayerPos();
        checkPlayerHp();
        
    }
    private void checkPlayerPos()
    {
        if (trsPlayer == null)
        {
            return;
        }
        transform.position = trsPlayer.position - new Vector3(0, 0.65f, 0);

    }
    private void checkPlayerHp()
    {
        playerHp.value = Mathf.Lerp(playerHp.value, (float)curHp / (float)maxHp, Time.deltaTime * 10);
    }

    public void SetPlayerHp(float _curHp, float _maxHp)
    {
        maxHp = _maxHp;
        curHp = _curHp;

    }

}
