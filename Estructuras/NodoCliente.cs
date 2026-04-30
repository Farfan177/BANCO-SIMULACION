using BancoSimulador.Entidades;

namespace BancoSimulador.Estructuras
{
    /// <summary>
    /// Nodo de la lista enlazada de clientes.
    /// Contiene el dato del cliente y la referencia al siguiente nodo.
    /// </summary>
    public class NodoCliente
    {
        public Cliente Dato { get; set; }
        public NodoCliente Siguiente { get; set; }

        public NodoCliente(Cliente cliente)
        {
            Dato = cliente;
            Siguiente = null;
        }
    }
}
