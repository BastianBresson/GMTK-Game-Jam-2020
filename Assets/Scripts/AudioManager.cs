using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private List<AudioClip> clips = default;


    private int nextSong;
    private static AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clips[0];
        audioSource.Play();
    }

    public void OnGameStart()
    {
        // fade

        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = clips[1];
        nextSong = 2;

        Invoke("PlayNextSong", 2f);

    }

    private void PlayNextSong()
    {
        audioSource.clip = clips[nextSong];
        audioSource.Play();

        nextSong++;
        if (nextSong == clips.Count)
        {
            nextSong = 1;
        }

        Invoke("PlayNextSong", audioSource.clip.length + 5f);

    }


    public void OnGameCompleted()
    {
        audioSource.Stop();
        CancelInvoke();

        Invoke("PlayVictoryMusic", 2f);
    }

    private void PlayVictoryMusic()
    {
        audioSource.clip = clips[0];
        audioSource.loop = true;
        audioSource.Play();
    }
}
