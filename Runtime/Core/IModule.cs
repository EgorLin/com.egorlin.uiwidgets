using EgorLin.UIWidgets.Components.Basic;
using UnityEngine;

namespace EgorLin.UIWidgets.Core {

    public abstract class WindowSystemModule : ScriptableObject {

        public abstract void OnStart();
        public virtual void OnUpdate() {}
        public abstract void OnDestroy();

    }

    public interface ICanClickCheckModule {

        bool CanClick(IInteractable obj);

    }

}