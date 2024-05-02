using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public void SetAnimationSize(float _size)
    {
        Vector3 scale = transform.localScale;
        scale *= _size / 300.0f;
        transform.localScale = scale;
    }

    public void DoDestroyEvent()
    {
        Destroy(gameObject);
    }
}
