using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drip : MonoBehaviour
{
    public Transform gooDrop;
    private float gooCooldown = 0.8f;
    public LayerMask layerMask;
    public float gooSpread;
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
            gooSpread = Random.Range(-1f, 1f);
            Transform gooDroplet = Instantiate(gooDrop, new Vector3(gooSpread + this.transform.position.x, this.transform.position.y,
        this.transform.position.z), Quaternion.identity);
            if (Physics2D.OverlapCircle(gooDroplet.position, 0.1f, layerMask)) Destroy(gooDroplet.gameObject);
            yield return new WaitForSeconds(gooCooldown);
        }

    }
}
