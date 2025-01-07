using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.InputSystem;

public class TruckController : MonoBehaviour
{
    public InputActionProperty accelerateAction; //right trigger
    public InputActionProperty decelerateAction; //left trigger
    public InputActionProperty grabSteeringR; //left trigger
    public InputActionProperty grabSteeringL; //left trigger

    public XRLever lever;
    public XRKnob wheelKnob;

    private float steeringInput, accelerateInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;
    private bool isReverse;

    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;
    public float maxSpeed = 100f;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private EngineAudio engineAudio;


    public int isEngineRunning = 0;
    
    private void Start()
    {
        engineAudio = GetComponent<EngineAudio>(); // Get the reference to the EngineAudio script
    }
    
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleEngineAudio();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        isReverse = lever.value;

        // Steering Input
        steeringInput = Mathf.Lerp(-1, 1, wheelKnob.value);

        // Acceleration Input
        accelerateInput = accelerateAction.action.ReadValue<float>();

        if (isReverse)
        {
            accelerateInput = accelerateInput * -1; //to neg
        }
        else if (!isReverse && accelerateInput < 0)
        {
            accelerateInput = accelerateInput * -1; //to pos
        }

        // Breaking Input
        isBreaking = decelerateAction.action.ReadValue<float>() > 0.1f;
    }

    private void HandleMotor()
    {
        if (engineAudio != null)
        {
            engineAudio.isEngineRunning = (accelerateInput != 0f || isBreaking);
        }

        frontLeftWheelCollider.motorTorque = accelerateInput * motorForce;
        frontRightWheelCollider.motorTorque = accelerateInput * motorForce;

        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * steeringInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    public float GetSpeedRatio()
    {
        // Calculate speed ratio based on your truck's speed and max speed
        float speed = (frontLeftWheelCollider.rpm + frontRightWheelCollider.rpm) / 2f * frontLeftWheelCollider.radius * 2 * Mathf.PI * 60 / 1000f;
        return Mathf.Clamp(speed / maxSpeed, -1f, 1f);
    }

    private void HandleEngineAudio()
    {
        if (engineAudio != null)
        {
            // Set engine running state based on acceleration input, braking, or idle state
            engineAudio.isEngineRunning = (accelerateInput != 0f || isBreaking || (accelerateInput == 0f && !isBreaking));
        }
    }
}