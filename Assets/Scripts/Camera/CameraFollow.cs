using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float height;
    private Transform targetTransform;

    void Start()
    {
        targetTransform = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        transform.position = new Vector3(targetTransform.position.x, height, targetTransform.position.z - 20f);
    }
}
