namespace Ramand_Application.Model
{
    public class BaseConnectionString
    {
        public string ConnectionString 
        {
            get 
            {
             return "Data Source=HP-6th-Thin\\MSSQLSERVER1;Initial Catalog=Ramand_Db;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
            }
        }
    }
}