using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;

public class SceneCameraTrigger : MonoBehaviour
{
    [SerializeField] private float transitionDuration = 2.5f;
    [SerializeField] private AudioMixerGroup sfxGroup = null;
    [SerializeField] private AudioMixerGroup musicGroup = null;
    [SerializeField] private AudioMixerGroup specialMusicGroup = null;

    private CinemachineVirtualCamera virtualCamera = null;
    private bool isActivated;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(ActivateAmbient());
    }

    private IEnumerator ActivateAmbient()
    {
        isActivated = true;
        virtualCamera.Priority = 90;

        float transitionStartTime = Time.time;
        float transitionEndTime = Time.time + transitionDuration;
        while (Time.time <= transitionEndTime)
        {
            float t = (Time.time - transitionStartTime) / transitionDuration;
            sfxGroup.audioMixer.SetFloat("VolSfx", Mathf.Lerp(-20, 0, 1f - t));
            sfxGroup.audioMixer.SetFloat("VolMusic", Mathf.Lerp(-20, 0, 1f - t));
            sfxGroup.audioMixer.SetFloat("VolMusicSpecial", Mathf.Lerp(-80, 0, t));
            yield return null;
        }

        while (isActivated)
            yield return null;

        transitionStartTime = Time.time;
        transitionEndTime = Time.time + transitionDuration;
        while (Time.time <= transitionEndTime)
        {
            float t = (Time.time - transitionStartTime) / transitionDuration;
            sfxGroup.audioMixer.SetFloat("VolSfx", Mathf.Lerp(-20, 0, t));
            sfxGroup.audioMixer.SetFloat("VolMusic", Mathf.Lerp(-20, 0, t));
            sfxGroup.audioMixer.SetFloat("VolMusicSpecial", Mathf.Lerp(-80, 0, 1f - t));
            yield return null;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        virtualCamera.Priority = 0;
        isActivated = false;
    }
}
