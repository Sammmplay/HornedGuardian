using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplocionDestruction : MonoBehaviour
{

    [SerializeField] float _explocionForce = 500f;
    [SerializeField] float _explosionRadius = 2f;
    [SerializeField] float _upwardModifier = 1f; //Modificador para que las partes salgan hacia arriaba
    // Update is called once per frame
    void Update()
    {
        
        foreach (Transform t in transform) {
            Debug.Log(t.childCount);
            Rigidbody _rb = t.GetComponentInChildren<Rigidbody>();
            _rb.isKinematic = false;
            _rb.AddExplosionForce(_explocionForce, transform.position, _explosionRadius, _upwardModifier, ForceMode.Impulse);
            Destroy(gameObject, 0.5f);
        }
    }
}
