using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BeFir.Models
{
    public class Admin
    {
        [Key]
        [Required(ErrorMessage = "Username required")]
        [StringLength(20, ErrorMessage = "username is too long")]
        public string Username { set; get; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password must be between 8 and 20 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{4,20}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
        public string Password { set; get; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Phone number must be between 10 and 15 digits and can start with a +")]
        public string PhoneNumber { set; get; }

        public static Admin loginAdmin(string username, string password)
        {
            SqlConnection cn = new SqlConnection();

            Admin admin = null;

            try
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BeFit_DB;Integrated Security=True";
                cn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cn;

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "select * from Admin where Username=@Username and Password=@Password ";
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                //cmd.Parameters.AddWithValue("@RegistrationId", id);


                SqlDataReader dr = cmd.ExecuteReader();

                

                if (dr.Read())
                {
                    admin = new Admin();
                    admin.Username = dr.GetString("Username");
                    admin.Password = dr.GetString("Password");
                    admin.Email = dr.GetString("Email");
                    admin.PhoneNumber = dr.GetString("PhoneNumber");
                }
                else
                {
                    Console.WriteLine("Admin data not found !!!");
                }

                dr.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                cn.Close();
            }


            return admin;
            //var log = db.TblUsers.Where(a => a.userName.Equals(tblUser
            //                .userName) && a.password.Equals(tblUser.password)).FirstOrDefault();

        }
    }

   

}
