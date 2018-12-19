using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovaAgendaContatos
{
    public partial class FrmAgendaContatos : Form
    {
        private OperacaoEnum acao;

        public FrmAgendaContatos()
        {
            InitializeComponent();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            AlterarBotoesIncluirAlterarExcluir(false);
            AlterarBotoesSalvarECancelar(true);
            AlterarEstadoCampos(true);
            acao = OperacaoEnum.INCLUIR;
        }

        private void FrmAgendaContatos_Shown(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesIncluirAlterarExcluir(true);
            CarregarListaContatos();
            AlterarEstadoCampos(false);
        }

        private void AlterarBotoesSalvarECancelar(bool estado)
        {
            btnSalvar.Enabled = estado;
            BtnCancelar.Enabled = estado;
        }

        private void AlterarBotoesIncluirAlterarExcluir(bool estado)
        {
            btnIncluir.Enabled = estado;
            btnAlterar.Enabled = estado;
            BtnExcluir.Enabled = estado;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            AlterarBotoesIncluirAlterarExcluir(false);
            AlterarBotoesSalvarECancelar(true);
            AlterarEstadoCampos(true);
            acao = OperacaoEnum.ALTERAR;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Contatos contato = new Contatos()
            {
                Nome = txbNome.Text,
                Email = txbEmail.Text,
                NumeroTelefone = txbTelefone.Text
            };

            List<Contatos> contatosList = new List<Contatos>();
            foreach (Contatos contatoDaLista in LbxContatos.Items)
            {
                contatosList.Add(contatoDaLista);
            }

            if (acao == OperacaoEnum.INCLUIR)
            { 
                contatosList.Add(contato);
            }

            else
            {
                int indice = LbxContatos.SelectedIndex;
                contatosList.RemoveAt(indice);
                contatosList.Insert(indice, contato);
                
            }
                        
                           
            ManipuladorArquivos.EscreverArquivos(contatosList);
            CarregarListaContatos();
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesIncluirAlterarExcluir(true);
            LimparCampos();
            AlterarEstadoCampos(false);

        }

        private void CarregarListaContatos ()
        {
            LbxContatos.Items.Clear();
            LbxContatos.Items.AddRange(ManipuladorArquivos.LerArquivo().ToArray());
            LbxContatos.SelectedIndex = 0;
        }

        private void LimparCampos ()
        {
            txbNome.Text = "";
            txbEmail.Text = "";
            txbTelefone.Text = "";

        }

        private void AlterarEstadoCampos (bool estado)
        {
            txbNome.Enabled = estado;
            txbEmail.Enabled = estado;
            txbTelefone.Enabled = estado;
         }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            AlterarBotoesIncluirAlterarExcluir(true);
            AlterarBotoesSalvarECancelar(false);
            AlterarEstadoCampos(false);
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Tem Certeza?", "Pergunta", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int indiceExcluido = LbxContatos.SelectedIndex;
                LbxContatos.SelectedIndex = 0;
                LbxContatos.Items.RemoveAt(indiceExcluido);
                List<Contatos> contatosList = new List<Contatos>();
                foreach (Contatos contato in LbxContatos.Items)
                {
                    contatosList.Add(contato);
                }
                ManipuladorArquivos.EscreverArquivos(contatosList);
                CarregarListaContatos();
                LimparCampos();
            }
        }

        private void LbxContatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Contatos contato = (Contatos) LbxContatos.Items[LbxContatos.SelectedIndex];
            txbNome.Text = contato.Nome;
            txbEmail.Text = contato.Email;
            txbTelefone.Text = contato.NumeroTelefone;
                   
        }
    }
}
