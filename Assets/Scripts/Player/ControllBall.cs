using UnityEngine;

public class ControllBall : MonoBehaviour
{
    public float _speed;
    private float _damage;
    private void Start() {
        _damage = PlayerController.instance._damage;
    }
    private void Update() {
        transform.position += Vector3.forward * _speed * Time.deltaTime ;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.GetComponent<EnemyController>()) {
            EnemyController _contacto= collision.collider.GetComponent<EnemyController>();
            _contacto.PerderVida(PlayerController.instance._damage);
            Destroy(gameObject);
        }else if (collision.collider.CompareTag("Bunker")) {
            Destroy(gameObject);
        }
        Debug.Log(collision.gameObject.name);
    }
}

