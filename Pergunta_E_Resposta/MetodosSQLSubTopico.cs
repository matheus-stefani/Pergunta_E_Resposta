using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pergunta_E_Resposta
{
    internal class MetodosSQLSubTopico
    {
       public static void PegarTabelasSubTopico(ref DataGridView oda, ref TextBox txtBusca, int idDoTopicoAtual)
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={FormMain.caminho_para_DB}"))
            {
                conn.Open();
                StringBuilder query = new StringBuilder();
                query.Append($"SELECT name FROM sqlite_master WHERE  name like \"%%\" AND name like \"%SUB_%\" AND name like \"%Tb{idDoTopicoAtual}%\"");
                SqliteCommand cmd = new SqliteCommand( query.ToString(), conn );

                var a=cmd.ExecuteReader();
                List<Topicos> list = new List<Topicos>();
                while (a.Read())
                {
                    string nome = a["name"] as string;
                    list.Add(new Topicos(nome));
                }
                oda.DataSource = list;
            }

            

        }
        public static int PegarIdString(string stringPegarId)
        {

            string b = "";
            for (int i = 3; i < stringPegarId.Length; i++)
            {
                if (stringPegarId[i] == '_') break;

                b += stringPegarId[i];
            }
            return Convert.ToInt32(b);

        }

        public static int PegarIdDoUltimo(int idDoTopicoAtual)
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={FormMain.caminho_para_DB}"))
            {
                conn.Open();
                StringBuilder query = new StringBuilder();
                query.Append($"SELECT name FROM sqlite_master WHERE  name like \"%%\" AND name like \"%SUB_%\" AND name like \"%Tb{idDoTopicoAtual}%\"");
                SqliteCommand cmd = new SqliteCommand(query.ToString(), conn);

                var a = cmd.ExecuteReader();
                string pegarUltimo = "";
                while (a.Read())
                {
                    pegarUltimo = a["name"] as string;
                }
                if (pegarUltimo == "") return 0;

                return PegarIdString(pegarUltimo) + 1;


            }
        }

        public static void CriarSubTabela(ref TextBox txtPegarTopico, int idTopicoAtual)
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={FormMain.caminho_para_DB}"))
            {
                conn.Open();
                StringBuilder query = new StringBuilder();
                query.Append($"CREATE TABLE IF NOT EXISTS SUB{PegarIdDoUltimo(idTopicoAtual)}_Tb{idTopicoAtual}_{txtPegarTopico.Text.Replace(" ","_")}(");
                query.Append("ID INTEGER PRIMARY KEY,");
                query.Append("PERGUNTA VARCHAR(255) NOT NULL,");
                query.Append("RESPOSTA VARCHAR(255) NOT NULL);");
                SqliteCommand cmd =  new SqliteCommand( query.ToString(),conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
