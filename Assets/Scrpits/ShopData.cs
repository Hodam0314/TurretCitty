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
        string json = jsontext.text; //string json�� jsontext.text�� �־��ش�.

        ShopSell = JsonConvert.DeserializeObject<List<Shopitem>>(json); //ShopSell ����Ʈ �ȿ� json�� ����Ȱ��� class Shopitem �������� ShopSell�ȿ� �־��ش�.
        int count = ShopSell.Count; // int count�ȿ� ShopSell ����Ʈ ������ŭ ī��Ʈ�� �������ش�.
        for (int iNum = 0; iNum < count; ++iNum) // �ݺ��� ShopSell���� ����Ʈ��ŭ
        {
            Shopitem data = ShopSell[iNum]; // ����Ʈ ShopSell ���� [iNum]��°�� ����� ������ data ������ �־��ش�.

            GameObject go = Instantiate(fabShopItem, contents); //���ӿ�����Ʈ fabShopItem�� contents��ġ�� �����ϰ� ������ ���ӿ�����Ʈ�� �������� go�� �����Ѵ�.
            ShopContents goSc = go.GetComponent<ShopContents>(); // ������ ������ ���ӿ�����Ʈ go���� ShopContents��� ��ũ��Ʈ�� ������ �������� �װ� ShopContents��ũ��Ʈ�� ������ goSc�� ����ش�.
            goSc.SetData(getSpriteShopImg(data.spritename), data.iteminfo, data.itempay); //������ ������ �̿��� ShopContents�� SetData ��ũ��Ʈ�� ShopSell���� iNum��° �����͵��� �־��ش�.
        }
    }

    private Sprite getSpriteShopImg(string _name)
    {
        return shopImg.Find((shopImgSprite) => shopImgSprite.name == _name); //ShopData��ũ��Ʈ�� ����� �̹��� ������ �̸��� ������ �̹��������� ã���ִ� �ڵ�
    }

}
