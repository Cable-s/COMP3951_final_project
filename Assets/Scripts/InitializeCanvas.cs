using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class InitializeCanvas : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button nextDayButton;
    [SerializeField] private TextMeshProUGUI peopleText;
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI waterText;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI metalText;
    [SerializeField] private TextMeshProUGUI dayCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // float width = this.GetComponent<RectTransform>().rect.width;
        // float height = this.GetComponent<RectTransform>().rect.height;

        // dayCount.transform.position = new Vector3(50, height - 50, 0);

        // nextDayButton.transform.position = new Vector3(width - 50, 50, 0);

        // peopleText.transform.position = new Vector3(width - 50, height - 50, 0);
        // foodText.transform.position = new Vector3(width - 50, height - 75, 0);
        // waterText.transform.position = new Vector3(width - 50, height - 100, 0);
        // woodText.transform.position = new Vector3(width - 50, height - 125, 0);
        // metalText.transform.position = new Vector3(width - 50, height - 150, 0);

    }
}
