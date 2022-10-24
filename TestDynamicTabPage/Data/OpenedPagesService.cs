namespace TestDynamicTabPage.Data
{
    public class OpenedPagesService
    {
        #region Construct
        public List<Pagina> Paginas { get; set; }
        public Dictionary<int, Dictionary<string, object>> Estado { get; set; }

        public OpenedPagesService()
        {
            this.Paginas = new List<Pagina>();
            Paginas.Add(new Pagina
            {
                Numero = 1,
                Nombre = "Home",
                Tipo = Activator.CreateInstance("TestDynamicTabPage", $"TestDynamicTabPage.Pages.Index").Unwrap().GetType()
            });
            this.Estado = new Dictionary<int, Dictionary<string, object>>();
            var Parametros = new Dictionary<string, object>();
            this.Estado = new Dictionary<int, Dictionary<string, object>>();
            this.Estado.Add(1, Parametros);
        }
        #endregion

        public void agregarPaginaNueva(Pagina pagina)
        {
            this.Paginas.Add(pagina);
            var Parametros = new Dictionary<string, object>();
            this.Estado.Add(pagina.Numero, Parametros);
        }

        public void AgregarValorAlEstado(int numero, string key, object valor)
        {
            Estado[numero][key] = valor;
            NotifyStateChanged();
        }

        public void cerrarPagina(Pagina pagina)
        {
            Paginas.Remove(pagina);
            Estado.Remove(pagina.Numero);
        }

        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

    public class Pagina
    {
        public string Nombre { get; set; }
        public Type Tipo { get; set; }
        public int Numero { get; set; }
    }
}
