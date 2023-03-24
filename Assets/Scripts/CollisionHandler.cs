using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 3f;
    [SerializeField] AudioClip audioCrash;
    [SerializeField] AudioClip audioSuccess;
    [SerializeField] ParticleSystem particlesSuccess;
    [SerializeField] ParticleSystem particlesCrash;
    

    AudioSource _audioSource;
    
    bool isTransitioning = false;
    bool collisionDisabled = false;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
    

    private void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisabled == true) {return;}
        switch (other.gameObject.tag)
        {
        case "Friendly":
            //Debug.Log("You've bumped into friendly object");
            break;
        case "Finish":
            StartSuccessSequence();
            break;
        default:
            StartCrashSequence();
            break;
        }
        
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(audioCrash);
        particlesCrash.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        
    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(audioSuccess);
        particlesCrash.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
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
        
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
        SceneManager.LoadScene(nextSceneIndex);  
    }
    
}
