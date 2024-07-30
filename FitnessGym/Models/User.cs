using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BeFir.Models
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "Usersname required")]
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
        [RegularExpression(@"^\d{10,10}$", ErrorMessage = "Enter valid Phone Number")]
        public string PhoneNumber { set; get; }




        public static void insertUser(User user)
        {
            SqlConnection cn = new SqlConnection();

            try
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BeFit_DB;Integrated Security=True";
                cn.Open();

                SqlCommand cmdInsert = new SqlCommand();

                cmdInsert.Connection = cn;

                cmdInsert.CommandType = System.Data.CommandType.Text;

                cmdInsert.CommandText = "insert into Users values(@UserName , @Password , @Email , @PhoneNumber)";

                cmdInsert.Parameters.AddWithValue("@UserName", user.Username);
                cmdInsert.Parameters.AddWithValue("@Password", user.Password);
                cmdInsert.Parameters.AddWithValue("@Email", user.Email);
                cmdInsert.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);

                cmdInsert.ExecuteNonQuery();
                Console.WriteLine(cmdInsert.CommandText);

                Console.WriteLine("Data inseted successfully !!");

                Console.WriteLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                cn.Close();
            }

        }


        // to update the data

        public static void updateUser(User user)
        {
            SqlConnection cn = new SqlConnection();

            try
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BeFit_DB;Integrated Security=True";
                cn.Open();

                SqlCommand cmdInsert = new SqlCommand();

                cmdInsert.Connection = cn;

                cmdInsert.CommandType = System.Data.CommandType.Text;

                cmdInsert.CommandText = "update Users set Password = @Password , Email = @Email , PhoneNumber = @PhoneNumber where Username = @UserName";

                cmdInsert.Parameters.AddWithValue("@UserName", user.Username);
                cmdInsert.Parameters.AddWithValue("@Password", user.Password);
                cmdInsert.Parameters.AddWithValue("@Email", user.Email);
                cmdInsert.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);

                cmdInsert.ExecuteNonQuery();

                Console.WriteLine(cmdInsert.CommandText);
                Console.WriteLine("Data updated successfully !!");
                Console.WriteLine();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                cn.Close();
            }




        }


        // to delete the data
        public static void deleteUser(string username)
        {
            SqlConnection cn = new SqlConnection();

            try
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BeFit_DB;Integrated Security=True";
                cn.Open();

                SqlCommand cmdInsert = new SqlCommand();

                cmdInsert.Connection = cn;

                cmdInsert.CommandType = System.Data.CommandType.Text;

                cmdInsert.CommandText = "delete from Users where Username = @UserName";

                cmdInsert.Parameters.AddWithValue("@UserName", username);


                cmdInsert.ExecuteNonQuery();

                Console.WriteLine(cmdInsert.CommandText);
                Console.WriteLine("Data deleted successfully !!");
                Console.WriteLine();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                cn.Close();
            }
        }




        // to get the single data of employee
        public static User getSingleUser(string username)
        {
            SqlConnection cn = new SqlConnection();

            User user = null;

            try
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BeFit_DB;Integrated Security=True";
                cn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cn;

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "select * from Users where Username = @Username";

                cmd.Parameters.AddWithValue("@Username", username);


                SqlDataReader dr = cmd.ExecuteReader();

                user = new User();

                if (dr.Read())
                {
                    user.Username = dr.GetString("Username");
                    user.Password = dr.GetString("Password");
                    user.PhoneNumber = dr.GetString("PhoneNumber");
                    user.Email = dr.GetString("Email");
                }
                else
                {
                    Console.WriteLine("Employee data not found !!!");
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


            return user;
        }

        public static User loginUser(string username, string password)
        {
            SqlConnection cn = new SqlConnection();

            User user = null;

            try
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BeFit_DB;Integrated Security=True";
                cn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cn;

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "select * from Users where Username=@Username and Password=@Password ";
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                //cmd.Parameters.AddWithValue("@RegistrationId", id);


                SqlDataReader dr = cmd.ExecuteReader();

                

                if (dr.Read())
                {
                    user = new User();
                    user.Username = dr.GetString("Username");
                    user.Password = dr.GetString("Password");
                    user.Email = dr.GetString("Email");
                    user.PhoneNumber = dr.GetString("PhoneNumber");
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


            return user;
            //var log = db.TblUsers.Where(a => a.userName.Equals(tblUser
            //                .userName) && a.password.Equals(tblUser.password)).FirstOrDefault();

        }
        /*
        // to get all employees data 
        public static List<Employee> getAllEmployees()
        {
            SqlConnection cn = new SqlConnection();

            List<Employee> empList = new List<Employee>();


            try
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JKJune2024;Integrated Security=True";

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cn;

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "select * from Employees";

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Employee emp = new Employee();
                    emp.EmpNo = rd.GetInt32("EmpNo");
                    emp.Name = rd.GetString("Name");
                    emp.Basic = rd.GetDecimal("Basic");
                    emp.DeptNo = rd.GetInt32("DeptNo");

                    empList.Add(emp);
                }

                rd.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { cn.Close(); }


            return empList;
        }
        */

    }
}
