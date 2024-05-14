using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    InvenDrag invendrag;
    InvenDrop invendrop;
    Player player;
    [SerializeField] GameObject inventoryUI;
    [SerializeField] Transform slotList;
    [SerializeField] private List<Transform> slots = new List<Transform>();
    [SerializeField] Button btnInven;
    [SerializeField] Button btnExit;
    [SerializeField] GameObject InventoryItem;
    
    private int slotCount;
    private bool checkMenu;




    private void Awake()
    {
        if (Instance == null) // 싱글턴
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        player = GetComponent<Player>();

        #region 버튼 기능
        btnInven.onClick.AddListener(() =>
        {
            if(checkMenu == false)
            {
                inventoryUI.SetActive(true);
                GameManager.Instance.ActiveInven();
                Time.timeScale = 0.0f;
            }
        });

        btnExit.onClick.AddListener(() =>
        {
            inventoryUI.SetActive(false);
            GameManager.Instance.DisableInven();
            Time.timeScale = 1.0f;
        });

        #endregion
    }

    #region 싱글턴 활용
    public void SetInvenDrop(InvenDrop _value)
    {
        invendrop = _value;
    }

    public InvenDrop GetInvenDrop()
    {
        return invendrop;
    }

    public void SetInvenDrag(InvenDrag _value)
    {
        invendrag = _value;
    }

    public InvenDrag GetInvenDrag()
    {
        return invendrag;
    }
    #endregion

    void Start()
    {
        GameManager.Instance.SetInven(this);

        if (inventoryUI.activeSelf == true)
        {
            inventoryUI.SetActive(false);
        }
        slotCount = 4;
        initslots();
    }

    void Update()
    {
        OpenInventory();
        checkSlots();
    }

    private void initslots() //슬롯의 리스트를 담아주는 코드
    {
        slots.Clear();

        int count = slotList.childCount;
        for (int iNum = 0; iNum < count; iNum++)
        {
            slots.Add(slotList.GetChild(iNum));
        }

        //slots.AddRange(slotList.GetComponentsInChildren<Transform>());
        //slots.RemoveAt(0);
    }

    private void OpenInventory()//인벤토리를 열거나 닫는 코드
    {
        if (Input.GetKeyDown(KeyCode.I) && checkMenu == false)
        {
            if(inventoryUI.activeSelf == true)
            {
                inventoryUI.SetActive(false);
                GameManager.Instance.DisableInven();
                Time.timeScale = 1.0f;
            }
            else 
            {
                inventoryUI.SetActive(true);
                GameManager.Instance.ActiveInven();
                Time.timeScale = 0.0f;
            }
        }
    }

    private void checkSlots() //자신이 사용가능한 인벤토리 슬롯을 체크해주는 코드
    {
        for(int iNum = 0; iNum < slots.Count; ++iNum)
        {
            if (iNum < slotCount)
            {
                slots[iNum].GetComponent<Button>().interactable = true;
            }
            else if(iNum >= slotCount)
            {
                slots[iNum].GetComponent<Button>().interactable = false;
            }

        }
    }

    public void GetSlots() //다른 스크립트로부터 슬롯확장권한을 받았을때 작동하여 사용가능한 슬롯수를 늘려주는 코드
    {
        slotCount++;
    }

    public void ActiveMenu() //게임매니저로부터 메뉴활성 체크 받아오는 코드
    {
        checkMenu = true;
    }

    public void DisableMenu() //게임매니저로부터 메뉴비활성 체크 받아오는 코드
    {
        checkMenu = false;
    }

    private int GetEmptySlots() //빈슬롯을 체크하는 코드
    {
        int count = slots.Count;

        for(int i = 0; i < count; ++i)
        {
            Transform slot = slots[i];
            if(slot.childCount == 0 && slot.GetComponent<Button>().interactable == true)
            {
                return i;
            }
        }
        return -1;
    }

    public bool GetItem(Sprite _spr, Item.ItemType itemType) //아이템을 플레이어가 얻었을때 동작하는 코드
    {
        int slotNum = GetEmptySlots();
        if(slotNum == -1)
        {
            return false;
        }

        InvenDrag drg = Instantiate(InventoryItem, slots[slotNum]).GetComponent<InvenDrag>();
        drg.SetItem(_spr,itemType);
     

        return true;
    }

    public void hp()
    {
        player.HpRecovery();
    }

    public void speed()
    {
        player.SpeedUp();
    }

    public void coin()
    {
        player.UseCoin();
    }

}
