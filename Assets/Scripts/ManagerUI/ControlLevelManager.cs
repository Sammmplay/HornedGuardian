using UnityEngine;
using UnityEngine.SceneManagement;


public class ControlLevelManager : MonoBehaviour
{
    public Transform _spawnPoint;
    private void Start() {
        if(SceneManager.GetActiveScene().buildIndex >= 3 ){
            InstantiateSelectedGuardian();
        }
    }
    public void InstantiateSelectedGuardian() {

        var selectedGuardian = GameManager.Instance._selecteGuardian;
        if(selectedGuardian != -1 && selectedGuardian < GameManager.Instance._guardians.Length) {
            Instantiate(GameManager.Instance._guardians[selectedGuardian], _spawnPoint.position, Quaternion.identity);
        }
    }
}
