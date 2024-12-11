using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] _guardians;

    public int _selecteGuardian = -1;
    public int _selectedLevelIndex = -1;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
    }
}

