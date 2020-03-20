using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 20f;
    public float damage = 10f;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        CheckHealth();
    }

    public void TakeDamage(float damage)
    {
        anim.SetTrigger("hit");
        health -= damage;
    }

    private void Dead()
    {
        Destroy(gameObject);
    }

    private bool IsDead()
    {
        return health <= 0;
    }

    private void CheckHealth()
    {
        if(IsDead())
        {
            Dead();
        }
    }
}
