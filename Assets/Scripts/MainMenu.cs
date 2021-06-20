using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Canvas _mainMenu,_options;
    void Start()
    {
        _options.enabled = false;
        _mainMenu.enabled = true;
    }



    public void ExitGame()
    {
        Application.Quit();
    }

    public void OptionsMenu()
    {
        _mainMenu.enabled = false;
        _options.enabled = true;
    }

    public void StartGame()
    {
        StartCoroutine(StartGameWait());
    }
    IEnumerator StartGameWait()
    {
        yield return new WaitForSeconds(.4f);

        SceneManager.LoadScene(1);
    }

    public void ExitOptions()
    {
        _mainMenu.enabled = true ;
        _options.enabled = false;
    }

}
