using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private LevelManager levelManager;
    public GameObject checkpointFFXPrefab;

    private void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelManager.lastCheckpointPos = transform.position;
            Vector2 heartVec = new(transform.position.x, transform.position.y + 2f);
            GameObject heartObject = Instantiate(checkpointFFXPrefab, heartVec, Quaternion.identity);
            Destroy(heartObject, 1f);
        }
    }
}
