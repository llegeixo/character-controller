using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapcontroller : MonoBehaviour
{
    public GameObject _target;
    public Vector3 _cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = _target.transform.position + _cameraOffset;
    }
}
