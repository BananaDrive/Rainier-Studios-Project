using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Transform weapon;

    // Update is called once per frame
    void Update()
    {
        float aimDirection = Input.GetAxisRaw("Vertical");
        weapon.rotation = Quaternion.Euler(weapon.rotation.x, weapon.rotation.y, aimDirection * 60f);
        Vector2 rotationDistance = new(0.5f * (1 - Mathf.Abs(aimDirection)) + 1f, aimDirection * 0.9f);
        weapon.position = new(transform.position.x + rotationDistance.x, transform.position.y + rotationDistance.y);
    }
}
