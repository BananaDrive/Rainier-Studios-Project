using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxScrolling : MonoBehaviour
{
    //VALUES
    [SerializeField] float ScrollSpeed;
    [SerializeField] bool LeftScroll;

    float singleTextureWidth;
    
    void Start()
    {
        BackgroundSetup();
        if (LeftScroll) ScrollSpeed = -ScrollSpeed;
    }

    void BackgroundSetup()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        singleTextureWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }

    void Scrolling()
    {
        float delta = ScrollSpeed * Time.deltaTime;
        transform.position += new Vector3(delta, 0f, 0f);
    }

    void CheckReset()
    {
        if( (Mathf.Abs(transform.position.x) - singleTextureWidth) > 0)
        {
            transform.position = new Vector3(0.0f, transform.position.y, transform.position.z);
        }
    }
    
    void Update()
    {
        Scrolling();
        CheckReset();
    }
}
