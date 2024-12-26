using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PisoController : MonoBehaviour
{
    [SerializeField] Material _pisoMaterial;

    [SerializeField] float _speed = 0.1f;
    Vector2 _offset = Vector2.zero;
    [SerializeField] bool _enabled = false;
    private void Update() {
        if (_enabled) {
            _offset.y += _speed * Time.deltaTime;
            _offset.y %= 1;//cuando llegue a 1 se reinicia a 0;

            // aplica el offset al material
            _pisoMaterial.SetTextureOffset("_BaseMap", _offset);
        }
    }
}
