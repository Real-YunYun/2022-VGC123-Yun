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
		PlayerController._lives = 3;
		Button btnGUTSMAN = buttonGUTSMAN.GetComponent<Button>();
		Button btnBOMBMAN = buttonBOMBMAN.GetComponent<Button>();
		btnGUTSMAN.onClick.AddListener(LoadLevelGUTSMAN);
		btnBOMBMAN.onClick.AddListener(LoadLevelBOMBMAN);
	}

	void LoadLevelGUTSMAN()
	{
		SceneManager.LoadScene(2);
	}
	
	void LoadLevelBOMBMAN()
	{
		SceneManager.LoadScene(3);
	}
}
