using System;
using UnityEngine;

namespace Ging1991.Animaciones.Efectos {

	public class Mover : AnimacionBase {

		private readonly Vector3 posicionInicial;
		private readonly Vector3 posicionFinal;
		private readonly Transform transformador;

		public Mover(Transform transformador, Vector3 posicionInicial, Vector3 posicionFinal, int duracion) : base(duracion) {
			this.transformador = transformador ?? throw new ArgumentNullException(nameof(transformador));
			this.posicionInicial = posicionInicial;
			this.posicionFinal = posicionFinal;
			this.transformador.localPosition = posicionInicial;
		}


		protected override void Animar() {
			Vector3 nuevaPos = Vector3.Lerp(posicionInicial, posicionFinal, ((float)(pasosTotales - pasosRestantes)) / pasosTotales);
			transformador.localPosition = nuevaPos;
		}


	}

}