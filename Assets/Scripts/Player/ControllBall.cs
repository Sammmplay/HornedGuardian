using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllBall : MonoBehaviour
{
    public float _speed;

    private void Update() {
        transform.position += Vector3.forward * _speed * Time.deltaTime ;
    }
}
