using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoopBackground : MonoBehaviour
{
    public GameObject[] LoopImages;
    public float updateDistance = 5.96f * 4;
    private float updateYPos = -10f;

    private void Update()
    {
        foreach (GameObject image in LoopImages)
        {
            if(image.transform.position.y < updateYPos)
            {
                image.transform.position += Vector3.up * updateDistance;
            }
        }
    }
}
