using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Jogar()
    {
        SceneManager.LoadScene("main");
    }
    public void JogarChase()
    {
        SceneManager.LoadScene("mainchase");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
