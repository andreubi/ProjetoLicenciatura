using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshFlying : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;
    public float playerx;
    public float playerz;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerx = player.transform.position.x;
        playerz = player.transform.position.z;

        agent.SetDestination(player.transform.position);
    }

}
