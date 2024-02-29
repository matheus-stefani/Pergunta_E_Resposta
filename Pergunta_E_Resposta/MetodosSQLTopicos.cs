using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pergunta_E_Resposta
{
    internal class MetodosSQLTopicos
    {
        public static void DeletarTopico(int id)
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={FormMain.caminho_para_DB}"))
            {
                conn.Open();
                StringBuilder query = new StringBuilder();
                query.Append($"DROP TABLE IF EXISTS {FormMain.TopicosNomes[id].Topico}");
                SqliteCommand cmd = new SqliteCommand(query.ToString(), conn);

                cmd.ExecuteNonQuery();

            }
        }
        public static void CriarDBMaisTabela()
        {

            using (SqliteConnection conn = new SqliteConnection($"Filename={FormMain.caminho_para_DB}"))
            {
                conn.Open();
                FormMain.PegarTopico = FormMain.PegarTopico.Replace(" ", "_");
                string nomeTabela = "Tb_" + FormMain.PegarTopico;


                StringBuilder query = new StringBuilder();

                query.Append($"CREATE TABLE IF NOT EXISTS {nomeTabela} (");
                query.Append("ID INTEGER PRIMARY KEY);");
                //query.Append("PERGUNTA VARCHAR(255) NOT NULL,");
                //query.Append("RESPOSTA VARCHAR(255) NOT NULL);");
                SqliteCommand cmd = new SqliteCommand(query.ToString(), conn);
                cmd.ExecuteNonQuery();
            }
        }
        
        public static void PegarTodasAsTabelas(ref DataGridView oda)
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={FormMain.caminho_para_DB}"))
            {
                conn.Open();
                StringBuilder query = new StringBuilder();
                query.Append("SELECT name FROM sqlite_master");
                SqliteCommand cmd = new SqliteCommand(query.ToString(), conn);
                var a = cmd.ExecuteReader();
                FormMain.TopicosNomes = new List<Topicos>();
                while (a.Read())
                {
                    string topico = a["name"] as string;
                    FormMain.TopicosNomes.Add(new Topicos(topico));
                }

                oda.DataSource = FormMain.TopicosNomes;

            }

        }
        public static void EditarNomeTopico(int id,ref TextBox txtPegarTopico)
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={FormMain.caminho_para_DB}"))
            {
                conn.Open();
                StringBuilder query = new StringBuilder();
                FormMain.PegarTopico = "Tb_" + txtPegarTopico.Text.Replace(" ", "_");
                query.Append($"ALTER TABLE {FormMain.TopicosNomes[id].Topico}  RENAME TO {FormMain.PegarTopico};");
                SqliteCommand cmd = new SqliteCommand(query.ToString(), conn);
                cmd.ExecuteNonQuery();
            }
        }
        ///////////////////
    }
}
