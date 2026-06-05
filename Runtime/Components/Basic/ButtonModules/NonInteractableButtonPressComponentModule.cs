using System;
using EgorLin.UIWidgets.Components.Basic.Base;
using UnityEngine.EventSystems;

namespace EgorLin.UIWidgets.Components.Basic.ButtonModules
{
	public class NonInteractableButtonPressComponentModule : ButtonComponentModule, UnityEngine.EventSystems.IPointerDownHandler {

		private bool interactable;
		private Action onPointerDown;

		public override void OnInit() {

			base.OnInit();

			this.interactable = this.buttonComponent.IsInteractable();

		}

		public override void OnInteractableChanged(bool state) {

			base.OnInteractableChanged(state);

			this.interactable = state;

		}

		public void OnPointerDown(PointerEventData eventData) {

			if (this.interactable == false) {

				this.onPointerDown?.Invoke();

			}

		}

		public void SetCallback(Action callback) {

			this.onPointerDown = callback;

		}

		public override void OnDeInit() {

			base.OnDeInit();

			this.onPointerDown = null;

		}

	}

}
