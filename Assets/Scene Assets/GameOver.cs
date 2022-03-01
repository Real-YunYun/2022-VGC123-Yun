using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public Button ContinueButton;
	public Button SelectStageButton;
	public Button QuitGameButton;

	void Start()
	{

		Button temp1 = ContinueButton.GetComponent<Button>();
		Button temp2 = SelectStageButton.GetComponent<Button>();
		Button temp3 = QuitGameButton.GetComponent<Button>();

		if (GameManager.state.lives == 0) temp1.enabled = false;
		else temp1.enabled = true;

		temp1.onClick.AddListener(LoadLevel);
		temp2.onClick.AddListener(LoadTitleScreen);
		temp3.onClick.AddListener(QuitGame);
	}

	void LoadLevel()
	{
		SceneManager.LoadScene(GameManager.state.PlayingLevel);
	}

	void LoadTitleScreen()
	{
		GameManager.state.PlayingLevel = 0;
		SceneManager.LoadScene(0);
	}

	void QuitGame()
    {
		Application.Quit(0);
    }
}