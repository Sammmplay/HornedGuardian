using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBunker : MonoBehaviour
{
    [SerializeField] float _lifemaxBunker;
    public float _lifeBunker;
    public Slider _sliderLife;
    private void Start() {
        _sliderLife.maxValue = _lifemaxBunker;
        _sliderLife.value = _lifemaxBunker;
    }
    public void PerderVida(float damage) {

        _sliderLife.value -= damage;
        _lifeBunker=_sliderLife.value;
        if (_lifeBunker <= 0) {
            //Haremos la animacion de destruccion aca
            Destroy(gameObject/*, tiempo de animacion antes de destruir*/);
        }
    }
}
