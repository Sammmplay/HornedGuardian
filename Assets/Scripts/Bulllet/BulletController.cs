using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float _ballSpeed;
    float _damage;
    [SerializeField] float _timeLifeObject = 8;
    EnemyController _scrip;
    private void Start() {
        
        _scrip = GetComponentInParent<EnemyController>();
        _damage = _scrip._damage;
        Destroy(gameObject,_timeLifeObject);
    }
    private void Update() {
        transform.position += Vector3.back * _ballSpeed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision) {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if(other.GetComponent<PlayerController>()!= null) {
                other.GetComponent<PlayerController>().DestructionObject();
            }
            Destroy(gameObject);
        } else if (other.CompareTag("Bunker")) {
            Debug.Log("ColisionConBunker");
            LifeBunker _scriptBunker = other.GetComponent<LifeBunker>();
            if (_scriptBunker != null) {
                _scriptBunker.AnimacionGolpeShield();
                _scriptBunker.PerderVida(_damage);
                other.GetComponent<AudioSource>().Play();
            } else {
                Debug.Log("No se encontro LifeBunker");
            }

            Destroy(gameObject);
        }
    }
}
