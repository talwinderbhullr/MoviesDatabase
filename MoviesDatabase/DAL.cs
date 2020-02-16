using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace MoviesDatabase
{
    public class DAL
    {
        SqlCommand Sql_Command_Object = new SqlCommand();

        String Sql_Query_Object;
        SqlConnection Sql_Connection_Object = new SqlConnection("Data Source=LAPTOP-RAKIOMBV\\SQLEXPRESS;Initial Catalog=MoviesDatabase;Integrated Security=True");

        // Movies functions
        // Read Movies from Database
        public DataTable Movies()
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "Select * from Movies";

                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Connection_Object.Open();
                    SqlReader = Sql_Command_Object.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }
                    Sql_Connection_Object.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
                return null;
            }
        }
        // Add Movies to Database
        public bool AddMovie(string Title, string Genre, string RentalCost, string Year, string Plot, string Copies, string Rating)
        {
            bool MovieAdded = false;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "Insert into Movies(Title,Genre,Rental_Cost,Year,Plot,Copies,Rating) values (@Title, @Genre, @Rental_Cost, @Year, @Plot, @Copies, @Rating)";

                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Command_Object.Parameters.AddWithValue("@Title", Title);
                    Sql_Command_Object.Parameters.AddWithValue("@Genre", Genre);
                    Sql_Command_Object.Parameters.AddWithValue("@Rental_Cost", RentalCost);
                    Sql_Command_Object.Parameters.AddWithValue("@Year", Year);
                    Sql_Command_Object.Parameters.AddWithValue("@Plot", Plot);
                    Sql_Command_Object.Parameters.AddWithValue("@Copies", Copies);
                    Sql_Command_Object.Parameters.AddWithValue("@Rating", Rating);

                    Sql_Command_Object.CommandText = Sql_Query_Object;

                    Sql_Connection_Object.Open();
                    int rowsadded = Sql_Command_Object.ExecuteNonQuery();

                    if (rowsadded > 0)
                    {
                        MovieAdded = true;
                    }
                    Sql_Connection_Object.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
            }
            return MovieAdded;
        }
        // Edit Movies in Database
        public bool EditMovies(string Title, string Genre, string RentalCost, string Year, string Plot, string Copies, string Rating, int MovieID)
        {
            bool MovieEdited = false;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "Update Movies set Title = @Title, Genre = @Genre, Rental_Cost = @Rental_Cost, Year = @Year, " +
                    " Plot = @Plot, Copies = @Copies, Rating = @Rating where MovieID = @MovieID";

                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Command_Object.Parameters.AddWithValue("@Title", Title);
                    Sql_Command_Object.Parameters.AddWithValue("@Genre", Genre);
                    Sql_Command_Object.Parameters.AddWithValue("@Rental_Cost", RentalCost);
                    Sql_Command_Object.Parameters.AddWithValue("@Year", Year);
                    Sql_Command_Object.Parameters.AddWithValue("@Plot", Plot);
                    Sql_Command_Object.Parameters.AddWithValue("@Copies", Copies);
                    Sql_Command_Object.Parameters.AddWithValue("@Rating", Rating);
                    Sql_Command_Object.Parameters.AddWithValue("@MovieID", MovieID);

                    Sql_Command_Object.CommandText = Sql_Query_Object;

                    Sql_Connection_Object.Open();
                    int rowsmodified = Sql_Command_Object.ExecuteNonQuery();

                    if (rowsmodified > 0)
                    {
                        MovieEdited = true;
                    }
                    Sql_Connection_Object.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
            }
            return MovieEdited;
        }
        // Delete Movies from Database
        public bool DeleteMovies(int MovieID)
        {
            bool MovieDeleted = false;

            try
            {
                using (SqlCommand SqlStr = Sql_Connection_Object.CreateCommand())
                {
                    SqlStr.CommandText = "DeleteMovie";
                    SqlStr.CommandType = CommandType.StoredProcedure;
                    SqlStr.Parameters.AddWithValue("@MovieID", MovieID);
                    Sql_Connection_Object.Open();
                    Int32 rowsadded = SqlStr.ExecuteNonQuery();

                    if (rowsadded > 0)
                    {
                        MovieDeleted = true;
                    }
                    Sql_Connection_Object.Close();
                }
                return MovieDeleted;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
                return false;
            }
        }
        //Search Movies in Database
        public DataTable SearchMovie(string Title, string Genre, string Year)
        {
            DataTable Data_table_Object = new DataTable();
            SqlDataReader Sql_Reader_Object;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "Select * from Movies where Title like '%" + Title + "%' and Genre like '%" + Genre + "%' and Year like '%" + Year + "%'";

                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Connection_Object.Open();
                    Sql_Reader_Object = Sql_Command_Object.ExecuteReader();

                    if (Sql_Reader_Object.HasRows)
                    {
                        Data_table_Object.Load(Sql_Reader_Object);
                    }
                    Sql_Connection_Object.Close();
                    return Data_table_Object;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
                return null;
            }
        }

        // Customers Function
        // Read Customers from Database
        public DataTable Customer()
        {
            DataTable Data_Table_Object = new DataTable();
            SqlDataReader Sql_Reader_Object;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "Select * from Customer";

                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Connection_Object.Open();
                    Sql_Reader_Object = Sql_Command_Object.ExecuteReader();

                    if (Sql_Reader_Object.HasRows)
                    {
                        Data_Table_Object.Load(Sql_Reader_Object);
                    }
                    Sql_Connection_Object.Close();
                    return Data_Table_Object;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
                return null;
            }
        }
        // Create or Add Customer in Databse
        public bool AddNewCustomer(string FirstName, string LastName, string Address, string Phone)
        {
            bool CustomerAdded = false;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "Insert into Customer(FirstName,LastName,Address,Phone) values (@FirstName, @LastName, @Address, @Phone)";

                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Command_Object.Parameters.AddWithValue("@FirstName", FirstName);
                    Sql_Command_Object.Parameters.AddWithValue("@LastName", LastName);
                    Sql_Command_Object.Parameters.AddWithValue("@Address", Address);
                    Sql_Command_Object.Parameters.AddWithValue("@Phone", Phone);

                    Sql_Command_Object.CommandText = Sql_Query_Object;

                    Sql_Connection_Object.Open();
                    int rowsadded = Sql_Command_Object.ExecuteNonQuery();

                    if (rowsadded > 0)
                    {
                        CustomerAdded = true;
                    }
                    Sql_Connection_Object.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
            }
            return CustomerAdded;
        }
        // Delete Customer from DataBase
        public bool DeleteExistingCustomer(int CustID)
        {
            bool CustomerDeleted = false;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "Delete from Customer where CustID= @CustID";

                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Command_Object.Parameters.AddWithValue("@CustID", CustID);
                    Sql_Connection_Object.Open();
                    Int32 rowsadded = Sql_Command_Object.ExecuteNonQuery();

                    if (rowsadded > 0)
                    {
                        CustomerDeleted = true;
                    }
                    Sql_Connection_Object.Close();
                }
                return CustomerDeleted;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
                return false;
            }
        }
        // Edit Customer in Database
        public bool UpDateCustomerDetails(string FirstName, string LastName, string Address, string Phone, int CustID)
        {
            bool customerEdited = false;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "Update Customer set FirstName = @FirstName, LastName = @LastName, Address = @Address, Phone = @Phone where CustID = @CustID";

                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Command_Object.Parameters.AddWithValue("@FirstName", FirstName);
                    Sql_Command_Object.Parameters.AddWithValue("@LastName", LastName);
                    Sql_Command_Object.Parameters.AddWithValue("@Address", Address);
                    Sql_Command_Object.Parameters.AddWithValue("@Phone", Phone);
                    Sql_Command_Object.Parameters.AddWithValue("@CustID", CustID);

                    Sql_Command_Object.CommandText = Sql_Query_Object;

                    Sql_Connection_Object.Open();
                    int rowsmodified = Sql_Command_Object.ExecuteNonQuery();

                    if (rowsmodified > 0)
                    {
                        customerEdited = true;
                    }
                    Sql_Connection_Object.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
            }
            return customerEdited;
        }
        //Search Customers in Database
        public DataTable SearchCustomer(string firstname, string LastName, string Address, string Phone)
        {
            DataTable Data_Table_Object = new DataTable();
            SqlDataReader Sql_Reader_Object;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "Select * from Customer where firstname like '%" + firstname + "%' and lastname like '%" + LastName + "%' and Address like '%" + Address + "%' and Phone like '%" + Phone + "%'";

                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Connection_Object.Open();
                    Sql_Reader_Object = Sql_Command_Object.ExecuteReader();

                    if (Sql_Reader_Object.HasRows)
                    {
                        Data_Table_Object.Load(Sql_Reader_Object);
                    }
                    Sql_Connection_Object.Close();
                    return Data_Table_Object;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
                return null;
            }
        }
        // Issues Functions
        // Customer search issue page search by address
        public DataTable SearchCustomer1(string Address)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "Select * from Customer where Address like '%" + Address + "%'";

                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Connection_Object.Open();
                    SqlReader = Sql_Command_Object.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }
                    Sql_Connection_Object.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
                return null;
            }
        }
        // Movies Search Issue page
        public DataTable SearchMovie1(string Title)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "Select * from Movies where Title like '%" + Title + "%'";

                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Connection_Object.Open();
                    SqlReader = Sql_Command_Object.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }
                    Sql_Connection_Object.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
                return null;
            }
        }
        // Select Customer for issue updates datagrid to all current and previously rented movies
        public DataTable Issued(int CustID)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "Select FirstName, LastName, Address, Title, RMID, DateRented, DateReturned " +
                    "from Customer c INNER JOIN RentedMovies rm on c.CustID = rm.CustIDFK INNER JOIN Movies m on m.MovieID = rm.MovieIDFK where CustID = " + CustID + "order by DateRented desc";

                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Connection_Object.Open();
                    SqlReader = Sql_Command_Object.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }
                    Sql_Connection_Object.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
                return null;
            }
        }
        // Issue Movies to Customer
        public bool IssueMovie(string MovieID, string CustID, DateTime DateTime)
        {
            bool IssueMovie = false;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "Insert into RentedMovies(MovieIDFK,CustIDFK,DateRented) values(@MovieIDFK, @CustIDFK, @DateRented)";

                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Command_Object.Parameters.AddWithValue("@MovieIDFK", MovieID);
                    Sql_Command_Object.Parameters.AddWithValue("@CustIDFK", CustID);
                    Sql_Command_Object.Parameters.AddWithValue("@DateRented", DateTime.Now);

                    Sql_Command_Object.CommandText = Sql_Query_Object;

                    Sql_Connection_Object.Open();
                    int rowsadded = Sql_Command_Object.ExecuteNonQuery();

                    if (rowsadded > 0)
                    {
                        IssueMovie = true;
                    }
                    Sql_Connection_Object.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
            }
            return IssueMovie;
        }
        // Return Movie
        public bool ReturnMovie(string RMID, DateTime DateTime)
        {
            bool ReturnMovie = false;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "Update RentedMovies set DateReturned = @DateReturned where RMID = @RMID;";



                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Command_Object.Parameters.AddWithValue("@DateReturned", DateTime.Now);
                    Sql_Command_Object.Parameters.AddWithValue("@RMID", RMID);
                    Sql_Command_Object.CommandText = Sql_Query_Object;

                    Sql_Connection_Object.Open();
                    int rowsadded = Sql_Command_Object.ExecuteNonQuery();

                    if (rowsadded > 0)
                    {
                        ReturnMovie = true;
                    }
                    Sql_Connection_Object.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
            }
            return ReturnMovie;
        }
        // Stats Page data
        public DataTable Stats()
        {
            DataTable Data_Table_Object = new DataTable();
            SqlDataReader Sql_Reader_Object;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "select * from PopularVideo";

                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Connection_Object.Open();
                    Sql_Reader_Object = Sql_Command_Object.ExecuteReader();

                    if (Sql_Reader_Object.HasRows)
                    {
                        Data_Table_Object.Load(Sql_Reader_Object);
                    }
                    Sql_Connection_Object.Close();
                    return Data_Table_Object;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
                return null;
            }
        }
        public DataTable BestCustomer()
        {
            DataTable Data_Table_object = new DataTable();
            SqlDataReader Sql_Reader_Object;

            try
            {
                Sql_Command_Object.Connection = Sql_Connection_Object;
                Sql_Query_Object = "select * from PopularCustomer";

                using (Sql_Command_Object = new SqlCommand(Sql_Query_Object, Sql_Connection_Object))
                {
                    Sql_Connection_Object.Open();
                    Sql_Reader_Object = Sql_Command_Object.ExecuteReader();

                    if (Sql_Reader_Object.HasRows)
                    {
                        Data_Table_object.Load(Sql_Reader_Object);
                    }
                    Sql_Connection_Object.Close();
                    return Data_Table_object;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                Sql_Connection_Object.Close();
                return null;
            }
        }
    }
}
