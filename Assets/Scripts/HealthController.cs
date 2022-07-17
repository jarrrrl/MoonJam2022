using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        if (CompareTag("Player")) health = 20f;
        else health = GetComponent<EnemyAI>().health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealth(float newHealth)
    {
        health = newHealth;
        print(health);
        if(newHealth <= 0)
        {
            KillEntity();
        }
    }

    private void KillEntity()
    {
        Destroy(gameObject);
        print("Killed");
    }
}
