using UnityEngine;

public class Placeable : MonoBehaviour
{
    public LayerMask layerToHit, layerToAvoid;
    public float despawnTime;

    public void Despawn()
    {
        Destroy(this.gameObject);
    }
}
