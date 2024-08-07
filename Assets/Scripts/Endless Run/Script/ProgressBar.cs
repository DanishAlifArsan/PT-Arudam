using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private RectTransform endPos;
    [SerializeField] private RectTransform playerIcon;
    [SerializeField] private RectTransform enemyIcon;
    [SerializeField] private float playerStartingSpeed;
    [SerializeField] private float enemyStartingSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelerationRate;
    [SerializeField] private float decreasingRate;
    [SerializeField] private float stopTime;
    [SerializeField] private float distance;
    [SerializeField] private bool isShopScene = false;
    private float decreasingCooldown = 4;
    private float decreasingTime = 0;
    private bool isStopped = false;
    private float startingSpeed, enemySpeed;

    public void Setup() {
        playerIcon.anchoredPosition = new Vector3( -419.8003f, 21f, 0);
        enemyIcon.anchoredPosition = new Vector3( -305f, 21f, 0);
        decreasingTime = decreasingCooldown;
        startingSpeed = playerStartingSpeed;
        enemySpeed = enemyStartingSpeed;
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (!isShopScene)
        {
            decreasingTime = decreasingCooldown;
            startingSpeed = playerStartingSpeed;
            enemySpeed = enemyStartingSpeed;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        float enemyPosX = enemyIcon.transform.localPosition.x + Time.deltaTime * enemySpeed;
        enemyIcon.transform.localPosition = new Vector2(enemyPosX, enemyIcon.transform.localPosition.y);
        decreasingTime -= Time.deltaTime;

        if (decreasingTime <= 0)
        {
            decreasingTime = decreasingCooldown;
            if (enemySpeed * decreasingRate > minSpeed)
            {
                 enemySpeed *= decreasingRate;
            }
            if (startingSpeed / accelerationRate < maxSpeed)
            {
                startingSpeed /= accelerationRate;
            }
        }

        if (isStopped)
        {
            return;
        }

        float playerPosX = playerIcon.transform.localPosition.x + Time.deltaTime * startingSpeed;
        playerIcon.transform.localPosition = new Vector2(playerPosX, playerIcon.transform.localPosition.y);

        if (Vector2.Distance(enemyIcon.localPosition, endPos.localPosition) <= distance)
        {
            if (isShopScene)
            {
               EndlessRunManager.instance.EndlessRunEnd(false);
            } else {
                SceneManager.LoadScene(0); // to do ke home screen
            }
        }
    }

    public void DamagePlayer() {
        StartCoroutine(StopPlayer());
    }

    private IEnumerator StopPlayer() {
        isStopped = true;
        yield return new WaitForSeconds(stopTime);
        isStopped = false;
    }

    public bool CheckDistance() {
        return Vector2.Distance(playerIcon.localPosition, enemyIcon.localPosition) <= distance;
    }
}
