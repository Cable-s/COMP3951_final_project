using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuyMenuManager : MonoBehaviour
{
    private TextMeshProUGUI SideBarName;
    static private bool BuyMenuOpen = false;

    public void toggle(MapGenerator mapGenerator)
    {
        print("Toggle called.");

        if (BuyMenuOpen)
        {
            this.transform.position = new Vector3(this.transform.position.x - 200, this.transform.position.y, 0);
            BuyMenuOpen = false;
            print("Buy Menu Open if true.");
        }
        else
        {
            print("Buy Menu Open if false.");

            TextMeshProUGUI[] ts = this.transform.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI t in ts)
            {
                if (t.name == "Header") SideBarName = t;
            }

            if (Input.GetMouseButton(0) == mapGenerator.waterTile)
            {
                SideBarName.text = "River";
            }
            else if (Input.GetMouseButton(0) == mapGenerator.grasslandTile)
            {
                SideBarName.text = "Grasslands";

            }
            else if (Input.GetMouseButton(0) == mapGenerator.forestTile)
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
