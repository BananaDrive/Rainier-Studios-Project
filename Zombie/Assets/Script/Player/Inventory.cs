using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public BaseItem foundItem;
    public LayerMask itemLayer;
    public GameObject[] Items;
    public int selectedItem;

    private bool itemfound;

    public void Start()
    {
        Items = new GameObject[4];
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ItemDetection();

            foreach (GameObject baseitem in Items)
            {
                Debug.Log(baseitem);
            }
        }
    }

    public void ItemDetection()
    {
        itemfound = false;
        foundItem = null;
        float minDistance = 2f;
        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, 2f, itemLayer))
        {
            float itemDistance = Vector3.Distance(transform.position, collider.transform.position);

            if (itemDistance < minDistance)
            {
                foundItem = collider.GetComponent<BaseItem>();
                minDistance = itemDistance;
                itemfound = true;
            }
        }

        if (foundItem != null)
        {
            Items[CheckInventory()] = foundItem.gameObject;
            Destroy(foundItem.gameObject);
        }
    }

    public int CheckInventory()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
                return i;
        }
        return selectedItem;
    }
}
