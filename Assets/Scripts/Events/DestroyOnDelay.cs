using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDelay : MonoBehaviour
{
    public float destroyDelay = 5f;

    public void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
