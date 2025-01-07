using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
    public static EnemyController instance;
    [SerializeField] GameObject _prefabProyectil;
    [SerializeField] GameObject _destruction;
    Slider _slider;
    [SerializeField] ParticleSystem _efects;
    [SerializeField] GameObject[] _efectsGameObject;
    [SerializeField] Transform _punteroBullet;
    public float _life;
    public float _maxLife;
    float _timeNextFire;
    public float _damage;
    [SerializeField] float _timeInstanceBulletMax;
    [SerializeField] float _timeInstanceBulletMin;
    [SerializeField] float _distanceRaycast;
    [SerializeField] float _explocionForce=500f; 
    [SerializeField] float _explosionRadius =2f;
    [SerializeField] float _upwardModifier = 1f; //Modificador para que las partes salgan hacia arriaba
    public float _puntuacion = 0f;
    [SerializeField] ManagerEnemi _managerEnemy;
    
    private float _targetZ;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    private void Start() {
        _life = _maxLife;
        _slider = GetComponentInChildren<Slider>();
        _managerEnemy = FindObjectOfType<ManagerEnemi>();
        InicializarVida(_life, _maxLife);
        _timeNextFire = Random.Range(_timeInstanceBulletMin, _timeInstanceBulletMax);
    }

    private void Update() {

        if (gameObject.transform.position.y != 0) {
            Vector3 newPosition = transform.position;
            newPosition.y = 0;
            transform.position = newPosition;
        }
        if (PuedeAtaquar()) {
            Disparos();
        }
    }
    public void InicializarVida(float lifeactual, float maxlife) {

        _slider.maxValue = maxlife;
        _slider.value = lifeactual;
    }
    public void PerderVida(float damage) {
        _life -= damage;
        _slider.value = _life;
        if (_efects != null) {
            _efects.Play();
        }
        
        if (_life <= 0) {
            Collider _col = GetComponent<Collider>();
            _col.enabled = false;
            ManagerEnemi.Instance._countEnemies--;
            GameManager.Instance.AddPuntuacion(_puntuacion);
            MeshRenderer _meshR = GetComponent<MeshRenderer>();
            _meshR.enabled = false;
            AudioSource _source = GetComponent<AudioSource>();
            _source.Play();
            if(ManagerEnemi.Instance._countEnemies <= 0) {
                ControlUi.Instance._congratulations.GetChild(0).gameObject.SetActive(true);
                GameManager.Instance.TiempoHecho();
                Time.timeScale = 0;
            }
            DestruccionEfect();

        }
    }
    void Disparos() {
        _timeNextFire -= Time.deltaTime;
        if (_timeNextFire <= 0) {
            InstanciarProyectiil();
            _timeNextFire = Random.Range(_timeInstanceBulletMin, _timeInstanceBulletMax);
        }
    }
    void InstanciarProyectiil() {
        GameObject _instanciaProyectil = Instantiate(_prefabProyectil, _punteroBullet);
        _instanciaProyectil.transform.localPosition = Vector3.zero;
    }
    bool PuedeAtaquar() {

        if (Physics.Raycast(transform.position, Vector3.back, out RaycastHit hit, _distanceRaycast)) {
            return !hit.collider.CompareTag("Enemigo");

        }
        return true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ball")) {
            if (ManagerSounds.Instance != null) {
                ManagerSounds.Instance.PlaySound(6);// llamando a sonido impacto 
            } 
        }
    }
    public void DestruccionEfect() {
        _destruction.SetActive(true);
        if (_efectsGameObject[0] != null || _efectsGameObject[1] != null ) {
            GameObject _efect1 = Instantiate(_efectsGameObject[0], transform.position, Quaternion.identity);
            _efect1.GetComponent<ParticleSystem>().Play();
            GameObject _efect2 = Instantiate(_efectsGameObject[1], transform.position, Quaternion.identity);
            _efect2.GetComponent<ParticleSystem>().Play();
            GameObject _efect3 = Instantiate(_efectsGameObject[2], transform.position, Quaternion.identity);
            
        }
        Destroy(gameObject, 0.5f);
    }
}
