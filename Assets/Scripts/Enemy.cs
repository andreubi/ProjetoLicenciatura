using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public GameObject GameObject;


    [SerializeField] float health, maxhealth = 3.0f; //serialize mostra values no inspector
    public static event Action<Enemy> OnEnemyKilled;
    public static event Action<Enemy> OnEnemySpawned;

    // Params for taking dmg function
    MeshRenderer meshRenderer;
    Color originalColor;
    float flashtime = .15f;

    private void Awake()
    {
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;

    }
    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDestroy()
    {
        if (GameObject.FindGameObjectWithTag("EnemySpawner") != null)
        {
            GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<Enemy_Spawner>().spawnedEnemies.Remove(gameObject);
        }

    }

    //TakeDamage is called on bullet "OnCollisionEnter", no need to call it here.
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(GameObject);
            OnEnemyKilled?.Invoke(this); //if enemy killed, envia um aviso para o resto do sistema
        }
    }

    /*
     * esta função serve somente para quando o spawner spawnar um inimigo, vamos acessar esta função desse inimigo e chamar o evento
     * Está feita deste modo pois preciso de invocar um tipo "Enemy" no Enemy_Spawner.cs, porém somente tenho acesso a GameObject, e não dá para converter para Enemy
    */
    public void EnemySpawned()
    {
        OnEnemySpawned?.Invoke(this);
    }
    private void OnCollisionEnter(Collision collision)
    {
        FlashStart();
    }
    void FlashStart()
    {
        meshRenderer.material.color = Color.red;
        Invoke("FlashStop", flashtime);
    }
    void FlashStop()
    {
        meshRenderer.material.color = originalColor;
    }





}

