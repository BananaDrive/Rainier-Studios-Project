using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class BuffPanel : MonoBehaviour
{
    public class Buffs
    {
        public TMP_Text text;
        public Image image;
    }
    public Buffs[] buffSlots;

    void Start()
    {
        buffSlots = new Buffs[transform.childCount];
        for (int i = 0; i < buffSlots.Length; i++)
        {
            buffSlots[i] = new()
            {
                text = transform.GetChild(i).GetChild(0).GetComponentInChildren<TMP_Text>()
            };
        }
    }
}
