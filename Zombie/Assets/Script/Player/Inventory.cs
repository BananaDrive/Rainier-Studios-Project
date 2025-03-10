using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject weapon;
    public BuffsHandler buffs;
    public BaseItem foundItem;
    public LayerMask itemLayer;
    public BaseItem[] Items;


    public void Start()
    {
        Items = new BaseItem[4];
    }

    public void FixedUpdate()
    {
        ItemDetection();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Items[0] != null)
        {
            Items[0].UseItem();
            Items[0] = null;
            SortInv();
        }
    }

    public void ItemDetection()
    {
        foundItem = null;
        float minDistance = 2f;
        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, 2f, itemLayer))
        {
            float itemDistance = Vector3.Distance(transform.position, collider.transform.position);

            if (itemDistance < minDistance)
            {
                foundItem = collider.GetComponent<BaseItem>();
                minDistance = itemDistance;
            }
        }

        int temp = CheckInventory();

        if (foundItem != null && temp != 4)
        {
            foundItem.buffs = buffs;
            Items[temp] = foundItem;
            foundItem.gameObject.SetActive(false);
        }
    }

    public int CheckInventory()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
                return i;
        }
        return 4;
    }

    public void SortInv()
    {
        for (int i = 0; i < Items.Length - 1; i++)
        {
            Items[i] = Items[i + 1];
            Items[i + 1] = null;
        }
    }
}
