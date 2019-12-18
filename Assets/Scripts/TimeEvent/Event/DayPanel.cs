using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private Image weatherImage;
    [SerializeField] private TextMeshProUGUI weatherText;
    [SerializeField] private TextMeshProUGUI weatherTextPercent;
    [SerializeField] private TextMeshProUGUI eventText;

    public void DisplayWeather (string newdayText, Sprite newweatherImage, string newweatherText, string newweatherTextPercent, string neweventText)
    {
        dayText.text = newdayText;
        weatherText.text = newweatherText;
        weatherImage.sprite = newweatherImage;
        weatherTextPercent.text = newweatherTextPercent + "%";
        eventText.text = neweventText;
    }
}
