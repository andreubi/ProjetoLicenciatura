using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 30;

    //AUDIO
    public AudioSource src;
    public AudioClip sfxshoot;
    public PlayerHealthSystem hpScript;
    public int playerHp;
    private void Awake()
    {
        hpScript = GameObject.Find("Player").GetComponent<PlayerHealthSystem>();
        src = this.gameObject.GetComponent<AudioSource>();
        src.clip = sfxshoot;
    }
    private void Update()
    {
        playerHp = hpScript.health;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (playerHp != 0)
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                src.Play();
            }
        }
    }
}
