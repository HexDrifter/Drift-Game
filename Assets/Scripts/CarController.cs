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
    [SerializeField] public float convergencyAngle;
    public Vector2 playerInput;

    public float getSpeed()
    {
        float pi = Mathf.PI;
        float rpmAxle = (frontLeftTireCollider.rpm + frontRightTireCollider.rpm)/2f;
        float wheelDiameter = frontLeftTireCollider.radius * 2f;

        float metersPerSecond = (pi * wheelDiameter * rpmAxle) / 60f;
        float kilometersPerHours = metersPerSecond * 3.6f;
        return kilometersPerHours;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");



        if (playerInput.y >= 0)
        {
            rearLeftTireCollider.motorTorque = engineForce * playerInput.y;
            rearRightTireCollider.motorTorque = engineForce * playerInput.y;
        }
        else
        {
            rearLeftTireCollider.motorTorque = engineForce * 0;
            rearRightTireCollider.motorTorque = engineForce * 0;
        }
        if (playerInput.y < 0)
        {
            frontLeftTireCollider.brakeTorque = brakeForce * playerInput.y * -1f;
            frontRightTireCollider.brakeTorque = brakeForce * playerInput.y * -1f;
            rearLeftTireCollider.brakeTorque = brakeForce * playerInput.y * -1f;
            rearRightTireCollider.brakeTorque = brakeForce * playerInput.y * -1f;
        }
        else
        {
            frontLeftTireCollider.brakeTorque = 0;
            frontRightTireCollider.brakeTorque = 0;
            rearLeftTireCollider.brakeTorque = 0;
            rearRightTireCollider.brakeTorque = 0;
        }
        frontLeftTireCollider.steerAngle = (maxAngle * playerInput.x) + convergencyAngle;
        frontRightTireCollider.steerAngle = (maxAngle * playerInput.x) - convergencyAngle;
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
