using System;
using System.Windows.Forms;
using System.IO;

namespace BelieveOrNotBelieve
{
    public partial class Form1 : Form
    {
        // База данных с вопросами
        // Савенко В.
        // Задание из методички
        // Связываем свойство value с textbox  
        // Защита от не созданной базы данных
        // Защита от слишком большой базы -  5000 байт
        // косметические улучшение
        // Добавил пункт о программе
        TrueFalse database;
        public Form1()
        {
            InitializeComponent();
        }
        // Обработчик пункта меню Exit
        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Обработчик пункта меню
        private void miNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                database = new TrueFalse(sfd.FileName);
                database.Add("123", true);
                database.Save();
                nudNumber.Minimum = 1;
                nudNumber.Maximum = 1;
                nudNumber.Value = 1;
            };
        }
       
        private void nudNumber_ValueChanged(object sender, EventArgs e)
        {
            // связываем свойство Value с  текст боксом
            // делаем защиту от пустой базы
                if (database != null)
                {
                    tboxQuestion.Text = nudNumber.Value.ToString() + ": " + database[(int)nudNumber.Value - 1].text;
                    cboxTrue.Checked = database[(int)nudNumber.Value - 1].trueFalse;
                }
                else
                {
                    MessageBox.Show("База данных не загружена!");
                }
        }
        // Обработчик кнопки Добавить
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (database == null)
            {
                MessageBox.Show("Создайте новую базу данных", "Сообщение");
                return;
            }
            database.Add((database.Count + 1).ToString(), true);
            nudNumber.Maximum = database.Count;
            nudNumber.Value = database.Count;
        }
        // Обработчик кнопки Удалить
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (database == null)
            {
                MessageBox.Show("База данных не загружена!");
                    return;
            }
            else
            {
                if (nudNumber.Maximum == 1)
                {
                    MessageBox.Show("Нельзя удалить единственную запись в базе!");
                    return;
                } 
            }
            database.Remove((int)nudNumber.Value);
            nudNumber.Maximum--;
            if (nudNumber.Value > 1) nudNumber.Value = nudNumber.Value;
        }
        // Обработчик пункта меню Save
        private void miSave_Click(object sender, EventArgs e)
        {
            if (database != null)
            {
                database.Save();
                MessageBox.Show("База успешно сохранена!");    
            }
            else MessageBox.Show("База данных не создана");
        }
        // Обработчик пункта меню Open
        private void miOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileInfo fl = new FileInfo(ofd.FileName);
                if (fl.Length <5000)
                {
                    database = new TrueFalse(ofd.FileName);
                    database.Load();
                    nudNumber.Minimum = 1;
                    nudNumber.Maximum = database.Count;
                    nudNumber.Value = 1;
                }
                else
                    MessageBox.Show("Файл слишком большой!\nМаксимальный размер файла 5000 байт...");

            }
        }
        // Обработчик кнопки Сохранить (вопрос)
        private void btnSaveQuest_Click(object sender, EventArgs e)
        {
            if (database != null)
            {
                database[(int)nudNumber.Value - 1].text = tboxQuestion.Text.Substring(2,tboxQuestion.Text.Length-3);
                database[(int)nudNumber.Value - 1].trueFalse = cboxTrue.Checked;
               
            }
            else MessageBox.Show("База данных не загружена!");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (database != null)
            {
                if (MessageBox.Show("Хотите сохранить последние изменения?!","Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    database[(int)nudNumber.Value - 1].text = tboxQuestion.Text;
                    database[(int)nudNumber.Value - 1].trueFalse = cboxTrue.Checked;
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Разработчик программы: Geekbrains.ru\nСо разработчик программы: Савенко В.\nВерсия программы: 1.2\nТекущее время: "+DateTime.Now.ToString(),"О программе:");
        }
    }
}
