using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AcousticOrchestrator : MonoBehaviour
{ 
    [FormerlySerializedAs("_coreResonance")] [SerializeField] private AudioMixer resonanceCoreModule;
    [FormerlySerializedAs("waveSwitchIcon")] [SerializeField] private Image waveToggleButton;
    [FormerlySerializedAs("melodySwitchIcon")] [SerializeField] private Image melodySwitchGlyph;
    [FormerlySerializedAs("pulseActivateGlyph")] [SerializeField] private Sprite pulseActivationGlyph;
    [FormerlySerializedAs("pulseDeactivateGlyph")] [SerializeField] private Sprite pulseDeactivationGlyph;

    private bool sonicEchoEnabled;
    private bool audioFlowThread;

    void Start()
    {
        sonicEchoEnabled = PlayerPrefs.GetInt(OmniConfigGovernor.BackgroundMusicLevel, 1) == 1;
        audioFlowThread = PlayerPrefs.GetInt(OmniConfigGovernor.AudioEffectsLevel, 1) == 1;
        
        CraftAudioMatrix();
        WarpAudioTone();
    }

    public void FlipSonicPulse()
    {
        sonicEchoEnabled = !sonicEchoEnabled;
        CraftAudioMatrix();
        MarkPauseState();
    }

    public void IgniteResonanceField()
    {
        audioFlowThread = !audioFlowThread;
        WarpAudioTone();
        MarkPauseState();
    }

    private void CraftAudioMatrix()
    {
        resonanceCoreModule.SetFloat(OmniConfigGovernor.AudioEffectsLevel, sonicEchoEnabled ? 0f : -80f);
        waveToggleButton.sprite = sonicEchoEnabled ? pulseActivationGlyph : pulseDeactivationGlyph;
    }

    private void WarpAudioTone()
    {
        resonanceCoreModule.SetFloat(OmniConfigGovernor.BackgroundMusicLevel, audioFlowThread ? 0f : -80f);
        melodySwitchGlyph.sprite = audioFlowThread ? pulseActivationGlyph : pulseDeactivationGlyph;
    }
    
    public void MarkPauseState()
    {
        PlayerPrefs.SetInt(OmniConfigGovernor.BackgroundMusicLevel, sonicEchoEnabled ? 1 : 0);
        PlayerPrefs.SetInt(OmniConfigGovernor.AudioEffectsLevel, audioFlowThread ? 1 : 0);
        PlayerPrefs.Save();
    }
}