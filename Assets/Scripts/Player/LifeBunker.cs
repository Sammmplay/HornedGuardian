using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBunker : MonoBehaviour
{
    [SerializeField] float _lifemaxBunker;
    public float _lifeBunker;
    public Slider _sliferLife;
    private void Start() {
        _sliferLife.maxValue = _lifemaxBunker;
        _sliferLife.value = _lifemaxBunker;
    }
    public void PerderVida(float damage) {

        _sliferLife.value -= damage;
    }
}
