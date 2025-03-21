using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Transform weapon;
    public Movement movement;

    // Update is called once per frame
    void Update()
    {
        float aimSide = movement.sprite.flipX ? -1 : 1;
        float aimDirection = Input.GetAxisRaw("Vertical");

        weapon.rotation = Quaternion.Euler(weapon.rotation.x, movement.sprite.flipX ? 180 : 0, aimDirection * 90f); //determines the weapon direction from the player direction and its vertical input
        Vector2 rotationDistance = new(0.15f * aimSide * Mathf.Cos(aimDirection * Mathf.PI / 2), 0.3f + (0.3f * Mathf.Sin(aimDirection * Mathf.PI / 2))); //makes the weapon seem like its orbiting the player
        weapon.position = new(transform.position.x + rotationDistance.x, transform.position.y + rotationDistance.y);
    }
}
