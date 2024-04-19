using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHp : MonoBehaviour
{
    [SerializeField] Transform trsPlayer;
    Slider playerHp;
    private float _curHp;
    private float _maxHp;

    private void Awake()
    {
        playerHp = GetComponent<Slider>();
    }

    void Start()
    {
        GameManager.Instance.SetPlayerHp(this);
        playerHp.value =  _curHp / _maxHp;
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
        playerHp.value = Mathf.Lerp(playerHp.value, _curHp / _maxHp, Time.deltaTime * 10);
    }

    public void getPlayerHp(float plycurHp , float plymaxHp)
    {
        _curHp = plycurHp;
        _maxHp = plymaxHp;
    }

}
