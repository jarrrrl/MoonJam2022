using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxAI : MonoBehaviour
{
    Transform foxTransform;
    Rigidbody2D rb;
    public float speed = 4f;
    public float projSpeed = 200f;
    public GameObject foxProjectilePrefab;
    public GameObject foxFireRing;
    public List<FoxFire> foxFireProjectiles;

    public GameObject playerObject;


    public Vector3 direction = new Vector3(0, 0, 1);
    private void Start()
    {
        foxTransform = this.transform;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        foxFireRing.transform.RotateAround(foxTransform.position, direction, projSpeed * Time.deltaTime);


    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            foreach (FoxFire foxFireProjectile in foxFireProjectiles)
            {
                if (!foxFireProjectile.gameObject.activeSelf)
                {
                    yield return null;
                }
                else
                {
                    yield return new WaitForSeconds(3f);

                    foxFireProjectile.gameObject.SetActive(false);
                    GameObject foxFireObject = Instantiate(foxProjectilePrefab, foxFireProjectile.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(10f);
                    Destroy(foxFireObject);


                    foxFireProjectile.gameObject.SetActive(true);
                }
            }
        }
    }

}
