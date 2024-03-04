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

        public static void PegarSubTopicosRelacionados(int id)
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={FormMain.caminho_para_DB}"))
            {
                conn.Open();
                StringBuilder query = new StringBuilder();
                query.Append($"SELECT name FROM sqlite_master WHERE name like \"%%\" AND name like \"%SUB%\" AND name like \"%Tb{PegarIdString(FormMain.TopicosNomes[id].Topico)}%\"");
                SqliteCommand cmd2 = new SqliteCommand(query.ToString(), conn);
                var a = cmd2.ExecuteReader();
                List<Topicos> pegarSubRelacionados = new List<Topicos>();
               
                while (a.Read())
                {
                    
                   string nome = a["name"] as string;
                    
                    pegarSubRelacionados.Add(new Topicos(nome));
                }
                FormMain.SubTopicosRelacionados = pegarSubRelacionados;
            }
        }

        public static void DeletarSubTopicosRelacionados(string subRelacionados)
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={FormMain.caminho_para_DB}"))
            {
                conn.Open();
               
                StringBuilder query = new StringBuilder();
                query.Append($"DROP TABLE IF EXISTS {subRelacionados}");
                SqliteCommand cmd = new SqliteCommand(query.ToString(), conn);
                cmd.ExecuteNonQuery();

            }
        }
            public static int PegarIdString(string stringPegarId)
        {
            
            string b = "";
            
            for(int i = 2; i < stringPegarId.Length; i++)
            {
                if (stringPegarId[i] == '_') break;
                
                b += stringPegarId[i];
            }
            return Convert.ToInt32(b);

        }

        

        public static int PegarOuCriarId()
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={FormMain.caminho_para_DB}"))
            {
                conn.Open();
                StringBuilder query = new StringBuilder();
                query.Append("SELECT name FROM sqlite_master WHERE name like \"Tb%\"");
                SqliteCommand cmd = new SqliteCommand(query.ToString(), conn);
                var a = cmd.ExecuteReader();
                
                int retorno = -1;
                string topico = "";
                while (a.Read())
                {
                    topico = a["name"] as string;
                    
                }

                if(topico == "") return 0;
                    
                return PegarIdString(topico);
            }
        }

        public static void CriarDBMaisTabela()
        {

            using (SqliteConnection conn = new SqliteConnection($"Filename={FormMain.caminho_para_DB}"))
            {
                conn.Open();
                FormMain.PegarTopico = FormMain.PegarTopico.Replace(" ", "_");
                string nomeTabela = $"Tb{PegarOuCriarId()+1}_" + FormMain.PegarTopico;


                StringBuilder query = new StringBuilder();

                query.Append($"CREATE TABLE IF NOT EXISTS {nomeTabela} (");
                query.Append("ID INTEGER PRIMARY KEY);");
                //query.Append("PERGUNTA VARCHAR(255) NOT NULL,");
                //query.Append("RESPOSTA VARCHAR(255) NOT NULL);");
                SqliteCommand cmd = new SqliteCommand(query.ToString(), conn);
                cmd.ExecuteNonQuery();
            }
        }
        
        public static void PegarTodasAsTabelas(ref DataGridView oda, ref TextBox txtBusca)
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={FormMain.caminho_para_DB}"))
            {
                conn.Open();
                StringBuilder query = new StringBuilder();
                query.Append($"SELECT name FROM sqlite_master WHERE  name like \"%{txtBusca.Text}%\" AND name like \"Tb%\" ");
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
                FormMain.PegarTopico = $"Tb{PegarIdString(FormMain.TopicosNomes[id].Topico)}_" + txtPegarTopico.Text.Replace(" ", "_");
                query.Append($"ALTER TABLE {FormMain.TopicosNomes[id].Topico}  RENAME TO {FormMain.PegarTopico};");
                SqliteCommand cmd = new SqliteCommand(query.ToString(), conn);
                cmd.ExecuteNonQuery();
            }
        }
        /*
        public static void BuscarNoBancoTopico(ref TextBox txtPegarTopico)
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={FormMain.caminho_para_DB}"))
            {
                conn.Open();
                StringBuilder query = new StringBuilder();
                query.Append($"SELECT name FROM Topicos.sqlite_master WHERE  name like \"%{txtPegarTopico.Text}%\" ");
                SqliteCommand cmd = new SqliteCommand(query.ToString(), conn);
                var a = cmd.ExecuteReader();
                while(a.Read())
                {

                }
            }
        }
        */
        ///////////////////
    }
}
