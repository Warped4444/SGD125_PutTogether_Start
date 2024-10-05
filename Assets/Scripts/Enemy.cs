using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    int currentNode;
    int previousNode;
    public enum EnemyState
    {
        patrol,
        chase
    };
    EnemyState enemyState = EnemyState.patrol;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentNode = Random.Range(0,GameManager.gm.nodes.Length);
        previousNode = currentNode;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.patrol: Patrol(); break;
                    case EnemyState.chase: Chase(); break;
        }

        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Node")
        {
            currentNode = Random.Range(0, GameManager.gm.nodes.Length);
            while(currentNode == previousNode)
            {
                currentNode = Random.Range(0, GameManager.gm.nodes.Length);
            }
            previousNode = currentNode;
        }
        if (other.tag == "Player")
        {
            enemyState = EnemyState.chase;
        }


    }

    void Patrol()
    {
        
        agent.destination = GameManager.gm.nodes[currentNode].position;
    }

    void Chase()
    {
        agent.destination = GameManager.gm.player.transform.position;
    }
}
