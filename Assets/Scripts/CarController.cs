using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheels")]
    [SerializeField]public WheelCollider frontLeftTire;
    [SerializeField]public WheelCollider frontRightTire;
    [SerializeField]public WheelCollider rearLeftTire;
    [SerializeField]public WheelCollider rearRightTire;
    [Header("Config")]
    [SerializeField] public float maxAngle;
    public Vector2 playerInput;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");

        frontLeftTire.steerAngle = maxAngle * playerInput.x;
    }
}
