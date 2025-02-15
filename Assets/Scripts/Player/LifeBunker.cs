using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBunker : MonoBehaviour
{
    [SerializeField] float _lifemaxBunker;
    public float _lifeBunker;
    public Slider _sliderLife;
    [SerializeField] Animator _anim;
    private void Start() {
        _sliderLife.maxValue = _lifemaxBunker;
        _sliderLife.value = _lifemaxBunker;
        if (_anim == null) {
            _anim = GetComponentInChildren<Animator>();
        }
        GameManager.Instance._bunkers.Add(this);
    }

    public void AnimacionGolpeShield() {
        _anim.SetTrigger("Golpe");
    }
    public void RestoreToLife() {
        _lifeBunker = _lifemaxBunker;
    }
    public void PerderVida(float damage) {

        _sliderLife.value -= damage;
        _lifeBunker=_sliderLife.value;
        if (_lifeBunker <= 0) {
            //Haremos la animacion de destruccion aca
            gameObject.SetActive(false);
        }
    }
}
