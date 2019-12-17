using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class WeatherTypeSO : ScriptableObject
{
    public enum WeatherType
    {
        SUN,
        CLOUDY,
        SNOW,
        MIST,
        STORM
    }
    [SerializeField] private WeatherType type;
    public WeatherType Type => type;
    [SerializeField] private float percentAppearance;
    public float PercentAppearance => percentAppearance;
    [SerializeField] private Sprite weatherImage;
    public Sprite WeatherImage => weatherImage;
    [SerializeField] private string name;
    public string Name => name;
}
