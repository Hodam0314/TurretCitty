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
        if (Instance == null) // �̱���
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        player = GetComponent<Player>();

        #region ��ư ���
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

    #region �̱��� Ȱ��
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

    private void initslots() //������ ����Ʈ�� ����ִ� �ڵ�
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

    private void OpenInventory()//�κ��丮�� ���ų� �ݴ� �ڵ�
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

    private void checkSlots() //�ڽ��� ��밡���� �κ��丮 ������ üũ���ִ� �ڵ�
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

    public void GetSlots() //�ٸ� ��ũ��Ʈ�κ��� ����Ȯ������� �޾����� �۵��Ͽ� ��밡���� ���Լ��� �÷��ִ� �ڵ�
    {
        slotCount++;
    }

    public void ActiveMenu() //���ӸŴ����κ��� �޴�Ȱ�� üũ �޾ƿ��� �ڵ�
    {
        checkMenu = true;
    }

    public void DisableMenu() //���ӸŴ����κ��� �޴���Ȱ�� üũ �޾ƿ��� �ڵ�
    {
        checkMenu = false;
    }

    private int GetEmptySlots() //�󽽷��� üũ�ϴ� �ڵ�
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

    public bool GetItem(Sprite _spr, Item.ItemType itemType) //�������� �÷��̾ ������� �����ϴ� �ڵ�
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
