using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Game/Audio Library")]
public class AudioLibrary : ScriptableObject
{
    public AudioClip mainMusic;
    public AudioClip defaultButtonClick;
    public AudioClip newHighScore;
}
