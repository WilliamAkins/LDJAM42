using UnityEngine;

/// <summary>
/// Rotates the ship's satellite dish
/// </summary>
public class SatelliteDishRotator : MonoBehaviour
{
    /// <summary>
    /// Reference to the satellite dish game object
    /// </summary>
    [SerializeField]
    private GameObject satelliteDish;

    /// <summary>
    /// Speed in which the dish boi rotates
    /// </summary>
    [SerializeField]
    private float rotationSpeed = 32f;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private bool flip = false;
    
	void Update()
    {
        if (!flip) satelliteDish.transform.Rotate(0, 6.0f * rotationSpeed * Time.deltaTime, 0);
        else satelliteDish.transform.Rotate(0, 0, 6.0f * rotationSpeed * Time.deltaTime);
    }
}