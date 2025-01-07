using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionZ : MonoBehaviour
{
    private void OnTriggerStay(Collider other) {
        ManagerEnemi _scrip = FindObjectOfType<ManagerEnemi>();
        _scrip._movingInZ = false;
    }
    private void OnTriggerExit(Collider other) {
        ManagerEnemi _script = FindObjectOfType<ManagerEnemi>();
        _script._movingInZ = true;
    }
}
