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
        string json = jsontext.text; //string json에 jsontext.text를 넣어준다.

        ShopSell = JsonConvert.DeserializeObject<List<Shopitem>>(json); //ShopSell 리스트 안에 json에 저장된것을 class Shopitem 형식으로 ShopSell안에 넣어준다.
        int count = ShopSell.Count; // int count안에 ShopSell 리스트 개수만큼 카운트를 선언해준다.
        for (int iNum = 0; iNum < count; ++iNum) // 반복문 ShopSell안의 리스트만큼
        {
            Shopitem data = ShopSell[iNum]; // 리스트 ShopSell 안의 [iNum]번째의 저장된 정보를 data 변수에 넣어준다.

            GameObject go = Instantiate(fabShopItem, contents); //게임오브젝트 fabShopItem을 contents위치에 생성하고 생성된 게임오브젝트의 변수명을 go로 설정한다.
            ShopContents goSc = go.GetComponent<ShopContents>(); // 위에서 생성한 게임오브젝트 go에서 ShopContents라는 스크립트의 정보를 가져오고 그걸 ShopContents스크립트의 변수인 goSc에 담아준다.
            goSc.SetData(getSpriteShopImg(data.spritename), data.iteminfo, data.itempay); //가져온 정보를 이용해 ShopContents의 SetData 스크립트에 ShopSell안의 iNum번째 데이터들을 넣어준다.
        }
    }

    private Sprite getSpriteShopImg(string _name)
    {
        return shopImg.Find((shopImgSprite) => shopImgSprite.name == _name); //ShopData스크립트에 저장된 이미지 파일의 이름과 동일한 이미지파일을 찾아주는 코드
    }

}
