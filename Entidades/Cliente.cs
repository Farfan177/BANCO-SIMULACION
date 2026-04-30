namespace BancoSimulador.Entidades
{
    /// <summary>
    /// Representa un cliente del banco con sus datos básicos.
    /// </summary>
    public class Cliente
    {
        public string Identificacion { get; set; }
        public string NombreCompleto { get; set; }
        public string NumeroCuenta { get; set; }
        public double Saldo { get; set; }

        public Cliente(string identificacion, string nombreCompleto, string numeroCuenta, double saldoInicial)
        {
            Identificacion = identificacion;
            NombreCompleto = nombreCompleto;
            NumeroCuenta = numeroCuenta;
            Saldo = saldoInicial;
        }

        public override string ToString()
        {
            return $"  ID: {Identificacion} | Nombre: {NombreCompleto} | Cuenta: {NumeroCuenta} | Saldo: ${Saldo:F2}";
        }
    }
}
