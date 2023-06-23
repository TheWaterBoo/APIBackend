namespace APIBackend.Responses
{
    public class MovimientoRes
    {
        public DateTime Fecha { get; set; }
        public string NombreCliente { get; set; }
        public int NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public int SaldoInicial { get; set; }
        public string Estado { get; set; }
        public string Movimiento { get; set; }
        public int SaldoDisponible { get; set; }
    }
}
