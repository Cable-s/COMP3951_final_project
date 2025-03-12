using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [field: SerializeField] internal int dayCount { get; set; } = 1;
    //[field: SerializeField] to have a serialized field on a property.
    [field: SerializeField] internal int peopleCount { get; set; }
    [field: SerializeField] internal int foodCount { get; set; }
    [field: SerializeField] internal int waterCount { get; set; }
    [field: SerializeField] internal int woodCount { get; set; }
    [field: SerializeField] internal int metalCount { get; set; }
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI peopleText;
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI waterText;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI metalText;

    private void Start()
    {
        updateResourceCountText();
    }

    public void updateResourceCountText()
    {
        dayText.text = "Day " + dayCount;
        peopleText.text = "People " + peopleCount;
        foodText.text = "Food " + foodCount;
        waterText.text = "Water " + waterCount;
        woodText.text = "Wood " + woodCount;
        metalText.text = "Metal " + metalCount;
    }
}
