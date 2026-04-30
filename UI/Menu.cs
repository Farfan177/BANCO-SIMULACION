using BancoSimulador.Entidades;
using BancoSimulador.Logica;

namespace BancoSimulador.UI
{
    /// <summary>
    /// Clase encargada de la interacción con el usuario a través de la consola.
    /// Presenta el menú principal y gestiona todas las opciones disponibles.
    /// </summary>
    public class Menu
    {
        private Banco _banco;

        public Menu(Banco banco)
        {
            _banco = banco;
        }

        // ─────────────────────────────────────────────
        //  MENÚ PRINCIPAL
        // ─────────────────────────────────────────────

        public void Ejecutar()
        {
            MostrarBienvenida();

            bool salir = false;
            while (!salir)
            {
                MostrarMenuPrincipal();
                string opcion = (Console.ReadLine() ?? string.Empty).Trim();

                switch (opcion)
                {
                    case "1":  RegistrarCliente(); break;
                    case "2":  ListarClientes(); break;
                    case "3":  BuscarCliente(); break;
                    case "4":  AgregarACola(); break;
                    case "5":  AtenderSiguiente(); break;
                    case "6":  Depositar(); break;
                    case "7":  Retirar(); break;
                    case "8":  ConsultarSaldo(); break;
                    case "9":  DeshacerTransaccion(); break;
                    case "10": MostrarCola(); break;
                    case "11": MostrarTotalClientes(); break;
                    case "12": MostrarTotalDinero(); break;
                    case "13": salir = ConfirmarSalida(); break;
                    default:
                        MostrarError("Opción inválida. Por favor ingrese un número del 1 al 13.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine();
                    Console.Write("  Presione ENTER para continuar...");
                    Console.ReadLine();
                }
            }

            MostrarDespedida();
        }

        // ─────────────────────────────────────────────
        //  PRESENTACIÓN
        // ─────────────────────────────────────────────

        private void MostrarBienvenida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine($"║          BIENVENIDO AL {_banco.NombreBanco,-28} ║");
            Console.WriteLine("║                  Sistema Bancario                    ║");
            Console.WriteLine("║                Estructuras de Datos                  ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("  Presione ENTER para continuar...");
            Console.ReadLine();
        }

        private void MostrarMenuPrincipal()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  {_banco.NombreBanco,-51}║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.ResetColor();

            Console.WriteLine("║  GESTIÓN DE CLIENTES  (Lista Enlazada)               ║");
            Console.WriteLine("║   1. Registrar cliente                               ║");
            Console.WriteLine("║   2. Listar clientes                                 ║");
            Console.WriteLine("║   3. Buscar cliente                                  ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║  COLA DE ATENCIÓN  (Cola FIFO)                       ║");
            Console.WriteLine("║   4. Agregar cliente a la cola                       ║");
            Console.WriteLine("║   5. Atender siguiente cliente                       ║");
            Console.WriteLine("║  10. Mostrar cola de atención                        ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║  OPERACIONES BANCARIAS  (Pila de transacciones)      ║");
            Console.WriteLine("║   6. Realizar depósito                               ║");
            Console.WriteLine("║   7. Realizar retiro                                 ║");
            Console.WriteLine("║   8. Consultar saldo                                 ║");
            Console.WriteLine("║   9. Deshacer última transacción                     ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║  INFORMACIÓN GENERAL                                 ║");
            Console.WriteLine("║  11. Mostrar total de clientes                       ║");
            Console.WriteLine("║  12. Mostrar total de dinero del banco               ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("║  13. Salir                                           ║");
            Console.ResetColor();
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
            Console.Write("\n  Seleccione una opción: ");
        }

        private void MostrarEncabezado(string titulo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  ══════════════════════════════════");
            Console.WriteLine($"   {titulo}");
            Console.WriteLine($"  ══════════════════════════════════\n");
            Console.ResetColor();
        }

        private void MostrarExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ✔ {mensaje}");
            Console.ResetColor();
        }

        private void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  ✘ {mensaje}");
            Console.ResetColor();
        }

        private void MostrarInfo(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"  ► {mensaje}");
            Console.ResetColor();
        }

        // ─────────────────────────────────────────────
        //  OPCIONES DEL MENÚ
        // ─────────────────────────────────────────────

        private void RegistrarCliente()
        {
            MostrarEncabezado("REGISTRAR NUEVO CLIENTE");

            Console.Write("  Ingrese identificación (cédula): ");
            string id = Console.ReadLine() ?? string.Empty;
            id = id.Trim();

            Console.Write("  Ingrese nombre completo: ");
            string nombre = Console.ReadLine() ?? string.Empty;
            nombre = nombre.Trim();

            Console.Write("  Ingrese número de cuenta: ");
            string cuenta = Console.ReadLine() ?? string.Empty;
            cuenta = cuenta.Trim();

            Console.Write("  Ingrese saldo inicial: $");
            string saldoStr = Console.ReadLine() ?? string.Empty;
            saldoStr = saldoStr.Trim();

            if (!double.TryParse(saldoStr, out double saldo))
            {
                MostrarError("El saldo ingresado no es válido.");
                return;
            }

            var (exito, mensaje) = _banco.RegistrarCliente(id, nombre, cuenta, saldo);
            if (exito) MostrarExito(mensaje);
            else MostrarError(mensaje);
        }

        private void ListarClientes()
        {
            MostrarEncabezado("LISTADO DE CLIENTES REGISTRADOS");
            _banco.ListarClientes();
            Console.WriteLine($"\n  Total de clientes: {_banco.TotalClientes()}");
        }

        private void BuscarCliente()
        {
            MostrarEncabezado("BUSCAR CLIENTE");
            Console.Write("  Ingrese la identificación del cliente: ");
            string id = (Console.ReadLine() ?? string.Empty).Trim();

            Cliente cliente = _banco.BuscarCliente(id);
            if (cliente == null)
                MostrarError("No se encontró ningún cliente con esa identificación.");
            else
            {
                MostrarExito("Cliente encontrado:");
                Console.WriteLine($"\n{cliente}");
            }
        }

        private void AgregarACola()
        {
            MostrarEncabezado("AGREGAR CLIENTE A COLA DE ATENCIÓN");
            Console.Write("  Ingrese la identificación del cliente: ");
            string id = (Console.ReadLine() ?? string.Empty).Trim();

            var (exito, mensaje) = _banco.AgregarACola(id);
            if (exito) MostrarExito(mensaje);
            else MostrarError(mensaje);
        }

        private void AtenderSiguiente()
        {
            MostrarEncabezado("ATENDER SIGUIENTE CLIENTE");

            var (exito, mensaje, cliente) = _banco.AtenderSiguiente();
            if (!exito)
            {
                MostrarError(mensaje);
                return;
            }

            MostrarExito(mensaje);
            if (cliente != null)
                Console.WriteLine($"\n{cliente}");
        }

        private void Depositar()
        {
            MostrarEncabezado("REALIZAR DEPÓSITO");
            Console.Write("  Ingrese la identificación del cliente: ");
            string id = (Console.ReadLine() ?? string.Empty).Trim();

            Console.Write("  Ingrese el monto a depositar: $");
            string montoStr = (Console.ReadLine() ?? string.Empty).Trim();

            if (!double.TryParse(montoStr, out double monto))
            {
                MostrarError("El monto ingresado no es válido.");
                return;
            }

            var (exito, mensaje) = _banco.Depositar(id, monto);
            if (exito) MostrarExito(mensaje);
            else MostrarError(mensaje);
        }

        private void Retirar()
        {
            MostrarEncabezado("REALIZAR RETIRO");
            Console.Write("  Ingrese la identificación del cliente: ");
            string id = (Console.ReadLine() ?? string.Empty).Trim();

            Console.Write("  Ingrese el monto a retirar: $");
            string montoStr = (Console.ReadLine() ?? string.Empty).Trim();

            if (!double.TryParse(montoStr, out double monto))
            {
                MostrarError("El monto ingresado no es válido.");
                return;
            }

            var (exito, mensaje) = _banco.Retirar(id, monto);
            if (exito) MostrarExito(mensaje);
            else MostrarError(mensaje);
        }

        private void ConsultarSaldo()
        {
            MostrarEncabezado("CONSULTAR SALDO");
            Console.Write("  Ingrese la identificación del cliente: ");
            string id = (Console.ReadLine() ?? string.Empty).Trim();

            var (exito, mensaje, saldo) = _banco.ConsultarSaldo(id);
            if (!exito)
            {
                MostrarError(mensaje);
                return;
            }

            MostrarExito(mensaje);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n  Saldo actual: ${saldo:F2}");
            Console.ResetColor();
        }

        private void DeshacerTransaccion()
        {
            MostrarEncabezado("DESHACER ÚLTIMA TRANSACCIÓN");

            var (exito, mensaje) = _banco.DeshacerUltimaTransaccion();
            if (exito) MostrarExito(mensaje);
            else MostrarError(mensaje);
        }

        private void MostrarCola()
        {
            MostrarEncabezado("COLA DE ATENCIÓN ACTUAL");
            _banco.MostrarCola();
        }

        private void MostrarTotalClientes()
        {
            MostrarEncabezado("TOTAL DE CLIENTES");
            int total = _banco.TotalClientes();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"  Clientes registrados en el banco: {total}");
            Console.ResetColor();
        }

        private void MostrarTotalDinero()
        {
            MostrarEncabezado("TOTAL DE DINERO EN EL BANCO");
            double total = _banco.TotalDinero();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"  Total de dinero almacenado en cuentas: ${total:F2}");
            Console.ResetColor();
        }

        private bool ConfirmarSalida()
        {
            Console.Write("\n  ¿Está seguro que desea salir? (s/n): ");
            string respuesta = (Console.ReadLine() ?? string.Empty).Trim().ToLower();
            return respuesta == "s" || respuesta == "si" || respuesta == "sí";
        }

        private void MostrarDespedida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔══════════════════════════════════════════════╗");
            Console.WriteLine("║  Gracias por usar el Simulador Bancario.     ║");
            Console.WriteLine("║  ¡Hasta pronto!                              ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝\n");
            Console.ResetColor();
        }
    }
}
