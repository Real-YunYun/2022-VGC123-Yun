using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	private int callToPlayerController;
	public Button ContinueButton;
	public Button SelectStageButton;
	public Button QuitGameButton;

	void Start()
	{
		callToPlayerController = PlayerController._lives;

		Button temp1 = ContinueButton.GetComponent<Button>();
		Button temp2 = SelectStageButton.GetComponent<Button>();
		Button temp3 = QuitGameButton.GetComponent<Button>();

		if (callToPlayerController == 0) temp1.enabled = false;
		else temp1.enabled = true;

		temp1.onClick.AddListener(LoadLevel1);
		temp2.onClick.AddListener(LoadTitleScreen);
		temp3.onClick.AddListener(QuitGame);
	}

	void LoadLevel1()
	{
		SceneManager.LoadScene(1);
	}

	void LoadTitleScreen()
	{
		SceneManager.LoadScene(0);
	}

	void QuitGame()
    {
		Application.Quit(0);
    }
}