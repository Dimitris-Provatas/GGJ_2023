using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip ambient;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip closeDrawer;
    [SerializeField] private AudioClip hover;
    [SerializeField] private AudioClip openDrawer;
    [SerializeField] private AudioClip pickUp;
    [SerializeField] private AudioClip putItem;
    [SerializeField] private AudioClip rootsTheme; // Menu
    [SerializeField] private AudioClip rootsThemeInspector; // Inspect
    [SerializeField] private AudioClip rootsThemeGramophone; // directional, plays all the time
    [SerializeField] private AudioClip rootsThemeWin;
    [SerializeField] private AudioClip rootsThemeLose;
    [SerializeField] private AudioClip drawClose;
    [SerializeField] private AudioClip drawOpen;
    [SerializeField] private AudioClip fireplace;
    [SerializeField] private AudioClip walkCarpet;
    [SerializeField] private AudioClip walkWood;
    [SerializeField] private AudioClip easterEgg;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource gramophoneAudioSource;
    [SerializeField] private AudioSource inspectorAudioSource;
    [SerializeField] private AudioSource soundEffectSource;
    [SerializeField] private AudioSource ambientSoundSource;
    [SerializeField] private AudioSource stepSoundSource;

    private bool haveThemeWithHighPassPlaying = false;

    public static SoundManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        gramophoneAudioSource.clip = rootsThemeGramophone;
        gramophoneAudioSource.loop = true;
        gramophoneAudioSource.playOnAwake = true;
        gramophoneAudioSource.mute = false;

        inspectorAudioSource.clip = rootsThemeInspector;
        inspectorAudioSource.loop = true;
        inspectorAudioSource.playOnAwake = true;
        inspectorAudioSource.mute = true;

        ambientSoundSource.clip = ambient;
        ambientSoundSource.loop = true;
        ambientSoundSource.playOnAwake = true;
        ambientSoundSource.mute = false;

        stepSoundSource.clip = walkWood;
    }
    public void PlayWalkingSound(bool isOnWood)
    {
        if (stepSoundSource.isPlaying) return;

        stepSoundSource.clip = isOnWood ? walkWood : walkCarpet;
        stepSoundSource.Play();
    }

    public void ToggleThemePlaying()
    {
        haveThemeWithHighPassPlaying = JournalController.instance.journalPanel.activeInHierarchy;

        if (haveThemeWithHighPassPlaying)
        {
            gramophoneAudioSource.mute = true;
            inspectorAudioSource.mute = false;
            ambientSoundSource.mute = true;
        }
        else
        {
            gramophoneAudioSource.mute = false;
            inspectorAudioSource.mute = true;
            ambientSoundSource.mute = false;
        }
    }

    public void PlaySoundEffect(string soundEffectName)
    {
        switch (soundEffectName)
        {
            case "click":
                soundEffectSource.PlayOneShot(click);
                break;
            case "closeDrawer":
                soundEffectSource.PlayOneShot(closeDrawer);
                break;
            case "hover":
                soundEffectSource.PlayOneShot(hover);
                break;
            case "openDrawer":
                soundEffectSource.PlayOneShot(openDrawer);
                break;
            case "pickUp":
                soundEffectSource.PlayOneShot(pickUp);
                break;
            case "putItem":
                soundEffectSource.PlayOneShot(putItem);
                break;
            case "drawClose":
                soundEffectSource.PlayOneShot(drawClose);
                break;
            case "drawOpen":
                soundEffectSource.PlayOneShot(drawOpen);
                break;
            case "easterEgg":
                soundEffectSource.PlayOneShot(easterEgg);
                break;
            default:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                Debug.LogError($"There is no sound clip named {soundEffectName}!");
#else
        Application.Quit();
#endif
                break;
        }
    }
}
