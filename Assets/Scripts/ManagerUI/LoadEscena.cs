using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadEscena : MonoBehaviour
{
    public static LoadEscena Instance;
    [SerializeField] GameObject _backgorund;
    [SerializeField] Slider _sliderCarga;
    [SerializeField] TextMeshProUGUI _progressText;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

    }
    private void Start() {
        _backgorund.SetActive(false);
    }
    public void LoadEscenaActual(int index) {
        StartCoroutine(ChanchEscene(index));
    }
    IEnumerator ChanchEscene(int index) {
        _backgorund.SetActive(true);
        //Cargar la escena de forma Asyncrona
        AsyncOperation asyncOpetarion = SceneManager.LoadSceneAsync(index);
        //esèrar a que la escena se cargue completamente 
        while (!asyncOpetarion.isDone) {
            float progres = Mathf.Clamp01(asyncOpetarion.progress/0.9f);
            _sliderCarga.value = progres;
            float porcentaje = progres * 100;
            _progressText.text = porcentaje.ToString()+ "%";
            yield return null;
        }
        ControlUi.Instance.ActiveObject();
        //esperar 0.5 segundos adicionales despues de que la carga este completada
        yield return new WaitForSeconds(0.5f);
        _backgorund.SetActive(false);
    }
}
