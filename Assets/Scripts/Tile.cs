using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    [SerializeField] internal Color mountainColor, riverColor, grasslandsColor, forestColor;
    [SerializeField] internal SpriteRenderer spriteRenderer;

    private static System.Random rnd = new System.Random();
    private static int random = rnd.Next(-1000, 1000);
    private static BuyMenuManager buyMenu = null;

    private void Start()
    {
        if (buyMenu == null) buyMenu = Transform.FindAnyObjectByType<BuyMenuManager>();
    }

    public void Init(bool isOffset)
    {
        float noise = Mathf.PerlinNoise(this.transform.position.x / 16 + random, this.transform.position.y / 9 + random);
        if (noise < 0.25f)
        {
            spriteRenderer.color = riverColor;
        }
        else if (noise >= 0.25f && noise < 0.5f)
        {
            spriteRenderer.color = grasslandsColor;

        }
        else if (noise >= 0.5f && noise < 0.75f)
        {
            spriteRenderer.color = forestColor;

        }
        else {
            spriteRenderer.color = mountainColor;
        }
    }
    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    void OnMouseExit() {
        highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        buyMenu.toggle(this);
    }
}

