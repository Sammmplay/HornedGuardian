using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletMiniGame : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision != null) {
            Button _buton = collision.collider.GetComponent<Button>();
            if (_buton != null) {
                _buton.onClick.Invoke();
            }

        }
    }

}
