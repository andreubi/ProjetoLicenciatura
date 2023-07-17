using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public void Setup(int score)
    {
        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        scoreText.text = "SCORE: " + score.ToString();
    }
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
