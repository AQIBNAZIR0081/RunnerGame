using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Setting Panel")]
    public GameObject settingPanel;

    [Space]
    [Header("Game Setting")]
    public GameObject mute;
    public GameObject unmute;
    public AudioSource backgroundMusic;

    private void Awake()
    {
        // Ensure the background music plays on main menu load
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
            Mute(false);
        }
    }

    // Start the game on button click
    public void StartGame()
    {
        SceneManager.LoadScene("EnvironmentSelection");
    }


    #region GameSetting
    // Mute or unmute the background music on button click
    public void MuteOrUnmuteSound()
    {
        // Check if the background music is playing
        if (backgroundMusic.isPlaying)
        {
            // Pause the background music
            backgroundMusic.Pause();

            Mute(true);
        }
        else
        {
            // Play the background music
            backgroundMusic.Play();

            Mute(false);
        }
    }

    private void Mute(bool isMute)
    {
        mute.SetActive(isMute);
        unmute.SetActive(!isMute);
    }

    #endregion

    #region SettingActivateDeactivate

    // Open the setting panel
    public void ClickOnSettingButton()
    {
        settingPanel.SetActive(true);
        settingPanel.gameObject.GetComponent<Animator>().Play("Open_SettingPanel");
    }

    // Close the setting panel
    public void ClickOnCloseButton()
    {
        settingPanel.gameObject.GetComponent<Animator>().SetBool("SettingClosed", true);
        Invoke(nameof(DeactivateSettingPanel), 0.8f);
    }

    private void DeactivateSettingPanel()
    {
        settingPanel.SetActive(false);
    }

    #endregion
}