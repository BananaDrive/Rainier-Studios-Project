using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform inventoryPanel;
    public BuffPanel buffPanel;
    public PlayerHealthBar playerHealthBar;
    public SpriteRenderer blackScreen;
    public Image hurtOverlay;
    public Image[] inventorySlots;
    public GameObject itemStatsPanel;
    public TMP_Text[] statAmounts;
    public TMP_Text itemName;
    public TMP_Text scoreText;

    public float score;

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

    public void ChangeItemPanel(Enhancers enhancer)
    {
        itemStatsPanel.SetActive(true);
        itemName.SetText(enhancer.itemName);

        statAmounts[0].SetText(CheckStat(enhancer.damage) + enhancer.damage + "%");
        statAmounts[1].SetText(CheckStat(enhancer.fireRate) + enhancer.fireRate + "%");
        statAmounts[2].SetText(CheckStat(enhancer.clipSize) + enhancer.clipSize);
        statAmounts[3].SetText(CheckStat(enhancer.bulletSpeed) + enhancer.bulletSpeed + "%");
        statAmounts[4].SetText(CheckStat(enhancer.reloadSpeed) + enhancer.reloadSpeed + "%");
        statAmounts[5].SetText(CheckStat(enhancer.accuracy) + enhancer.accuracy + "%");
        statAmounts[6].SetText(CheckStat(enhancer.shotAmount) + enhancer.shotAmount);
    }

    string CheckStat(float stat)
    {
        if (stat < 0)
            return "";
        return "+";
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

    public void DisplayBuffs(List<ItemStats> itemStats_)
    {
        List<ItemStats> itemStats = new(itemStats_);
        int slotsTaken = 0;

        for (int i = 0; i < itemStats.Count; i++)
        {
            if (itemStats[i].buffSprite == null)
            {   
                itemStats.Remove(itemStats[i]);
                i--;
            }   
        }

        if (itemStats.Count == 0)
        {
            for (int i = 0; i < buffPanel.buffSlots.Length; i++)
            {
                buffPanel.buffSlots[i].text.SetText("");
                buffPanel.buffSlots[i].image.sprite = null;
            }
            return;
        }
            
        for (int i = 0; i < itemStats.Count && slotsTaken < 4; i++)
        {
            Debug.Log(i);
            buffPanel.buffSlots[i].text.SetText("");
            if (itemStats[i].buffSprite != null)
            {
                buffPanel.buffSlots[slotsTaken].image.sprite = itemStats[i].buffSprite;
                buffPanel.buffSlots[slotsTaken].text.SetText(((int)itemStats[i].duration).ToString());
                slotsTaken++;
            }
        }
    }

    public void UpdateScore(float add)
    {
        score += add;
        scoreText.SetText("Score: " + score);
    }
}
