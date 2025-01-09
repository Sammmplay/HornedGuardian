using UnityEngine;

public abstract class PowerUps : MonoBehaviour
{
    public float fallSpeed = 10;

    protected virtual void Update() {
        transform.position += Vector3.back * fallSpeed * Time.deltaTime;
    }
    public abstract void ApplyEffect(PlayerController player);
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            ApplyEffect(other.GetComponent<PlayerController>());
            Destroy(gameObject);
        }
    }
}
