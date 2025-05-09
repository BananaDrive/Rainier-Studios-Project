using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemAcess : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;


    public void Select(GameObject toSelect)
    {
        if (eventSystem != null)
            eventSystem.SetSelectedGameObject(toSelect);
    }
}