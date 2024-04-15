using UnityEngine;

public class BookSoundController : MonoBehaviour
{
    [Header("Book open")]
    [SerializeField] private AudioSource _bookOpenOne;
    [SerializeField] private AudioSource _bookOpenOnetwo;

    [Header("Book close")]
    [SerializeField] private AudioSource _bookCloseOne;
    [SerializeField] private AudioSource _bookClosetwo;

    [Header("Book page")]
    [SerializeField] private AudioSource _bookPageOne;
    [SerializeField] private AudioSource _bookPageTwo;
    [SerializeField] private AudioSource _bookPageThree;
    [SerializeField] private AudioSource _bookPageFour;

    public void OnBookOpenButtonPressed()
    {
        AudioSource[] soundClips = { _bookOpenOne, _bookOpenOnetwo };

        int randomIndex = Random.Range(0, soundClips.Length);

        soundClips[randomIndex].Play();
        Debug.Log($"{randomIndex} this sound has play");
    }

    public void OnBookCloseButtonPressed()
    {
        AudioSource[] soundClips = { _bookCloseOne, _bookClosetwo };

        int randomIndex = Random.Range(0, soundClips.Length);

        soundClips[randomIndex].Play();
        Debug.Log($"{randomIndex} this sound has play");
    }

    public void OnBookPageButtonPressed()
    {
        AudioSource[] soundClips = { _bookPageOne, _bookPageTwo, _bookPageThree, _bookPageFour };

        int randomIndex = Random.Range(0, soundClips.Length);

        soundClips[randomIndex].Play();
        Debug.Log($"{randomIndex} this sound has play");
    }
}
