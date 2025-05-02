using UnityEngine;

public class EnviromentCollision : MonoBehaviour
{
    public LayerMask layersToTrigger;
    public Animator anim;

    public string triggerName;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if ((layersToTrigger & (1 << other.gameObject.layer)) != 0)
        {
            anim.SetTrigger(triggerName);
        }
    }
}
