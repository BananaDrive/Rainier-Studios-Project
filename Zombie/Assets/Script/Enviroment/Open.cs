using System.Collections;
using System.Threading;
using UnityEngine;

public class Open : MonoBehaviour
{
    public GameObject openObject;
    public Transform highest;
    public Transform lowest;
    public float duration;

    public void Start()
    {
        openObject.transform.position = lowest.position;
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
            openObject.transform.position = new (transform.position.x, Mathf.Lerp(lowest.position.y, highest.position.y, lerpvalue));
        }
    }

}
