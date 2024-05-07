using Agenda.Models;
using System;
using System.Windows.Forms;

namespace Agenda
{
    public partial class MainForm : Form
    {
        private ContactDiary diary = new ContactDiary();
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, System.EventArgs e)
        {
            string name = txtName.Text;
            string phone = txtPhone.Text;

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(phone)) 
            {
                diary.store(new Contact { name = name, phone = phone });
                updateListContact();
            }
            else
            {
                MessageBox.Show("Por favor, preencha o nome e o telefone");
            }
        }

        private void updateListContact()
        {
            lstContacts.Items.Clear();
            foreach (Contact contact in diary.index())
            {
                lstContacts.Items.Add(contact.name);
            }
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (lstContacts.SelectedItem != null)
            {
                string name = lstContacts.SelectedItem.ToString();
                diary.destroy(name);
                updateListContact();
            }
        }

        private void btnView_Click(object sender, System.EventArgs e)
        {
            updateListContact();
        }

        private void btnExport_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Arquivo CSV (*.csv)|*.csv";
            saveFileDialog.Title = "Exportar Contatos CSV";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    diary.ExportToCSV(saveFileDialog.FileName);
                    MessageBox.Show("Exportado com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro ao exportar. Detalhe: {ex.Message}");
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm:ss");
        }
    }
}
