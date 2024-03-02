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
        public static List<Topicos> SubTopicosNomes = new List<Topicos>();
        public static string PegarTopico = string.Empty;
        public static int PegarIdEditar = -1;
        int idDoTopicoAtual = -1;
        public FormMain()
        {
            InitializeComponent();
            MetodosSQLTopicos.PegarTodasAsTabelas(ref dgvMain, ref txtBusca);

        }

        private void btnTopico_Click(object sender, EventArgs e)
        {
            if (btnTopico.Text == Constantes.CriarSubTopico)
            {
                if (idDoTopicoAtual == -1)
                {
                    MessageBox.Show("Algo deu errado no subtopico botao tabela");
                }
                MetodosSQLSubTopico.CriarSubTabela(ref txtPegarTopico,idDoTopicoAtual);

                MetodosSQLSubTopico.PegarTabelasSubTopico(ref dgvMain, ref txtBusca, idDoTopicoAtual);
                MessageBox.Show("subTopico criado com sucesso");
                
                return;
            }
            if (btnTopico.Text == Constantes.EditarTopico)
            {
               
                try
                {
                    var a = MessageBox.Show($"Tem certeza que deseja mudar o nome de {TopicosNomes[PegarIdEditar].Topico} para {"Tb" + MetodosSQLTopicos.PegarIdString(TopicosNomes[PegarIdEditar].Topico) + "_" + txtPegarTopico.Text}", "Editar certeza?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (a == DialogResult.No)
                {
                    PegarIdEditar = -1;
                    AlteraLabelAndBotao(Constantes.Digite_o_NomeDoTopico, Constantes.CriarTopico);
                    txtPegarTopico.Text = "";
                    
                    return;
                }
                    
                MetodosSQLTopicos.EditarNomeTopico(PegarIdEditar, ref txtPegarTopico);
                PegarIdEditar = -1;
                AlteraLabelAndBotao(Constantes.Digite_o_NomeDoTopico, Constantes.CriarTopico);
                MetodosSQLTopicos.PegarTodasAsTabelas(ref dgvMain, ref txtBusca);
                
                return;
                }
                catch
                {
                    MessageBox.Show("Erro ao editar possivelmente tabela vazia");
                }

            }
            PegarTopico = txtPegarTopico.Text;
            MetodosSQLTopicos.CriarDBMaisTabela();
            MetodosSQLTopicos.PegarTodasAsTabelas(ref dgvMain, ref txtBusca);

            PegarTopico = string.Empty;
            txtPegarTopico.Text = string.Empty;


        }


        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblEditar.Text = "";
            if (e.ColumnIndex == 0)
            {

                if (MessageBox.Show($"Tem certeza que quer deletar {TopicosNomes[e.RowIndex].Topico}"
                    , "Tem Certeza?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    MetodosSQLTopicos.DeletarTopico(e.RowIndex);
                    AlteraLabelAndBotao(Constantes.Digite_o_NomeDoTopico, Constantes.CriarTopico);
                    MetodosSQLTopicos.PegarTodasAsTabelas(ref dgvMain, ref txtBusca);

                }


            }
            if (e.ColumnIndex == 1)
            {
               
                if (lbeNome.Text == Constantes.Digite_o_NomeDoTopico || lbeNome.Text == Constantes.Digite_o_NovoNomeDoTopico)
                {
                    AlteraLabelAndBotao(Constantes.Digite_o_NovoNomeDoTopico, Constantes.EditarTopico);
                    PegarIdEditar = e.RowIndex;
                    lblEditar.Text = TopicosNomes[PegarIdEditar].Topico;
                }
            }
            if (e.ColumnIndex == 2)
            {
                if (lbeNome.Text == Constantes.Digite_o_Nome_DoSubTopico)
                {
                    MessageBox.Show("Entrou em subtopico");
                }
            }
            if (e.ColumnIndex == 3)
            {
                AlteraLabelAndBotao(Constantes.Digite_o_NomeDoTopico, Constantes.CriarTopico);
                MetodosSQLTopicos.PegarTodasAsTabelas(ref dgvMain, ref txtBusca);
                idDoTopicoAtual = -1;
                
            }
            if (e.ColumnIndex == 4)
            {
                if(btnTopico.Text == Constantes.CriarSubTopico)
                {
                    MessageBox.Show("Voce ja esta em SubTopico!!!","SubTopico",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                AlteraLabelAndBotao(Constantes.Digite_o_Nome_DoSubTopico, Constantes.CriarSubTopico);
                idDoTopicoAtual = MetodosSQLTopicos.PegarIdString(TopicosNomes[e.RowIndex].Topico);
                if(idDoTopicoAtual == -1)
                {
                    MessageBox.Show("Algo deu errado no subtopico botao tabela");
                }
                
                MetodosSQLSubTopico.PegarTabelasSubTopico(ref dgvMain, ref txtBusca,idDoTopicoAtual);

            }
            
        }

        private void AlteraLabelAndBotao(string lbe, string btn, string lbeE = "")
        {
            lbeNome.Text = lbe;
            btnTopico.Text = btn;
            lblEditar.Text = lbeE;
        }

        private void btnExibir_Click(object sender, EventArgs e)
        {

            MetodosSQLTopicos.PegarTodasAsTabelas(ref dgvMain, ref txtBusca);

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPegarTopico.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            MetodosSQLTopicos.PegarTodasAsTabelas(ref dgvMain, ref txtBusca);
        }

        private void btnClear2_Click(object sender, EventArgs e)
        {
            txtBusca.Text = "";
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }




        ////////////////
    }
}
