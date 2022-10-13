using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) { return; }
  
        switch(collision.gameObject.tag)
        {
            case "Finish":
                StartSuccessSequence();
                Debug.Log("Hit finish");
                break;
            case "Friendly":
                Debug.Log("Hit friendly");
                break;
            default:
                StartCrashSequence();
                Debug.Log("Hit obstacle");
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", LevelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", LevelLoadDelay);
    }

    void ReloadLevel()
    {
       int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);

    }
}
