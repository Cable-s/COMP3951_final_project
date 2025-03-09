using TMPro;
using UnityEngine;

public class BuyMenuManager : MonoBehaviour
{
    private TextMeshProUGUI SideBarName;
    static private bool BuyMenuOpen = false;

    public void toggle(Tile tile)
    {
        if (BuyMenuOpen)
        {
            this.transform.position = new Vector3(this.transform.position.x - 200, this.transform.position.y, 0);
            BuyMenuOpen = false;
        }
        else
        {
            TextMeshProUGUI[] ts = this.transform.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI t in ts)
            {
                if (t.name == "Header") SideBarName = t;
            }

            if (tile.spriteRenderer.color == tile.riverColor)
            {
                SideBarName.text = "River";
            }
            else if (tile.spriteRenderer.color == tile.grasslandsColor)
            {
                SideBarName.text = "Grasslands";

            }
            else if (tile.spriteRenderer.color == tile.forestColor)
            {
                SideBarName.text = "Forest";
            }
            else
            {
                SideBarName.text = "Mountain";
            }
            this.transform.position = new Vector3(this.transform.position.x + 200, this.transform.position.y, 0);
            BuyMenuOpen = true;
        }
    }
}
