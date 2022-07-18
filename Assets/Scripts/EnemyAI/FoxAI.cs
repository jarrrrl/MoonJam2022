using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxAI : EnemyAI
{
    Transform foxTransform;
    public float speed = 4f;
    public float projSpeed = 200f;
    public GameObject foxProjectilePrefab;
    public GameObject foxFireRing;
    public List<FoxFire> foxFireProjectiles;

    public GameObject playerObject;
    public Vector3 direction = new Vector3(0, 0, 1);

    public float foxFireLastTime = 5f;
    public float reloadTime = 3f;

    private void Start()
    {
        foxTransform = transform;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Shoot());
    }

    protected override void Update()
    {
        base.Update();
        foxFireRing.transform.RotateAround(foxTransform.position, direction, projSpeed * Time.deltaTime);

    }

    private IEnumerator Shoot()
    {
        while (true)
        {

            foreach (FoxFire foxFireProjectile in foxFireProjectiles)
            {
                if (!foxFireProjectile.gameObject.activeSelf || !isActive)
                {
                    yield return null;
                }
                else
                {
                    yield return new WaitForSeconds(reloadTime);

                    foxFireProjectile.gameObject.SetActive(false);
                    GameObject foxFireObject = Instantiate(foxProjectilePrefab, foxFireProjectile.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(foxFireLastTime);
                    Destroy(foxFireObject);


                    foxFireProjectile.gameObject.SetActive(true);
                }
            }
        }
    }

}
