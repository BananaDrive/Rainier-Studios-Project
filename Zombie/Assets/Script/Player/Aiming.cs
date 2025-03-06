using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Transform weapon;
    public Movement movement;
    public float verticalAimMax;

    // Update is called once per frame
    void Update()
    {
        float aimSide = movement.sprite.flipX ? -1 : 1;
        float aimDirection = Input.GetAxisRaw("Vertical");

        weapon.rotation = Quaternion.Euler(weapon.rotation.x, movement.sprite.flipX ? 180 : 0, aimDirection * 60f); //determines the weapon direction from the player direction and its vertical input
        Vector2 rotationDistance = new(0.25f * (aimSide - Mathf.Abs(aimDirection) * aimSide) + aimSide, aimDirection * 1.2f); //makes the weapon seem like its orbiting the player
        weapon.position = new(transform.position.x + rotationDistance.x, transform.position.y + rotationDistance.y);
    }
}
