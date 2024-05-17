using UnityEngine;

public class StepSoundController : MonoBehaviour
{
    [SerializeField] private AudioClip stepSoundEffect = null;

    private AudioSource audioSource = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayStepSound()
    {
        audioSource.PlayOneShot(stepSoundEffect);
    }
}
