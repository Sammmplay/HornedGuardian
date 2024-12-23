using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameSelecPlayer : MonoBehaviour
{
    public float _speed = 2;
    [SerializeField] float _distanceMax;
    RectTransform _rect;
    private void Start() {
       _rect = GetComponent<RectTransform>();
    }
    private void Update() {
        float _axisHorizontal = Input.GetAxis("Horizontal");

        _rect.localPosition += new Vector3(_axisHorizontal, 0, 0) * _speed * Time.deltaTime * 100;

        Vector3 posicion = _rect.localPosition;
        posicion.x = Mathf.Clamp(posicion.x, -_distanceMax, _distanceMax);
        _rect.localPosition = posicion;
    }
}
