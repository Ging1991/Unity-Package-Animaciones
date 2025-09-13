using System.Collections.Generic;
using Ging1991.Animaciones.Efectos;
using UnityEngine;
using UnityEngine.UI;

namespace Ging1991.Animaciones {

	public class MotorPrefab : MonoBehaviour {

		[Header("Controlador de la animación")]
		public ControlAnimaciones animacion;
		public bool desactivarAlTerminar = true;

		[Header("Configuración Escalar")]
		public bool usarEscalar = true;
		public float escalaInicial = 1f;
		public float escalaFinal = 1.8f;
		public int escalaIteraciones = 10;

		[Header("Configuración Transparentar")]
		public bool usarTransparentar = true;
		[Range(0f, 1f)] public float alfaInicial = 1f;
		[Range(0f, 1f)] public float alfaFinal = 0f;
		public int alfaIteraciones = 10;
		public Image imagen;

		public string codigo;

		public void Inicializar(IFinalizar accion = null) {
			gameObject.SetActive(true);
			List<IProcesarCuadro> animaciones = new();

			if (usarEscalar) {
				animaciones.Add(new Escalar(transform, escalaIteraciones, escalaInicial, escalaFinal));
			}

			if (usarTransparentar) {
				if (imagen == null) {
					Debug.LogWarning("MotorPrefab: Se activó Transparentar pero no se asignó la imagen.");
				}
				else {
					animaciones.Add(new Transparentar(imagen, alfaInicial, alfaFinal, alfaIteraciones));
				}
			}
			if (desactivarAlTerminar) {
				animacion.Inicializar(animaciones, new Desactivar(gameObject));
			}
			else {
				animacion.Inicializar(animaciones, accion);
			}
		}


		private class Desactivar : IFinalizar {

			private GameObject objeto;

			public Desactivar(GameObject objeto) {
				this.objeto = objeto;
			}

			public void Finalizar() {
				objeto.SetActive(false);
			}

		}


	}
}
