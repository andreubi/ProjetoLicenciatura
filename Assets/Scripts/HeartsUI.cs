using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public PlayerHealthSystem playerhealth;

    public Image[] hearts;
    public Sprite fullheart;
    public Sprite emptyheart;

    // Start is called before the first frame update
    void Start()
    {
        health = playerhealth.health;
    }

    // Update is called once per frame
    void Update()
    {
        health = playerhealth.health;
        if (health > numOfHearts)
        {
            numOfHearts = health;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullheart;
            }
            else
            {
                hearts[i].sprite = emptyheart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

    }
}
