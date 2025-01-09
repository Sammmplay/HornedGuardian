using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;
//using LeanTweenType = LeanTweenType;
public class ControlUi : MonoBehaviour
{
    [Header("Control UI (Botones)")]
    [Tooltip("Todo lo referente a los menus de escen entre 0 1 2 que son los menus de inicio" +
        "principio de escena,seleccion de guardian(Player) y seleccion de escena")]
    public static ControlUi Instance;

    public GameObject _evetSystem;
    public LeanTweenType easeType;
    [Header("Ajuste de Scala Botones")]
    [SerializeField] float _scaleMultiply = 1.3f;
    [SerializeField] float _animationduration;

    
    #region Menu Principal ambas escenas
    [SerializeField] RectTransform _panelMaiMenu;
    [SerializeField] RectTransform _panelOptions;
    [SerializeField] RectTransform _panelSelectPlayer;
    [SerializeField] RectTransform _levels;
    [SerializeField] RectTransform _panelInGame;
    public RectTransform _congratulations;
    [SerializeField] Image[] _buttonsImages;
    
    [SerializeField] Vector3 _posMainOptions;
    
    [SerializeField] float _timeLTMainOptions;
    [SerializeField] float _timeLTParpadeo;
    [Header("MenuInicioInGame")]
    [SerializeField] RectTransform _controlInicioJuego;
    [Header("Menu Pause")]
    [Tooltip("Controles para configurar todo lo referente al menu de pausa")] 
    [SerializeField] GameObject[] _panelsInGame;


    [Tooltip("Como aparecera la confirmacion de salida ")]
    [Header("MenuSalida")]
    [SerializeField] RectTransform _botonAjuste;
    [SerializeField] RectTransform _confirmacionSalida;
    [SerializeField] float _timeAnimationconfExit = 1.0f;

    [SerializeField] RectTransform _menuPause;
    [SerializeField] float _timeShakeMenuPause;
    
    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else Destroy(gameObject);
    }
    private void Start() {
        ActiveObject();
    }
    public void ActiveObject() {
        if (FindObjectOfType<EventSystem>()) {
            Destroy(FindObjectOfType<EventSystem>().gameObject);
            Instantiate(_evetSystem);
            ManagerEnemi.Instance._starGame = false;
        }
        int indexScene = SceneManager.GetActiveScene().buildIndex;
        switch (indexScene) {
            case 0:
                //sucesos de entrada para la escena 0 (MainMenu)
                _panelMaiMenu.gameObject.SetActive(true);
                _panelOptions.gameObject.SetActive(false);
                _panelSelectPlayer.gameObject.SetActive(false);
                _levels.gameObject.SetActive(false);
                _botonAjuste.gameObject.SetActive(false);
                _controlInicioJuego.gameObject.SetActive(false);
                GameManager.Instance._cronometro.SetActive(false);
                GameManager.Instance._endGame.gameObject.SetActive(false);
                GameManager.Instance._puntuacion.SetActive(false);
                DisableCanvasInGame();
                if (FindObjectOfType<PlayerController>()!= null ) {
                    Destroy(FindObjectOfType<PlayerController>().gameObject);
                }
                if (_buttonsImages != null) {
                    AnimacionConLeanTwin();
                }
                Time.timeScale = 1.0f;
                break;
            case 1:
                //sucesos de entrada para la escena 1(PLayerSelect)
                _panelSelectPlayer.gameObject.SetActive(true);
                _panelMaiMenu.gameObject.SetActive(false);
                _panelOptions.gameObject.SetActive(false);
                _botonAjuste.gameObject.SetActive(false);
                _levels.gameObject.SetActive(false);
                _controlInicioJuego.gameObject.SetActive(false);
                _congratulations.GetChild(0).gameObject.SetActive(false);
                break;
            case 2:
                _panelMaiMenu.gameObject.SetActive(false);
                _panelOptions.gameObject.SetActive(false);
                _panelSelectPlayer.gameObject.SetActive(false);
                _levels.gameObject.SetActive(true);
                _botonAjuste.gameObject.SetActive(false);
                _controlInicioJuego.gameObject.SetActive(false);
                break;
            case >= 3:
                Time.timeScale = 1.0f;
                _panelMaiMenu.gameObject.SetActive(false);
                _panelOptions.gameObject.SetActive(false);
                _panelSelectPlayer.gameObject.SetActive(false);
                _levels.gameObject.SetActive(false);
                _panelInGame.gameObject.SetActive(true);
                _botonAjuste.gameObject.SetActive(true);
                _controlInicioJuego.gameObject.SetActive(true);
                _congratulations.GetChild(0).gameObject.SetActive(false);
                ManagerSounds.Instance.PlayBackgroudnMusic(4);//reproduciendo tomy para musica de fondo
                break;
            default:
                break;
        }
    }
    public void ActiveCanvasInGame(int index) {
        for (int i = 0; i < _panelsInGame.Length; i++) {
            _panelsInGame[i].SetActive(false);
        }
        _panelsInGame[index].SetActive(true);
    }
    public void DisableCanvasInGame() {
        for (int i = 0; i < _panelsInGame.Length; i++) {
            _panelsInGame[i].SetActive(false);
        }
        
    }
    public void AnimacionConLeanTwin() {
        // movimiento del panel principal
        LeanTween.move(_panelMaiMenu, _posMainOptions, _timeLTMainOptions).setEase(easeType);
        //parpadeo de botones 
        for (int i = 0; i < _buttonsImages.Length; i++) {
            LeanTween.alpha(_buttonsImages[i].rectTransform, 0f, _timeLTParpadeo).setLoopPingPong().setEase(easeType);
        }

    }
    public void OnAtras(InputValue value) {
        Debug.Log("press atras");
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            if (_panelOptions.gameObject.activeSelf) {
                _panelOptions.gameObject.SetActive(false);
            } else return;
        }
    }
    public void PlayGame() {
        // cargar la escena 1 
        _panelInGame.gameObject.SetActive(false);

        LoadEscena.Instance.LoadEscenaActual(1);
    }
    public void Opciones() {
        _panelOptions.gameObject.SetActive(true);
    }
    public void ExitGame() {
        // Verifica si estamos en el editor de Unity
#if UNITY_EDITOR
        // Detiene el modo de juego en el Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else // Cierra la aplicación en una build ejecutable
        Application.Quit();
#endif
    }
    #endregion
    public void AnimacionScaleBotones(GameObject _boton) {
        
        LeanTween.scale(_boton, Vector3.one * _scaleMultiply, _animationduration).setIgnoreTimeScale(true);
    }
    public void RestartScaleBotones(GameObject _boton) {
        LeanTween.scale(_boton, Vector3.one, _animationduration).setIgnoreTimeScale(true);
    }

    #region Comienzo De Partida
    public void BotonDefender() {
        ManagerEnemi _scriptEnemi = FindObjectOfType<ManagerEnemi>();
        if (_scriptEnemi != null && SceneManager.GetActiveScene().buildIndex>=3) {
            _scriptEnemi.ComenzarNivel();
        }
    }
    #endregion

    #region Menu pause del juego
    public void AnimacionBotonAjuste() {
        LeanTween.scale(_menuPause, Vector3.one*1.1f, _timeShakeMenuPause).setEaseShake().setOnComplete(()=> _menuPause.gameObject.SetActive(false));
    }
    public void AnimacionConfirmacionSalida() {
        _confirmacionSalida.gameObject.SetActive(true);
        LeanTween.move(_confirmacionSalida, new Vector3(0, -326, 0), _timeAnimationconfExit).setIgnoreTimeScale(true);
    }
    public void AnimacionBotonNo() {
        LeanTween.move(_confirmacionSalida, new Vector3(0, -780, 0), 0.1f).setIgnoreTimeScale(true).setOnComplete(()=> _confirmacionSalida.gameObject.SetActive(false));
    }
    public void BotonConfirmacionSalidaSi() {
        LeanTween.move(_confirmacionSalida, new Vector3(0, -780, 0), 0.1f).setIgnoreTimeScale(true).setOnComplete(() => {

            _confirmacionSalida.gameObject.SetActive(false);
            LoadEscena.Instance.LoadEscenaActual(0);
        });
        
    }
    public void PauseInGame() {
        if(Time.timeScale == 0) {
            Time.timeScale = 1;
        } else {
            Time.timeScale = 0f;
        } 
    }
    public void Reintentar() {
        LoadEscena.Instance.LoadEscenaActual(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel() {
        Time.timeScale = 1;
        LoadEscena.Instance.LoadEscenaActual(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void CongratulationsBotonNo() {
        Time.timeScale = 1;
        LoadEscena.Instance.LoadEscenaActual(0);
    }
    #endregion

}


