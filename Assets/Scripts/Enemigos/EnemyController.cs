using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float _life;
    public float _maxLife;
    Slider _slider;
    [SerializeField] Canvas _canvas;
    private void Start() {
        _life = _maxLife;
        _slider = GetComponentInChildren<Slider>();
        _canvas = GetComponentInChildren<Canvas>();
        if(_canvas.renderMode == RenderMode.WorldSpace) {
            _canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        InicializarVida(_life,_maxLife);
    }

    public void InicializarVida(float lifeactual, float maxlife) {
        
        _slider.maxValue = maxlife;
        _slider.value = lifeactual;
    }
    public void PerderVida(float damage) {
        _life -= damage;
        _slider.value = _life;
        if(_life<= 0) {
            Destroy(gameObject);
        }
    }
}
