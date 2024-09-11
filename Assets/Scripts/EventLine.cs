using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventLine : MonoBehaviour
{
    public SpriteRenderer[] itemSprites;
    public TextMeshProUGUI[] itemTexts;

    private void Update()
    {
        if (transform.position.y < -8f)
        {
            Game.Battle.entities.Remove(gameObject);
            Disappear();
        }
    }

    public void Disappear()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 0.05f);
    }
}
