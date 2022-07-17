using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drip : MonoBehaviour
{
    public Transform gooDrop;
    private float gooCooldown = 0.5f;
    // Update is called once per frame
  

    private void Start()
    {
        Destroy(gameObject, 4f);
        StartCoroutine(GooDrip());

    }

    private IEnumerator GooDrip()
    {
        while (true)
        {
            Instantiate(gooDrop, new Vector3(Random.Range(-1f, 1f) + this.transform.position.x, this.transform.position.y,
        this.transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(gooCooldown);
        }

    }
}
