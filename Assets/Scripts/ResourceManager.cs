using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private int peopleCount;
    [SerializeField] private int foodCount;
    [SerializeField] private int waterCount;
    [SerializeField] private int woodCount;
    [SerializeField] private int metalCount;
    [SerializeField] private TextMeshProUGUI peopleText;
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI waterText;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI metalText;

    private void Start()
    {
        updateResourceCountText();
    }

    private void updateResourceCountText()
    {
        peopleText.text = "People " + peopleCount;
        foodText.text = "Food " + foodCount;
        waterText.text = "Water " + waterCount;
        woodText.text = "Wood " + woodCount;
        metalText.text = "Metal " + metalCount;
    }
}
