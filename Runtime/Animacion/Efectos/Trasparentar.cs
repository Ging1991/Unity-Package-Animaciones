using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ging1991.Animaciones {

	public class Transparentar : AnimacionBase {

		private readonly Image imagen;
		private readonly float alfaInicial;
		private readonly float alfaFinal;

		public Transparentar(Image imagen, float alfaInicial, float alfaFinal, int iteraciones) : base(iteraciones) {
			this.imagen = imagen ?? throw new ArgumentNullException(nameof(imagen));
			this.alfaInicial = Mathf.Clamp01(alfaInicial);
			this.alfaFinal = Mathf.Clamp01(alfaFinal);
			SetAlfa(alfaInicial);
		}


		protected override void Animar() {
			float progreso = 1f - (float)pasosRestantes / pasosTotales;
			float nuevoAlfa = Mathf.Lerp(alfaInicial, alfaFinal, progreso);
			SetAlfa(nuevoAlfa);
		}


		private void SetAlfa(float alfa) {
			Color color = imagen.color;
			color.a = alfa;
			imagen.color = color;
		}


	}

}