using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float playerHealth;
    [SerializeField] private float enemyHealth;
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private Image enemyHealthBar;

    private float currentPlayerHealth, currentEnemyHealth;

    // Start is called before the first frame update
    private void Start()
    {
        currentPlayerHealth = playerHealth;
        currentEnemyHealth = enemyHealth;
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
            SceneManager.LoadScene(0);
        }
    }

    private void DamageEnemy(int damage) {
        currentEnemyHealth -= damage;
        enemyHealthBar.fillAmount = currentEnemyHealth/enemyHealth;

        if (currentEnemyHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
