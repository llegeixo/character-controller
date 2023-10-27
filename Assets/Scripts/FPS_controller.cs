using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_controller : MonoBehaviour
{

    CharacterController _controller;
    Transform _fpsCamera;

    [SerializeField] float _speed;
    [SerializeField] float _sensitivity = 200;
    [SerializeField] float _jumpHeight;

    float _XRotation = 0;

    // Start is called before the first frame update
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _fpsCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float  mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        float  mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

        _XRotation -= mouseY;
        _XRotation = Mathf.Clamp(_XRotation, -90, 90);

        _fpsCamera.localRotation = Quaternion.Euler(_XRotation, 0, 0);

        transform.Rotate(Vector3.up * mouseX);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move.normalized * _speed * Time.deltaTime);
    }
}
