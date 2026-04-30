using BancoSimulador.Entidades;

namespace BancoSimulador.Estructuras
{
    /// <summary>
    /// Lista enlazada simple implementada manualmente.
    /// Se usa para almacenar y gestionar todos los clientes del banco.
    /// </summary>
    public class ListaEnlazadaClientes
    {
        private NodoCliente _cabeza;

        public ListaEnlazadaClientes()
        {
            _cabeza = null;
        }

        /// <summary>
        /// Inserta un nuevo cliente al final de la lista.
        /// </summary>
        public void Insertar(Cliente cliente)
        {
            NodoCliente nuevo = new NodoCliente(cliente);

            if (_cabeza == null)
            {
                _cabeza = nuevo;
                return;
            }

            NodoCliente actual = _cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevo;
        }

        /// <summary>
        /// Busca un cliente por su número de identificación (cédula).
        /// Retorna null si no existe.
        /// </summary>
        public Cliente BuscarPorIdentificacion(string identificacion)
        {
            NodoCliente actual = _cabeza;
            while (actual != null)
            {
                if (actual.Dato.Identificacion == identificacion)
                    return actual.Dato;
                actual = actual.Siguiente;
            }
            return null;
        }

        /// <summary>
        /// Busca un cliente por su número de cuenta.
        /// Retorna null si no existe.
        /// </summary>
        public Cliente BuscarPorNumeroCuenta(string numeroCuenta)
        {
            NodoCliente actual = _cabeza;
            while (actual != null)
            {
                if (actual.Dato.NumeroCuenta == numeroCuenta)
                    return actual.Dato;
                actual = actual.Siguiente;
            }
            return null;
        }

        /// <summary>
        /// Verifica si ya existe un cliente con la misma identificación o número de cuenta.
        /// </summary>
        public bool ExisteCliente(string identificacion, string numeroCuenta)
        {
            NodoCliente actual = _cabeza;
            while (actual != null)
            {
                if (actual.Dato.Identificacion == identificacion || actual.Dato.NumeroCuenta == numeroCuenta)
                    return true;
                actual = actual.Siguiente;
            }
            return false;
        }

        /// <summary>
        /// Recorre y muestra todos los clientes registrados.
        /// </summary>
        public void ListarClientes()
        {
            if (_cabeza == null)
            {
                Console.WriteLine("  No hay clientes registrados.");
                return;
            }

            NodoCliente actual = _cabeza;
            int numero = 1;
            while (actual != null)
            {
                Console.WriteLine($"  {numero}. {actual.Dato}");
                actual = actual.Siguiente;
                numero++;
            }
        }

        /// <summary>
        /// Cuenta el total de clientes en la lista.
        /// </summary>
        public int ContarClientes()
        {
            int contador = 0;
            NodoCliente actual = _cabeza;
            while (actual != null)
            {
                contador++;
                actual = actual.Siguiente;
            }
            return contador;
        }

        /// <summary>
        /// Calcula la suma total del dinero en todas las cuentas.
        /// </summary>
        public double TotalDinero()
        {
            double total = 0;
            NodoCliente actual = _cabeza;
            while (actual != null)
            {
                total += actual.Dato.Saldo;
                actual = actual.Siguiente;
            }
            return total;
        }

        /// <summary>
        /// Indica si la lista está vacía.
        /// </summary>
        public bool EstaVacia()
        {
            return _cabeza == null;
        }
    }
}
