using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardianController : MonoBehaviour
{
   public enum GuardianType {
        Equilibrado,
        Rapido,
        Lento
    }

    //atributos para las diferentes naves
    [System.Serializable]
    public class Guardian {
        public GuardianType _type; //tipo de nave
        public int _id;         //identificador de la nave
        public float _speed;    // velocidad de la nave
        public float _damage;   // da´ño de la nave
    }
    public Guardian[] _guardians; // lista de guardianes 
    private Guardian _selectedGuardian; // el guardian seleccionado 

    public void SelectedGuardian(GuardianType _guardianType) {
        _selectedGuardian = GetGuardianByType(_guardianType);
        StartGame();
    }
    //optener el guardian por el tipo

    private Guardian GetGuardianByType(GuardianType _guardianType) {
        foreach(Guardian guardian in _guardians) {
            if(guardian._type == _guardianType) {
                return guardian;
            }
        }
        return null;
    }

    //metodo para iniciar el juego con el Guardian seleccionado

    void StartGame() {
        PlayerPrefs.SetInt("SelectGuardian", _selectedGuardian._id);
        PlayerPrefs.SetFloat("SelectedSpeed", _selectedGuardian._speed);
        PlayerPrefs.SetFloat("SelectedDamage", _selectedGuardian._damage);
        SceneManager.LoadScene(2);
    }
}
