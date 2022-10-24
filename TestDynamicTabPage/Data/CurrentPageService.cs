namespace TestDynamicTabPage.Data
{
    public class CurrentPageService
    {
        #region Construct
        private int contador = 2;
        public int Numero = 1;
        public string NombrePaginaActual { get; set; }
        public Type TypePaginaActual { get; set; }

        public CurrentPageService()
        {
            NombrePaginaActual = "Home";
            TypePaginaActual = Activator.CreateInstance("TestDynamicTabPage", $"TestDynamicTabPage.Pages.Index").Unwrap().GetType();
        }
        #endregion

        public void CrearNuevaPagina(string nombreDelapagina, string nombreDelComponente)
        {
            NombrePaginaActual = nombreDelapagina;
            Numero = contador;
            contador++;
            TypePaginaActual = Activator.CreateInstance("TestDynamicTabPage", $"{nombreDelComponente}").Unwrap().GetType();
            NotifyStateChanged();
        }

        public Pagina GetPagina()
        {
            return new Pagina
            {
                Nombre = NombrePaginaActual,
                Tipo = TypePaginaActual,
                Numero = Numero
            };
        }

        public void cambiarVentana(Pagina pagina)
        {
            this.NombrePaginaActual = pagina.Nombre;
            this.Numero = pagina.Numero;
            this.TypePaginaActual = pagina.Tipo;
            NotifyStateChanged();
        }
        
        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();        
    }
}
