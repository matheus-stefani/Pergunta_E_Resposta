using Microsoft.Data.Sqlite;
using System.Text;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;
using System.Xml.Linq;
using System.DirectoryServices.ActiveDirectory;
using System.ComponentModel;

namespace Pergunta_E_Resposta
{
    public partial class FormMain : Form
    {
        public static string caminho_para_DB = Path.Combine(Application.LocalUserAppDataPath, "Topicos.db");
        public static List<Topicos> TopicosNomes = new List<Topicos>();
        public static string PegarTopico = string.Empty;
        public static int PegarIdEditar = -1;
        public FormMain()
        {
            InitializeComponent();
            PegarTodasAsTabelas();
        }

        private void btnTopico_Click(object sender, EventArgs e)
        {
            if(btnTopico.Text == "Criar subTopico")
            {
                MessageBox.Show("subTopico criado com sucesso");
                return;
            }
            if(btnTopico.Text == "Editar Topico")
            {
                var a = MessageBox.Show($"Tem certeza que deseja mudar o nome de {TopicosNomes[PegarIdEditar].Topico} para {txtPegarTopico.Text}", "Editar certeza?",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                if (a == DialogResult.No)
                {
                    PegarIdEditar = -1;
                    lbeNome.Text = "Digite o nome do topico";
                    btnTopico.Text = "Criar Topico";
                    txtPegarTopico.Text = "";
                    return;
                }
                EditarNomeTopico(PegarIdEditar);
                PegarIdEditar = -1;
                lbeNome.Text = "Digite o nome do topico";
                btnTopico.Text = "Criar Topico";
                return;

            }
            PegarTopico = txtPegarTopico.Text;
            CriarDBMaisTabela();
            PegarTodasAsTabelas();
            PegarTopico = string.Empty;
            txtPegarTopico.Text = string.Empty;


        }
        public static void CriarDBMaisTabela()
        {

            using (SqliteConnection conn = new SqliteConnection($"Filename={caminho_para_DB}"))
            {
                conn.Open();
                PegarTopico = PegarTopico.Replace(" ", "_");
                string nomeTabela = "Tb_" + PegarTopico;


                StringBuilder query = new StringBuilder();

                query.Append($"CREATE TABLE IF NOT EXISTS {nomeTabela} (");
                query.Append("ID INTEGER PRIMARY KEY);");
                //query.Append("PERGUNTA VARCHAR(255) NOT NULL,");
                //query.Append("RESPOSTA VARCHAR(255) NOT NULL);");
                SqliteCommand cmd = new SqliteCommand(query.ToString(), conn);
                cmd.ExecuteNonQuery();
            }
        }
        public void PegarTodasAsTabelas()
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={caminho_para_DB}"))
            {
                conn.Open();
                StringBuilder query = new StringBuilder();
                query.Append("SELECT name FROM sqlite_master");
                SqliteCommand cmd = new SqliteCommand(query.ToString(), conn);
                var a = cmd.ExecuteReader();
                TopicosNomes = new List<Topicos>();
                while (a.Read())
                {
                    string topico = a["name"] as string;
                    TopicosNomes.Add(new Topicos(topico));

                }

                dgvMain.DataSource = TopicosNomes;
            }
        }

        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                if (MessageBox.Show($"Tem certeza que quer deletar {TopicosNomes[e.RowIndex].Topico}"
                    , "Tem Certeza?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    DeletarTopico(e.RowIndex);
                }

            }
            if(e.ColumnIndex == 1)
            {
                if(lbeNome.Text == "Digite o nome do topico")
                {
                    lbeNome.Text = "Digite o novo nome do Topico";
                    btnTopico.Text = "Editar Topico";
                    PegarIdEditar = e.RowIndex;
                }
            }
            if(e.ColumnIndex == 2)
            {
                if (lbeNome.Text == "Digite o nome do subTopico") 
                {
                    MessageBox.Show("Entrou em subtopico");
                }
            }
            if(e.ColumnIndex == 3) {
                lbeNome.Text = "Digite o nome do Topico";
                btnTopico.Text = "Criar Topico";
            }
            if(e.ColumnIndex == 4)
            {
                lbeNome.Text = "Digite o nome do subTopico";
                btnTopico.Text = "Criar subTopico";
            }
        }

        private void EditarNomeTopico(int id)
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={caminho_para_DB}"))
            {
                conn.Open();
                StringBuilder query = new StringBuilder();
                PegarTopico = "Tb_"+txtPegarTopico.Text.Replace(" ","_");
                query.Append($"ALTER TABLE {TopicosNomes[id].Topico}  RENAME TO {PegarTopico};");
                SqliteCommand cmd = new SqliteCommand(query.ToString(),conn);
                cmd.ExecuteNonQuery();
            }
        }
        private void DeletarTopico(int id)
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={caminho_para_DB}"))
            {
                conn.Open();
                StringBuilder query = new StringBuilder();
                query.Append($"DROP TABLE IF EXISTS {TopicosNomes[id].Topico}");
                SqliteCommand cmd = new SqliteCommand(query.ToString(), conn);

                cmd.ExecuteNonQuery();
                PegarTodasAsTabelas();
            }
        }

        private void btnExibir_Click(object sender, EventArgs e)
        {
            PegarTodasAsTabelas();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPegarTopico_TextChanged(object sender, EventArgs e)
        {

        }


        ////////////////////////////////////////////////////////////////////////////////
    }
}
