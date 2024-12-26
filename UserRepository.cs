using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace project
{
    public class UserRepository
    {
        private string connectionString = @"Server=1MISA\MSSQLSERVER01;Database=tempdb;Trusted_Connection=True;";

        public void RegisterUser(string username, string password, string fullName, bool isAdmin = false)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Login, Password, Name, IsAdmin) VALUES (@Username, @Password, @FullName, @IsAdmin)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@FullName", fullName);
                command.Parameters.AddWithValue("@IsAdmin", isAdmin);

                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Пользователь зарегистрирован.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка регистрации: {ex.Message}");
                }
            }
        }

        public void DeleteUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Users WHERE UserID = @User Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@User Id", userId);

                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Пользователь удален.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления пользователя: {ex.Message}");
                }
            }
        }

        public void UpdateUser(int userId, string username, string password, string fullName, bool isAdmin)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET Login = @Username, Password = @Password, Name = @FullName, IsAdmin = @IsAdmin WHERE UserID = @User Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@User Id", userId);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@FullName", fullName);
                command.Parameters.AddWithValue("@IsAdmin", isAdmin);

                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Пользователь обновлен.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления пользователя: {ex.Message}");
                }
            }
        }

        public void GetAllUsers(DataGridView dataGridView)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT UserID, Login, Name, IsAdmin FROM Users";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                System.Data.DataTable table = new System.Data.DataTable();
                connection.Open();
                adapter.Fill(table);
                dataGridView.DataSource = table;
            }
        }
        public string GetUser_NameById(int id) // Исправлено имя метода
        {
            string userName = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Name FROM Users WHERE UserID = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        userName = result.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден.");
                    }
                }
            }

            return userName; // Возвращаем имя пользователя
        }
        public int? LoginUser(string username, string password) // Убедитесь, что метод правильно определен
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT UserID FROM Users WHERE Login = @Username AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : (int?)null;
            }
        }
    }
}