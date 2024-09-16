using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

class Program
{
    static void Main()
    {
        Form form = new Form();
        form.Text = "Criptografia RSA";
        form.Size = new Size(500, 350);

        Button addButton = new Button();
        addButton.Size = new Size(100, 50);
        addButton.Text = "Gerar Chave Pública";
        addButton.Location = new Point(60, 100);
        addButton.Click += Gerar_Chave_Publica;
        form.Controls.Add(addButton);

        Button encripitarButton = new Button();
        encripitarButton.Size = new Size(100, 50);
        encripitarButton.Text = "Encriptar";
        encripitarButton.Location = new Point(200, 100);
        encripitarButton.Click += Encriptar_Mensagem;
        form.Controls.Add(encripitarButton);

        Button desencripitarButton = new Button();
        desencripitarButton.Size = new Size(100, 50);
        desencripitarButton.Text = "Desencriptar";
        desencripitarButton.Location = new Point(350, 100);
        desencripitarButton.Click += Desencriptar;
        form.Controls.Add(desencripitarButton);

        Application.Run(form);
    }

    private static void Gerar_Chave_Publica(object sender, EventArgs e)
    {
        Form addForm = new Form();
        addForm.Text = "Gerar chave pública";
        addForm.Size = new Size(300, 150);

        // Caixas de texto para entrada de números
        Label numeroP = new Label();
        numeroP.Text = "P :";
        numeroP.Location = new Point(10, 10);
        numeroP.Size = new Size(20, 20);
        addForm.Controls.Add(numeroP);

        TextBox num1TextBox = new TextBox();
        num1TextBox.Location = new Point(40, 10);
        num1TextBox.Size = new Size(100, 20);
        addForm.Controls.Add(num1TextBox);

        Label numeroQ = new Label();
        numeroQ.Text = "Q :";
        numeroQ.Location = new Point(10, 40);
        numeroQ.Size = new Size(20, 20);
        addForm.Controls.Add(numeroQ);

        TextBox num2TextBox = new TextBox();
        num2TextBox.Location = new Point(40, 40);
        num2TextBox.Size = new Size(100, 20);
        addForm.Controls.Add(num2TextBox);

        Label numeroE = new Label();
        numeroE.Text = "E :";
        numeroE.Location = new Point(10, 70);
        numeroE.Size = new Size(20, 20);
        addForm.Controls.Add(numeroE);

        TextBox num3TextBox = new TextBox();
        num3TextBox.Location = new Point(40, 70);
        num3TextBox.Size = new Size(100, 20);
        addForm.Controls.Add(num3TextBox);

        // Botão de calcular
        Button calcularButton = new Button();
        calcularButton.Text = "Calcular";
        calcularButton.Location = new Point(150, 40);
        calcularButton.Click += (s, ev) =>
        {
            try
            {
                double num1 = Convert.ToDouble(num1TextBox.Text);
                double num2 = Convert.ToDouble(num2TextBox.Text);
                double num3 = Convert.ToDouble(num3TextBox.Text);

                string resultado = Gerar_Chave_Publica_Backend(num1, num2, num3);

                MessageBox.Show($"Resultado: {resultado}", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };
        addForm.Controls.Add(calcularButton);

        // Exibir o formulário para adição
        addForm.ShowDialog();
    }

    private static void Encriptar_Mensagem(object sender, EventArgs e)
    {
        Form addForm = new Form();
        addForm.Text = "Encriptar Mensagem";
        addForm.Size = new Size(300, 200);

        // Caixas de texto para entrada de números
        Label Label_mensagem = new Label();
        Label_mensagem.Text = "Digite a mensagem para encriptar:";
        Label_mensagem.Location = new Point(10, 10);
        Label_mensagem.Size = new Size(180, 20);
        addForm.Controls.Add(Label_mensagem);

        TextBox mensagem = new TextBox();
        mensagem.Location = new Point(10, 30);
        mensagem.Size = new Size(260, 20);
        addForm.Controls.Add(mensagem);

        // Rótulo e TextBox para P
        Label numeroP = new Label();
        numeroP.Text = "E :";
        numeroP.Location = new Point(10, 60);
        numeroP.Size = new Size(20, 20);
        addForm.Controls.Add(numeroP);

        TextBox num1TextBox = new TextBox();
        num1TextBox.Location = new Point(40, 60);
        num1TextBox.Size = new Size(100, 20);
        addForm.Controls.Add(num1TextBox);

        // Rótulo e TextBox para Q
        Label numeroQ = new Label();
        numeroQ.Text = "N :";
        numeroQ.Location = new Point(10, 90);
        numeroQ.Size = new Size(20, 20);
        addForm.Controls.Add(numeroQ);

        TextBox num2TextBox = new TextBox();
        num2TextBox.Location = new Point(40, 90);
        num2TextBox.Size = new Size(100, 20);
        addForm.Controls.Add(num2TextBox);
        // Botão de calcular
        Button calcularButton = new Button();
        calcularButton.Text = "Encriptar";
        calcularButton.Location = new Point(90, 120);
        calcularButton.Click += (s, ev) =>
        {
            try
            {
                double num1 = Convert.ToDouble(num1TextBox.Text);
                double num2 = Convert.ToDouble(num2TextBox.Text);
                string resultado = Encriptar_Mensagem_Backend(mensagem.Text, num1, num2);
                MessageBox.Show($"Resultado: {resultado}", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };
        addForm.Controls.Add(calcularButton);

        // Exibir o formulário para adição
        addForm.ShowDialog();
    }

    private static string Gerar_Chave_Publica_Backend(double num1, double num2, double num3)
    {
        string executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        string workingDirectory = System.IO.Path.GetDirectoryName(executablePath);
        using (Process processo = new Process())
        {
            processo.StartInfo.FileName = "gerar_chave.exe"; // Nome do executável do backend
            processo.StartInfo.RedirectStandardInput = true;
            processo.StartInfo.RedirectStandardOutput = true;
            processo.StartInfo.UseShellExecute = false;
            processo.StartInfo.CreateNoWindow = true;
            processo.StartInfo.WorkingDirectory = workingDirectory;

            processo.Start();

            // Envia os números para o backend
            processo.StandardInput.WriteLine($"{num1} {num2} {num3}");
            processo.StandardInput.Flush();
            processo.StandardInput.Close();

            // Lê o resultado do backend
            string resultadoStr = processo.StandardOutput.ReadToEnd();

            // Aguarda o término do backend
            processo.WaitForExit();


            return resultadoStr;

        }
    }

    private static string Encriptar_Mensagem_Backend(string mensagem, double num1, double num2)
    {
        string executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        string workingDirectory = System.IO.Path.GetDirectoryName(executablePath);
        using (Process processo = new Process())
        {
            processo.StartInfo.FileName = "encriptar.exe"; // Nome do executável do backend
            processo.StartInfo.RedirectStandardInput = true;
            processo.StartInfo.RedirectStandardOutput = true;
            processo.StartInfo.UseShellExecute = false;
            processo.StartInfo.CreateNoWindow = true;
            processo.StartInfo.WorkingDirectory = workingDirectory;

            processo.Start();

            // Envia os números para o backend
            processo.StandardInput.WriteLine($"{num1} {num2} {mensagem}");
            processo.StandardInput.Flush();
            processo.StandardInput.Close();

            // Lê o resultado do backend
            string resultadoStr = processo.StandardOutput.ReadToEnd();

            // Aguarda o término do backend
            processo.WaitForExit();


            return resultadoStr;

        }
    }

    private static void Desencriptar(object sender, EventArgs e)
    {
        Form addForm = new Form();
        addForm.Text = "Encriptar Mensagem";
        addForm.Size = new Size(300, 220);

        // Caixas de texto para entrada de números
        Label Label_mensagem = new Label();
        Label_mensagem.Text = "Digite a mensagem para desencriptar:";
        Label_mensagem.Location = new Point(10, 10);
        Label_mensagem.Size = new Size(200, 20);
        addForm.Controls.Add(Label_mensagem);

        TextBox mensagem = new TextBox();
        mensagem.Location = new Point(10, 30);
        mensagem.Size = new Size(260, 20);
        addForm.Controls.Add(mensagem);

        // Rótulo e TextBox para P
        Label numeroP = new Label();
        numeroP.Text = "P :";
        numeroP.Location = new Point(10, 60);
        numeroP.Size = new Size(20, 20);
        addForm.Controls.Add(numeroP);

        TextBox num1TextBox = new TextBox();
        num1TextBox.Location = new Point(40, 60);
        num1TextBox.Size = new Size(100, 20);
        addForm.Controls.Add(num1TextBox);

        // Rótulo e TextBox para Q
        Label numeroQ = new Label();
        numeroQ.Text = "Q :";
        numeroQ.Location = new Point(10, 90);
        numeroQ.Size = new Size(20, 20);
        addForm.Controls.Add(numeroQ);

        TextBox num2TextBox = new TextBox();
        num2TextBox.Location = new Point(40, 90);
        num2TextBox.Size = new Size(100, 20);
        addForm.Controls.Add(num2TextBox);

        // Rótulo e TextBox para E
        Label numeroE = new Label();
        numeroE.Text = "E :";
        numeroE.Location = new Point(10, 120);
        numeroE.Size = new Size(20, 20);
        addForm.Controls.Add(numeroE);

        TextBox num3TextBox = new TextBox();
        num3TextBox.Location = new Point(40, 120);
        num3TextBox.Size = new Size(100, 20);
        addForm.Controls.Add(num3TextBox);

        // Botão de calcular
        Button calcularButton = new Button();
        calcularButton.Text = "Desencriptar";
        calcularButton.Size = new Size(80, 20);
        calcularButton.Location = new Point(90, 150);
        calcularButton.Click += (s, ev) =>
        {
            try
            {
                double num1 = Convert.ToDouble(num1TextBox.Text);
                double num2 = Convert.ToDouble(num2TextBox.Text);
                double num3 = Convert.ToDouble(num3TextBox.Text);
                string resultado = Desencriptar_Mensagem_Backend(mensagem.Text, num1, num2, num3);
                MessageBox.Show($"Resultado: {resultado}", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };
        addForm.Controls.Add(calcularButton);

        // Exibir o formulário para adição
        addForm.ShowDialog();
    }

    private static string Desencriptar_Mensagem_Backend(string mensagem, double num1, double num2, double num3)
    {
        string executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        string workingDirectory = System.IO.Path.GetDirectoryName(executablePath);
        using (Process processo = new Process())
        {
            processo.StartInfo.FileName = "desencriptar.exe"; // Nome do executável do backend
            processo.StartInfo.RedirectStandardInput = true;
            processo.StartInfo.RedirectStandardOutput = true;
            processo.StartInfo.UseShellExecute = false;
            processo.StartInfo.CreateNoWindow = true;
            processo.StartInfo.WorkingDirectory = workingDirectory;

            processo.Start();

            // Envia os números para o backend
            processo.StandardInput.WriteLine($"{num1} {num2} {num3} {mensagem}");
            processo.StandardInput.Flush();
            processo.StandardInput.Close();

            // Lê o resultado do backend
            string resultadoStr = processo.StandardOutput.ReadToEnd();

            // Aguarda o término do backend
            processo.WaitForExit();


            return resultadoStr;

        }
    }
}