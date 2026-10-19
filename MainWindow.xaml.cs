using System;
using System.Linq;
using System.Windows;

namespace ПР5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        MARSH[] marshes = new MARSH[8];
        int[] numbers = new int[8];
        private int i = 0;
        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxBegst.Text == "")
                {
                    MessageBox.Show("Введите название начального пункта маршрута"); return;
                }
                if (textBoxTerm.Text == "")
                {
                    MessageBox.Show("Введите название конечного пункта маршрута"); return;
                }
                if (textBoxNumber.Text == "")
                {
                    MessageBox.Show("Введите номер маршрута"); return;
                }

                MARSH marsh = new MARSH();
                marsh.BEGST = textBoxBegst.Text;
                marsh.TERM = textBoxTerm.Text;
                marsh.NUMBER = int.Parse(textBoxNumber.Text);
                if (marsh.NUMBER <= 0) { MessageBox.Show("Номер маршрута не может быть меньше или равен нулю!"); return; }

                if (numbers.Contains(marsh.NUMBER))
                {
                    MessageBox.Show("Такой маршрут уже есть");
                    textBoxBegst.Text = "";
                    textBoxTerm.Text = "";
                    textBoxNumber.Text = "";
                    return;
                }

                numbers[i] = marsh.NUMBER;
                marshes[i] = marsh;
                i++;

                listBox.Items.Add(marsh);


                if (i == 8)
                {
                    labelTitle.IsEnabled = false;
                    labelBegst.IsEnabled = false;
                    textBoxBegst.IsEnabled = false;
                    labelTerm.IsEnabled = false;
                    textBoxTerm.IsEnabled = false;
                    labelNumber.IsEnabled = false;
                    textBoxNumber.IsEnabled = false;
                    buttonCreate.IsEnabled = false;

                    separator1.IsEnabled = false;
                    separator2.IsEnabled = false;

                    buttonInfo.IsEnabled = true;

                    Array.Sort(marshes);

                    return;
                }


                labelTitle.Content = "Введите данные " + (i + 1) + "-го маршрута";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void ButtonInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string result = Microsoft.VisualBasic.Interaction.InputBox("Введите номер маршрута", "");
                int number = int.Parse(result);

                if (numbers.Contains(number))
                {
                    foreach (MARSH i in marshes)
                    {
                        if (i.NUMBER == number)
                        {
                            MessageBox.Show(i.ToString());
                        }
                    }
                }
                else { MessageBox.Show("Такого маршрута нет!"); }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return; }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            labelTitle.Content = "Введите данные " + (i + 1) + "-го маршрута";
        }
    }

    struct MARSH : IComparable<MARSH>
    {
        private string _BEGST;
        private string _TERM;
        private int _NUMBER;

        public string BEGST
        {
            get { return _BEGST; }
            set { _BEGST = value; }
        }

        public string TERM
        {
            get { return _TERM; }
            set { _TERM = value; }
        }

        public int NUMBER
        {
            get { return _NUMBER; }
            set { _NUMBER = value; }
        }

        public override string ToString()
        {
            return "Начальный пункт назначения - " + BEGST + "\nКонечный пункт назначения - " + TERM +
                "\nНомер маршрута - " + NUMBER;
        }

        public int CompareTo(MARSH other)
        {
            return NUMBER.CompareTo(other.NUMBER);
        }
    }
}
