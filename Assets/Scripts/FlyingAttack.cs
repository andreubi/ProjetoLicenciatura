using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAttack : MonoBehaviour
{
    //Projectile attacks, se tiverem
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public Transform target;

    public float bulletSpeed = 15;
    public float cooldown;
    public float lastShot;

    //AUDIO
    public AudioSource src;
    public AudioClip sfxshoot;

    // Start is called before the first frame update
    void Start()
    {
        src = this.gameObject.GetComponent<AudioSource>();
        target = GameObject.FindWithTag("Player").transform;
        src.clip = sfxshoot;
    }

    // Update is called once per frame
    void Update()
    {
        //Atacar
        if (Time.time - lastShot < cooldown)
        {
            return;
        }
        lastShot = Time.time;

        //this.gameObject.transform.LookAt(target); //olhar para player antes de disparar
        bulletSpawnPoint.LookAt(target); //olhar para player antes de disparar
        src.Play();
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);


        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }

}
