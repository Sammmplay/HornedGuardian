using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplocionBullet : MonoBehaviour
{

    [SerializeField] float _speed = 5.0f;

    public GameObject _Destruction;
    private void Start() {
        Destroy(gameObject, 5);
    }
    private void Update() {
        transform.position += Vector3.forward * _speed * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<EnemyController>()) {
            Instantiate(_Destruction, other.transform);
            Destroy(gameObject);

        }
    }
}
