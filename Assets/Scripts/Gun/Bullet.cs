using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { A, B, Enemy }
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private BulletType type = BulletType.A;
    [SerializeField] private float speed;
    [SerializeField] private float deactivateTime = 2;
    private float deactivateTimer = 0;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (type == BulletType.Enemy) speed *= -1;
    }

    private void Update()
    {
        rb.velocity = new Vector2(0,speed * Time.deltaTime);
        if (gameObject.activeSelf)
        {
            deactivateTimer += Time.deltaTime;
            if (deactivateTimer >= deactivateTime) { deactivateTimer = 0; gameObject.SetActive(false); }
        }
        else deactivateTimer = 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (type)
        {
            default:
            case BulletType.A:
                if (col.transform.CompareTag("Enemy B")) gameObject.SetActive(false);
                if (col.transform.CompareTag("Enemy A")) gameObject.SetActive(false);
                break;
            case BulletType.B:
                if (col.transform.CompareTag("Enemy B")) gameObject.SetActive(false);
                if (col.transform.CompareTag("Enemy A")) gameObject.SetActive(false);
                break;
            case BulletType.Enemy:
                if (col.transform.CompareTag("Player")) gameObject.SetActive(false);
                break;
        }
    }
}

