using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecteionControll : MonoBehaviour
{
    public void SelectGuardian(int _guardianIndex) {
        GameManager.Instance._selecteGuardian = _guardianIndex;
    }
    public void SelectLevel(int _levelIndex) {
        GameManager.Instance._selectedLevelIndex = _levelIndex;
        LoadLevel();
    }
    void LoadLevel() {
        if(GameManager.Instance._selecteGuardian != -1 && GameManager.Instance._selectedLevelIndex != -1) {
            SceneManager.LoadScene(GameManager.Instance._selectedLevelIndex+2);
        }
    }
}