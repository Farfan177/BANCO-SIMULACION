using BancoSimulador.Entidades;

namespace BancoSimulador.Estructuras
{
    /// <summary>
    /// Pila de transacciones implementada manualmente (LIFO).
    /// Se usa para guardar el historial de operaciones y permitir deshacer la última.
    /// </summary>
    public class PilaTransacciones
    {
        private NodoPila _cima;

        public PilaTransacciones()
        {
            _cima = null;
        }

        /// <summary>
        /// Apila una transacción (push).
        /// </summary>
        public void Apilar(Transaccion transaccion)
        {
            NodoPila nuevo = new NodoPila(transaccion);
            nuevo.Siguiente = _cima;
            _cima = nuevo;
        }

        /// <summary>
        /// Desapila y retorna la última transacción (pop).
        /// Retorna null si la pila está vacía.
        /// </summary>
        public Transaccion Desapilar()
        {
            if (_cima == null)
                return null;

            Transaccion transaccion = _cima.Dato;
            _cima = _cima.Siguiente;
            return transaccion;
        }

        /// <summary>
        /// Consulta la última transacción sin desapilarla (peek).
        /// Retorna null si la pila está vacía.
        /// </summary>
        public Transaccion ConsultarUltima()
        {
            return _cima?.Dato;
        }

        /// <summary>
        /// Indica si la pila está vacía.
        /// </summary>
        public bool EstaVacia()
        {
            return _cima == null;
        }
    }
}
