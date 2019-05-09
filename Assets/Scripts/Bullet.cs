using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    public float speed = 50f;
    public GameObject BulletHolePrefab;
    public Transform line;

    private Rigidbody rigid;


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        line.transform.rotation = Quaternion.LookRotation(rigid.velocity);
    }

    public void Fire(Vector3 lineOrigin, Vector3 direction)
    {
        rigid.transform.position = lineOrigin;
        rigid.AddForce(direction * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision col)
    {
        ContactPoint contact = col.contacts[0];

        Instantiate(BulletHolePrefab, contact.point, Quaternion.LookRotation(contact.normal));

        Destroy(gameObject);
    }
}
