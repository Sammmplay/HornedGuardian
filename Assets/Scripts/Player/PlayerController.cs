using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] GameObject _ballPrefab;
    [SerializeField] GameObject _destruction;
    [SerializeField] GameObject _efects;
    [SerializeField] Transform[] _ballTransform;
              public float _speed;
              public float _damage;
    [SerializeField] float _masAngleRotation;
    [SerializeField] float _menorAngleRotation;
    [SerializeField] float _angleSpeed;
    [SerializeField] float _timer;
    [SerializeField] float _recollding;
    int _currentPositionIndex;
    [SerializeField] float _limitMinX;
    [SerializeField] float _limitMaxX;
    [SerializeField] bool _onFire = true;
    
    [SerializeField] bool _isFire;

    [SerializeField] Vector2 _input;
    bool _canmove = true;

    Rigidbody _rb;
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);

    }
    private void Start() {
        Collider _col = GetComponent<Collider>();
        _col.enabled = true;
        Light _ligt = GetComponentInChildren<Light>();
        _ligt.enabled = true;
        _rb = GetComponent<Rigidbody>();
        if (transform.position.y != 0) {
            Vector3 newPosition = transform.position;
            newPosition.y = 0;
            newPosition.z = -1.25f;
            transform.position = newPosition;
        }
    }
    private void Update() {
        _timer += Time.deltaTime;
        if (_timer  >_recollding) {
            _onFire = true;
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
        if (!_canmove) return;
        float moveX = _input.x * _speed * Time.deltaTime;

        Vector2 dir = _rb.position + new Vector3(moveX, 0);
        dir.x = Mathf.Clamp(dir.x, _limitMinX, _limitMaxX);

        _rb.MovePosition(dir);
    }
    public void Angleplayer(float input) {
        if (!_canmove) return;
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
        if (!_canmove) return;
        if (_timer > _recollding && _onFire ) {
            Transform _spawPosition = _ballTransform[_currentPositionIndex];
            GameObject _ball = Instantiate(_ballPrefab, _spawPosition.position, Quaternion.identity);
            //alternar entre el indice 0 y 1
            _currentPositionIndex = (_currentPositionIndex + 1) % _ballTransform.Length;
            _timer = 0;
            _onFire = false;
            Destroy(_ball, 5);
        }
    }
    public void DestructionObject() {
        _canmove = false;
        _destruction.SetActive(true);
        Collider _col = GetComponent<Collider>();
        _col.enabled = false;
        GameManager.Instance.PerderVida();
        if (_efects != null) {
            _efects.SetActive(true);
        }
        AudioSource _sound = GetComponent<AudioSource>();
        _sound.Play();
        MeshRenderer _mesh = GetComponent<MeshRenderer>();
        _mesh.enabled = false;
        Light _light = gameObject.GetComponentInChildren<Light>();
        if (_light!=null){
            _light.enabled = false;
        }
        Destroy(gameObject, 2.0f);
    }
}
