using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Unity.Properties;

public class ShopData : MonoBehaviour
{
    [SerializeField] TextAsset jsontext;
    [SerializeField] Transform contents;
    [SerializeField] GameObject fabShopItem;

    public class Shopitem
    {
        public string spritename;
        public string iteminfo;
        public int itempay;
    }

    List<Shopitem> ShopSell = new List<Shopitem>();

    [SerializeField] List<Sprite> shopImg;

    void Start()
    {
        string json = jsontext.text;

        ShopSell = JsonConvert.DeserializeObject<List<Shopitem>>(json);
        int count = ShopSell.Count;
        for (int iNum = 0; iNum < count; ++iNum)
        {
            Shopitem data = ShopSell[iNum];

            GameObject go = Instantiate(fabShopItem, contents);
            ShopContents goSc = go.GetComponent<ShopContents>();
            goSc.SetData(getSpriteShopImg(data.spritename), data.iteminfo, data.itempay);
        }
    }

    private Sprite getSpriteShopImg(string _name)
    {
        return shopImg.Find((shopImgSprite) => shopImgSprite.name == _name);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
