using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private GameObject stopWatch;
    [SerializeField] private Scrollbar volumeBar;
    [SerializeField] private Scrollbar cutoffBar;
    [SerializeField] private Scrollbar octaveBar;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        volumeBar.onValueChanged.AddListener(val => ToggleVolume());
        cutoffBar.onValueChanged.AddListener(val => ToggleCutoff());
        octaveBar.onValueChanged.AddListener(val => ToggleOctave());
    }
    private void OnDisable()
    {
        volumeBar.onValueChanged.RemoveListener(val => ToggleVolume());
        cutoffBar.onValueChanged.RemoveListener(val => ToggleCutoff());
        octaveBar.onValueChanged.RemoveListener(val => ToggleOctave());
    }
    private void Start()
    {
        volumeBar.value = audioSource.volume;
        octaveBar.value = audioSource.pitch;
    }

    private void ToggleVolume()
    {
        audioSource.volume = volumeBar.value;
    }
    private void ToggleCutoff()
    {
        Debug.Log("Change Cutoff");
    }
    private void ToggleOctave()
    {
        audioSource.pitch = octaveBar.value;
    }

}
