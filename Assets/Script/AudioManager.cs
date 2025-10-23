using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioInstance;

    public AudioSource bgmSource, sfxSource;

    public AudioClip mainmenuBgm, IngameBgm, winSfx;


    private void Awake()
    {
        if (AudioInstance == null)
        {
            AudioInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void bgmPlay(AudioClip bgmSound)
    {
        if(bgmSource.isPlaying)
            bgmSource.Stop();

        bgmSource.clip = bgmSound; 
        bgmSource.Play();
    }

    public void sfxPlay(AudioClip sfxSound)
    {
        sfxSource.PlayOneShot(sfxSound);
    }



}
