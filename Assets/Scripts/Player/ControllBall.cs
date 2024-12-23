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
        transform.position += Vector3.forward * _speed * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<EnemyController>()) {
            EnemyController _contacto = other.GetComponent<EnemyController>();
            _contacto.PerderVida(PlayerController.instance._damage);
            Destroy(gameObject);
        } else if (other.CompareTag("Bunker")) {
            Destroy(gameObject);
        }
        Debug.Log(other.gameObject.name);
    }
}

