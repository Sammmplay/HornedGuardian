using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] GameObject _ballPrefab;

    [SerializeField] Transform _ballTransform;
    public float _speed;
    public float _damage;
    [SerializeField] float _masAngleRotation;
    [SerializeField] float _menorAngleRotation;
    [SerializeField] float _angleSpeed;
    [SerializeField] float _timer;
    [SerializeField] float _recollding;
    [SerializeField] bool _onFire = true;
    [SerializeField] Vector2 _input;
    [SerializeField] bool _isFire;
    
    Rigidbody _rb;
    private void Awake() {
        if (instance == null) {

            instance = this;
        } else Destroy(gameObject);

    }
    private void Start() {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update() {
        _timer += Time.deltaTime;
        if (_timer  >_recollding) {
            _onFire = true;
        }
        if (transform.position.y != 0) {
            Vector3 newPosition = transform.position;
            newPosition.y = 0;
            transform.position = newPosition;
        }
        if (_isFire) {
            disparar(); ;
        }
    }
    private void FixedUpdate() {
        Angleplayer(_input.x);
    }

    public void OnMove(InputValue value) {
        if(transform.position.x < 10 && transform.position.x> -10) {
            _input = value.Get<Vector2>();
            Vector2 dir = new Vector2(_input.x * _speed, transform.position.y);
            _rb.velocity = dir;
            

        }

    }

    public void Angleplayer(float input) {
        float anglecurrent = -90;
        if (input > 0) {
            anglecurrent = _masAngleRotation;
        }else if (input < 0) {
            anglecurrent = _menorAngleRotation;
        }else if (input == 0) {
            anglecurrent = 0;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, anglecurrent), _angleSpeed * Time.fixedDeltaTime);
        
    }

    public void OnFire(InputValue value) {
        _isFire = value.isPressed;

    }
    void disparar() {
        if (_timer > _recollding && _onFire) {
            GameObject _ball = Instantiate(_ballPrefab, _ballTransform.position, Quaternion.identity);
            Destroy(_ball, 5);
            _timer = 0;
            _onFire = false;
        }
    }
}
