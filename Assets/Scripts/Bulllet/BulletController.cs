using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float _ballSpeed;
    float _damage;
    [SerializeField] float _timeLifeObject = 8;
    [SerializeField] EnemyController _scrip;
    private void Start() {
        
        _scrip = GetComponentInParent<EnemyController>();
        _damage = _scrip._damage;
        Destroy(gameObject,_timeLifeObject);
    }
    private void Update() {
        transform.position += Vector3.back * _ballSpeed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Player")) {
            
            Destroy(gameObject);
        }
        else if (collision.collider.CompareTag("Bunker")) {
            LifeBunker _scriptBunker = collision.collider.GetComponent<LifeBunker>();
            if (_scriptBunker != null) {
                _scriptBunker.PerderVida(_damage);
            } else {
                Debug.Log("No se encontro LifeBunker");
            }
            
            Destroy(gameObject);
        }
    }
}
