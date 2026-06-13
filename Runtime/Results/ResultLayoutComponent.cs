using EgorLin.Keys.Ids;
using EgorLin.UIWidgets.Components.Base;

namespace EgorLin.UIWidgets.Results
{
    public readonly struct ResultLayoutComponent<T> where T : WindowComponent
    {
        public readonly T Component;
        public readonly KeyId KeyId;

        public ResultLayoutComponent(T component, KeyId keyId)
        {
            Component = component;
            KeyId = keyId;
        }
    }
}