using UnityEngine;

public class ControllBall : MonoBehaviour
{
    public float _speed;
    private float _damage;
    Rigidbody _rb;
    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _damage = PlayerController.instance._damage;
    }
    private void Update() {
        Vector3 _newPosition = transform.position;
        _newPosition.y = 0f;
        _newPosition += Vector3.forward * _speed * Time.deltaTime;
        transform.position = _newPosition;  
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<EnemyController>()) {
            EnemyController _contacto = other.GetComponent<EnemyController>();
            if (_contacto != null) {
                _contacto.PerderVida(_damage);
                
            }
            Destroy(gameObject);
        } else if (other.CompareTag("Bunker")) {
            Destroy(gameObject);
        }
    }
}

