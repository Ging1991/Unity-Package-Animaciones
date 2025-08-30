namespace Ging1991.Animaciones {

	public abstract class AnimacionBase : IProcesarCuadro {

		private readonly int salto;
		private int contadorSalto;
		protected int pasosRestantes;
		protected int pasosTotales;

		public AnimacionBase(int iteraciones, int salto = 1) {
			this.pasosRestantes = iteraciones;
			this.salto = salto;
			this.contadorSalto = 0;
			pasosTotales = pasosRestantes;
		}


		public void ProcesarCuadro() {
			if (SigueAnimando()) {
				contadorSalto++;
				if (contadorSalto >= salto) {
					contadorSalto = 0;
					Animar();
					pasosRestantes--;
				}
			}
		}


		protected abstract void Animar();


		public bool SigueAnimando() {
			return pasosRestantes > 0;
		}
		

    }

}