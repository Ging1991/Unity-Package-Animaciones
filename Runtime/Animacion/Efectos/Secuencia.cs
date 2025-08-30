using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ging1991.Animaciones {

	public class Secuencia : AnimacionBase {

		private readonly Image imagen;
		private readonly List<Sprite> cuadros;
		private int cuadroActual;

        public Secuencia(Image imagen, List<Sprite> cuadros, int iteraciones, int salto) : base(iteraciones, salto) {
			this.imagen = imagen ?? throw new ArgumentNullException(nameof(imagen));
			if (cuadros == null || cuadros.Count == 0)
				throw new ArgumentException("La lista de cuadros no puede estar vacía", nameof(cuadros));
				
			cuadroActual = 0;
			imagen.sprite = cuadros[cuadroActual];
		}


		protected override void Animar() {
			cuadroActual++;
			if (cuadroActual >= cuadros.Count)
				cuadroActual = 0;
			imagen.sprite = cuadros[cuadroActual];
		}


	}

}