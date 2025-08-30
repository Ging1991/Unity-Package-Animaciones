using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ging1991.Animaciones {

	public class EfectoVisual : MonoBehaviour {

		public List<GameObject> efectos;

		public void Animar(string codigo) {
			Animar(codigo, Color.white);
		}


		public void Animar(string codigo, Color color) {
			GameObject instancia = null;
			foreach (GameObject efecto in efectos) {
				if (efecto.GetComponent<MotorPrefab>().codigo == codigo) {
					instancia = efecto;
					break;
				}
			}
			if (instancia != null) {
				instancia.GetComponent<MotorPrefab>().Inicializar();
				instancia.GetComponentInChildren<Image>().color = color;
			} else {
				Debug.LogError($"No se encontró un efecto con el código {codigo}");
			}
		}



	}

}