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

    public void SetData(Sprite _spr, string _info, int _pay)
    {
        itemImg.sprite = _spr;
        info.text = _info;
        pay.text = _pay.ToString();
    }
}
