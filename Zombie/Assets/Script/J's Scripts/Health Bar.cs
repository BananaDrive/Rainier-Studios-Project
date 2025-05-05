using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform fill;
    float size;
    float offset;

    void Start()
    {
        size = fill.localScale.x;
    }
    public void UpdateHealth(float ratio)
    {
        fill.localScale = new Vector2(size * ratio, fill.localScale.y);
        fill.localPosition = new Vector2((1 - size * ratio) / 2, fill.localPosition.y);
    }
}