namespace CRUDAppUsingADO
{
    public static class ConnectionString
    {
        private static string cs = "Server=DESKTOP-N5S1FRS;Database=CrudADOdb;TrustServerCertificate=True;Trusted_Connection=True;Encrypt=False;";
        public static string dbcs { get => cs; }
    }
}
