using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab25
{
    public partial class Form1 : Form
    {
        DBController dBController = new DBController();

        // Метод оновлення даних у DataGridView. Використовує метод getAllInto з DBController.
        public void updateData()
        {
            dBController.getAllInto(dBController.GetConnection(), SportDataGridView);
        }

        // Конструктор форми. При виклику ініціалізує компоненти, а також оновлює DataGridView.
        public Form1()
        {
            InitializeComponent();
            updateData();
        }

        // Обробник кнопки add. Створює новий об'єкт Sport з даних які ввів користувач та передає його у метод add класу DBController.
        private void addButton_Click(object sender, EventArgs e)
        {
            Sport sport = new Sport(
                nameTextBox.Text, 
                descriptionTichTextBox.Text, 
                isOlympicCheckBox.Checked);
            dBController.add(dBController.GetConnection(), sport);
            updateData();
        }

        // Обробник кнопки update. Перевіряє чи обраний рядок не порожній. Якщо рядок не порожній, отримує айді та передає його разом з
        // об'єктом Sport (з даних які ввів користувач) у метод update класу DBController.
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (SportDataGridView.CurrentRow != null)
            {
                int id = Convert.ToInt32(SportDataGridView.CurrentRow.Cells["id"].Value);

                Sport sport = new Sport(
                    nameTextBox.Text,
                    descriptionTichTextBox.Text,
                    isOlympicCheckBox.Checked
                );

                dBController.update(dBController.GetConnection(), sport, id);

                updateData();
            }
        }

        // Обробник кнопки delete. Перевіряє чи обраний рядок не пустий. Якщо рядок не пустий, отримує айді та передає у метод delete
        // класу DBController.
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (SportDataGridView.CurrentRow != null)
            {
                int id = Convert.ToInt32(SportDataGridView.CurrentRow.Cells["id"].Value);

                dBController.delete(dBController.GetConnection(), id);

                updateData();
            }
        }
    }
}
