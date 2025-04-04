using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform inventoryPanel;
    public BuffPanel buffPanel;
    public Image[] inventorySlots;
    public GameObject itemStatsPanel;
    public TMP_Text itemName;
    public TMP_Text itemStats;

    public void Start()
    {
        InitializeComponents(); 
    }

    public void InitializeComponents()
    {
        inventorySlots = new Image[inventoryPanel.childCount];
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = inventoryPanel.GetChild(i).GetComponent<Image>();
        }
    }

    public void ChangeItemPanel(string name, string stats)
    {
        itemStatsPanel.SetActive(true);
        itemName.SetText(name);
        itemStats.SetText(stats);
    }

    public void DisplayInv(BaseItem[] items)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (items[i] == null)
            {
                inventorySlots[i].sprite = null;
                continue;
            }
            inventorySlots[i].sprite = items[i].sprite;
        }
    }

    public void DisplayBuffs(List<ItemStats> itemStats)
    {
        for (int i = 0; i < buffPanel.buffSlots.Length; i++)
        {
            buffPanel.buffSlots[i].text.SetText("");
            if (i < itemStats.Count)
                buffPanel.buffSlots[i].text.SetText(((int)itemStats[i].duration).ToString());
        }
    }
}
