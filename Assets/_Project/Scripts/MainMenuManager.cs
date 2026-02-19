using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
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
        SceneManager.LoadScene("MissionSelection");
    }

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

}
