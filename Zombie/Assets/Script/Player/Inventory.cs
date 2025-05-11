using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public BuffsHandler buffs;
    internal BaseItem foundItem;
    internal Enhancers enhancer;
    internal Elevator elevator;
    internal Gate gate;
    public LayerMask itemLayer, enhancerLayer, interactableLayer;
    public BaseItem[] Items;
    public AudioSource Speaker;

    public int inventorySpace;


    public void Start()
    {
        Items = new BaseItem[inventorySpace];
    }

    public void FixedUpdate()
    {
        elevator = GeneralDetection<Elevator>(2f, interactableLayer);
        gate = GeneralDetection<Gate>(2f, interactableLayer);
        ItemDetection();
        EnhancerDetection();
    }

    public void Update()
    {
        var gamepad = Gamepad.current;
        bool controller = false;
        if (gamepad != null)
            controller = true;

        if (Input.GetKeyDown(KeyCode.F) || (controller && gamepad.buttonEast.wasPressedThisFrame))
        {
            if (gate != null && gate.canOpen)
            {
                gate.OpenGate();
                return;
            }
            if (elevator != null && !elevator.onCooldown)
            {
                elevator.MoveToPosition(transform);
                return;
            }
            if (enhancer != null)
            {
                buffs.ApplyEnhancer(enhancer);
                enhancer.gameObject.SetActive(false);
                enhancer = null;
                return;
            }
            if (Items[0] != null && enhancer == null)
            {
                Items[0].UseItem();
                Items[0] = null;
                SortInv();
            }
        }
    }

    public T GeneralDetection<T>(float detectDistance, LayerMask layerDetect)
    {
        T detectedScript = default;
        float minDistance = detectDistance;
        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, detectDistance, layerDetect))
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);

            if (collider.TryGetComponent<T>(out var scriptDetected) && distance < minDistance)
            {
                detectedScript = scriptDetected;
                minDistance = distance;
            }
        }
        return detectedScript;
    }

    public void ItemDetection()
    {
        foundItem = GeneralDetection<BaseItem>(2f, itemLayer);
        int temp = CheckInventory();
        if (foundItem != null && foundItem.canPickUp && temp != inventorySpace)
        {
            foundItem.buffs = buffs;
            Items[temp] = foundItem;
            foundItem.gameObject.SetActive(false);
            GameManager.Instance.UIManager.DisplayInv(Items);
            if (Speaker != null)
                Speaker.Play();
        }
    }

    public void EnhancerDetection()
    {
        enhancer = GeneralDetection<Enhancers>(2f, enhancerLayer);
        if (enhancer == null)
            GameManager.Instance.UIManager.itemStatsPanel.SetActive(false);
        else
            GameManager.Instance.UIManager.ChangeItemPanel(enhancer);
    }

    public int CheckInventory()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
                return i;
        }
        return inventorySpace;
    }

    public void SortInv()
    {
        for (int i = 0; i < Items.Length - 1; i++)
        {
            Items[i] = Items[i + 1];
            Items[i + 1] = null;
        }
        GameManager.Instance.UIManager.DisplayInv(Items);
    }
}
