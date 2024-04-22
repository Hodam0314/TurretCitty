using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHp : MonoBehaviour
{
    Player player;
    Slider playerHp;
    [SerializeField] private float _curHp = 100f;
    [SerializeField] private float _maxHp = 100f;
    Camera camMain;

    private void Awake()
    {
        playerHp = GetComponent<Slider>();
    }

    void Start()
    {
        playerHp.value = _curHp / _maxHp;
        playerHp.maxValue = _maxHp;
        camMain = Camera.main;
    }


    void Update()
    {
        checkPlayerPos();
        getPlayerHp();
        checkPlayerHp();
    }
    private void checkPlayerPos()
    {
        if (player == null)
        {
            player = GameManager.Instance.GetPlayer();
            return;
        }

        Vector3 screenPos = camMain.WorldToScreenPoint(player.transform.position);

        transform.position = screenPos - new Vector3(0, 0.65f, 0);

    }

    private void checkPlayerHp()                                                                             
    {
        playerHp.value = Mathf.Lerp(0, _maxHp, _curHp / _maxHp);
    }

    public void getPlayerHp()
    {
        if (playerHp == null && player == null) return;

        (float _cur, float _max) playerhp = player.GetPlayerHp(); //Player스크립트에서 사용했던 튜플의 활용방법
        _curHp = playerhp._cur;
        _maxHp = playerhp._max;
    }

}
