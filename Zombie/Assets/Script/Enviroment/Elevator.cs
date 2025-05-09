using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public float cooldownDuration, duration;
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
    public IEnumerator Overlay()
    {
        Color currentColor;
        float timer = 0;
        while (timer < duration)
        {
            yield return null;
            timer += Time.deltaTime;
            float lerpvalue = Mathf.Clamp01(timer / duration);
            currentColor = GameManager.Instance.UIManager.blackScreen.color;
            currentColor.a = Mathf.Lerp(0f, 1f, lerpvalue);
            GameManager.Instance.UIManager.blackScreen.color = currentColor;
        }
    }
}
