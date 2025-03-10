using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextDay : MonoBehaviour
{
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(nextDay);
    }

    void nextDay()
    {
        print("hello");
        //increment day count

        //get income from buildings
            //get all buildings on board
            //add that buildings resource to running count

        //update UI for resources to have updated count
    }
}
