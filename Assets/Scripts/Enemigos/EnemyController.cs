using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    public float _life;
    public float _maxLife;
    float _timeNextFire;
    [SerializeField] float _timeInstanceBulletMax;
    [SerializeField] float _timeInstanceBulletMin;
    [SerializeField] GameObject _prefabProyectil;
    [SerializeField] float _distanceRaycast;
    Slider _slider;
    [SerializeField] Canvas _canvas;
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else Destroy(gameObject);
    }
    private void Start() {
        _life = _maxLife;
        _slider = GetComponentInChildren<Slider>();
        _canvas = GetComponentInChildren<Canvas>();
        if(_canvas.renderMode == RenderMode.WorldSpace) {
            _canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        InicializarVida(_life,_maxLife);
        _timeNextFire = Random.Range(_timeInstanceBulletMin, _timeInstanceBulletMax);
    }

    private void Update() {
        if (!PuedeAtaquar()) return;
            Disparos();
            
    }

    public void InicializarVida(float lifeactual, float maxlife) {
        
        _slider.maxValue = maxlife;
        _slider.value = lifeactual;
    }
    public void PerderVida(float damage) {
        _life -= damage;
        _slider.value = _life;
        if(_life<= 0) {
            Destroy(gameObject);
        }
    }
    void Disparos() {
        _timeNextFire -= Time.deltaTime;
        if(_timeNextFire <= 0) {
            InstanciarProyectiil();
            _timeNextFire = Random.Range(_timeInstanceBulletMin, _timeInstanceBulletMax);
        }
    }
    void InstanciarProyectiil () {
        GameObject _instanciaProyectil = Instantiate(_prefabProyectil);
    }
    bool PuedeAtaquar() {

        if (Physics.Raycast(transform.position, Vector3.back, out RaycastHit hit, _distanceRaycast)) {
            return !hit.collider.CompareTag("Enemigo");

        }
        return true;
    }
}
