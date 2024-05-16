using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;

public class SceneCameraTrigger : MonoBehaviour
{
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

        float countdown = 2.5f;
        while (countdown > 0f)
        {
            float t = countdown / 2.5f;
            sfxGroup.audioMixer.SetFloat("VolSfx", Mathf.Lerp(-20, 0, t));
            sfxGroup.audioMixer.SetFloat("VolMusic", Mathf.Lerp(-20, 0, t));
            sfxGroup.audioMixer.SetFloat("VolMusicSpecial", Mathf.Lerp(-80, 0, 1f - t));
            countdown -= Time.deltaTime;
            yield return null;
        }

        while (isActivated)
            yield return null;

        countdown = 2.5f;
        while (countdown > 0f)
        {
            float t = countdown / 2.5f;
            sfxGroup.audioMixer.SetFloat("VolSfx", Mathf.Lerp(-20, 0, 1f - t));
            sfxGroup.audioMixer.SetFloat("VolMusic", Mathf.Lerp(-20, 0, 1f - t));
            sfxGroup.audioMixer.SetFloat("VolMusicSpecial", Mathf.Lerp(-80, 0, t));

            countdown -= Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        virtualCamera.Priority = 0;
        isActivated = false;
    }
}
