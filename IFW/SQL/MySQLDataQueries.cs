using IFW.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace IFW.SQL
{
    public class MySQLDataQueries
    {
        public string GetUserDetails(string UserName, string Password) 
        {

            try
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfiguration _configuration = builder.Build();
                using var con = new MySqlConnection(_configuration.GetConnectionString("mySQLconnString"));
                con.Open();
                var stm = "SELECT * FROM tblUsers WHERE UserName = @UserName AND Password = @Password;";
                var cmd = new MySqlCommand(stm, con);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);

                using MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string json = JsonConvert.SerializeObject(

                        new UserObjectFromMySQL()
                        {
                            UserName = rdr["UserName"].ToString(),
                            Password = rdr["Password"].ToString(),
                            Email = rdr["Email"].ToString(),
                            DepartmentName = rdr["DepartmentName"].ToString(),
                            IsAdmin = rdr["IsAdmin"].ToString(),
                            IsManager = rdr["IsManager"].ToString(),
                            EmploymentDate = rdr["EmploymentDate"].ToString()
                        }
                    );
                    con.Close();
                    return json;
                }
                string jsonNotFound = JsonConvert.SerializeObject(

                        new UserObjectFromMySQL()
                        {
                            UserName = "",
                            Password = "",
                            Email = "",
                            DepartmentName = "",
                            IsAdmin = "",
                            IsManager = "",
                            EmploymentDate = ""
                        }
                    );
                con.Close();
                return jsonNotFound;
            }
            catch (Exception)
            {

                return "";
            }
        }

        public void CreateNewUser(string UserName, string Password, string FirstName, string Surname, string Email,
                                  string DepartmentName, string IsManager, string IsAdmin, string EmploymentDate)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration _configuration = builder.Build();
            using var conn = new MySqlConnection(_configuration.GetConnectionString("mySQLconnString"));

            conn.Open();
            var sql = "INSERT INTO tblUsers(UserName, Password, FirstName, Surname, Email, DepartmentName, IsManager, IsAdmin, EmploymentDate) " +
                      "VALUES(@UserName, @Password, @FirstName, @Surname, @Email, @DepartmentName, @IsManager, @IsAdmin, @EmploymentDate)";
            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@Surname", Surname);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@DepartmentName", DepartmentName);
            cmd.Parameters.AddWithValue("@IsManager", IsManager);
            cmd.Parameters.AddWithValue("@IsAdmin", IsAdmin);
            cmd.Parameters.AddWithValue("@EmploymentDate", EmploymentDate);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
    }
}
