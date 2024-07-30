using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace BeFir.Models
{
    public class Contact
    {
        [Key]
        [Required(ErrorMessage = "Username required")]
        //[StringLength(20, ErrorMessage = "username is too long")]
        public string Name { set; get; }

        //[Required(ErrorMessage = "Password is required")]
        //[DataType(DataType.Password)]
        //[StringLength(20, MinimumLength = 4, ErrorMessage = "Password must be between 8 and 20 characters")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{4,20}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { set; get; }


        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Phone number must be between 10 and 15 digits and can start with a +")]
        public string PhoneNumber { set; get; }

        public string Message { set; get; }

        public static Boolean insertContactData(Contact contact)
        {
            SqlConnection cn = new SqlConnection();

            try
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BeFit_DB;Integrated Security=True";
                cn.Open();

                SqlCommand cmdInsert = new SqlCommand();

                cmdInsert.Connection = cn;

                cmdInsert.CommandType = System.Data.CommandType.Text;

                cmdInsert.CommandText = "insert into Contact values(@Name, @Email, @PhoneNumber, @Message)";

                cmdInsert.Parameters.AddWithValue("@Name", contact.Name);
                cmdInsert.Parameters.AddWithValue("@Message", contact.Message);
                cmdInsert.Parameters.AddWithValue("@Email", contact.Email);
                cmdInsert.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);

                cmdInsert.ExecuteNonQuery();
                Console.WriteLine(cmdInsert.CommandText);

                Console.WriteLine("Data inseted successfully !!");

                Console.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
                return false;
            }

            finally
            {
                cn.Close();
            }
        }
    }
}
