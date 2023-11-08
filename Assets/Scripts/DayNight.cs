using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    [SerializeField] float _sunVelocity = 5;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_sunVelocity * Time.deltaTime, 0, 0);
    }
}
