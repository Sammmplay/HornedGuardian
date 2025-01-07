using System;
using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] _guardians;
    [SerializeField] Transform _spawPositionPlayer;
    public int _selecteGuardian = -1;
    public int _selectedLevelIndex = -1;
    [Header("Vidas")]
    [SerializeField] GameObject[] _lifes;
    [SerializeField] GameObject[] _lifeCurrent;
    [Header("Cronometro")]
    [SerializeField] GameObject _cronometro;
    [SerializeField] TextMeshProUGUI _textCronometro;
    [SerializeField] TextMeshProUGUI _textTimeGame;
    [SerializeField] float _time;
    [Header("Sistema de puntuacion")]
    [SerializeField] GameObject _puntuacion;
    [SerializeField] TextMeshProUGUI _puntCurrentText;
    [SerializeField] TextMeshProUGUI _puntMaxText;
    [SerializeField] float _puntCurrent;
    [SerializeField] float _puntMax;
    public  bool _starCount;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
    }
    private void Start() {
        if (_lifes[0] == null) {
            _lifes[0] = GameObject.Find("Lifes Phantom");
        }
        if (_lifes[1] == null) {
            _lifes[1] = GameObject.Find("LifesAegis");
        }
        if (_lifes[2] == null) {
            _lifes[2] = GameObject.Find("Lifes Colossus");
        }
        
    }
    private void Update() {
        StarCount();
    }
    public void InstantiateSelectedGuardian() {
        
        Instantiate(GameManager.Instance._guardians[_selecteGuardian], _spawPositionPlayer.position, Quaternion.identity);
        ControlUi.Instance.ActiveCanvasInGame(_selecteGuardian);
    }


    #region Sistema de vidas
    public void PerderVida() {
        if (_lifes[_selecteGuardian].transform.childCount > 0) {
            Transform lastChild = _lifes[_selecteGuardian].transform.GetChild(_lifes[_selecteGuardian].transform.childCount - 1);
            //desactiva el ultimo objeto
            lastChild.gameObject.SetActive(false);
            if (lastChild.childCount > 0) {
                Invoke("InstantiateSelectedGuardian", 1.5f);
            }
        }
    }
    #endregion

    #region Cronometro
    void StarCount() {
        if (_starCount) {
            _time += Time.deltaTime;
            TimeSpan tiempoFormado = TimeSpan.FromSeconds(_time);
            _textCronometro.text = tiempoFormado.ToString(@"hh\:mm\:ss");
        }
    }
    public void TiempoHecho() {
        TimeSpan tiempoFormado = TimeSpan.FromSeconds(_time);
        _textTimeGame.text = tiempoFormado.ToString(@"hh\:mm\:ss");
    }
    #endregion

    #region Sistema de puntuacion

    public void AddPuntuacion(float valor) {
        _puntCurrent += valor; ;
        if (_puntCurrentText != null) {
            _puntCurrentText.text = _puntCurrent.ToString();
        }
        if (_puntCurrent > _puntMax) {
            _puntMax = _puntCurrent;
            _puntMaxText.text = _puntMax.ToString();
            SavePunt();
        }
    }
    public void SavePunt() {
        PlayerPrefs.SetFloat("MaxPunt", _puntMax);
        PlayerPrefs.Save();
    }
    public void LoadPunt() {
        _puntMax = PlayerPrefs.GetFloat("MaxPunt", 0);
        _puntMaxText.text = _puntMax.ToString();
    }
    #endregion

    public void ComenzarJuego() {
        _puntuacion.SetActive(true);
        _cronometro.SetActive(true);
        _puntCurrentText.text = 0.ToString();
        _starCount = true;
        LoadPunt();
    }
}

