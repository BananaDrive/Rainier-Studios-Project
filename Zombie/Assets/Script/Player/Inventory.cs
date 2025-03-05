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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ItemDetection();
        }

        if (Input.GetKeyDown(KeyCode.F) && Items[0] != null)
        {
            Items[0].UseItem();
            Items[0] = null;
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

        if (foundItem != null)
        {
            foundItem.buffs = buffs;
            Items[CheckInventory()] = foundItem;
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
        return 3;
    }
}
