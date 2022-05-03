using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerHealthManager : MonoBehaviour
{
    public float playerHealth = 100f;
    bool isGameOver;
    [SerializeField] private Text playerHealthText;
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthText.text = playerHealth.ToString();

        if (isGameOver)
        {
            Debug.Log("Game Over");
        }
    }

    public void takeDamage(float damagetaken)
    {
        playerHealth -= damagetaken;
        Debug.Log("Damage -> " + damagetaken);
        if (playerHealth <= 0f)
        {
            isGameOver = true;
        }
    }
}
