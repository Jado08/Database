using SimpleStudentRegistrationSystem.Model;
using SimpleStudentRegistrationSystem.Repository;
using System;
using System.Windows.Forms;

namespace SimpleStudentRegistrationSystem
{
    public partial class Student : UserControl
    {
        private StudentRepository _studentRepository = new StudentRepository();
        public Student()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var students = _studentRepository.GetStudents();
            dataGridViewStudents.DataSource = students;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var student = new StudentInfo
            {
                Name = txtBoxName.Text,
                Email = txtBoxEmail.Text,
                PhoneNumber = txtBoxPhoneNumber.Text
            };
            if (string.IsNullOrEmpty(student.Name) || string.IsNullOrEmpty(student.Email))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }
            _studentRepository.CreateStudent(student);
            MessageBox.Show("Student added successfully!");
            LoadStudents();
            ClearForm();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.SelectedRows.Count > 0)
            {
                int selectedStudentId = Convert.ToInt32(dataGridViewStudents.SelectedRows[0].Cells["StudentId"].Value);
                var student = new StudentInfo
                {
                    StudentId = selectedStudentId,
                    Name = txtBoxName.Text,
                    Email = txtBoxEmail.Text,
                    PhoneNumber = txtBoxPhoneNumber.Text
                };
                _studentRepository.UpdateStudent(student);
                MessageBox.Show("Student updated successfully!");
                LoadStudents();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Please select a student to update.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.SelectedRows.Count > 0)
            {
                int selectedStudentId = Convert.ToInt32(dataGridViewStudents.SelectedRows[0].Cells["StudentId"].Value);
                _studentRepository.DeleteStudent(selectedStudentId);
                LoadStudents();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Please select a student to delete.");
            }
        }
        private void LoadStudents()
        {
            var students = _studentRepository.GetStudents();
            dataGridViewStudents.DataSource = students;
        }
        private void ClearForm()
        {
            txtBoxName.Clear();
            txtBoxEmail.Clear();
            txtBoxPhoneNumber.Clear();
        }

        private void dataGridViewStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewStudents.Rows[e.RowIndex];
                txtBoxName.Text = row.Cells["Name"].Value.ToString();
                txtBoxEmail.Text = row.Cells["Email"].Value.ToString();
                txtBoxPhoneNumber.Text = row.Cells["PhoneNumber"].Value.ToString();
            }
        }
    }
}
