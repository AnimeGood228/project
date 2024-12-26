using System;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace project
{
    public partial class statement_1 : BaseStatement
    {
        public statement_1(int id)
        {
            InitializeComponent();
            id_ = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPdfFromDatabase(1, SavePdfWithText);
        }

        private void SavePdfWithText(byte[] pdfData, string outputPath)
        {
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllBytes(tempFilePath, pdfData);

            using (PdfReader reader = new PdfReader(tempFilePath))
            {
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    using (Document document = new Document())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(document, fs);
                        document.Open();

                        BaseFont bf = BaseFont.CreateFont("C:\\Windows\\Fonts\\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            document.NewPage();
                            PdfContentByte cb = writer.DirectContent;
                            PdfImportedPage page = writer.GetImportedPage(reader, i);
                            cb.AddTemplate(page, 0, 0);

                            if (!string.IsNullOrEmpty(selectedImagePath))
                            {
                                try
                                {
                                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(selectedImagePath);
                                    img.ScaleToFit(80f, 80f);
                                    img.SetAbsolutePosition(500f, 365f);
                                    cb.AddImage(img);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Ошибка при добавлении изображения: {ex.Message}");
                                }
                            }

                            string role = textBox_role.Text;
                            string countDays = textBox_count_day.Text;
                            string name = GetUser_NameById(id_);

                            
                            DateTime firstDate = dateTimePicker1.Value; 
                            DateTime lastDate = dateTimePicker2.Value; 

                           
                            string firstDay = firstDate.Day.ToString();
                            string firstMonth = firstDate.Month.ToString();
                            string firstYear = (firstDate.Year % 100).ToString();
                            string lastDay = lastDate.Day.ToString();
                            string lastMonth = lastDate.Month.ToString();
                            string lastYear = (lastDate.Year % 100).ToString();
                            string workYears = textBox_work_years.Text;
                            string[] workYearsParts = workYears.Split(new[] { ' ' }, 2);
                            string workYearsFirstPart = workYearsParts.Length > 0 ? workYearsParts[0] : string.Empty;
                            string workYearsSecondPart = workYearsParts.Length > 1 ? workYearsParts[1] : string.Empty;
                            string currentDate = DateTime.Now.ToString("dd.MM.yyyy");

                            var textToAdd = new (string Text, float X, float Y)[]
                            {
                                (Text: $"От {name}", X: 425, Y: 695),
                                (Text: $"{role}", X: 425, Y: 665),
                                (Text: $"{countDays}", X: 210, Y: 520),
                                (Text: $"{firstDay}", X: 370, Y: 520),
                                (Text: $"{firstMonth}", X: 420, Y: 520),
                                (Text: $"{firstYear}", X: 505, Y: 520),
                                (Text: $"{lastDay}", X: 110, Y: 488),
                                (Text: $"{lastMonth}", X: 180, Y: 488),
                                (Text: $"{lastYear}", X: 246, Y: 488),
                                (Text: $"{workYearsSecondPart}", X: 398, Y: 488),
                                (Text: $"{workYearsFirstPart}", X: 435, Y: 488),
                                (Text: $"{currentDate}", X: 150, Y: 390)
                            };

                            cb.BeginText();
                            cb.SetFontAndSize(bf, 12);

                            foreach (var (Text, X, Y) in textToAdd)
                            {
                                cb.SetTextMatrix(X, Y);
                                cb.ShowText(Text);
                            }

                            cb.EndText();
                        }

                        document.Close();
                    }
                }
            }

            File.Delete(tempFilePath);
            MessageBox.Show($"PDF файл сохранен на рабочем столе: {outputPath}");
            Close();
        }

        private void button_signature_Click(object sender, EventArgs e)
        {
            selectedImagePath = LoadImage();
        }
    }
}
