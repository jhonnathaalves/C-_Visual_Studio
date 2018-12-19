using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaAgendaContatos
{
    class ManipuladorArquivos
    {
        private static String EnderecoArquivo = AppDomain.CurrentDomain.BaseDirectory + "contatos.txt";

        public static List<Contatos> LerArquivo()
        {
            List<Contatos> contatosList = new List<Contatos>();
            if (File.Exists(EnderecoArquivo))
            {
                using (StreamReader sr = File.OpenText(EnderecoArquivo))
                {
                    while (sr.Peek() >= 0)
                    {
                        string linha = sr.ReadLine();
                        string[] linhaComSplit = linha.Split(';');
                        if (linhaComSplit.Count() == 3)
                        {
                            Contatos contatos = new Contatos();
                            contatos.Nome = linhaComSplit[0];
                            contatos.Email = linhaComSplit[1];
                            contatos.NumeroTelefone = linhaComSplit[2];
                            contatosList.Add(contatos);
                        }

                    }

                }
            }
            return contatosList;
        }


        public static void EscreverArquivos(List<Contatos> contatosList)
        {
            using (StreamWriter sw = new StreamWriter(EnderecoArquivo, false))
            {
                foreach (Contatos contato in contatosList)
                {
                    String Line = String.Format("{0};{1};{2}", contato.Nome, contato.Email, contato.NumeroTelefone);
                    sw.WriteLine(Line);
                }
                sw.Flush();
            }
        }
    }


   
}

