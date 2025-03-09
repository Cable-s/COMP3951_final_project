using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private Color mountainColor, riverColor, grasslandsColor, forestColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private static System.Random rnd = new System.Random();
    private static int random = rnd.Next(-1000, 1000);
    private GameObject buyMenu;
    static private bool BuyMenuOpen = false;

    public void Start()
    {
        buyMenu = GameObject.Find("BuyMenu");
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
        if (BuyMenuOpen)
        {
            buyMenu.transform.position = new Vector3(buyMenu.transform.position.x - 200, buyMenu.transform.position.y, 0);
            BuyMenuOpen = false;
            //if (spriteRenderer.color == riverColor) {
            //    cardName.text = "River";
            //}
            //else if (spriteRenderer.color == grasslandsColor)
            //{
            //    cardName.text = "Grasslands";
///
            //}
            //else if (spriteRenderer.color == forestColor)
            //{
            //    cardName.text = "Forest";
            //}
            //else
            //{
            //    cardName.text = "Mountain";
            //}
        }
        else
        {
            buyMenu.transform.position = new Vector3(buyMenu.transform.position.x + 200, buyMenu.transform.position.y, 0);
            BuyMenuOpen = true;
        }
    }
}

