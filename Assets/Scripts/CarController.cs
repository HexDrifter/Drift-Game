using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheels")]
    [SerializeField] public WheelCollider frontLeftTireCollider;
    [SerializeField] public WheelCollider frontRightTireCollider;
    [SerializeField] public WheelCollider rearLeftTireCollider;
    [SerializeField] public WheelCollider rearRightTireCollider;
    [SerializeField] public Transform frontLeftTire;
    [SerializeField] public Transform frontRightTire;
    [SerializeField] public Transform rearLeftTire;
    [SerializeField] public Transform rearRightTire;
    [SerializeField] public Transform frontLeftCaliper;
    [SerializeField] public Transform frontRightCaliper;
    [SerializeField] public Transform rearLeftCaliper;
    [SerializeField] public Transform rearRightCaliper;
    [Header("Config")]
    [SerializeField] public float engineForce;
    [SerializeField] public float brakeForce;
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


        rearLeftTireCollider.motorTorque = engineForce * playerInput.y;
        rearRightTireCollider.motorTorque = engineForce * playerInput.y;
        frontLeftTireCollider.steerAngle = maxAngle * playerInput.x;
        frontRightTireCollider.steerAngle = maxAngle * playerInput.x;
        updateVisualModels();
        
    }

    private void updateVisualModels()
    {
        frontLeftTireCollider.GetWorldPose(out Vector3 frontLeftWheelPose, out Quaternion frontLeftWheelRotation);
        frontRightTireCollider.GetWorldPose(out Vector3 frontRightWheelPose, out Quaternion frontRightWheelRotation);
        rearLeftTireCollider.GetWorldPose(out Vector3 rearLeftWheelPose, out Quaternion rearLeftWheelRotation);
        rearRightTireCollider.GetWorldPose(out Vector3 rearRightWheelPose, out Quaternion rearRightWheelRotation);

        

        frontLeftWheelRotation *= Quaternion.Euler(0, -90, 0);
        frontRightWheelRotation *= Quaternion.Euler(0, 90, 0);
        rearLeftWheelRotation *= Quaternion.Euler(0, -90, 0);
        rearRightWheelRotation *= Quaternion.Euler(0, 90, 0);
        frontLeftTire.transform.rotation = frontLeftWheelRotation;
        frontRightTire.transform.rotation = frontRightWheelRotation;
        rearLeftTire.transform.rotation = rearLeftWheelRotation;
        rearRightTire.transform.rotation = rearRightWheelRotation;

        frontLeftTire.transform.position = frontLeftWheelPose;
        frontRightTire.transform.position = frontRightWheelPose;
        rearLeftTire.transform.position = rearLeftWheelPose;
        rearRightTire.transform.position = rearRightWheelPose;

        frontLeftCaliper.transform.position = frontLeftWheelPose;
        frontLeftCaliper.transform.rotation = Quaternion.Euler(0,frontLeftWheelRotation.eulerAngles.y,0);

        frontRightCaliper.transform.position = frontRightWheelPose;
        frontRightCaliper.transform.rotation = Quaternion.Euler(0, frontRightWheelRotation.eulerAngles.y, 0);

        rearLeftCaliper.transform.position = rearLeftWheelPose;
        rearLeftCaliper.transform.rotation = Quaternion.Euler(0, rearLeftWheelRotation.eulerAngles.y, 0);

        rearRightCaliper.transform.position = rearRightWheelPose;
        rearRightCaliper.transform.rotation = Quaternion.Euler(0, rearRightWheelRotation.eulerAngles.y, 0);
    }
}
