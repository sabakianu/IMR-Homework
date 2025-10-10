using System.Collections;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float damage = 10f;
    [SerializeField] float speed = 1f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public float GetHealth() => health;
    public float GetDamage() => damage;
    public float GetSpeed() => speed;

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }
    public void TakeDamageAnim()
    {
        anim.SetTrigger("GetDamage");
    }
    public void DieAnim()
    {
        anim.SetTrigger("Die");
    }
    public void Die()
    {
        Destroy(gameObject, 1f);
    }
    public void SetWalking(bool state)
    {
        anim.SetBool("Present", state);
    }

    public void PlayAttack()
    {
        anim.SetTrigger("Attack");
    }
}
