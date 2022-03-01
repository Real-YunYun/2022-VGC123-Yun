using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    static GameManager _state = null;
    public static GameManager state
    {
        get { return _state; }
        set { _state = value; }
    }

    int _score = 0;
    int _lives = 1;
    public int maxLives = 3;
    public int PlayingLevel;
    public GameObject PlayerPrefab;
    [HideInInspector] public GameObject PlayerInstance;
    [HideInInspector] public Transform MegaMan;
    [HideInInspector] public Level currentLevel;

    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
        }
    }

    public int lives
    {
        get { return _lives; }
        set
        {
            if (_lives > value)
            {
                //respawn our character at checkpoint
                Destroy(PlayerInstance);
                SceneManager.LoadScene("Game Over");
            }


            _lives = value;
            if (_lives > maxLives)
                _lives = maxLives;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (state)
        {
            Destroy(gameObject);
        }
        else
        {
            state = this;
            DontDestroyOnLoad(gameObject);
        }

        MegaMan = PlayerInstance.transform.Find("Mega Man").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        PlayerInstance = Instantiate(PlayerPrefab, spawnLocation.position, spawnLocation.rotation);
        MegaMan = PlayerInstance.transform.GetChild(0);
    }

}