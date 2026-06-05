using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Components.Basic.Base;

namespace EgorLin.UIWidgets.Modules.Audio.Components.ProgressModules {

    [ComponentModuleDisplayName("Play SFX on progress change")]
    public class PlaySfxOnSlideComponentModule : ProgressComponentModule, IAudioComponentModule {

        public UIWSAudioEvent clip;

        public override void OnValueChanged(float value) {

            this.clip.Play();

        }

    }

}
