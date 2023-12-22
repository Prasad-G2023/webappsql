using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System.Data.SqlClient;
using webappsql.Vendors;

namespace webappsql.Certification
{
    public class VendorServices
        {
          //  private static string db_database = "SQLmigratePG";
          //  private static string db_password = "Azure@123";
           // private static string db_source = "sqlmigrateapac.database.windows.net";
          //  private static string db_user = "sqladmin";
        private SqlConnection GetConnection()
            {
                TokenCredential tokenCredential = new DefaultAzureCredential();

                string KeyVaultUrl = "https://sqlwebapp.vault.azure.net/";
                string secretName = "dbconnect";

                SecretClient secretClient = new SecretClient(new Uri(KeyVaultUrl), tokenCredential);
                
                var secret = secretClient.GetSecret(secretName);
                
                string connectionString= secret.Value.Value;

                return new SqlConnection(connectionString);

              //var _builder = new SqlConnectionStringBuilder();
             //_builder.DataSource = db_source;
            // _builder.UserID = db_user;
            // _builder.Password = db_password;
            // _builder.InitialCatalog = db_database;
           //  return new SqlConnection(_builder.ConnectionString);
        }
                //public List<Offerings> GetCourses(string VendorName, string CourseID, string CourseName, int Price, int DiscountPercent)
        public List<Offerings> GetCourses()
        {
            List<Offerings> _course_lst = new List<Offerings>();

            SqlConnection _conn = GetConnection();       

            string _statement = "SELECT VendorName,CourseID,CourseName,Price,DiscountPercent from Courses";

            _conn.Open();

            SqlCommand _sqlcommand = new SqlCommand(_statement, _conn);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())

            {
                while (_reader.Read())
                {
                    Offerings course = new Offerings()
                    {
                        VendorName = _reader.GetString(0),
                        CourseID = _reader.GetString(1),
                        CourseName = _reader.GetString(2),
                        Price = _reader.GetString(3),
                        DiscountPercent = _reader.GetInt32(4)
                    };
                    _course_lst.Add(course);
                }
                _conn.Close();

                return _course_lst;
            }
        }
        //private List<courses> _courses_lst;

        //private string CourseID;
        //private string CourseName;
        //private int Price;
        //private int Discountpercentage;


    }
}
