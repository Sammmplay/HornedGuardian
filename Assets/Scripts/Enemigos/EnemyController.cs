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
    public float _addVelocityX { get; private set; } = 0.2f;
    public float _addTimeZ { get; private set; } = 0.015f;
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
    [Header("Efectos")]
    [SerializeField] GameObject[] _powerUpsPrefabs;
    [SerializeField] float _dropChanceMax = 0.3f;
    [SerializeField] float _dropChanceMin = 0.0f;
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
            ManagerEnemi.Instance.Addvelocity(_addVelocityX,_addTimeZ);
            if (ManagerEnemi.Instance._countEnemies <= 0) {
                Debug.Log("N hay mas naves");
                ControlUi.Instance._congratulations.GetChild(0).gameObject.SetActive(true);
                GameManager.Instance.WinGame();
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
        BulletController _scrip = _instanciaProyectil.GetComponent<BulletController>();
        _scrip._damage = _damage;
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
    public void DropPowerUp() {
        // Seleccionar un Power-Up aleatorio de la lista
        int randomIndex = Random.Range(0, _powerUpsPrefabs.Length);
        // Instanciar el Power-Up en la posición del bloque
        GameObject _poweUp = Instantiate(_powerUpsPrefabs[randomIndex], transform.position, _powerUpsPrefabs[randomIndex].transform.rotation);
        _poweUp.transform.SetParent(null);
    }
    public void DestruccionEfect() {
        DropPowerUp();
        _destruction.SetActive(true);
        if (_efectsGameObject[0] != null || _efectsGameObject[1] != null ) {
            GameObject _efect1 = Instantiate(_efectsGameObject[0], transform.position, Quaternion.identity);
            _efect1.GetComponent<ParticleSystem>().Play();
            GameObject _efect2 = Instantiate(_efectsGameObject[1], transform.position, Quaternion.identity);
            if (_efect2) {
                Destroy(_efect2,2f);
            }
            _efect2.GetComponent<ParticleSystem>().Play();
            GameObject _efect3 = Instantiate(_efectsGameObject[2], transform.position, Quaternion.identity);
            Destroy(_efect3,1.5f);
        }
        Destroy(gameObject, 0.5f);
    }
}
