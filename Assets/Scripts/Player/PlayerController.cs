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
    [SerializeField] public float _recollding;
    [SerializeField] float _timeDestroyThis = 3.5f;
    int _currentPositionIndex;
    [SerializeField] float _limitMinX;
    [SerializeField] float _limitMaxX;
    [SerializeField] bool _onFire = true;
    [SerializeField] bool _isFire;

    [SerializeField] Vector2 _input;
    [SerializeField] bool _canmove = false;
    [Header("PowerUps")]
    [SerializeField] float _originalRecolding;
    [SerializeField] float _originalMove;
    [SerializeField] float _durationInvert;

    [SerializeField] float _endRecolding;
    [SerializeField] float _endMove;
    [SerializeField] float _endInvertMove;


    [SerializeField] bool _isEffectActiveFire = false;
    [SerializeField] bool _isEffectActiveMove = false;
    [SerializeField] bool _isEffectActiveInvert = false;
    Rigidbody _rb;
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
        _originalRecolding = _recollding;
        _originalMove = _speed;
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
        // Comprueba si el efecto activo debe terminar.
        if (_isEffectActiveFire && Time.time >= _endRecolding) {
            _recollding = _originalRecolding; // Restaura la velocidad original.
            _isEffectActiveFire = false;
        }
        if (_isEffectActiveMove && Time.time >= _endMove) {
            _speed = _originalMove; // Restaura la velocidad original.
            _isEffectActiveMove = false;
        }
        if (_isFire) {
            disparar(); ;
        }
        // Comprueba si el efecto de inversión debe terminar.
        if (_isEffectActiveInvert && Time.time >= _endInvertMove) {
            _isEffectActiveInvert = false;
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
       
        float moveX = (_isEffectActiveInvert ? -_input.x : _input.x) * _speed * Time.deltaTime;

        Vector2 dir = _rb.position + new Vector3(moveX, 0);
        dir.x = Mathf.Clamp(dir.x, _limitMinX, _limitMaxX);
        _rb.MovePosition(dir);

    }
    public void Angleplayer(float input) {
        if (!_canmove) return;
        input = _isEffectActiveInvert ? -input : input;
        float anglecurrent = -90;
        if (input > 0) {
            anglecurrent = _masAngleRotation;
        }else if (input < 0) {
            anglecurrent = _menorAngleRotation;
        }else if (input == 0) {
            anglecurrent = 0;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.Euler(0, 0, anglecurrent), _angleSpeed * Time.fixedDeltaTime);
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
        if (GameManager.Instance._lifeCount <= 0) {
            StartCoroutine(EndGame());
        }
        Destroy(gameObject, _timeDestroyThis+0.1f);
    }
    IEnumerator EndGame() {
        yield return new WaitForSeconds(_timeDestroyThis-0.1f);
        Debug.Log("EndGameInstance");
        GameManager.Instance.EndGame();
        Destroy(gameObject);
    }
    public void ApplySpeedFire(float newRecolding,float duration) {
        if (_isEffectActiveFire) {
            _endRecolding = Time.time + duration;
            return;
        }
        _recollding -= newRecolding;
        _isEffectActiveFire = true;
        _endRecolding = Time.time+ duration;
    }
    public void ApplyspeedSpeed(float _newVel,float duration) {
        if (_isEffectActiveMove) {
            _endMove = Time.time + duration;
            return;
        }
        _speed += _newVel;
        _isEffectActiveMove = true;
        _endMove = Time.time + duration;
    }
    public void ApplyInverControls(float duration) {
        _isEffectActiveInvert = true;
        _durationInvert = Time.time + duration;
    }
    public void CanDoIt(bool yes) {
        _canmove = yes;
    
    }
}
