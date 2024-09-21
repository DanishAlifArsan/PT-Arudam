using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float playerHealth;
    [SerializeField] private float enemyHealth;
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private Image enemyHealthBar;
    [SerializeField] private PlayableDirector battleEndDirector;
    [SerializeField] private bool isShop = false;
    [SerializeField] private AudioClip enemyAttackSound;
    [SerializeField] private Battler player, enemy;
    public RectTransform qteBar;
    [SerializeField] private AudioClip victoryMusic;
    [SerializeField] private AudioClip loseMusic;
    [SerializeField] private AudioSource battleMusic;
    private float currentPlayerHealth, currentEnemyHealth;
    private bool isWin;

    public void Setup(float health) {
        currentPlayerHealth = playerHealth;
        enemyHealth = health;
        currentEnemyHealth = enemyHealth;
        playerHealthBar.fillAmount = 1;
        enemyHealthBar.fillAmount = 1;
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (!isShop)
        {
            currentPlayerHealth = playerHealth;
            currentEnemyHealth = enemyHealth;
        } 
    }

    public void Damage(Slider.Status status) {
        switch (status)
        {
            case Slider.Status.Full:
                AttackEnemy(2);
                break;
            case Slider.Status.Half:
                AttackEnemy(1);
                break;
            case Slider.Status.Miss:
                AttackPlayer(1);
                break;
        }
    }

    private void AttackPlayer(int damage) {
        
        AudioManager.instance.PlaySound(enemyAttackSound);
        enemy.StartAttack(this, damage, false);
    }
    private void AttackEnemy(int damage) {
        player.StartAttack(this, damage, true);
    }
    public void CalculateDamage(int damage, bool isEnemy) {
        if (isEnemy)
        {
            currentEnemyHealth -= damage;
            enemyHealthBar.fillAmount = currentEnemyHealth/enemyHealth;
        } else {
            currentPlayerHealth -= damage;
            playerHealthBar.fillAmount = currentPlayerHealth/playerHealth;
        }
    }

    public void EndAttack() {
        if (currentEnemyHealth <= 0)
        {
            isWin = true;
            AudioManager.instance.PlaySound(victoryMusic);
            battleMusic.Stop();
            battleEndDirector.Play();
        } else if (currentPlayerHealth <= 0)
        {
            isWin = false;
            AudioManager.instance.PlaySound(loseMusic);
            battleMusic.Stop();
            battleEndDirector.Play();
        }
    }

    public void BattleEnd() {
        if (isShop)
        {
            BattleManager.instance.BattleEnd(isWin);
        } else {
            SceneManager.LoadScene(0);
        }
    }
}
