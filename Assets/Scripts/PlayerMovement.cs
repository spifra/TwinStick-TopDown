using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Range(0f, 10f)]
    public float movingSpeed = 5f;

    private Rigidbody rigidbody;

    Vector3 movement = Vector3.zero;

    Vector3 rotation = Vector3.zero;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Movement
        rigidbody.MovePosition(rigidbody.position + movement * movingSpeed * Time.fixedDeltaTime);

        //Rotation
        if (rotation != Vector3.zero)
        {
            rigidbody.rotation = Quaternion.LookRotation(rotation * Time.fixedDeltaTime);
        }
    }

    #region INPUT
    private void OnMove(InputValue value)
    {
        Vector2 inputValue = value.Get<Vector2>();
        movement = new Vector3(inputValue.x, 0, inputValue.y);
    }

    private void OnRotate(InputValue value)
    {
        Vector2 inputValue = value.Get<Vector2>();
        rotation = new Vector3(inputValue.x, 0, inputValue.y);
    }
    #endregion
}
