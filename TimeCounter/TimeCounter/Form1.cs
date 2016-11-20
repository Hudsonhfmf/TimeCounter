using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeCounter
{
    public partial class Form1 : Form
    {
        private int seg = 0;
        private int min = 0;
        private int segAnt = 0;
        private int minAnt = 0;
        // private int task = 0;
        private Participante objParticipante;
        private DataTable Tabela;
        private IList<Participante> listaParticipantes = new List<Participante>();


        public Form1()
        {
            InitializeComponent();
            //  dataGridView1.AutoGenerateColumns = false;
            Tabela = new DataTable();
            Tabela.Columns.Add("Idade", typeof(string));
            Tabela.Columns.Add("Sexo", typeof(string));
            Tabela.Columns.Add("Profissao", typeof(string));
            Tabela.Columns.Add("Formacao", typeof(string));
            Tabela.Columns.Add("TempoGasto", typeof(string));
            Tabela.Columns.Add("QtdErros", typeof(string));
            Tabela.Columns.Add("Tarefa", typeof(string));

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seg++;
            if (seg == 60)
            {
                min++;
                seg = 0;
            }
            lblContador.Text = min.ToString().PadLeft(2, '0') + ":" + seg.ToString().PadLeft(2, '0');

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    StartCounter();
                    break;
                case Keys.F2:
                    AddCheckPoint();
                    break;
                case Keys.F3:
                    StopCounterTask();
                    break;
                case Keys.F4:
                    objParticipante.QtdErros++;
                    break;
            }
        }


        private void StartCounter()
        {
            if (timer1.Enabled == false)
            {
                timer1.Enabled = true;
                lblContador.Text = "00:00";
                seg = segAnt = 0;
                min = minAnt = 0;
            }
        }
        private void AddCheckPoint()
        {
            objParticipante.listaTempo.Add(new SaveTime() { Tarefa = txtTarefa.Text, Pausas = ((min - minAnt).ToString().PadLeft(2, '0') + ":" + (seg - segAnt).ToString().PadLeft(2, '0')) });
            segAnt = seg;
            minAnt = min;
            
        }

        private void StopCounterTask()
        {
            timer1.Enabled = false;
            AddCheckPoint();
            objParticipante.TempoGasto = lblContador.Text;
            CarregarDataGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            objParticipante = new Participante() { Formacao = txtFormacao.Text, Idade = txtIdade.Text, Profissao = txtProfissao.Text, Sexo = txtSexo.Text, listaTempo = new List<SaveTime>(), Tarefa = txtTarefa.Text };
            listaParticipantes.Add(objParticipante);
            lblContador.Text = "00:00";
            CarregarDataGridView();
        }

        private void CarregarDataGridView()
        {
            Tabela?.Rows.Clear();

            foreach (var listaParticipante in listaParticipantes)
            {
                DataRow row = Tabela.NewRow();
                row["Sexo"] = listaParticipante.Sexo;
                row["Idade"] = listaParticipante.Idade;
                row["Profissao"] = listaParticipante.Profissao;
                row["Formacao"] = listaParticipante.Formacao;
                row["TempoGasto"] = listaParticipante.TempoGasto;
                row["QtdErros"] = listaParticipante.QtdErros;
                row["Tarefa"] = listaParticipante.Tarefa;
                Tabela.Rows.Add(row);
            }
            dataGridView1.DataSource = Tabela;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var Formacao = dataGridView1.CurrentRow.Cells["Formacao"].Value;
            var Idade = dataGridView1.CurrentRow.Cells["Idade"].Value;
            var profissao = dataGridView1.CurrentRow.Cells["Profissao"].Value;
            var tarefa = dataGridView1.CurrentRow.Cells["Tarefa"].Value;
            int qtdErros = Convert.ToInt32(dataGridView1.CurrentRow.Cells["QtdErros"].Value);
            dataGridView2.DataSource = listaParticipantes.FirstOrDefault(
                    x =>
                        x.Formacao == Formacao && x.Idade == Idade && x.Profissao == profissao && x.Tarefa == tarefa &&
                        x.QtdErros == qtdErros).listaTempo;
        }
    }
}
