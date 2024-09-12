using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemObject : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y < -8f)
        {
            Disappear();
        }
    }
    public void Disappear()
    {
        Game.Battle.entities.Remove(gameObject);
        gameObject.SetActive(false);
        Destroy(gameObject, 0.05f);
    }
}
