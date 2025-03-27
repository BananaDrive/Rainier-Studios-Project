using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject openObject;
    public Transform highest;
    public Transform lowest;
    public float duration;

    public bool canOpen;

    public void Start()
    {
        openObject.transform.position = lowest.position;
    }
    public void OpenGate()
    {
        canOpen = false;
        StartCoroutine(OpenUp());
    }
    public IEnumerator OpenUp()
    {
        float timer = 0;
        while (timer < duration)
        {
            yield return null;
            timer += Time.deltaTime;
            float lerpvalue = Mathf.Clamp01(timer / duration);
            openObject.transform.position = new (openObject.transform.position.x, Mathf.Lerp(lowest.position.y, highest.position.y, lerpvalue));
        }
    }
}
