using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;
//using LeanTweenType = LeanTweenType;
public class ControlUi : MonoBehaviour
{
    public static ControlUi Instance;
    public LeanTweenType easeType;
    #region Menu Principal ambas escenas
    [SerializeField] RectTransform _panelMaiMenu;
    [SerializeField] RectTransform _panelOptions;
    [SerializeField] RectTransform _panelSelectPlayer;
    [SerializeField] RectTransform _levels;
    [SerializeField] RectTransform _panelInGame;
    [SerializeField] Image[] _buttonsImages;
    
    [SerializeField] Vector3 _posMainOptions;
    
    [SerializeField] float _timeLTMainOptions;
    [SerializeField] float _timeLTParpadeo;
    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else Destroy(gameObject);
    }
    private void Start() {
        int indexScene = SceneManager.GetActiveScene().buildIndex;
        switch (indexScene) {
            case 0:
                //sucesos de entrada para la escena 0 (MainMenu)
                _panelMaiMenu.gameObject.SetActive(true);
                _panelOptions.gameObject.SetActive(false);
                _panelSelectPlayer.gameObject.SetActive(false);
                _levels.gameObject.SetActive(false);
                if (_buttonsImages != null) {
                    AnimacionConLeanTwin();
                }break;
                case 1:
                //sucesos de entrada para la escena 1(PLayerSelect)
                _panelSelectPlayer.gameObject.SetActive(true);
                _panelMaiMenu.gameObject.SetActive(false);
                _panelOptions.gameObject.SetActive(false);
                
                _levels.gameObject.SetActive(false);

                break;
                case 2:
                _panelMaiMenu.gameObject.SetActive(false);
                _panelOptions.gameObject.SetActive(false);
                _panelSelectPlayer.gameObject.SetActive(false);
                _levels.gameObject.SetActive(true);
                break;
            case > 3:
                _panelMaiMenu.gameObject.SetActive(false);
                _panelOptions.gameObject.SetActive(false);
                _panelSelectPlayer.gameObject.SetActive(false);
                _levels.gameObject.SetActive(false);
                break;
            default:
                break;
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

        SceneManager.LoadScene(1);
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

}


