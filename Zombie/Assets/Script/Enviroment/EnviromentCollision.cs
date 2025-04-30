using UnityEngine;

public class EnviromentCollision : MonoBehaviour
{
    public LayerMask layersToTrigger;
    public Animator anim;
    public AnimationClip animClip;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if ((layersToTrigger & (1 << other.gameObject.layer)) != 0)
        {
            anim.Play(animClip.ToString());
        }
    }
}
