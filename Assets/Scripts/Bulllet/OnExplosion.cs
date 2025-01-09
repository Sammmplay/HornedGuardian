using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnExplosion : MonoBehaviour
{
    [SerializeField] float _damage;

    private void OnTriggerEnter(Collider other) {
        Collider[] _colliders = other.GetComponents<Collider>();


        foreach (Collider _hit in _colliders) {
            if (_hit != null  && _hit.GetComponent<EnemyController>()) {
                _hit.GetComponent<EnemyController>().PerderVida(_damage);
            }
            
            
        }
        Destroy(gameObject, 4.0f);
    }
}
