using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerEnemi : MonoBehaviour
{
    [SerializeField] GameObject[] _prefabEnemy;
    [SerializeField] Transform _instanciaTransform;
    [SerializeField] Transform[] _limites;
    [SerializeField] Transform _playerTransform;
    [SerializeField] int _filas = 3;
    [SerializeField] int _Columnas = 5;

    [SerializeField] int _countEnemis;

    [SerializeField] float _timeMovingX;
    [SerializeField] float _timeMovingZ;
    [SerializeField] float _spaceInX;
    [SerializeField] float _spaceInZ;
    [SerializeField] float distanceX = 6;
    [SerializeField] float distanceZ = 2;


    [SerializeField] bool _movinInX = true;
    [SerializeField] bool _movingInZ = false;

    [SerializeField] Vector3 targetPosicionInZ;
    [SerializeField] Vector2 _moveDirection = Vector2.right;

    [SerializeField] List<EnemyController> enemies = new List<EnemyController>();

    private void Start() {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        InstantiatEnemy();
        _countEnemis = enemies.Count;
        
        //MoveX();
    }
    private void Update() {
        //MoveEnemy();

        //MovingInx();
        for (int j = 0; j < enemies.Count; j++) {
            if (enemies[j] == null) {
                enemies.RemoveAt(j);
                break;
            }
        }
        for (int i = 0; i < enemies.Count; i++) {
            if (_movinInX && !_movingInZ) {
                enemies[i].transform.position += (Vector3)_moveDirection * (_timeMovingX/100);
                if (enemies[i].transform.position.x > _limites[0].transform.position.x
                    || enemies[i].transform.position.x <= _limites[1].transform.position.x) {
                    
                    if (enemies[i].transform.position.z >= _limites[2].transform.position.z) {
                        DistanceRespectPlayer();
                        targetPosicionInZ.z -= distanceZ;
                        _movinInX = false;
                        _movingInZ = true;
                        
                    }
                    _moveDirection = -_moveDirection;
                }
            } else if(_movingInZ && !_movinInX){
                enemies[i].transform.position += Vector3.back * (_timeMovingZ/100);
                if (enemies[i].transform.position.z < targetPosicionInZ.z) {
                    _movingInZ = false;
                    _movinInX = true;
                }
            }
        }
    }
    void InstantiatEnemy() {
        for (int i = 0; i < _filas; i++) {
            for (int j = 0; j < _Columnas; j++) {
                //seleccionamos el prefab segun la fila
                GameObject _seletPrefab = _prefabEnemy[i % _prefabEnemy.Length];// si i supera la longitud, usamos el resto 
                //posicion de instancia
                Vector3 spawPosition = new Vector3(j * _spaceInX, 0, i * _spaceInZ) + _instanciaTransform.position;
                //instanciar el enemigo
                GameObject enemy = Instantiate(_seletPrefab, spawPosition, Quaternion.Euler(0, 180, 0));
                //agregar a la lista enemigos
                enemies.Add(enemy.GetComponent<EnemyController>());
            }
        }
    }

    void MovingInx() {
        for (int i = 0; i < enemies.Count; i++) {
            if (_movinInX && enemies[i]!=null) {
                enemies[i].transform.LeanMoveLocalX(enemies[i].transform.localPosition.x + distanceX, _timeMovingX).setOnComplete(() => {
                    _movinInX = false;
                });
            }
        }
    }
    void DistanceRespectPlayer() {
        float closeDistance = Mathf.Infinity;
        for (int i = 0; i < enemies.Count; i++) {
            float distancePlayer = Vector3.Distance(enemies[i].transform.position, _playerTransform.position);
            if (distancePlayer < closeDistance) {
                closeDistance = distancePlayer;
                targetPosicionInZ = enemies[i].transform.position;
            }

        }
    }
    //controlador para la velocidad de de movimiento de los enemigos
    public void AumentarVelocidad(float speedEnemiesX, float speedEnemiesZ) {
        _spaceInX += speedEnemiesX;
        _spaceInZ += speedEnemiesZ;
    }
    /*public void MoveX() {
        float targetX = _movingToRigth ? _limitXmax : _limitXmin;
        LeanTween.moveLocalX(_instanciaTransform.gameObject, targetX, _timeMovingX).setOnComplete(() => {
            MoveZ();
        });    
    }*/

    /*void MoveZ() {
        float targetZ = _instanciaTransform.localPosition.z - _distanceZ;
        LeanTween.moveLocalZ(_instanciaTransform.gameObject, targetZ, _timeMovingZ).setOnComplete(() => {
            _movingToRigth = !_movingToRigth;
            MoveX();
        });
           

    }*/
    
}

   

