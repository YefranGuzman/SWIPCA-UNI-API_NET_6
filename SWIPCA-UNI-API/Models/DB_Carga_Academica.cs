namespace SWIPCA_UNI_API.Models
{
    public class DB_Carga_Academica
    {
        private string connectionstring = string.Empty;
        public DB_Carga_Academica()
        {
            var connection = new ConfigurationBuilder().SetBasePath
                (Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json").Build();

            connectionstring = connection.GetSection
                ("DBCargaAcademica").Value;
        }
        public string ConexionSQL()
        {
            return connectionstring;
        }
    }
}
