using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler : MonoBehaviour
{
    [SerializeField] private AudioClip attackSound;
    private Animator anim;
    private Health health;
    private int damage;
    private bool isEnemy;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartAttack(Health _health, int _damage, bool _isEnemy) {
        health = _health;
        damage = _damage;
        isEnemy = _isEnemy;
        health.qteBar.gameObject.SetActive(false);
        anim.SetTrigger("attack");
    }

    public void PlaySound() {
        AudioManager.instance.PlaySound(attackSound);
        health.CalculateDamage(damage, isEnemy);
    }

    public void EndAttack() {
        health.qteBar.gameObject.SetActive(true);
        health.EndAttack();
    }   
}
