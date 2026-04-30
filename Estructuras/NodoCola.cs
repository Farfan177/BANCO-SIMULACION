using BancoSimulador.Entidades;

namespace BancoSimulador.Estructuras
{
    /// <summary>
    /// Nodo de la cola de atención.
    /// Contiene la identificación del cliente y referencia al siguiente nodo.
    /// </summary>
    public class NodoCola
    {
        public string IdentificacionCliente { get; set; }
        public NodoCola Siguiente { get; set; }

        public NodoCola(string identificacionCliente)
        {
            IdentificacionCliente = identificacionCliente;
            Siguiente = null;
        }
    }
}
