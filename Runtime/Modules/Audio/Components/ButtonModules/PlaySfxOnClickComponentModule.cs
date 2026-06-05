using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Components.Basic.Base;

namespace EgorLin.UIWidgets.Modules.Audio.Components.ButtonModules {

    [ComponentModuleDisplayName("Play SFX on Click")]
    public class PlaySfxOnClickComponentModule : ButtonComponentModule, IAudioComponentModule {

        public UIWSAudioEvent clip;

        public override void OnInit() {
            
            this.buttonComponent.button.onClick.AddListener(this.OnClick);
            
            base.OnInit();
            
        }

        public override void OnDeInit() {
            
            this.buttonComponent.button.onClick.RemoveListener(this.OnClick);

            base.OnDeInit();
            
        }

        private void OnClick() {

            this.clip.Play();
            
        }

    }

}
