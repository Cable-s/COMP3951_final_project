using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Editor")]

/// <summary>
/// Tracks and updates resource values
/// </summary>
public class ResourceManager : MonoBehaviour
{
    [field: SerializeField] internal int dayCount { get; set; } = 1;
    //[field: SerializeField] to have a serialized field on a property.
    [field: SerializeField] internal int peopleCount { get; set; }
    [field: SerializeField] internal int populationCount { get; set; }
    [field: SerializeField] internal int foodCount { get; set; }
    [field: SerializeField] internal int stoneCount { get; set; }
    [field: SerializeField] internal int woodCount { get; set; }
    [field: SerializeField] internal int metalCount { get; set; }
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI peopleText;
    [SerializeField] private TextMeshProUGUI populationText;
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI metalText;

    private void Start()
    {
        updateResourceCountText();
    }

    /// <summary>
    /// Updates UI text per resource values
    /// </summary>
    public void updateResourceCountText()
    {
        dayText.text = "Day " + dayCount;
        peopleText.text = "People " + peopleCount + " / " + populationCount;
        foodText.text = "Food " + foodCount;
        stoneText.text = "Stone " + stoneCount;
        woodText.text = "Wood " + woodCount;
        metalText.text = "Metal " + metalCount;
    }
}
