using UnityEngine;

/// <summary>
/// Enhances aspects of the weather
/// </summary>
public class WeatherEnhancer : MonoBehaviour
{
    /// <summary>
    /// Minimum particle start size
    /// </summary>
    [SerializeField]
    private int startSizeMin = 2;

    /// <summary>
    /// Maximum particle start size
    /// </summary>
    [SerializeField]
    private int startSizeMax = 25;

    void Awake() { GetComponent<ParticleSystem>().startSize = Random.Range(startSizeMin, startSizeMax); }
}