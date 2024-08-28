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
    private float currentPlayerHealth, currentEnemyHealth;
    private bool isWin;

    public void Setup(float health) {
        currentPlayerHealth = playerHealth;
        currentEnemyHealth = health;
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
                DamageEnemy(2);
                break;
            case Slider.Status.Half:
                DamageEnemy(1);
                break;
            case Slider.Status.Miss:
                DamagePlayer(1);
                break;
        }
    }

    private void DamagePlayer(int damage) {
        currentPlayerHealth -= damage;
        playerHealthBar.fillAmount = currentPlayerHealth/playerHealth;

        if (currentPlayerHealth <= 0)
        {
            isWin = false;
            battleEndDirector.Play();
        }
    }

    private void DamageEnemy(int damage) {
        currentEnemyHealth -= damage;
        enemyHealthBar.fillAmount = currentEnemyHealth/enemyHealth;

        if (currentEnemyHealth <= 0)
        {
            isWin = true;
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
