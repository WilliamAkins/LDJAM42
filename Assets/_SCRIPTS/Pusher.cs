using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Pusher : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private float speed = 30f;
	
	void FixedUpdate() { transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime); }
}