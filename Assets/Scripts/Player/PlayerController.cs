using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float _speed;
    public float _damage;
    [SerializeField] float _masAngleRotation;
    [SerializeField] float _menorAngleRotation;
    [SerializeField] float _angleSpeed;
    [SerializeField] Vector2 _input;
    Rigidbody _rb;
    private void Awake() {
        if (instance == null) {

            instance = this;
        } else Destroy(gameObject);
    }
    private void Start() {
        _rb = GetComponent<Rigidbody>();
    }
    public void OnMove(InputValue value) {
        _input = value.Get<Vector2>();
        Vector2 dir = new Vector2(_input.x * _speed, 0) * Time.deltaTime * 100;
        _rb.velocity = dir;
        Angleplayer(_input.x);
    }
    public void Angleplayer(float input) {
        float anglecurrent = -90;
        if (input > 0) {
            anglecurrent = _masAngleRotation;
        }else if (input < 0) {
            anglecurrent = _menorAngleRotation;
        }else if (input == 0) {
            anglecurrent = -90;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(anglecurrent, -90,-90),_angleSpeed*Time.deltaTime);
        
    }
}
