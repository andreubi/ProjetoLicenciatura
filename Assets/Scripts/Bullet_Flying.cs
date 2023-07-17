using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Flying : MonoBehaviour
{
    public float life = 5;
    private void Awake()
    {
        Destroy(gameObject, life);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bala destruida");
        if (collision.gameObject.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem player))
        {
            Debug.Log("Bullet encontrou inimigo");
            player.TakeDamage(1);
            Destroy(gameObject);

        }
        if (collision.collider.gameObject.layer != 9) //para não colidir nele mesmo e outros flying enemies
        {
            Destroy(gameObject);
        }
    }
}


