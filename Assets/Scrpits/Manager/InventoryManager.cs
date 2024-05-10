using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] GameObject inventoryUI;
    [SerializeField] Transform slotList;
    private List<Transform> slots = new List<Transform>();
    private int slotCount;


    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

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
    }

    private void initslots()
    {
        slots.Clear();
        slots.AddRange(slotList.GetComponentsInChildren<Transform>());
        slots.RemoveAt(0);
    }

    private void OpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryUI.activeSelf == true)
            {
                inventoryUI.SetActive(false);
            }
            else
            {
                inventoryUI.SetActive(true);
            }
        }
    }


}
