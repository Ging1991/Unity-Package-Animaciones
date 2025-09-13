using System.Collections.Generic;
using Ging1991.Relojes;
using UnityEngine;

namespace Ging1991.Animaciones {

	public class ControlAnimaciones : MonoBehaviour, IEjecutable {

		public AudioSource sonido;
		private List<IProcesarCuadro> cambios;
		private IFinalizar accion;
		private bool animando = false;

		public void Inicializar(List<IProcesarCuadro> cambios, IFinalizar accion = null) {
			this.cambios = cambios ?? new List<IProcesarCuadro>();
			this.accion = accion;
			TocarSonido();

			gameObject.SetActive(true);
			if (!animando) {
				Reloj.GetInstanciaGlobal().decimas.Suscribir(this);
				animando = true;
			}
		}


		private void TocarSonido() {
			if (sonido == null)
				return;

			if (sonido.isPlaying) {
				sonido.Stop();
			}

			sonido.time = 0f;
			sonido.Play();
		}


		public void Ejecutar() {
			for (int i = cambios.Count - 1; i >= 0; i--) {
				var cambio = cambios[i];
				cambio.ProcesarCuadro();
				if (!cambio.SigueAnimando())
					cambios.RemoveAt(i);
			}

			if (cambios.Count == 0) {
				DetenerAnimacion();
			}
		}


		void OnDestroy() {
			DetenerAnimacion();
        }


        private void DetenerAnimacion()	{
			if (animando)
			{
				Reloj.GetInstanciaGlobal().Desuscribir(this);
				accion?.Finalizar();
				animando = false;
			}
		}

	}

}