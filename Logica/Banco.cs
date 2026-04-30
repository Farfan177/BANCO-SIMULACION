using BancoSimulador.Entidades;
using BancoSimulador.Estructuras;

namespace BancoSimulador.Logica
{
    /// <summary>
    /// Clase central del banco. Contiene las tres estructuras principales:
    /// - Lista enlazada de clientes
    /// - Cola de atención por turnos
    /// - Pila de transacciones para historial y reversión
    /// </summary>
    public class Banco
    {
        public string NombreBanco { get; private set; }
        private ListaEnlazadaClientes _clientes;
        private ColaAtencion _colaAtencion;
        private PilaTransacciones _pilaTransacciones;

        public Banco(string nombreBanco)
        {
            NombreBanco = nombreBanco;
            _clientes = new ListaEnlazadaClientes();
            _colaAtencion = new ColaAtencion();
            _pilaTransacciones = new PilaTransacciones();
        }

        // ─────────────────────────────────────────────
        //  GESTIÓN DE CLIENTES (Lista Enlazada)
        // ─────────────────────────────────────────────

        /// <summary>
        /// Registra un nuevo cliente. Valida duplicados por ID y número de cuenta.
        /// </summary>
        public (bool exito, string mensaje) RegistrarCliente(string id, string nombre, string cuenta, double saldo)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(cuenta))
                return (false, "Todos los campos son obligatorios.");

            if (saldo < 0)
                return (false, "El saldo inicial no puede ser negativo.");

            if (_clientes.ExisteCliente(id, cuenta))
                return (false, "Ya existe un cliente con esa identificación o número de cuenta.");

            Cliente nuevo = new Cliente(id, nombre, cuenta, saldo);
            _clientes.Insertar(nuevo);
            return (true, $"Cliente '{nombre}' registrado exitosamente.");
        }

        /// <summary>
        /// Busca un cliente por identificación.
        /// </summary>
        public Cliente BuscarCliente(string identificacion)
        {
            return _clientes.BuscarPorIdentificacion(identificacion);
        }

        /// <summary>
        /// Busca un cliente por número de cuenta.
        /// </summary>
        public Cliente BuscarClientePorCuenta(string numeroCuenta)
        {
            return _clientes.BuscarPorNumeroCuenta(numeroCuenta);
        }

        /// <summary>
        /// Lista todos los clientes registrados.
        /// </summary>
        public void ListarClientes()
        {
            _clientes.ListarClientes();
        }

        /// <summary>
        /// Retorna el total de clientes registrados.
        /// </summary>
        public int TotalClientes()
        {
            return _clientes.ContarClientes();
        }

        /// <summary>
        /// Retorna el total de dinero en todas las cuentas.
        /// </summary>
        public double TotalDinero()
        {
            return _clientes.TotalDinero();
        }

        // ─────────────────────────────────────────────
        //  COLA DE ATENCIÓN (Cola)
        // ─────────────────────────────────────────────

        /// <summary>
        /// Agrega un cliente a la cola de atención por su ID.
        /// </summary>
        public (bool exito, string mensaje) AgregarACola(string identificacion)
        {
            Cliente cliente = _clientes.BuscarPorIdentificacion(identificacion);

            if (cliente == null)
                return (false, "No existe ningún cliente con esa identificación.");

            if (_colaAtencion.EstaEnCola(identificacion))
                return (false, $"El cliente '{cliente.NombreCompleto}' ya está en la cola de atención.");

            _colaAtencion.Encolar(identificacion);
            return (true, $"Cliente '{cliente.NombreCompleto}' agregado a la cola de atención.");
        }

        /// <summary>
        /// Atiende al siguiente cliente en la cola.
        /// </summary>
        public (bool exito, string mensaje, Cliente cliente) AtenderSiguiente()
        {
            if (_colaAtencion.EstaVacia())
                return (false, "No hay clientes en la cola de atención.", null);

            string id = _colaAtencion.Desencolar();
            Cliente cliente = _clientes.BuscarPorIdentificacion(id);

            if (cliente == null)
                return (false, "Error: cliente no encontrado en el sistema.", null);

            return (true, $"Atendiendo a: {cliente.NombreCompleto}", cliente);
        }

        /// <summary>
        /// Muestra la cola actual de atención.
        /// </summary>
        public void MostrarCola()
        {
            _colaAtencion.MostrarCola();
        }

        // ─────────────────────────────────────────────
        //  OPERACIONES BANCARIAS (Pila de transacciones)
        // ─────────────────────────────────────────────

        /// <summary>
        /// Realiza un depósito. Registra la transacción en la pila.
        /// </summary>
        public (bool exito, string mensaje) Depositar(string identificacion, double monto)
        {
            if (monto <= 0)
                return (false, "El monto del depósito debe ser mayor a cero.");

            Cliente cliente = _clientes.BuscarPorIdentificacion(identificacion);
            if (cliente == null)
                return (false, "No existe ningún cliente con esa identificación.");

            double saldoAnterior = cliente.Saldo;
            Transaccion transaccion = new Transaccion(cliente.NumeroCuenta, TipoTransaccion.Deposito, monto, saldoAnterior);

            cliente.Saldo += monto;
            _pilaTransacciones.Apilar(transaccion);

            return (true, $"Depósito de ${monto:F2} realizado. Nuevo saldo: ${cliente.Saldo:F2}");
        }

        /// <summary>
        /// Realiza un retiro. Valida saldo suficiente y registra en la pila.
        /// </summary>
        public (bool exito, string mensaje) Retirar(string identificacion, double monto)
        {
            if (monto <= 0)
                return (false, "El monto del retiro debe ser mayor a cero.");

            Cliente cliente = _clientes.BuscarPorIdentificacion(identificacion);
            if (cliente == null)
                return (false, "No existe ningún cliente con esa identificación.");

            if (monto > cliente.Saldo)
                return (false, $"Saldo insuficiente. Saldo disponible: ${cliente.Saldo:F2}");

            double saldoAnterior = cliente.Saldo;
            Transaccion transaccion = new Transaccion(cliente.NumeroCuenta, TipoTransaccion.Retiro, monto, saldoAnterior);

            cliente.Saldo -= monto;
            _pilaTransacciones.Apilar(transaccion);

            return (true, $"Retiro de ${monto:F2} realizado. Nuevo saldo: ${cliente.Saldo:F2}");
        }

        /// <summary>
        /// Consulta el saldo de un cliente.
        /// </summary>
        public (bool exito, string mensaje, double saldo) ConsultarSaldo(string identificacion)
        {
            Cliente cliente = _clientes.BuscarPorIdentificacion(identificacion);
            if (cliente == null)
                return (false, "No existe ningún cliente con esa identificación.", 0);

            return (true, $"Saldo de {cliente.NombreCompleto} (Cuenta {cliente.NumeroCuenta})", cliente.Saldo);
        }

        // ─────────────────────────────────────────────
        //  REVERSIÓN DE OPERACIONES (Pila)
        // ─────────────────────────────────────────────

        /// <summary>
        /// Deshace la última transacción realizada usando la pila.
        /// </summary>
        public (bool exito, string mensaje) DeshacerUltimaTransaccion()
        {
            if (_pilaTransacciones.EstaVacia())
                return (false, "No hay transacciones para deshacer.");

            Transaccion ultima = _pilaTransacciones.Desapilar();
            Cliente cliente = _clientes.BuscarPorNumeroCuenta(ultima.NumeroCuenta);

            if (cliente == null)
                return (false, "Error: no se encontró la cuenta asociada a la transacción.");

            // Restaurar el saldo anterior
            cliente.Saldo = ultima.SaldoAnterior;

            string tipo = ultima.Tipo == TipoTransaccion.Deposito ? "depósito" : "retiro";
            return (true,
                $"Se deshizo el {tipo} de ${ultima.Monto:F2} en la cuenta {ultima.NumeroCuenta}.\n" +
                $"  Saldo restaurado a: ${cliente.Saldo:F2}");
        }
    }
}
