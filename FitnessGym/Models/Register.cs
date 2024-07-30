using Microsoft.Data.SqlClient;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace BeFir.Models
{
    public class Register
    {
        [Key]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain only digits")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegistrationId {  get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^\d{10,10}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { set; get; }

        [Required(ErrorMessage = "Adddress is required")]
        [StringLength(300, ErrorMessage = "Address is too long")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Package is required")]
        public string Package { get; set; }

        [Required(ErrorMessage = "Trainer is required")]
        public string Trainer { get; set; }





        public static void insertForm(Register register)
        {
            SqlConnection cn = new SqlConnection();

            try
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Befit_DB;Integrated Security=True";
                cn.Open();

                SqlCommand cmdInsert = new SqlCommand();

                cmdInsert.Connection = cn;

                //cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;
               cmdInsert.CommandType = System.Data.CommandType.Text;



               cmdInsert.CommandText = "InsertFormData";

                cmdInsert.CommandText = "INSERT INTO Register VALUES (@Name , @Gender , @Email , @PhoneNumber , @Address , @Package , @Trainer)";

               // cmdInsert.Parameters.AddWithValue("@RegistrationId", register.RegistrationId);
                cmdInsert.Parameters.AddWithValue("@Name", register.Name);
                cmdInsert.Parameters.AddWithValue("@Gender", register.Gender);
                cmdInsert.Parameters.AddWithValue("@Email", register.Email);
                cmdInsert.Parameters.AddWithValue("@PhoneNumber", register.PhoneNumber);
                cmdInsert.Parameters.AddWithValue("@Address", register.Address);
                cmdInsert.Parameters.AddWithValue("@Package", register.Package);
                cmdInsert.Parameters.AddWithValue("@Trainer", register.Trainer);

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

        public static void updateForm(Register register)
        {
            SqlConnection cn = new SqlConnection();

            try
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BeFit_DB;Integrated Security=True";
                cn.Open();

                SqlCommand cmdInsert = new SqlCommand();

                cmdInsert.Connection = cn;

                cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;

                //cmdInsert.CommandText = "update Employees set Name = @Name , Basic = @Basic , DeptNo = @DeptNo where EmpNo = @EmpNo";

                cmdInsert.CommandText = "UpdateForm";

                cmdInsert.Parameters.AddWithValue("@RegistrationId", register.RegistrationId);
                cmdInsert.Parameters.AddWithValue("@Name", register.Name);
                cmdInsert.Parameters.AddWithValue("@Gender", register.Gender);
                cmdInsert.Parameters.AddWithValue("@Email", register.Email);
                cmdInsert.Parameters.AddWithValue("@PhoneNumber", register.PhoneNumber);
                cmdInsert.Parameters.AddWithValue("@Address", register.Address);
                cmdInsert.Parameters.AddWithValue("@Package", register.Package);
                cmdInsert.Parameters.AddWithValue("@Trainer", register.Trainer);

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
        public static void deleteForm(int id)
        {
            SqlConnection cn = new SqlConnection();

            try
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BeFit_DB;Integrated Security=True";
                cn.Open();

                SqlCommand cmdInsert = new SqlCommand();

                cmdInsert.Connection = cn;

                cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;

                cmdInsert.CommandText = "DeleteForm";

                cmdInsert.Parameters.AddWithValue("@RegistrationId", id);


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
        public static Register getSingleFormData(int id)
        {
            SqlConnection cn = new SqlConnection();

            Register registerData = null;

            try
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BeFit_DB;Integrated Security=True";
                cn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cn;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "FetchSingleData";

                cmd.Parameters.AddWithValue("@RegistrationId", id);


                SqlDataReader dr = cmd.ExecuteReader();

                registerData = new Register();

                if (dr.Read())
                {
                    registerData.RegistrationId = dr.GetInt32("RegistrationId");
                    registerData.Name = dr.GetString("Name");
                    registerData.Gender = dr.GetString("Gender");
                    registerData.Email = dr.GetString("Email");
                    registerData.PhoneNumber = dr.GetString("PhoneNumber");
                    registerData.Address = dr.GetString("Address");
                    registerData.Package = dr.GetString("Package");
                    registerData.Trainer = dr.GetString("Trainer");
                 
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


            return registerData;
        }

        // to get all employees data 
        public static List<Register> GetAllForms()
        {
            SqlConnection cn = new SqlConnection();

            List<Register> allForms = new List<Register>();


            try
            {
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BeFit_DB;Integrated Security=True";

                cn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cn;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "FetchAllData";

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Register registerData = new Register();
                    registerData.RegistrationId = dr.GetInt32("RegistrationId");
                    registerData.Name = dr.GetString("Name");
                    registerData.Gender = dr.GetString("Gender");
                    registerData.Email = dr.GetString("Email");
                    registerData.PhoneNumber = dr.GetString("PhoneNumber");
                    registerData.Address = dr.GetString("Address");
                    registerData.Package = dr.GetString("Package");
                    registerData.Trainer = dr.GetString("Trainer");

                    allForms.Add(registerData);
                }

                dr.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { cn.Close(); }


            return allForms;
        }


        

    }
}
