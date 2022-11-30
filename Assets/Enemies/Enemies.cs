using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    private NavMeshAgent agent;
    private float health = 100;

    void Awake()
	{
        agent = GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        health = 100;
        transform.position = transform.parent.position;
        agent.destination = EnemySpawner.basePos;
    }

    public void TakeDamage(float damage = 20)
	{
        health -= damage;
        if(health <= 0)
		{
            PlayerManager.instance.Gold = 25;
            gameObject.SetActive(false);
		}
	}

    private void OnTriggerEnter(Collider col)
	{
        if(col.gameObject.name == "baseRange")
		{
            PlayerManager.instance.Health = -10;
            gameObject.SetActive(false);
        }
	}
}
