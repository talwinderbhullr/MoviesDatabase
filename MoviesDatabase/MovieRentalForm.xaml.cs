using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MoviesDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DAL Database_Object = new DAL();

        public MainWindow()
        {
            InitializeComponent();
        }
        // Click on Movie in Datagrid populate all Movie Fields
        private void DGVMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGVMovies.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)DGVMovies.SelectedItems[0];

                txtTitle.Text = Convert.ToString(row["Title"]);
                txtGenre.Text = Convert.ToString(row["Genre"]);
                txtRentalCost.Text = Convert.ToString(row["Rental_Cost"]);
                txtYearReleased.Text = Convert.ToString(row["Year"]);
                txtMovieID.Text = Convert.ToString(row["MovieID"]);
                txtRating.Text = Convert.ToString(row["Rating"]);
                txtPlot.Text = Convert.ToString(row["Plot"]);
                if (txtCopies.Text == "")
                {
                    txtCopies.Text = "0";
                }
                else txtCopies.Text = Convert.ToString(row["Copies"]);
            }
        }

        // Movies Events
        // Add Movie Function
        private void btnAddMovie_Click(object sender, RoutedEventArgs e)
        {
            string Title = txtTitle.Text;
            string Genre = txtGenre.Text;
            string RentalCost = txtRentalCost.Text;
            string YearReleased = txtYearReleased.Text;
            string Plot = txtPlot.Text;
            string Copies = txtCopies.Text;
            string MPAA = txtRating.Text;

            if (txtTitle.Text != "" && txtGenre.Text != "" && txtRentalCost.Text != "" && txtYearReleased.Text != "" && txtPlot.Text != "" && txtCopies.Text != "" && txtRating.Text != "")
            {
                bool MovieAdded = Database_Object.AddMovie(txtTitle.Text, txtGenre.Text, txtRentalCost.Text, txtYearReleased.Text, txtPlot.Text, txtCopies.Text, txtRating.Text);

                if (MovieAdded == true)
                {
                    MessageBox.Show("Movie Added");
                    txtTitle.Clear();
                    txtGenre.Clear();
                    txtYearReleased.Clear();
                    txtRentalCost.Clear();
                    txtCopies.Clear();
                    txtRating.Clear();
                    txtMovieID.Clear();
                    txtPlot.Clear();
                    DGVMovies.ItemsSource = Database_Object.Movies().DefaultView;
                }
            }
            else
            {
                MessageBox.Show("Please fill all the details");
            }
        }
        // Update Movie details funtion
        private void btnUpdateMovie_Click(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text != "" && txtGenre.Text != "" && txtRentalCost.Text != "" && txtYearReleased.Text != "" && txtPlot.Text != "" && txtCopies.Text != "" && txtRating.Text != "")
            {
                string Title = txtTitle.Text;
                string Genre = txtGenre.Text;
                string RentalCost = txtRentalCost.Text;
                string YearReleased = txtYearReleased.Text;
                string Plot = txtPlot.Text;
                string Copies = txtCopies.Text;
                string Rating = txtRating.Text;
                string MovieID = txtMovieID.Text;
                Database_Object.EditMovies(Title, Genre, RentalCost, YearReleased, Plot, Copies, Rating, Convert.ToInt32(MovieID));

                bool MovieEdited = Database_Object.EditMovies(txtTitle.Text, txtGenre.Text, txtRentalCost.Text, txtYearReleased.Text, txtPlot.Text, txtCopies.Text, txtRating.Text, Convert.ToInt32(MovieID));

                if (MovieEdited == true)
                {
                    MessageBox.Show("Updates are done");
                    txtTitle.Clear();
                    txtGenre.Clear();
                    txtYearReleased.Clear();
                    txtRentalCost.Clear();
                    txtCopies.Clear();
                    txtRating.Clear();
                    txtMovieID.Clear();
                    txtPlot.Clear();
                    DGVMovies.ItemsSource = Database_Object.Movies().DefaultView;
                }
                else
                {
                    MessageBox.Show("Please fill all fields");
                }
            }
            else
            {
                MessageBox.Show("Please select movie that you want to Update");
            }
        }
        // Delete Movie Function
        private void btnDeleteMovie_Click(object sender, RoutedEventArgs e)
        {
            if (DGVMovies.SelectedIndex > -1)
            {
                MessageBoxResult dialogResult = MessageBox.Show("If you want to delete movie than press Yes.", "Movies", MessageBoxButton.YesNo);

                if (dialogResult.ToString() == "Yes")
                {
                    DataRowView row = (DataRowView)DGVMovies.SelectedItems[0];
                    Int32 MovieID = Convert.ToInt32(row["MovieID"]);
                    {
                        Database_Object.DeleteMovies(MovieID);
                        MessageBox.Show("Movie Has Been Deleted");
                        txtTitle.Clear();
                        txtGenre.Clear();
                        txtYearReleased.Clear();
                        txtRentalCost.Clear();
                        txtCopies.Clear();
                        txtRating.Clear();
                        txtMovieID.Clear();
                        txtPlot.Clear();
                        DGVMovies.ItemsSource = Database_Object.Movies().DefaultView;
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Movie To Delete");
            }
        }
        // Text change events for all Movie text boxes
        private void txtTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGVMovies.ItemsSource = Database_Object.SearchMovie(txtTitle.Text, txtGenre.Text, txtYearReleased.Text).DefaultView;
        }
        private void txtGenre_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGVMovies.ItemsSource = Database_Object.SearchMovie(txtTitle.Text, txtGenre.Text, txtYearReleased.Text).DefaultView;
        }
        private void txtRentalCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGVMovies.ItemsSource = Database_Object.SearchMovie(txtTitle.Text, txtGenre.Text, txtYearReleased.Text).DefaultView;
        }
        private void txtYearReleased_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGVMovies.ItemsSource = Database_Object.SearchMovie(txtTitle.Text, txtGenre.Text, txtYearReleased.Text).DefaultView;
        }
        private void btnClearMovies_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Clear();
            txtGenre.Clear();
            txtYearReleased.Clear();
            txtRentalCost.Clear();
            txtCopies.Clear();
            txtRating.Clear();
            txtMovieID.Clear();
            txtPlot.Clear();
        }

        // Click on customer in Datagrid populate all customer fields
        private void DGVCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGVCustomers.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)DGVCustomers.SelectedItems[0];

                txtFirstName.Text = Convert.ToString(row["FirstName"]);
                txtLastName.Text = Convert.ToString(row["LastName"]);
                txtAddress.Text = Convert.ToString(row["Address"]);
                txtPhoneNumber.Text = Convert.ToString(row["Phone"]);
                txtCustomerID.Text = Convert.ToString(row["CustID"]);
            }
        }

        // Customer events
        // Load Customers into grid
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DGVCustomers.ItemsSource = Database_Object.Customer().DefaultView;
            DGVMovies.ItemsSource = Database_Object.Movies().DefaultView;
            dgCustomers1.ItemsSource = Database_Object.Customer().DefaultView;
            dgMovies1.ItemsSource = Database_Object.Movies().DefaultView;
        }
        // Add Customer Button function
        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            string firstname = txtFirstName.Text;
            string lastname = txtLastName.Text;
            string address = txtAddress.Text;
            string phone = txtPhoneNumber.Text;

            if (txtFirstName.Text != "" && txtLastName.Text != "" && txtAddress.Text != "" && txtPhoneNumber.Text != "")
            {
                bool CustomerAdded = Database_Object.AddNewCustomer(txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtPhoneNumber.Text);

                if (CustomerAdded == true)
                {
                    MessageBox.Show("Customer Added");
                    txtFirstName.Clear();
                    txtLastName.Clear();
                    txtAddress.Clear();
                    txtPhoneNumber.Clear();
                    txtCustomerID.Clear();
                    DGVCustomers.ItemsSource = Database_Object.Customer().DefaultView;
                }
            }
            else
            {
                MessageBox.Show("Please enter details in all fields");
            }
        }
        // Update customer button function
        private void btnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            {
                if (txtFirstName.Text != "" && txtLastName.Text != "" && txtAddress.Text != "" && txtPhoneNumber.Text != "")
                {
                    string firstname = txtFirstName.Text;
                    string lastname = txtLastName.Text;
                    string address = txtAddress.Text;
                    string phone = txtPhoneNumber.Text;
                    string CustID = txtCustomerID.Text;
                    Database_Object.UpDateCustomerDetails(firstname, lastname, address, phone, Convert.ToInt32(CustID));

                    bool customerEdited = Database_Object.UpDateCustomerDetails(txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtPhoneNumber.Text, Convert.ToInt32(CustID));

                    if (customerEdited == true)
                    {
                        MessageBox.Show("Customer Details Updated");
                        txtFirstName.Clear();
                        txtLastName.Clear();
                        txtAddress.Clear();
                        txtPhoneNumber.Clear();
                        txtCustomerID.Clear();
                        DGVCustomers.ItemsSource = Database_Object.Customer().DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("Complete All Fields");
                    }
                }
                else
                {
                    MessageBox.Show("Select a Customer To Update");
                }
            }
        }
        // Delete Customer button Function
        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (DGVCustomers.SelectedIndex > -1)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are You Sure You Want To DELETE This Customer??", "Customers", MessageBoxButton.YesNo);

                if (dialogResult.ToString() == "Yes")
                {
                    DataRowView row = (DataRowView)DGVCustomers.SelectedItems[0];
                    Int32 CustID = Convert.ToInt32(row["CustID"]);
                    {
                        Database_Object.DeleteExistingCustomer(CustID);
                        MessageBox.Show("Customer Has Been Deleted");
                        txtFirstName.Clear();
                        txtLastName.Clear();
                        txtAddress.Clear();
                        txtPhoneNumber.Clear();
                        txtCustomerID.Clear();
                        DGVCustomers.ItemsSource = Database_Object.Customer().DefaultView;
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Customer To Delete");
            }
        }
        // Text change enents for all customer text boxes
        private void txtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGVCustomers.ItemsSource = Database_Object.SearchCustomer(txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtPhoneNumber.Text).DefaultView;
        }

        private void txtLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGVCustomers.ItemsSource = Database_Object.SearchCustomer(txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtPhoneNumber.Text).DefaultView;
        }

        private void txtAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGVCustomers.ItemsSource = Database_Object.SearchCustomer(txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtPhoneNumber.Text).DefaultView;
        }

        private void txtPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGVCustomers.ItemsSource = Database_Object.SearchCustomer(txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtPhoneNumber.Text).DefaultView;
        }
        // Clear Buttons
        private void btnClearCustomer_Click(object sender, RoutedEventArgs e)
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAddress.Clear();
            txtPhoneNumber.Clear();
            txtCustomerID.Clear();
        }

        // Text Change Events for Issue Page
        private void txtAddressSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgCustomers1.ItemsSource = Database_Object.SearchCustomer1(txtAddressSearch.Text).DefaultView;
        }

        private void txtMovieSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgMovies1.ItemsSource = Database_Object.SearchMovie1(txtMovieSearch.Text).DefaultView;
        }

        private void dgCustomers1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCustomers1.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)dgCustomers1.SelectedItems[0];
                txtFirstNameRentals.Text = Convert.ToString(row["FirstName"]);
                txtLastNameRentals.Text = Convert.ToString(row["LastName"]);
                txtCustomerIDRentals.Text = Convert.ToString(row["CustID"]);
            }
            try
            {
                DataRowView row = (DataRowView)dgCustomers1.SelectedItems[0];
                int CustID = Convert.ToInt32(row["CustID"]);
                dgCustomerIssues.ItemsSource = Database_Object.Issued(CustID).DefaultView;
            }
            catch
            {
            }
        }
        // Issues Page
        // Issues Button
        private void btnIssue_Click(object sender, RoutedEventArgs e)
        {

            if (dgCustomers1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Customer");
                return;
            }
            if (dgMovies1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Movie");
                return;
            }

            bool movieIssued = Database_Object.IssueMovie(txtMovieIDRentals.Text, txtCustomerIDRentals.Text, DateTime.Today);
            if (movieIssued == true)
            {
                MessageBox.Show("Movie Issued");
                txtTitle.Clear();
                txtRentalCost.Clear();
            }
            try
            {
                DataRowView row = (DataRowView)dgCustomers1.SelectedItems[0];
                int CustID = Convert.ToInt32(row["CustID"]);
                dgCustomerIssues.ItemsSource = Database_Object.Issued(CustID).DefaultView;
            }
            catch
            {

            }

        }
        // Selection of movies
        private void dgMovies1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (dgMovies1.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)dgMovies1.SelectedItems[0];
                txtTitleRentals.Text = Convert.ToString(row["Title"]);
                txtCostRentals.Text = Convert.ToString(row["Rental_Cost"]);
                txtMovieIDRentals.Text = Convert.ToString(row["MovieID"]);
            }
            try
            {
                DataRowView row = (DataRowView)dgMovies1.SelectedItems[0];
                int CustID = Convert.ToInt32(row["CustID"]);
                dgCustomerIssues.ItemsSource = Database_Object.Issued(CustID).DefaultView;
            }
            catch
            {
            }
        }
        // Returns button
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomerIssues.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Movie to return from Movie Rental History" +
                    " This will populate after selection a customer");
                return;
            }

            DataRowView row = (DataRowView)dgCustomerIssues.SelectedItems[0];
            string testdate = Convert.ToString(row["DateReturned"]);
            if (testdate != "")
            {
                MessageBox.Show("This movie has alreay been returned");
                return;
            }

            try
            {
                bool movieReturned = Database_Object.ReturnMovie(txtRMID.Text, DateTime.Today);
                if (movieReturned == true)
                {
                    MessageBox.Show("Movie Returned");
                    DGVMovies.ItemsSource = Database_Object.Movies().DefaultView;
                    int CustID = Convert.ToInt32(txtCustomerIDRentals.Text);

                    dgCustomerIssues.ItemsSource = Database_Object.Issued(CustID).DefaultView;
                }
            }
            catch
            {

            }
        }
        // Customer selection change
        private void dgCustomerIssues_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCustomerIssues.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)dgCustomerIssues.SelectedItems[0];
                txtTitleRentals.Text = Convert.ToString(row["Title"]);
                txtRMID.Text = Convert.ToString(row["RMID"]);
            }
        }
        // Stats Page
        private void rbMostPopularMovie_Checked(object sender, RoutedEventArgs e)
        {
            if (rbMostPopularMovie.IsChecked == true)
            {
                dgStats.ItemsSource = Database_Object.Stats().DefaultView;
            }
        }

        private void rbBestCustomer_Checked(object sender, RoutedEventArgs e)
        {
            if (rbBestCustomer.IsChecked == true)
            {
                dgStats.ItemsSource = Database_Object.BestCustomer().DefaultView;
            }
        }
        private void validateNumbers(TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtFirstName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            validateNumbers(e);
        }

        private void txtLastName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            validateNumbers(e);
        }

        private void txtPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
