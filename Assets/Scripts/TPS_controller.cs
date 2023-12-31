using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPS_controller : MonoBehaviour
{
    private CharacterController _controller;
    private Transform _camera;
    private float _horizontal;
    private float _vertical;
    private Animator _animator;

    public int _shootDamage;

    [SerializeField] private float playerSpeed = 5;
    [SerializeField] private float _jumpHeight = 1;

    private float _gravity = -9.81f;
    private Vector3 _playerGravity;

    //variables para rotacion

    private float turnSmoothVelocity;
    [SerializeField] float turnSmoothTime = 0.1f;

    //variables para sensor

    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private float _sensorRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    private bool _isGrounded;

    private bool _playerLive = true;

    // Start is called before the first frame update
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _camera = Camera.main.transform;
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        
        if(Input.GetButton("Fire2") && _playerLive == true)
        {
            AimMovement();
        }
        else if (_playerLive == true)
        {
            Movement();
        }

        
        if(_playerLive == true)
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            RayTest();
        }
    }

    void Movement ()
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical);
        
        _animator.SetFloat("VelX", 0);
        _animator.SetFloat("VelZ", direction.magnitude);
        


        if(direction != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            _controller.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
    }

    void AimMovement ()
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical);

        _animator.SetFloat("VelX", _horizontal);
        _animator.SetFloat("VelZ", _vertical);

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _camera.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);

        transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
        
        if(direction != Vector3.zero)
        {

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            _controller.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        
        _isGrounded = Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer);

        /*_isGrounded = Physics.Raycast(_sensorPosition.position, Vector3.down, _sensorRadius, _groundLayer);
        Debug.DrawRay(_sensorPosition.position, Vector3.down* _sensorRadius, Color.red);
        ground sensor versión raycast*/
        
        if(_isGrounded && _playerGravity.y < 0)
        {
            _playerGravity.y = -2;
        }
        
        if(_isGrounded && Input.GetButtonDown("Jump"))
        {
            _playerGravity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
            _animator.Play("jump");
        }
    

        _playerGravity.y += _gravity * Time.deltaTime;

        _controller.Move(_playerGravity * Time.deltaTime);
    }

    void RayTest()
    {
        /* Raycast simple
        if(Physics.Raycast(transform.position, transform.forward, 10))
        {
            Debug.Log("Hit");
            Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        }*/

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            Debug.Log(hit.transform.name);
            Debug.Log(hit.transform.position);
            //Destroy(hit.transform.gameObject);

            Box caja = hit.transform.GetComponent<Box>();

            if(caja != null)
            {
                caja.TakeDamage(_shootDamage);
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DeadZone"))
        {
           _animator.Play("death"); 
           _playerLive = false;
        }
        
    }
}
