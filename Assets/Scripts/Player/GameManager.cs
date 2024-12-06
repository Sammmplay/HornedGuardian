using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float _playerSpeed;
    float _playerDamage;

    private void Start() {
        //obtener la informacion del guardian seleccionado 
        int _guardianId = PlayerPrefs.GetInt("SelectedSpeed");
        _playerSpeed = PlayerPrefs.GetFloat("SelectedDamage");

        
    }

    void SetupPlayer() {
        
    }
}
