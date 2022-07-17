using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float health = 10f;
    public HealthController healthController;

    // Start is called before the first frame update
    void Start()
    {
        healthController = this.GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
