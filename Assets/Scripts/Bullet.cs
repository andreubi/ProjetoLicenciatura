using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 5;
    private void Awake()
    {
        Destroy(gameObject, life);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bala destruida");
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            Debug.Log("Bullet encontrou inimigo");
            enemyComponent.TakeDamage(1);

        }
        if (collision.collider.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
