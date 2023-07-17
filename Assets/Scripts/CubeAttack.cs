using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAttack : MonoBehaviour
{
    [SerializeField]
    public GameObject itself;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Cube Attack");
        if (collision.gameObject.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem player))
        {
            Debug.Log("Cubo collision detected");
            player.TakeDamage(1);
        }
    }
}
