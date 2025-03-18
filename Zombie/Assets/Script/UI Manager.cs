using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject itemStatsPanel;
    public TMP_Text itemName;
    public TMP_Text itemStats;

    public void ChangeItemPanel(string name, string stats)
    {
        itemStatsPanel.SetActive(true);
        itemName.SetText(name);
        itemStats.SetText(stats);
    }
}
