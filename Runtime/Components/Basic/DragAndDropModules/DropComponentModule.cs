using System;
using EgorLin.UIWidgets.Components.Base;
using UnityEngine.EventSystems;

namespace EgorLin.UIWidgets.Components.Basic.DragAndDropModules {

	public class DropComponentModule : WindowComponentModule, IDropHandler {

		private Action<PointerEventData> callback;

		public void SetCallback(System.Action<PointerEventData> callback) {

			this.callback = callback;

		}

		public void AddCallback(System.Action<PointerEventData> callback) {

			this.callback += callback;

		}

		public void RemoveCallback(System.Action<PointerEventData> callback) {

			this.callback -= callback;

		}

		public void RemoveAllCallbacks() {

			this.callback = null;

		}

		public void OnDrop(PointerEventData eventData) {

			if (eventData.pointerDrag != null) {

				callback?.Invoke(eventData);

			}

		}

	}

}
