using EgorLin.Keys.Selectors.Assets;
using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Core.Base;

namespace EgorLin.UIWidgets.Core.Layouts {

    public class WindowLayoutElement : WindowComponent, ILayoutInstance {

        public int tagId;
        public WindowLayout innerLayout;
        public bool hideInScreen;
        public KeySelector KeySelector;

        WindowLayout ILayoutInstance.windowLayoutInstance {
            get;
            set;
        }

    }

}