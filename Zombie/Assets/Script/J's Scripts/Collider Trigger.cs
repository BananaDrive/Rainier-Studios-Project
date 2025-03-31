using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;


public class ColliderTrigger : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent Events;

    private void OnTriggerEnter2D(Collider2D Collider)
    {
        Events.Invoke();
    }
}
