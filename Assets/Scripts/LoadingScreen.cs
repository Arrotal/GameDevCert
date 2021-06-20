using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _loadingBar;
    [SerializeField] private TMP_Text _loading, _pressSpace;
    private void Start()
    {

        StartCoroutine(LoadScene());
    }
    WaitForEndOfFrame wfef = new WaitForEndOfFrame();
    private bool _isLoaded;
    private float _loadedamount;
    IEnumerator LoadScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(2);
        asyncOperation.allowSceneActivation = true;
        while (!asyncOperation.isDone)
        {
            _loadingBar.fillAmount = asyncOperation.progress;
            _loadedamount = asyncOperation.progress * 100;
            _loading.text = _loadedamount.ToString()+ "%";
            yield return wfef;
        }
        if (asyncOperation.isDone)
        {
            _isLoaded = true;
            _loading.text = "Complete";
            _pressSpace.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (_isLoaded && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(2);
        }
    }

}
