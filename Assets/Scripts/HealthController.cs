using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    protected float health;
    // Start is called before the first frame update
    void Start()
    {
        health = 1f;
    }

    // Update is called once per frame

    public void ChangeHealth(float newHealth)
    {
        health += newHealth;
        print(health);
        if(newHealth <= 0)
        {
            KillEntity();
        }
    }

    private void KillEntity()
    {
        if (CompareTag("Player"))
        {
            GetComponent<RespawnPlayer>().Respawn();
        }
        else
        {
            gameObject.GetComponent<EnemyAI>().KillEntity();
        }
    }
}
