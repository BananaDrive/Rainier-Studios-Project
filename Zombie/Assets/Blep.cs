using UnityEngine;

public class Blep : MonoBehaviour
{
    public Animator Anim;
    public bool Reloading=false;
    public Weapon weapon;
    public AnimationClip clip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (weapon.weaponReload == true)
        {
            Anim.SetTrigger("Reloading");
        }
        else
        {
            Anim.SetTrigger("NoReload");
        }
    }
}
