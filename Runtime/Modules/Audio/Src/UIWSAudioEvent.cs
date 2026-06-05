using EgorLin.UIWidgets.Modules.Audio.Taptic;
using UnityEngine;

namespace EgorLin.UIWidgets.Modules.Audio {

    [UnityEngine.CreateAssetMenu(menuName = "UI.Windows/Audio/Event")]
    public class UIWSAudioEvent : UnityEngine.ScriptableObject {

        [System.Serializable]
        public struct Parameters {

            public int maxCount;
            [Range(0f, 1f)] 
            public float lengthFactor;
            
            [Space(10f)]
            public bool changePitch;
            public bool randomPitch;
            public UnityEngine.Vector2 randomPitchValue;
            [Range(-3f, 3f)]
            public float pitchValue;
            
            [Space(10f)]
            public bool changeVolume;
            public bool randomVolume;
            public UnityEngine.Vector2 randomVolumeValue;
            [Range(0f, 1f)]
            public float volumeValue;
            
            [Space(10f)]
            public bool loop;

        }
        
        [Header("Audio")]
        #if FMOD_SUPPORT
        public FMODAudioComponent fmodAudioComponent;
        #else
        public WindowSystemAudio.EventType eventType;
        
        public UnityEngine.AudioClip audioClip;
        public UnityEngine.AudioClip[] randomClips;

        public Parameters parameters = new Parameters() {
            lengthFactor = 1f,
            pitchValue = 1f,
            volumeValue = 1f,
        };

        [Header("Music")]
        public WindowSystemAudio.Behaviour behaviour;
        [Range(1, WindowSystemAudio.CHANNELS_COUNT)]
        public int musicChannel;
        #endif

        [Header("Vibration")]
        public bool vibrate;
        public TapticType taptic;
        public bool tapticByCurve;
        public UnityEngine.AnimationCurve tapticCurve;
        
        public void Play() {

            var audio = WindowSystemAudio.Instance;
            if (audio == null) {
                UnityEngine.Debug.LogWarning("No audio module found. Did you forget to add Audio Module to your WindowSystem initializer?");
                return;
            }
            
            audio.Play(this);

        }

        public void Stop() {
            
            var audio = WindowSystemAudio.Instance;
            if (audio == null) {
                UnityEngine.Debug.LogWarning("No audio module found. Did you forget to add Audio Module to your WindowSystem initializer?");
                return;
            }
            
            audio.Stop(this);

        }

        public void Release() {
            #if FMOD_SUPPORT
            this.fmodAudioComponent.Release();
            #endif
        }

    }

}