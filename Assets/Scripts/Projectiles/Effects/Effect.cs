using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles.effects
{


    public abstract class Effect : MonoBehaviour
    {
        [Header("Variables")]
        public float effectRate = 1f;
        public float effectTimer = 0f;

        public int damage = 2;
        
        public GameObject visualEffectPrefab;
        [HideInInspector]public Transform hitObject;

        protected virtual void Start()
        {
            GameObject clone = Instantiate(visualEffectPrefab, hitObject.transform);
            clone.transform.position = transform.position;
            clone.transform.rotation = transform.rotation;
        }
        // Update is called once per frame
        protected virtual void Update()
        {
            effectRate += Time.deltaTime;
            if(effectRate >= 1f / effectRate)
            {
                RunEffect();
            }
        }

        public abstract void RunEffect();
        
    }
}