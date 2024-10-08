using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SimpleStudentRegistrationSystem.Model;
using System.Windows.Forms;

namespace SimpleStudentRegistrationSystem.Repository
{
    internal class StudentRepository
    {
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Orcine\\Documents\\StudentRegistrationDB.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public List<StudentInfo> GetStudents()
        {
            var students = new List<StudentInfo>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM  Student ORDER BY StudentId ASC"; //this is connected to Database Lesson in SQL Introduction which is the SQL SELECT Statement
                    using (SqlCommand comm = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentInfo student = new StudentInfo
                                {
                                    StudentId = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    PhoneNumber = reader.GetString(3)
                                };
                                students.Add(student);
                            }
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            return students;
        }
        public StudentInfo GetStudent(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM Student WHERE StudentId = @id"; //SQL SELECT Statement
                    using (SqlCommand comm = new SqlCommand (sql, conn))
                    {
                        comm.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                return new StudentInfo
                                {
                                    StudentId = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    PhoneNumber = reader.GetString(3)
                                };
                            }
                        }
                    }
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            return null;
        }
        public void CreateStudent (StudentInfo student)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    //SQL INSERT Statement
                    string sql = "INSERT INTO Student (Name, Email, PhoneNumber) VALUES (@name, @email, @phonenumber)";
                    using (SqlCommand comm = new SqlCommand(sql, conn))
                    {
                        comm.Parameters.AddWithValue("@name", student.Name);
                        comm.Parameters.AddWithValue("@email", student.Email);
                        comm.Parameters.AddWithValue("@phonenumber", student.PhoneNumber);

                        comm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message);
            }
        }
        public void UpdateStudent (StudentInfo student)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    //SQL UPDATE Statement
                    string sql = "UPDATE Student SET Name=@name, Email=@email, PhoneNumber=@phonenumber WHERE StudentId=@id";
                    using (SqlCommand comm = new SqlCommand(sql, conn))
                    {
                        comm.Parameters.AddWithValue("@name", student.Name);
                        comm.Parameters.AddWithValue("@email", student.Email);
                        comm.Parameters.AddWithValue("@phonenumber", student.PhoneNumber);
                        comm.Parameters.AddWithValue("@id", student.StudentId);

                        comm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
        public void DeleteStudent(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    //SQL DELETE Statement
                    string sql = "DELETE FROM Student WHERE StudentId=@id";
                    using (SqlCommand comm = new SqlCommand(sql, conn))
                    {
                        comm.Parameters.AddWithValue("@id", id);

                        comm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
}
