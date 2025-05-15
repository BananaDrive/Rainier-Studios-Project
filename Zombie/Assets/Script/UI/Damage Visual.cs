using System.Collections;
using UnityEngine;

public class DamageVisual : MonoBehaviour
{

    public void Start()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        transform.position = screenPosition;

        
    }

    //public IEnumerator FadeAndMove()
//{
    //float timer = 0;
    //Vector3 startPos = transform.position;
    //Color startColor = color;

    //while (timer < fadeDuration)
    //{
        //transform.position = startPos + (Vector3.up * moveSpeed * (timer / fadeDuration));
        //text.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1, 0, timer / fadeDuration));
        //timer += Time.deltaTime;
        //yield return null;
    //}
    //Destroy(gameObject);
//}
    
}
