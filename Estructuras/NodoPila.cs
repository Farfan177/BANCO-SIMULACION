using BancoSimulador.Entidades;

namespace BancoSimulador.Estructuras
{
    /// <summary>
    /// Nodo de la pila de transacciones.
    /// Contiene la transacción y referencia al nodo anterior (abajo en la pila).
    /// </summary>
    public class NodoPila
    {
        public Transaccion Dato { get; set; }
        public NodoPila Siguiente { get; set; }

        public NodoPila(Transaccion transaccion)
        {
            Dato = transaccion;
            Siguiente = null;
        }
    }
}
