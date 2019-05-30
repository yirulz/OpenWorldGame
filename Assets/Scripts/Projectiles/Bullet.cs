using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Projectiles.effects;

namespace Projectiles
{
    public class Bullet : Projectile
    {
        [Header("References")]
        public float speed = 50f;
        public GameObject effectPrefab;
        public Transform line;

        private Rigidbody rigid;
        private Vector3 start, end;


        void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }
        void Start()
        {
            start = transform.position;
        }

        // Update is called once per frame
        public override void Fire(Vector3 lineOrigin, Vector3 direction)
        {
            // Set line position to origin
            line.position = lineOrigin;
            // Set bullet flying in direction with speed
            rigid.AddForce(direction * speed, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision col)
        {
            end = transform.position;

            // Get contact point from collision
            ContactPoint contact = col.contacts[0];

            // Get bulletDirection
            Vector3 bulletDir = rigid.velocity.normalized;

            // Jordan
            Quaternion lookRotation = Quaternion.LookRotation(bulletDir);
            Quaternion rotation = lookRotation * Quaternion.AngleAxis(-90, Vector3.forward);

            // Ben
            // Spawn a BulletHole on that contact point
            GameObject clone = Instantiate(effectPrefab, contact.point, rotation);
            // Get angle between normal and bullet dir
            float impactAngle = 180 - Vector3.Angle(bulletDir, contact.normal);
            clone.transform.localScale = clone.transform.localScale / (1 + impactAngle / 45);
            Destroy(clone, 5f);
            Effect effect = clone.GetComponent<Effect>();
            effect.damage += damage;
            effect.hitObject = col.transform;

            // Destroy self
            Destroy(gameObject);
        }
    }
}