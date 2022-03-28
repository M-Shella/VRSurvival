using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour{
    public float speed = 1;
    public float gravity = -9.81f;
    public float additionalHeight = 0.2f;
    public LayerMask groundLayer;
    private float _fallingSpeed;
    private XRRig _xrRig;
    private CharacterController _character;
    public InputActionReference joystick;
    [SerializeField]private GameObject lController;
    void Start(){
        _character = GetComponent<CharacterController>();
        _xrRig = GetComponent<XRRig>();
    }
    void Update(){
    }

    private void FixedUpdate(){
        FollowHeadset();
        Quaternion headYaw = Quaternion.Euler(0, _xrRig.cameraGameObject.transform.eulerAngles.y,0);
        Vector3 direction = headYaw * new Vector3(joystick.action.ReadValue<Vector2>().x, 0,
            joystick.action.ReadValue<Vector2>().y);
        
        _character.Move(direction * (Time.fixedDeltaTime * speed));

        if (Grounded()) _fallingSpeed = 0;
        else _fallingSpeed += gravity * Time.fixedDeltaTime;
        _character.Move(Vector3.up * (_fallingSpeed * Time.fixedDeltaTime));
    }

    private void FollowHeadset(){
        _character.height = _xrRig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 center = transform.InverseTransformPoint(_xrRig.cameraGameObject.transform.position);
        _character.center = new Vector3(center.x, _character.height / 2 + _character.skinWidth, center.z);
    }
    private bool Grounded(){
        Vector3 rayStart = transform.TransformPoint(_character.center);
        float rayLength = _character.center.y + 0.01f;
        bool hit = Physics.SphereCast(rayStart, _character.radius, Vector3.down, out RaycastHit hitInfo, rayLength,
            groundLayer);
        return hit;
    }
}