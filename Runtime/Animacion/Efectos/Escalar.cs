using System;
using UnityEngine;

namespace Ging1991.Animaciones.Efectos {

	public class Escalar : AnimacionBase {

		private readonly float escalaInicial;
		private readonly float escalaFinal;
		private readonly Transform transformador;

		public Escalar(Transform transformador, int iteraciones, float escalaInicial, float escalaFinal) : base(iteraciones) {
			this.transformador = transformador ?? throw new ArgumentNullException(nameof(transformador));
			this.escalaInicial = escalaInicial;
			this.escalaFinal = escalaFinal;
			this.transformador.localScale = new Vector3(escalaInicial, escalaInicial, escalaInicial);
		}


		protected override void Animar() {
			float progreso = 1f - (float)pasosRestantes / pasosTotales;
			float nuevaEscala = Mathf.Lerp(escalaInicial, escalaFinal, progreso);
			transformador.localScale = new Vector3(nuevaEscala, nuevaEscala, nuevaEscala);
		}

		
    }
	
}