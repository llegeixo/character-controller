using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapcontroller : MonoBehaviour
{
    public GameObject _target;
    public Vector3 _cameraOffset;

    public GameObject _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (_target.transform.position.x, _target.transform.position.y + 86, _target.transform.position.z); 
        transform.rotation = Quaternion.Euler(90, _mainCamera.transform.localRotation.eulerAngles.y, 0);
    }

    void Awake()
    {
        _target = GameObject.Find("Player");
        _mainCamera = GameObject.Find("Main Camera");
    }
}
