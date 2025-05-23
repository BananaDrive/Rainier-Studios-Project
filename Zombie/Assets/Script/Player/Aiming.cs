using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Transform directionPoint, leftArm, rightArm;

    internal float aimDirection;
    public float xOffset, yOffset;

    void Update()
    {
        if (GameManager.Instance.isPaused)
            return;

        float aimSide = transform.eulerAngles.y == 180f? -1 : 1;
        aimDirection = Input.GetAxisRaw("Vertical") / 1f;

        directionPoint.rotation = Quaternion.Euler(directionPoint.rotation.x, transform.eulerAngles.y, aimDirection * 90); //determines the weapon direction from the player direction and its vertical input
        Vector2 rotationDistance = new(xOffset * aimSide * Mathf.Cos(aimDirection * Mathf.PI / 4), yOffset + (0.9f * Mathf.Sin(aimDirection * Mathf.PI / 4))); //makes the weapon seem like its orbiting the player
        directionPoint.position = new(transform.position.x + rotationDistance.x, transform.position.y + rotationDistance.y);

        if (leftArm != null)
            CalcDirection(leftArm);
        if (rightArm != null)
            CalcDirection(rightArm);
    }

    void CalcDirection(Transform objToRotate)
    {
        Vector3 direction = directionPoint.position - objToRotate.position;
        direction.x *= transform.eulerAngles.y == 180f? -1 : 1;
        objToRotate.rotation = Quaternion.Euler(objToRotate.eulerAngles.x, objToRotate.eulerAngles.y, Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward).eulerAngles.z);
    }
}
