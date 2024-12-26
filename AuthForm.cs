using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using project;

public class AuthForm : Form
{
    private Label lblUsername;
    private Label lblPassword;
    private Label lblFullName;
    private Label lblRole; // Метка для роли
    private TextBox txtUsername;
    private TextBox txtPassword;
    private TextBox txtFullName;
    private ComboBox cmbRole; // ComboBox для выбора роли
    private Button btnSubmit;
    private string action;
    private Form1 mainForm; // Ссылка на Form1
    private UserRepository userRepository; // Ссылка на UserRepository

    public AuthForm(string action, Form1 form)
    {
        this.action = action;
        this.mainForm = form; // Сохраняем ссылку на Form1
        this.userRepository = new UserRepository(); // Инициализация UserRepository
        this.Size = new System.Drawing.Size(400, action == "reg" ? 450 : 350); // Увеличиваем размер для роли
        this.Text = action == "reg" ? "Регистрация" : "Вход";
        this.StartPosition = FormStartPosition.CenterScreen; // Центрирование формы

        // Инициализация меток
        lblUsername = new Label()
        {
            Text = "Логин:",
            Location = new System.Drawing.Point(50, 100),
            Size = new System.Drawing.Size(100, 20),
            Font = new Font("Roboto", 10F, FontStyle.Bold | FontStyle.Italic)
        };

        lblPassword = new Label()
        {
            Text = "Пароль:",
            Location = new System.Drawing.Point(50, 150),
            Size = new System.Drawing.Size(100, 20),
            Font = new Font("Roboto", 10F, FontStyle.Bold | FontStyle.Italic)
        };

        // Инициализация текстовых полей
        txtUsername = new TextBox()
        {
            Location = new System.Drawing.Point(150, 100),
            Width = 200,
            Font = new Font("Roboto", 10F, FontStyle.Bold | FontStyle.Italic)
        };

        txtPassword = new TextBox()
        {
            Location = new System.Drawing.Point(150, 150),
            Width = 200,
            PasswordChar = '*',
            Font = new Font("Roboto", 10F, FontStyle.Bold | FontStyle.Italic)
        };

        // Если регистрация, добавляем поле для ФИО и выбора роли
        if (action == "reg")
        {
            lblFullName = new Label()
            {
                Text = "ФИО:",
                Location = new System.Drawing.Point(50, 200),
                Size = new System.Drawing.Size(100, 20),
                Font = new Font("Roboto", 10F, FontStyle.Bold | FontStyle.Italic)
            };

            txtFullName = new TextBox()
            {
                Location = new System.Drawing.Point(150, 200),
                Width = 200,
                Font = new Font("Roboto", 10F, FontStyle.Bold | FontStyle.Italic)
            };

            // Инициализация метки и ComboBox для выбора роли
            lblRole = new Label()
            {
                Text = "Роль:",
                Location = new System.Drawing.Point(50, 250),
                Size = new System.Drawing.Size(100, 20),
                Font = new Font("Roboto", 10F, FontStyle.Bold | FontStyle.Italic)
            };

            cmbRole = new ComboBox()
            {
                Location = new System.Drawing.Point(150, 250),
                Width = 200,
                Font = new Font("Roboto", 10F, FontStyle.Bold | FontStyle.Italic)
            };

            // Добавление значений в ComboBox
            cmbRole.Items.Add("Пользователь");
            cmbRole.Items.Add("Администратор");
            cmbRole.SelectedIndex = 0; // Устанавливаем значение по умолчанию

            this.Controls.Add(txtFullName);
            this.Controls.Add(lblFullName);
            this.Controls.Add(cmbRole);
        }

        // Инициализация кнопки
        btnSubmit = new Button()
        {
            Text = action == "reg" ? "Зарегистрироваться" : "Войти",
            Location = new System.Drawing.Point(100, action == "reg" ? 300 : 250),
            Width = 200,
            Height = 40,
            Font = new Font("Roboto", 12F, FontStyle.Bold),
            BackColor = Color.FromArgb(80, 227, 194), // Бирюзовый
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
            FlatAppearance = { BorderSize = 0 }
        };
        btnSubmit.FlatAppearance.MouseOverBackColor = Color.FromArgb(74, 144, 226); // Эффект наведения
        btnSubmit.FlatAppearance.MouseDownBackColor = Color.FromArgb(50, 227, 194); // Эффект нажатия
        btnSubmit.Click += BtnSubmit_Click;

        // Добавление элементов управления на форму
        this.Controls.Add(lblUsername);
        this.Controls.Add(lblPassword);
        this.Controls.Add(txtUsername);
        this.Controls.Add(txtPassword);
        this.Controls.Add(btnSubmit);
    }

    private void BtnSubmit_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text.Trim();
        string fullName = action == "reg" ? txtFullName.Text.Trim() : null;
        bool isAdmin = action == "reg" && cmbRole.SelectedItem.ToString() == "Администратор"; // Определяем роль

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Логин и пароль не могут быть пустыми.");
            return;
        }

        if (action == "reg")
        {
            userRepository.RegisterUser(username, password, fullName, isAdmin); // Передаем роль
            Close();
        }
        else if (action == "add")
        {
            int? userId = userRepository.LoginUser(username, password);
            if (userId.HasValue)
            {
                MessageBox.Show("Вход выполнен успешно.");
                mainForm.Hide();
                menu form = new menu(userId.Value);
                form.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.");
            }
        }
    }
}