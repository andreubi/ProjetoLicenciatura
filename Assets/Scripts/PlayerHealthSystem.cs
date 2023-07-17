using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public int MaxHealth = 5;
    public int health;
    public GameManager gameManager;
    //AUDIO
    public AudioSource src, bgm;
    public AudioClip sfxtakedmg, sfxgameover, sfxempty;
    // Start is called before the first frame update
    void Start()
    {
        bgm = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        src = this.gameObject.GetComponent<AudioSource>();
        src.clip = sfxtakedmg;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        health = MaxHealth;
    }
    public void TakeDamage(int damageAmount)
    {
        src.Play();
        health -= damageAmount;

        if (health == 0)
        {
            bgm.Stop();
            src.clip = sfxgameover;
            src.volume = 0.2f;
            src.Play();
            /*
             * Esta função deveria estar presente no script GameManager, e ser chamada aqui, faria mais sentido.
            */
            gameManager.GameOver();

        }
    }
}
