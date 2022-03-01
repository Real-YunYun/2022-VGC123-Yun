using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public Button yourButton;

	void Start()
	{
		GameManager.state.lives = 3;
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(LoadLevel);
	}

	void LoadLevel()
    {
		SceneManager.LoadScene(1);
    }
}