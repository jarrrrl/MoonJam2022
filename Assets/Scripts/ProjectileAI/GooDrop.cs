using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooDrop : MonoBehaviour
{

    public GameObject GooDropFFXPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
        GameObject GooFFX = Instantiate(GooDropFFXPrefab, this.transform.position, Quaternion.identity);
        Destroy(GooFFX, 1f);
    }
}
