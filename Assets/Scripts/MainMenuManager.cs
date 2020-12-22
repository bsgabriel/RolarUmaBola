using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //Função para começar o jogo (chamada quando clica no botão)
    public void StartGame()
    {
        //Função para chamar uma scene (nome exato da scene)
        SceneManager.LoadScene("Level0");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
	
	public void QuitToMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
