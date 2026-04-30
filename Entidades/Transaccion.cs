namespace BancoSimulador.Entidades
{
    /// <summary>
    /// Tipos de transacciones posibles en el sistema.
    /// </summary>
    public enum TipoTransaccion
    {
        Deposito,
        Retiro
    }

    /// <summary>
    /// Representa una transacción bancaria registrada en la pila de historial.
    /// </summary>
    public class Transaccion
    {
        public string NumeroCuenta { get; set; }
        public TipoTransaccion Tipo { get; set; }
        public double Monto { get; set; }
        public double SaldoAnterior { get; set; }
        public DateTime FechaHora { get; set; }

        public Transaccion(string numeroCuenta, TipoTransaccion tipo, double monto, double saldoAnterior)
        {
            NumeroCuenta = numeroCuenta;
            Tipo = tipo;
            Monto = monto;
            SaldoAnterior = saldoAnterior;
            FechaHora = DateTime.Now;
        }

        public override string ToString()
        {
            string tipoStr = Tipo == TipoTransaccion.Deposito ? "DEPÓSITO" : "RETIRO";
            return $"  [{tipoStr}] Cuenta: {NumeroCuenta} | Monto: ${Monto:F2} | Saldo anterior: ${SaldoAnterior:F2} | Fecha: {FechaHora:dd/MM/yyyy HH:mm:ss}";
        }
    }
}
