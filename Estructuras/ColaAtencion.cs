namespace BancoSimulador.Estructuras
{
    /// <summary>
    /// Cola de atención implementada manualmente (FIFO).
    /// Se usa para controlar el orden de atención de los clientes (primero en llegar, primero en ser atendido).
    /// </summary>
    public class ColaAtencion
    {
        private NodoCola _frente;
        private NodoCola _fin;

        public ColaAtencion()
        {
            _frente = null;
            _fin = null;
        }

        /// <summary>
        /// Agrega un cliente al final de la cola (encolar).
        /// </summary>
        public void Encolar(string identificacionCliente)
        {
            NodoCola nuevo = new NodoCola(identificacionCliente);

            if (_fin == null)
            {
                _frente = nuevo;
                _fin = nuevo;
            }
            else
            {
                _fin.Siguiente = nuevo;
                _fin = nuevo;
            }
        }

        /// <summary>
        /// Retira y retorna el primer cliente de la cola (desencolar).
        /// Retorna null si la cola está vacía.
        /// </summary>
        public string Desencolar()
        {
            if (_frente == null)
                return null;

            string identificacion = _frente.IdentificacionCliente;
            _frente = _frente.Siguiente;

            if (_frente == null)
                _fin = null;

            return identificacion;
        }

        /// <summary>
        /// Consulta el primer cliente en la cola sin retirarlo.
        /// Retorna null si la cola está vacía.
        /// </summary>
        public string VerSiguiente()
        {
            return _frente?.IdentificacionCliente;
        }

        /// <summary>
        /// Verifica si la identificación ya está en la cola.
        /// </summary>
        public bool EstaEnCola(string identificacionCliente)
        {
            NodoCola actual = _frente;
            while (actual != null)
            {
                if (actual.IdentificacionCliente == identificacionCliente)
                    return true;
                actual = actual.Siguiente;
            }
            return false;
        }

        /// <summary>
        /// Muestra todos los clientes en la cola en orden de atención.
        /// </summary>
        public void MostrarCola()
        {
            if (_frente == null)
            {
                Console.WriteLine("  La cola de atención está vacía.");
                return;
            }

            NodoCola actual = _frente;
            int turno = 1;
            while (actual != null)
            {
                string etiqueta = turno == 1 ? " ← SIGUIENTE" : "";
                Console.WriteLine($"  Turno {turno}: ID {actual.IdentificacionCliente}{etiqueta}");
                actual = actual.Siguiente;
                turno++;
            }
        }

        /// <summary>
        /// Indica si la cola está vacía.
        /// </summary>
        public bool EstaVacia()
        {
            return _frente == null;
        }
    }
}
