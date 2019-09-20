using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentRegistrationApp
{
    public partial class StudentRegistrationUI : Form
    {
        public StudentRegistrationUI()
        {
            InitializeComponent();
        }
        List<string> studentIds = new List<string>();
        List<string> studentNames = new List<string>();
        List<string> studentMobiles = new List<string>();
        List<string> studentAddresses = new List<string>();
        List<int> studentAges = new List<int>();
        List<double> studentGpas = new List<double>();
        int[] displayIndex = new int[2];

        private void addButton_Click(object sender, EventArgs e)
        {
            bool validData = false;
            string message="";
            validData = CheckData();
            if (validData)
            {
                message = SaveStudent();
            }
            else
                message = "Student information cannot saved.";
            MessageBox.Show(message);
        }
        private void showAllButton_Click(object sender, EventArgs e)
        {
            ClearInput();
            maxTextBox.Text = studentGpas.Max().ToString();
            double gpa = Convert.ToDouble(maxTextBox.Text);
            int index = studentGpas.IndexOf(gpa);
            maxNameTextBox.Text = studentNames[index];
            minTextBox.Text = studentGpas.Min().ToString();
            gpa = Convert.ToDouble(minTextBox.Text);
            index = studentGpas.IndexOf(gpa);
            minNameTextBox.Text = studentNames[index];
            avgTextBox.Text = studentGpas.Average().ToString();
            totalTextBox.Text = studentGpas.Sum().ToString();
            displayIndex[0] = 0;
            displayIndex[1] = studentIds.Count;
            DisplayStudentInformation(displayIndex);
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (idRadioButton.Checked)
            {
                if (studentIds.Contains(searchTextBox.Text))
                {
                    displayIndex[0] = studentIds.IndexOf(searchTextBox.Text);
                    displayIndex[1] = displayIndex[0] + 1;
                    DisplayStudentInformation(displayIndex);
                }
                else
                {
                    MessageBox.Show(searchTextBox.Text + " Student Id does not found");
                }
            }
            else if (nameRadioButton.Checked)
            {
                if (studentNames.Contains(searchTextBox.Text))
                {
                    displayIndex[0] = studentNames.IndexOf(searchTextBox.Text);
                    displayIndex[1] = displayIndex[0] + 1;
                    DisplayStudentInformation(displayIndex);
                }
                else
                {
                    MessageBox.Show(" Student Name " + searchTextBox.Text + " does not found");
                }
            }
            else
            {
                if (studentMobiles.Contains(searchTextBox.Text))
                {
                    displayIndex[0] = studentMobiles.IndexOf(searchTextBox.Text);
                    displayIndex[1] = displayIndex[0] + 1;
                    DisplayStudentInformation(displayIndex);

                }
                else
                {
                    MessageBox.Show(" Mobile number " + searchTextBox.Text + " does not found");
                }
            }
        }
        private string SaveStudent()
        {
            try
            {
                string id = idTextBox.Text;
                string name = nameTextBox.Text;
                string contact = mobileTextBox.Text;
                string address = addressTextBox.Text;
                int age = Convert.ToInt32(ageTextBox.Text);
                double gpa = Convert.ToDouble(gpaTextBox.Text);
                
                studentNames.Add(name);
                studentAddresses.Add(address);
                studentIds.Add(id);
                studentAges.Add(age);
                studentMobiles.Add(contact);
                studentGpas.Add(gpa);

                ClearInput();
                displayIndex[0] = studentIds.Count-1;
                displayIndex[1] = studentIds.Count;
                DisplayStudentInformation(displayIndex);
                return "Student added successfully.";
            }
            catch(Exception exception)
            {
                 return exception.Message;
            }
        }

        private void ClearInput()
        {
            idTextBox.Clear();
            nameTextBox.Clear();
            ageTextBox.Clear();
            mobileTextBox.Clear();
            addressTextBox.Clear();
            gpaTextBox.Clear();
            showRichTextBox.Clear();
        }

        private void DisplayStudentInformation(int[] index)
        {
            string message;
            message = "Student No\tID\tName\tMobile Number\tAge\tAddress\tGPA Point\n";
            for (int i = index[0]; i < index[1]; i++)
            {               
                message = message + Convert.ToInt16(i + 1)+ "\t\t" + studentIds[i] + "\t" + studentNames[i] + "\t" + studentMobiles[i] + "\t" + studentAges[i] + "\t" + studentAddresses[i] + "\t" + studentGpas[i] + "\n";                                   
            }
            showRichTextBox.Text = message;
        }
        private bool CheckData()
        {
            if (String.IsNullOrEmpty(idTextBox.Text))
            {
                idValidLabel.Text = "Please insert Student Id";
                return false;
            }
            else
            {
                if (idTextBox.Text.Length != 4)
                {
                    idValidLabel.Text = "Student Id must be 4 characters";
                    return false;
                }
                else if (studentIds.Contains(idTextBox.Text))
                {
                    idValidLabel.Text = idTextBox.Text + " ID is already exists";
                    return false;
                }
                else
                    idValidLabel.Text = "";

            }
            if (String.IsNullOrEmpty(nameTextBox.Text))
            {
                nameValidLabel.Text = "Please insert Student Name";
                return false;
            }
            else
            {
                if (nameTextBox.Text.Length > 30)
                {
                    nameValidLabel.Text = "Student Id must be in 30 characters";
                    return false;
                }
                else
                    nameValidLabel.Text = "";
            }
            if (String.IsNullOrEmpty(mobileTextBox.Text))
            {
                mobileValidLabel.Text = "Please insert Mobile number";
                return false;
            }
            else
            {
                if (mobileTextBox.Text.Length != 11)
                {
                    mobileValidLabel.Text = "Mobile number must be 11 digits";
                    return false;
                }
                else if (studentMobiles.Contains(mobileTextBox.Text))
                {
                    mobileValidLabel.Text = "This" + mobileTextBox.Text + " number is already used";
                }
                else
                    mobileValidLabel.Text = "";
            }
            if (String.IsNullOrEmpty(ageTextBox.Text))
            {
                ageValidLabel.Text = "Please insert age";
                return false;
            }
            if (String.IsNullOrEmpty(addressTextBox.Text))
            {
                addressValidLabel.Text = "Please insert address";
                return false;
            }
            if (String.IsNullOrEmpty(gpaTextBox.Text))
            {
                gpaValidLabel.Text = "Please insert GPA Point";
                return false;
            }
            else
            {
                try
                {
                    if (double.Parse(gpaTextBox.Text) > 4)
                    {
                        gpaValidLabel.Text = "GPA Point range is 0-4";
                        return false;
                    }
                    else
                        gpaValidLabel.Text = "";
                }
                
                catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return false;
            }
                
            }
            return true;
        }

        private void mobileTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }

        private void ageTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }
        
        

        private void idRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (idRadioButton.Checked )
            {
                searchButton.Enabled = true;
                searchTextBox.Enabled = true;
            }
            else
            {
                searchButton.Enabled = false;
                searchTextBox.Enabled = false;
            }
        }

        private void nameRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (nameRadioButton.Checked )
            {
                searchButton.Enabled = true;
                searchTextBox.Enabled = true;
            }
            else
            {
                searchButton.Enabled = false;
                searchTextBox.Enabled = false;
            }
        }

        private void MobileRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (MobileRadioButton.Checked)
            {
                searchButton.Enabled = true;
                searchTextBox.Enabled = true;
            }
            else
            {
                searchButton.Enabled = false;
                searchTextBox.Enabled = false;
            }
        }
    }
}
