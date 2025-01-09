using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerEnemi : MonoBehaviour
{
    public static ManagerEnemi Instance;
    [SerializeField] GameObject[] _prefabEnemy;
    [SerializeField] Transform _instanciaTransform;
    [SerializeField] Transform _playerTransform;
    [SerializeField] Collider _colLimitZ;
    [SerializeField] int _filas = 3;
    [SerializeField] int _Columnas = 5;

    [SerializeField] float _spaceInX;
    [SerializeField] float _spaceInZ;
    [SerializeField] float distanceX = 6;
    [SerializeField] float distanceZ = 2;

    [Header("Movimiento enemies")]
    [SerializeField] float _limitXMax;
    [SerializeField] float _limitXMin;
    [SerializeField] float _limitZ;
    [SerializeField] float _distanceZ;
    public float _velocity;
    public float _timeZ;
    [SerializeField]public  bool _starGame;
    [SerializeField] bool _movingToRigth = true;
    [SerializeField] bool _movingInX;
    public bool _movingInZ;
    
    [SerializeField] Vector3 targetPosicionInZ;
    [SerializeField] Vector2 _moveDirection = Vector2.right;
    public int _countEnemies;
    [SerializeField] List<EnemyController> enemies = new List<EnemyController>();
    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    private void Start() {
        _movingInX = true;
        LeanTween.init(800);
        GameObject _colZ = GameObject.Find("LimiteZ");
    }
    private void Update() {

        for (int j = 0; j < enemies.Count; j++) {
            if (enemies[j] == null) { 
                enemies.RemoveAt(j);
                break;
            }
        }
        if(_starGame){
            if (_movingInX) {
            MoveX();
            }
        }
    }
    public void ComenzarNivel() {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(InstantiateEnemiesSmoothly());
    }

    IEnumerator InstantiateEnemiesSmoothly() {
        for (int i = 0; i < _filas; i++) {
            for (int j = 0; j < _Columnas; j++) {
                //seleccionamos el prefab segun la fila
                GameObject _seletPrefab = _prefabEnemy[i % _prefabEnemy.Length];// si i supera la longitud, usamos el resto 
                //posicion de instancia
                Vector3 spawPosition = new Vector3(j * _spaceInX, 0, i * _spaceInZ) + _instanciaTransform.position;
                //instanciar el enemigo
                GameObject enemy = Instantiate(_seletPrefab, spawPosition, _seletPrefab.transform.rotation);
                //agregar a la lista enemigos
                enemies.Add(enemy.GetComponent<EnemyController>());
                
                yield return new WaitForSeconds(0.1f);
                if (enemies.Count == _filas * _Columnas) {
                    _starGame = true;
                    _countEnemies = enemies.Count;
                    GameObject _colZ = GameObject.Find("LimiteZ");
                    _colLimitZ = _colZ.GetComponent<Collider>();
                }
            }
        }
    }


    public void MoveX() {
        for (int i = 0; i < enemies.Count; i++) {
            float targetX = _movingToRigth ? _limitXMax : _limitXMin;
            enemies[i].transform.position = Vector3.MoveTowards(enemies[i].transform.position,
                new Vector3(targetX, enemies[i].transform.position.y, enemies[i].transform.position.z), _velocity * Time.deltaTime);

            //cambiar al movimiento en z si se alcanza los limites 
            if(Mathf.Abs(enemies[i].transform.position.x - (_movingToRigth ? _limitXMax : _limitXMin)) < 0.01) {
                
                _movingToRigth = !_movingToRigth;
                if(_movingInZ) {
                    _movingInX = false;
                    AnimationRespectZ();
                    break;
                }
            }
        }
    }
    public void AnimationRespectZ() {
        Debug.Log("dentreo de los limites de Z ");
        for(int i = 0;i < enemies.Count; i++) {
            if (enemies[i] != null) {
                float targetZ = enemies[i].transform.position.z - distanceZ;
                LeanTween.moveLocalZ(enemies[i].gameObject, targetZ, _timeZ).setOnComplete(() => { _movingInX = true; });
            }
        }
    }
    public void Addvelocity(float velocityX, float velocityZ) {
        _velocity += velocityX;
        _timeZ -= velocityZ;
    }
}

   

