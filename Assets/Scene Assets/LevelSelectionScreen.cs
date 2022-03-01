using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectionScreen : MonoBehaviour
{
	public Button buttonGUTSMAN;
	public Button buttonBOMBMAN;

	void Start()
	{
		GameManager.state.lives = 3;
		Button btnGUTSMAN = buttonGUTSMAN.GetComponent<Button>();
		Button btnBOMBMAN = buttonBOMBMAN.GetComponent<Button>();
		btnGUTSMAN.onClick.AddListener(LoadLevelGUTSMAN);
		btnBOMBMAN.onClick.AddListener(LoadLevelBOMBMAN);
	}

	void LoadLevelGUTSMAN()
	{
		GameManager.state.PlayingLevel = 2;
		SceneManager.LoadScene(GameManager.state.PlayingLevel);
	}
	
	void LoadLevelBOMBMAN()
	{
		SceneManager.LoadScene(3);
	}
}
