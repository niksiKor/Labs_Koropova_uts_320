namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private double a = 0;
        private double b = 0;
        private string action = "";
        private bool is_calulated = false;

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btn_click(object sender, EventArgs e)
        {
            if (is_calulated)
                richTextBox1.Clear();
            richTextBox1.Text += ((Button)sender).Text;
            
        }
        private void btn_action_click(object sender, EventArgs e)
        {
            try
            {
                a = Convert.ToDouble(richTextBox1.Text);// запомнили число
                action = ((Button)sender).Text;// запомнили действие
                richTextBox1.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
            richTextBox1.Clear();
        }
        private void btn_calulation_click(object sender, EventArgs e)
        {
            try
            {
                b = Convert.ToDouble(richTextBox1.Text);// запомнили число
                switch (action)
                {
                    case "+":
                        richTextBox1.Text = (a + b).ToString();
                        break;
                    case "*":
                        richTextBox1.Text = (a * b).ToString();
                        break;
                    case "-":
                        richTextBox1.Text = (a - b).ToString();
                        break;
                    case "/":
                        richTextBox1.Text = (a / b).ToString();
                        break;
                    case "^x":
                        richTextBox1.Text = Math.Pow(a,b).ToString();
                        break;
                }
                is_calulated = true;
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
            
        }
       
    }
}
