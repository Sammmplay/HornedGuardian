using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    [SerializeField] GameObject _prefabProyectil;

    Slider _slider;

    [SerializeField] Transform _punteroBullet;
    public float _life;
    public float _maxLife;
    float _timeNextFire;
    public float _damage;
    [SerializeField] float _timeInstanceBulletMax;
    [SerializeField] float _timeInstanceBulletMin;
    [SerializeField] float _distanceRaycast;
    [SerializeField] ManagerEnemi _managerEnemy;
    
    
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    private void Start() {
        _life = _maxLife;
        _slider = GetComponentInChildren<Slider>();
        _managerEnemy = FindObjectOfType<ManagerEnemi>();
        InicializarVida(_life,_maxLife);
        _timeNextFire = Random.Range(_timeInstanceBulletMin, _timeInstanceBulletMax);
    }

    private void Update() {
        if(gameObject.transform.position.y != 0) {
            Vector3 newPosition = transform.position;
            newPosition.y = 0;
            transform.position = newPosition;
        }
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
        GameObject _instanciaProyectil = Instantiate(_prefabProyectil,_punteroBullet);
        _instanciaProyectil.transform.localPosition = Vector3.zero;
    }
    bool PuedeAtaquar() {

        if (Physics.Raycast(transform.position, Vector3.back, out RaycastHit hit, _distanceRaycast)) {
            return !hit.collider.CompareTag("Enemigo");

        }
        return true;
    }
}
