using System;
using Unity.VisualScripting;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public SpriteRenderer sprite;
    internal GameObject player;

    public float regen, damage, fireRate, speed, health, accuracy;
    public string itemName;



    public void UseItem()
    {
        player.GetComponent<Health>().currentHealth += regen;
        
    }
}
