using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace project
{
    public class BaseStatement : Form
    {
        protected string selectedImagePath;
        protected int id_;
        protected string connectionString = @"Server=1MISA\MSSQLSERVER01;Database=tempdb;Trusted_Connection=True;";
        protected UserRepository userRepository; // Ссылка на UserRepository

        public BaseStatement()
        {
            userRepository = new UserRepository(); // Инициализация UserRepository
        }

        protected string GetUser_NameById(int id) // Исправлено имя метода
        {
            return userRepository.GetUser_NameById(id); // Исправлено имя метода
        }

        
    

        protected void LoadPdfFromDatabase(int id, Action<byte[], string> savePdfAction)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT FileData, FileName FROM PdfFiles WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] pdfData = (byte[])reader["FileData"];
                            string fileName = reader["FileName"].ToString();

                            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            string filePath = Path.Combine(desktopPath, fileName);

                            savePdfAction(pdfData, filePath);
                        }
                        else
                        {
                            MessageBox.Show("Файл не найден в базе данных.");
                        }
                    }
                }
            }
        }

        protected string LoadImage()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Выберите изображение";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }
            return null;
        }
    }
}
