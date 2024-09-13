using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSprite : MonoBehaviour
{
    public float speed;
    public GameObject spriteObject; 

    void Update()
    {
        if (spriteObject != null)
        {
            float rotationAmount = 360f * speed * Time.deltaTime;
            spriteObject.transform.Rotate(Vector3.forward, rotationAmount);
        }
    }
}