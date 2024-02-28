namespace Pergunta_E_Resposta
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvMain = new DataGridView();
            btnTopico = new Button();
            btnExibir = new Button();
            txtPegarTopico = new TextBox();
            lbeNome = new Label();
            Deletar = new DataGridViewButtonColumn();
            Editar = new DataGridViewButtonColumn();
            Entrar = new DataGridViewButtonColumn();
            Topico = new DataGridViewButtonColumn();
            Sub = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)dgvMain).BeginInit();
            SuspendLayout();
            // 
            // dgvMain
            // 
            dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvMain.Columns.AddRange(new DataGridViewColumn[] { Deletar, Editar, Entrar, Topico, Sub });
            dgvMain.Location = new Point(219, 47);
            dgvMain.Name = "dgvMain";
            dgvMain.RowTemplate.Height = 25;
            dgvMain.Size = new Size(835, 544);
            dgvMain.TabIndex = 0;
            dgvMain.CellContentClick += dgvMain_CellContentClick;
            // 
            // btnTopico
            // 
            btnTopico.Location = new Point(55, 95);
            btnTopico.Name = "btnTopico";
            btnTopico.Size = new Size(100, 23);
            btnTopico.TabIndex = 1;
            btnTopico.Text = "Criar Topico";
            btnTopico.UseVisualStyleBackColor = true;
            btnTopico.Click += btnTopico_Click;
            // 
            // btnExibir
            // 
            btnExibir.Location = new Point(66, 162);
            btnExibir.Name = "btnExibir";
            btnExibir.Size = new Size(75, 23);
            btnExibir.TabIndex = 2;
            btnExibir.Text = "Exibir";
            btnExibir.UseVisualStyleBackColor = true;
            btnExibir.Click += btnExibir_Click;
            // 
            // txtPegarTopico
            // 
            txtPegarTopico.Location = new Point(26, 57);
            txtPegarTopico.Name = "txtPegarTopico";
            txtPegarTopico.Size = new Size(169, 23);
            txtPegarTopico.TabIndex = 3;
            txtPegarTopico.TextChanged += txtPegarTopico_TextChanged;
            // 
            // lbeNome
            // 
            lbeNome.AutoSize = true;
            lbeNome.Location = new Point(45, 29);
            lbeNome.Name = "lbeNome";
            lbeNome.Size = new Size(136, 15);
            lbeNome.TabIndex = 4;
            lbeNome.Text = "Digite o nome do topico";
            lbeNome.Click += label1_Click;
            // 
            // Deletar
            // 
            Deletar.HeaderText = "Deletar";
            Deletar.Name = "Deletar";
            Deletar.Text = "D";
            Deletar.UseColumnTextForButtonValue = true;
            Deletar.Width = 50;
            // 
            // Editar
            // 
            Editar.HeaderText = "Editar";
            Editar.Name = "Editar";
            Editar.Text = "ED";
            Editar.UseColumnTextForButtonValue = true;
            Editar.Width = 43;
            // 
            // Entrar
            // 
            Entrar.HeaderText = "Entrar";
            Entrar.Name = "Entrar";
            Entrar.Text = "E";
            Entrar.UseColumnTextForButtonValue = true;
            Entrar.Width = 44;
            // 
            // Topico
            // 
            Topico.HeaderText = "Topico";
            Topico.Name = "Topico";
            Topico.Text = "T";
            Topico.UseColumnTextForButtonValue = true;
            Topico.Width = 48;
            // 
            // Sub
            // 
            Sub.HeaderText = "SubTopico";
            Sub.Name = "Sub";
            Sub.Text = "S";
            Sub.UseColumnTextForButtonValue = true;
            Sub.Width = 68;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1141, 648);
            Controls.Add(lbeNome);
            Controls.Add(txtPegarTopico);
            Controls.Add(btnExibir);
            Controls.Add(btnTopico);
            Controls.Add(dgvMain);
            Name = "FormMain";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvMain).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvMain;
        private Button btnTopico;
        private Button btnExibir;
        private TextBox txtPegarTopico;
        private Label lbeNome;
        private DataGridViewButtonColumn Deletar;
        private DataGridViewButtonColumn Editar;
        private DataGridViewButtonColumn Entrar;
        private DataGridViewButtonColumn Topico;
        private DataGridViewButtonColumn Sub;
    }
}
