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
    [SerializeField] float _limitMinX;
    [SerializeField] float _limitMaxX;

    [SerializeField] bool _onFire = true;
    
    [SerializeField] bool _isFire;

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
        MovimientoPlayer();
        Angleplayer(_input.x);

    }

    public void OnMove(InputValue value) {
        _input = value.Get<Vector2>();
    }
    public void MovimientoPlayer() {
        float moveX = _input.x * _speed * Time.deltaTime;

        Vector2 dir = _rb.position + new Vector3(moveX, 0);
        dir.x = Mathf.Clamp(dir.x, _limitMinX, _limitMaxX);

        _rb.MovePosition(dir);
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
        _isFire = value.Get<float>()>0.5;

    }
    void disparar() {
        if (_timer > _recollding && _onFire ) {
            GameObject _ball = Instantiate(_ballPrefab, _ballTransform.position, Quaternion.identity);
            Destroy(_ball, 5);
            _timer = 0;
            _onFire = false;
        }
    }
}
