using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public float cooldownDuration;
    public bool onCooldown;

    public void MoveToPosition(Transform transform)
    {
        onCooldown = true;
        transform.position = endPoint.position;
        StartCoroutine(Cooldown());
    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownDuration);
        onCooldown = false;
    }
}
