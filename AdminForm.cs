using System;
using System.Windows.Forms;

namespace project
{
    public partial class AdminForm : Form
    {
        private DataGridView dataGridViewUsers;
        private CheckBox checkBoxIsAdmin;
        private TextBox textBoxUsername;
        private TextBox textBoxPassword;
        private TextBox textBoxFullName;
        private Button buttonAddUser;
        private Button buttonUpdateUser;
        private Button buttonDeleteUser;
        private UserRepository userRepository;

        public AdminForm()
        {
            InitializeComponent();
            userRepository = new UserRepository();
            LoadUsers();
        }

        private void LoadUsers()
        {
            userRepository.GetAllUsers(dataGridViewUsers);
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            string fullName = textBoxFullName.Text;
            bool isAdmin = checkBoxIsAdmin.Checked;

            userRepository.RegisterUser(username, password, fullName, isAdmin);
            LoadUsers();
        }

        private void buttonUpdateUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["User ID"].Value);
                string username = textBoxUsername.Text;
                string password = textBoxPassword.Text;
                string fullName = textBoxFullName.Text;
                bool isAdmin = checkBoxIsAdmin.Checked;

                userRepository.UpdateUser(userId, username, password, fullName, isAdmin);
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для обновления.");
            }
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["User ID"].Value);
                userRepository.DeleteUser(userId);
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для удаления.");
            }
        }

        private void dataGridViewUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count > 0)
            {
                textBoxUsername.Text = dataGridViewUsers.SelectedRows[0].Cells["Login"].Value.ToString();
                textBoxFullName.Text = dataGridViewUsers.SelectedRows[0].Cells["Name"].Value.ToString();
                checkBoxIsAdmin.Checked = Convert.ToBoolean(dataGridViewUsers.SelectedRows[0].Cells["IsAdmin"].Value);
            }
        }

        private void InitializeComponent()
        {
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            this.checkBoxIsAdmin = new System.Windows.Forms.CheckBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxFullName = new System.Windows.Forms.TextBox();
            this.buttonAddUser = new System.Windows.Forms.Button();
            this.buttonUpdateUser = new System.Windows.Forms.Button();
            this.buttonDeleteUser = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.Location = new System.Drawing.Point(70, 12);
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.Size = new System.Drawing.Size(240, 150);
            this.dataGridViewUsers.TabIndex = 0;
            // 
            // checkBoxIsAdmin
            // 
            this.checkBoxIsAdmin.AutoSize = true;
            this.checkBoxIsAdmin.Location = new System.Drawing.Point(70, 168);
            this.checkBoxIsAdmin.Name = "checkBoxIsAdmin";
            this.checkBoxIsAdmin.Size = new System.Drawing.Size(80, 17);
            this.checkBoxIsAdmin.TabIndex = 1;
            this.checkBoxIsAdmin.Text = "checkBox1";
            this.checkBoxIsAdmin.UseVisualStyleBackColor = true;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(68, 207);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(120, 20);
            this.textBoxUsername.TabIndex = 2;
            this.textBoxUsername.Text = "Логин пользователя";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(68, 233);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(120, 20);
            this.textBoxPassword.TabIndex = 3;
            this.textBoxPassword.Text = "Пароль пользователя";
            // 
            // textBoxFullName
            // 
            this.textBoxFullName.Location = new System.Drawing.Point(68, 259);
            this.textBoxFullName.Name = "textBoxFullName";
            this.textBoxFullName.Size = new System.Drawing.Size(120, 20);
            this.textBoxFullName.TabIndex = 4;
            this.textBoxFullName.Text = "Фио пользователя";
            // 
            // buttonAddUser
            // 
            this.buttonAddUser.Location = new System.Drawing.Point(194, 205);
            this.buttonAddUser.Name = "buttonAddUser";
            this.buttonAddUser.Size = new System.Drawing.Size(163, 23);
            this.buttonAddUser.TabIndex = 5;
            this.buttonAddUser.Text = "Добавить пользователя";
            this.buttonAddUser.UseVisualStyleBackColor = true;
            // 
            // buttonUpdateUser
            // 
            this.buttonUpdateUser.Location = new System.Drawing.Point(194, 231);
            this.buttonUpdateUser.Name = "buttonUpdateUser";
            this.buttonUpdateUser.Size = new System.Drawing.Size(163, 23);
            this.buttonUpdateUser.TabIndex = 6;
            this.buttonUpdateUser.Text = "Обновить пользователя";
            this.buttonUpdateUser.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteUser
            // 
            this.buttonDeleteUser.Location = new System.Drawing.Point(194, 256);
            this.buttonDeleteUser.Name = "buttonDeleteUser";
            this.buttonDeleteUser.Size = new System.Drawing.Size(163, 23);
            this.buttonDeleteUser.TabIndex = 7;
            this.buttonDeleteUser.Text = "Удалить пользователя";
            this.buttonDeleteUser.UseVisualStyleBackColor = true;
            // 
            // AdminForm
            // 
            this.ClientSize = new System.Drawing.Size(384, 319);
            this.Controls.Add(this.buttonDeleteUser);
            this.Controls.Add(this.buttonUpdateUser);
            this.Controls.Add(this.buttonAddUser);
            this.Controls.Add(this.textBoxFullName);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.checkBoxIsAdmin);
            this.Controls.Add(this.dataGridViewUsers);
            this.Name = "AdminForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}