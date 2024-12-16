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

    [SerializeField] int _filas = 3;
    [SerializeField] int _Columnas = 5;


    [SerializeField] float _timeMovingX;
    [SerializeField] float _timeMovingZ;
    [SerializeField] float _spaceInX;
    [SerializeField] float _spaceInZ;
    [SerializeField] float _targetPosition;

    [SerializeField] float distanceX = 6;
    [SerializeField] float distanceZ = 2;


    [SerializeField] bool _movinInX = true;
    [SerializeField] bool _movingInZ = false;

    [SerializeField] Vector3 targetPosicionInZ;
    [SerializeField] Vector2 _moveDirection = Vector2.right;
    [SerializeField] List<EnemyController> enemies = new List<EnemyController>();

    private void Start() {
        InstantiatEnemy();
        //MoveX();
    }
    private void Update() {
        //MoveEnemy();

        //MovingInx();

        for (int i = 0; i < enemies.Count; i++) {
            if (_movinInX && !_movingInZ) {
                enemies[i].transform.position += (Vector3)_moveDirection * _timeMovingX;
                if (enemies[i].transform.position.x > _limites[0].transform.position.x
                    || enemies[i].transform.position.x < _limites[1].transform.position.x) {
                    _moveDirection = -_moveDirection;
                    if (enemies[i].transform.position.z <= _limites[2].transform.position.z) {
                        _movinInX = true;
                        _movingInZ = false;
                    } else {
                        _movinInX = false;
                        _movingInZ = true;
                    }
                    

                    targetPosicionInZ = enemies[i].transform.position + Vector3.back * distanceZ;
                }
            } else if(_movingInZ && !_movinInX){
                enemies[i].transform.position += Vector3.back * _timeMovingZ;
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

   

