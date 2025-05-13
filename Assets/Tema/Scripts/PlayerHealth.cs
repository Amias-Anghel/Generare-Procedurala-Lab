using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float damage;

    [SerializeField] private Slider lifeSlider;

    void Start()
    {
        health = 100;
        damage = 10;   
    }


    public void TakeDamage(float dmg) {
        health -= dmg;

        if (lifeSlider != null)
            lifeSlider.value = 0.01f * health;
        
        if (health <= 0) {
            Debug.Log("PLAYER DEAD");
        }
    }

}
