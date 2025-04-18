using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Transform weapon;
    public Movement movement;

    public float offsetX, offsetY;

    // Update is called once per frame
    void Update()
    {
        float aimSide = movement.transform.eulerAngles.y == 180f? -1 : 1;
        float aimDirection = Input.GetAxisRaw("Vertical");

        weapon.rotation = Quaternion.Euler(weapon.rotation.x, movement.transform.eulerAngles.y, aimDirection * 90); //determines the weapon direction from the player direction and its vertical input
        Vector2 rotationDistance = new(offsetX * aimSide * Mathf.Cos(aimDirection * Mathf.PI / 2), offsetY + (0.3f * Mathf.Sin(aimDirection * Mathf.PI / 2))); //makes the weapon seem like its orbiting the player
        weapon.position = new(transform.position.x + rotationDistance.x, transform.position.y + rotationDistance.y);
    }
}
