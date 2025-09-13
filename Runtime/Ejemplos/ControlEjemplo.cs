using System.Collections.Generic;
using Ging1991.Animaciones;
using Ging1991.Animaciones.Efectos;
using UnityEngine;
using UnityEngine.UI;

public class ControlEjemplo : MonoBehaviour {

	public GameObject[] objetos;
	public bool mostrarEjemploDirecto;
	public bool mostrarEjemploPrefabs;
	public bool mostrarEjemploEfectoVisual;
	public GameObject objetoDirecto;
	public GameObject efectoVisual;

	void Start() {
		if (mostrarEjemploPrefabs)
			MostrarEjemploConPrefabs();

		if (mostrarEjemploDirecto)
			MostrarEjemplosDirectos();

		if (mostrarEjemploEfectoVisual)
			MostraEjemploEfectoVisual();

	}


	private void MostraEjemploEfectoVisual() {
		efectoVisual.GetComponent<EfectoVisual>().Animar("EXPLOSION");
	}


	private void MostrarEjemplosDirectos() {
		List<IProcesarCuadro> animaciones = new();
		animaciones.Add(new Escalar(objetoDirecto.transform, 10, 1, 2));
		animaciones.Add(new Transparentar(objetoDirecto.GetComponent<Image>(), 1, 0, 10));
		objetoDirecto.GetComponent<ControlAnimaciones>().Inicializar(animaciones);
	}


	private void MostrarEjemploConPrefabs() {
		if (objetos == null || objetos.Length == 0) {
			Debug.LogWarning("No se asignaron prefabs en ControlEjemplo.objetos");
			return;
		}

		// Crear secuenciadores para cada prefab
		Secuenciador[] secuenciadores = new Secuenciador[objetos.Length];
		for (int i = 0; i < objetos.Length; i++) {
			secuenciadores[i] = new Secuenciador { objeto = objetos[i] };
			if (i == 0)
				secuenciadores[i].objetoActual = objetos[objetos.Length-1];
			else
				secuenciadores[i].objetoActual = objetos[i - 1];
		}

		// Encadenar cada secuenciador con el siguiente (ciclo)
		for (int i = 0; i < secuenciadores.Length; i++) {
			secuenciadores[i].siguiente = secuenciadores[(i + 1) % secuenciadores.Length];
			
		}

		// Iniciar la animación con el primer prefab
		var motor = secuenciadores[0].objeto.GetComponent<MotorPrefab>();
		if (motor != null) {
			motor.Inicializar(secuenciadores[0].siguiente);
		}
		else {
			Debug.LogWarning($"El objeto {secuenciadores[0].objeto.name} no tiene MotorPrefab asignado.");
		}
	}


	private class Secuenciador : IFinalizar {

		public GameObject objeto;
		public GameObject objetoActual;
		public Secuenciador siguiente;

		public void Finalizar() {
			objetoActual.SetActive(false);
			var motor = objeto.GetComponent<MotorPrefab>();
			if (motor != null) {
				motor.Inicializar(siguiente);
			}
			else {
				Debug.LogWarning($"El objeto {objeto.name} no tiene MotorPrefab asignado.");
			}
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