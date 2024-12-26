using System;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace project
{
    public partial class statement_2 : BaseStatement
    {
        public statement_2(int id)
        {
            InitializeComponent();
            id_ = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPdfFromDatabase(2, SavePdfWithImageAndDate);
        }

        private void SavePdfWithImageAndDate(byte[] pdfData, string outputPath)
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

                           
                            DateTime date = dateTimePicker1.Value; 

                            string day = date.Day.ToString();
                            string month = date.Month.ToString();
                            string year = (date.Year % 100).ToString();

                            var textToAdd = new (string Text, float X, float Y)[]
                            {
                                (Text: $"{day}", X: 365, Y: 530),
                                (Text: $"{month}", X: 425, Y: 530),
                                (Text: $"{year}", X: 495, Y: 530)
                            };

                            cb.BeginText();
                            cb.SetFontAndSize(bf, 12);
                            foreach (var (Text, X, Y) in textToAdd)
                            {
                                cb.SetTextMatrix(X, Y);
                                cb.ShowText(Text);
                            }
                            cb.EndText();

                            
                            DateTime currentDate = DateTime.Now;
                            string currentDay = currentDate.Day.ToString();
                            string currentMonth = currentDate.Month.ToString();
                            string currentYear = currentDate.Year.ToString();

                            string currentDateText = $"{currentDay}.{currentMonth}.{currentYear}";

                            cb.BeginText();
                            cb.SetFontAndSize(bf, 12);
                            cb.SetTextMatrix(150, 430);
                            cb.ShowText(currentDateText);
                            cb.EndText();

                            
                            string name = GetUser_NameById(id_);
                            cb.BeginText();
                            cb.SetFontAndSize(bf, 12);
                            cb.SetTextMatrix(450, 710);
                            cb.ShowText(name);
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

        private void button_signature_Click_1(object sender, EventArgs e)
        {
            selectedImagePath = LoadImage();
        }
    }
}
