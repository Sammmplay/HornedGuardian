using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniGameSelecPlayer : MonoBehaviour
{
    public float _speed = 2;
    [SerializeField] float _distanceMax;
    [SerializeField] float _speedBullet;
    [SerializeField] float _recolding;
    [SerializeField] float _time = 0;
    [SerializeField] float _count = 0;
    [SerializeField] bool _fireActive = false;
    RectTransform _rect;
    [SerializeField] RectTransform _leftBullet;
    [SerializeField] RectTransform _rigthBullet;
    [SerializeField] GameObject _prefabBullet;
    [SerializeField] List<GameObject> _list = new List<GameObject>();
    private void Start() {
        _rect = GetComponent<RectTransform>();
        if(_rigthBullet == null) {
            _rigthBullet = GameObject.Find("RigthBullet").GetComponent<RectTransform>();
        }
        if(_leftBullet == null) {
            _leftBullet = GameObject.Find("LeftBullet").GetComponent<RectTransform>();
        }
        
    }
    private void Update() {
        float _axisHorizontal = Input.GetAxis("Horizontal");

        _rect.localPosition += new Vector3(_axisHorizontal, 0, 0) * _speed * Time.deltaTime * 100;

        Vector3 posicion = _rect.localPosition;
        posicion.x = Mathf.Clamp(posicion.x, -_distanceMax, _distanceMax);
        _rect.localPosition = posicion;

        Disparo();
        MoveBullet();
    }
    public void Disparo() {
        _fireActive = Input.GetButton("Disparo");
         _time += Time.deltaTime;
        if (_fireActive && _time >= _recolding) {
            FireBullet();
            _time = 0;
            Debug.Log("disparo Activado");
        }
    }
    void FireBullet() {
        if (_count == 0) {
            GameObject _position = GameObject.Find("MiniJuegoSelecPlayer");
            GameObject _prefab = Instantiate(_prefabBullet, _rigthBullet.transform);
            _prefab.transform.SetParent(_position.transform);
            _list.Add(_prefab);
            _count = 1;
        }else if(_count == 1) {
            GameObject _position = GameObject.Find("MiniJuegoSelecPlayer");
            GameObject _prefab = Instantiate(_prefabBullet,_leftBullet.transform);
            _prefab.transform.SetParent(_position.transform);
            _list.Add(_prefab);
            _count = 0;
        }
        
        
    }
    void MoveBullet() {
        for (int i = 0; i < _list.Count; i++) {
            if (_list[i]== null) {
                _list.RemoveAt(i);
            } else {
                _list[i].transform.localPosition += Vector3.up * _speed * Time.deltaTime * 100;
                Destroy(_list[i], 5);
            }
            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision ) {
            Button _boton = collision.GetComponentInParent<Button>();
            Debug.Log(_boton.gameObject.name);
            PointerEventData _pointEvent = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(_boton.gameObject, _pointEvent, ExecuteEvents.pointerEnterHandler);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision != null) {
            Button _boton = collision.GetComponentInParent<Button>();
            if(_boton != null){
                PointerEventData _pointEvent = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(_boton.gameObject, _pointEvent, ExecuteEvents.pointerExitHandler);
            }
        }
    }
}
