using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEnemi : MonoBehaviour
{
    [SerializeField] GameObject[] _prefabEnemy;
    [SerializeField] Transform _instanciaTransform;
    [SerializeField] int _filas = 3;
    [SerializeField] int _Columnas = 5;
    [SerializeField] float _limitXmin = -8f;
    [SerializeField] float _limitXmax = 8f;
    [SerializeField] float _limitZDistance = 1f; //distancai que debe recorrer en el eje Z
    [SerializeField] float _distanceTraveldZ = 0f; // distancia recorrida en Z
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] float _spaceInX;
    [SerializeField] float _spaceInZ;
    public float _cronometro;
    [SerializeField] float _posicionEjeX;
    [SerializeField] bool _activeUp = false;
    [SerializeField] List<EnemyController> enemies = new List<EnemyController>();
    [SerializeField] Vector2 _movementDirection = Vector2.right;

    private void Start() {
        InstantiatEnemy();
    }
    private void Update() {
        MoveEbeny();
        
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
    public void MoveEbeny() {

        //_instanciaTransform.position += (Vector3)_movementDirection * _moveSpeed * Time.deltaTime;
        foreach(EnemyController enemy in enemies) {
            if (enemy == null) continue; // ignorar enemigos destruidos
            float xPos = enemy.transform.localPosition.x;
            
            if (!_activeUp) {
                _distanceTraveldZ = 0;
                enemy.transform.localPosition += (Vector3)_movementDirection * Time.deltaTime;
                if (xPos > _limitXmax || xPos < _limitXmin) {
                    _movementDirection *= -1;
                    _activeUp = true;
                }
                _posicionEjeX = xPos;
            } else {
                
                enemy.transform.localPosition -= Vector3.forward  * Time.deltaTime;
                // acumular la distancia recorrida en Z       
                _distanceTraveldZ += Time.deltaTime;
                if (_distanceTraveldZ > _limitZDistance) {
                    _activeUp = false;
                    
                }
            }
            
        }
    }
}
