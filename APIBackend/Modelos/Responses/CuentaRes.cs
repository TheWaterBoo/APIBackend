namespace APIBackend.Modelos.Responses
{
    public class CuentaRes
    {
        public int numeroCuenta { get; set; }
        public string tipo { get; set; }
        public int saldoInicial { get; set; }
        public string estado { get; set; }
        public string nombreCliente { get; set; }
    }
}
