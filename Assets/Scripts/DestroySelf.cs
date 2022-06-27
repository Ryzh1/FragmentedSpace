using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float DestroyTime = 1f;
    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
}
