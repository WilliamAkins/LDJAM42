using UnityEngine;

/// <summary>
/// Rotates the pirate ship's gun towards player
/// </summary>
public class GunFollow : MonoBehaviour
{
    /// <summary>
    /// Reference to the Gun's transform class
    /// </summary>
    [SerializeField]
    private Transform gun;

    /// <summary>
    /// Local reference to player boat
    /// </summary>
    private Transform player;

    void Start() { player = GameObject.FindGameObjectWithTag("boat").GetComponent<Transform>(); }

    void FixedUpdate()
    {
        gun.LookAt(player.transform.position);
        gun.rotation = Quaternion.Euler(0.0f, gun.rotation.eulerAngles.y, 0.0f);
    }
}