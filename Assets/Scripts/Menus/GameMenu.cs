using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    private PlayerControls controls;
    [SerializeField] GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Game Menu");
        controls = new PlayerControls();
        controls.Gameplay.Enable();
        controls.Gameplay.Pause.performed += PauseKey;
        controls.Gameplay.Reset.performed += ResetKey;
        controls.Gameplay.Quit.performed += QuitKey;
        controls.Gameplay.Continue.performed += ContinueKey;
    }

    private void PauseKey(InputAction.CallbackContext context)
    {
        Debug.Log("Pause key");
        Pause(true);
    }

    private void ResetKey(InputAction.CallbackContext context)
    {
        Debug.Log("Reset key");
        Reset();
    }

    private void ContinueKey(InputAction.CallbackContext context)
    {
        Debug.Log("Continue key");
        Pause(false);
    }

    private void QuitKey(InputAction.CallbackContext context)
    {
        Debug.Log("Continue key");
        SceneManager.LoadScene("LevelSelection");
    }

    public void Pause(bool doPause)
    {
        if (doPause)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        } 
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void Reset()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level" + Persistent.CurrentLevel);
    }


    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelection");
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
