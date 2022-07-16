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

    public Vector3 direction = new Vector3(0, 0, 1);
    private void Start()
    {
        foxTransform = this.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        foxFireRing.transform.RotateAround(foxTransform.position, direction, projSpeed * Time.deltaTime);


    }

}
