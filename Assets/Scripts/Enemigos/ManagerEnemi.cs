using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ManagerEnemi : MonoBehaviour
{
    [SerializeField] GameObject[] _prefabEnemy;
    [SerializeField] Transform _instanciaTransform;

    [SerializeField] int _filas = 3;
    [SerializeField] int _Columnas = 5;

    [SerializeField] float _limitXmax = 13f; // Límite máximo en X
    [SerializeField] float _limitXmin = -6f; // Límite mínimo en X
    [SerializeField] float _distanceZ = 1;
    [SerializeField] float _timeMovingX;
    [SerializeField] float _timeMovingZ;
    [SerializeField] float _spaceInX;
    [SerializeField] float _spaceInZ;
    [SerializeField] float _targetPosition;

    [SerializeField] bool _movingToRigth = false;

    [SerializeField] List<EnemyController> enemies = new List<EnemyController>();

    private void Start() {
        InstantiatEnemy();
        MoveX();
    }
    private void Update() {
        //MoveEnemy();


    }
    void InstantiatEnemy() {
        for (int i = 0; i < _filas; i++) {
            for (int j = 0; j < _Columnas; j++) {
                //seleccionamos el prefab segun la fila
                GameObject _seletPrefab = _prefabEnemy[i % _prefabEnemy.Length];// si i supera la longitud, usamos el resto 
                //posicion de instancia
                Vector3 spawPosition = new Vector3(j * _spaceInX, 0, i * _spaceInZ) + _instanciaTransform.position;
                //instanciar el enemigo
                GameObject enemy = Instantiate(_seletPrefab, spawPosition, Quaternion.Euler(0, 180, 0), _instanciaTransform);
                //agregar a la lista enemigos
                enemies.Add(enemy.GetComponent<EnemyController>());
            }
        }
    }
    public void MoveX() {
        float targetX = _movingToRigth ? _limitXmax : _limitXmin;
        LeanTween.moveLocalX(_instanciaTransform.gameObject, targetX, _timeMovingX).setOnComplete(() => {
            MoveZ();
        });
           
        
            
    }

    void MoveZ() {
        float targetZ = _instanciaTransform.localPosition.z - _distanceZ;
        LeanTween.moveLocalZ(_instanciaTransform.gameObject, targetZ, _timeMovingZ).setOnComplete(() => {
            _movingToRigth = !_movingToRigth;
            MoveX();
        });
           

    }
    
}

   

