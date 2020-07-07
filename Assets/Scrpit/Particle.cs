using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float LifeTime;
    private float CurTime = 0;

    private void OnEnable()
    {
        CurTime = 0;
    }

    private void Update()
    {
        CurTime += Time.deltaTime;

        if (CurTime > LifeTime)
            this.gameObject.SetActive(false);
    }
}
