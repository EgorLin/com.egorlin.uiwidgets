using EgorLin.UIWidgets.Components.Base;
using UnityEngine;

namespace EgorLin.UIWidgets.Components.Basic.ListModules.Base {

	[ComponentModuleDisplayName("Periodic rows coloring")]
	public class ListPeriodColoringComponentModule : ListComponentModule {

		public ImageComponent background;
		public int period;
		public Color firstColor;
		public Color secondColor;

		public override void OnShowBegin() {

			base.OnShowBegin();

			if (this.period == 0) return;

			var toggle = false;
			var targetColor = this.firstColor;
			for (int i = 0; i < this.listComponent.items.Count; i++) {

				if (i % this.period == 0) {
					
					toggle = !toggle;
					targetColor = (toggle == true ? this.secondColor : this.firstColor);
					
				}
				
				var imageObject = this.listComponent.items[i].transform.Find(this.background.name);
				imageObject.GetComponent<ImageComponent>().SetColor(targetColor);
				
			}

		}

	}

}
