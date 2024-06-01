using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopContents : MonoBehaviour
{
    [SerializeField] Image itemImg;
    [SerializeField] TMP_Text info;
    [SerializeField] TMP_Text pay;
    [SerializeField] Button btnbuy;
    Player player;
    private int itempay;
    private bool checktrade = false;

    private void Awake()
    {
        btnbuy.onClick.AddListener(() =>
        {
            if (player != null) //플레이어에게 접근이 가능한지 체크
            {
                if (player.MoneyCheck(itempay)) //플레이어가 구매가능한지 돈 체크
                {
                    player.buyItem();
                }
                else
                {
                    Debug.Log("돈이 부족합니다.");
                }
            }
        });
    }

    private void Start()
    {
        player = GameManager.Instance.GetPlayer();
    }

    public void SetData(Sprite _spr, string _info, int _pay) //다른곳에서 SetData()안에 값을 받아온다.
    {
        itemImg.sprite = _spr;
        info.text = _info;
        pay.text = _pay.ToString();
        itempay = _pay;
    }


}
