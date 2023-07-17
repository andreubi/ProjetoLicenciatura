using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int enemiesCount = 0;
    public int wave = 0;
    public int score = 0;
    public ChangeEnemiesLeftText changeEnemiesLeftScript;
    public ChangeWaveText changeWaveTextScript;
    public ChangeScoreText changeScoreScript;
    public PlayerHealthSystem playerHP;
    public int CurrentMaxPlayerHP;
    public Gun playerGun;
    public bool healthRestored;
    public bool gunUpgraded;

    //Game Over
    public GameOverScreen gameOverScreen;
    public PlayerInput playerInputs;
    public AudioListener playerAudio;

    private void Awake()
    {
        Time.timeScale = 1.0f; //define physics engine speed to 1.0f (because later its changed from 1.0f).

        /*
         * Standard code for variable declaration in Unity, nothing special
         */
        playerAudio = GameObject.Find("Player").GetComponent<AudioListener>();
        gameOverScreen = GameObject.Find("BackgroundGameOver").GetComponent<GameOverScreen>();
        playerHP = GameObject.Find("Player").GetComponent<PlayerHealthSystem>();
        CurrentMaxPlayerHP = playerHP.MaxHealth;
        playerGun = GameObject.Find("Weapon_01").GetComponent<Gun>();

        /*
         * Initial count of how many GameObjects with tag "ENEMY" exist to update variable "enemiesCount", used for text
         */
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            this.enemiesCount++;

        }
        UpdateEnemiesText();
    }
    void Start()
    {

        /*
         * One of many possible approaches to keep mouse cursor inside game window and avoid unwanted clicks outside of the game.
         */
        Cursor.visible = false;
        // Locks the cursor
        Cursor.lockState = CursorLockMode.Locked;
        // Confines the cursor

        /*
         * More variable code declaration, difference being that function Start() starts at first frame, and Awake() starts BEFORE the first frame.
         */
        changeEnemiesLeftScript = GameObject.Find("EnemiesLeftText").GetComponent<ChangeEnemiesLeftText>();
        changeWaveTextScript = GameObject.Find("WaveText").GetComponent<ChangeWaveText>();
        changeScoreScript = GameObject.Find("ScoreText").GetComponent<ChangeScoreText>();
    }
    private void OnEnable()
    {
        /*
         * Assign Events called from other scripts to desired functions in gameManager
         * For example, when an enemy is killed, the event needs to be handled by the enemy kill "tracker", in this case, "HandleEnemyDefeated"
         */
        Enemy.OnEnemyKilled += HandleEnemyDefeated;
        Enemy_Spawner.OnEnemySpawned += HandleEnemySpawned;
        Enemy_Spawner.OnWaveUpdate += HandleWaveUpdated;
    }
    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= HandleEnemyDefeated;
        Enemy_Spawner.OnEnemySpawned -= HandleEnemySpawned;
        Enemy_Spawner.OnWaveUpdate -= HandleWaveUpdated;

    }

    private void UpgradeSystem(int score)
    {
        /*
         * This functions updates some player variables to give him an advantage as the game progresses further (score)
         * Such as bullet speed for better accuracy and more Healt
         */
        if (wave == 3 && playerHP.MaxHealth == 3)
        {
            playerHP.health = 5;
            playerHP.MaxHealth = 5;
            CurrentMaxPlayerHP = 5;
        }
        if (score >= 1000 && !gunUpgraded)
        {
            playerGun.bulletSpeed = 50;
        }
        if (wave % 2 == 0 && !healthRestored)
        {
            playerHP.health = CurrentMaxPlayerHP;
            healthRestored = true;
        }
    }


    /*
     * The following functions are Event handlers, when they catch an event, they run the assigned "UpdateXtext" so that
     * the text in the player UI is updated to display the corrent info
    */

    void HandleEnemyDefeated(Enemy enemy)
    {
        this.enemiesCount--;
        if (enemy.gameObject.layer == 9)// Flying Layer
        {
            score += 200;
        }
        if (enemy.gameObject.layer == 7)// Ground Layer
        {
            score += 100;
        }
        UpdateScoreText();
        UpdateEnemiesText();
    }
    void HandleEnemySpawned(GameObject gameobject)
    {
        this.enemiesCount++;
        UpdateEnemiesText();
    }
    void HandleWaveUpdated(int wave)
    {
        this.wave++;
        UpdateWaveText();
    }


    /*
     * Functions that update the GameObjects responsible for showing the UI to the player.
     */
    private void UpdateEnemiesText()
    {
        Debug.Log("GameManager 4");
        Debug.Log(enemiesCount);
        changeEnemiesLeftScript.TextChange(enemiesCount);
    }
    private void UpdateWaveText()
    {
        Debug.Log(wave);
        changeWaveTextScript.TextChange(wave);
    }
    private void UpdateScoreText()
    {
        Debug.Log(score);
        UpgradeSystem(score);
        changeScoreScript.TextChange(score);
    }
    public void GameOver()
    {
        StartCoroutine(ScaleTime(1.0f, 0.0f, 3.0f)); // slow motion

    }


    /*
     * Slow motion, this function receives a start, end and time variables
     * 
     * lets say we have 3 variables:
     * 
     * start is 1.0f
     * end is 0.0f
     * time is 3.0f
     * 
     * All this function does is reduce the value from "start" variable until it reaches "end" value, the ammount it reduces the variable by depends on the "time" variable
     * So if I want to reduce from 1.0f to 0.0f in a matter of 3 seconds, I have to reduce the variable by 0.(3)4f each second. Each second has 60 frames so every frame reduces
     * the variable by 0.(3)4 / 60.
     * 
     */
    IEnumerator ScaleTime(float start, float end, float time)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while (timer < time)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer / time);
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }
        gameOverScreen.Setup(score);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = end;
    }
    // Update is called once per frame
    // Unused for this script
    void Update()
    {

    }
}
