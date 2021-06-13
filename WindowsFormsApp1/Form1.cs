using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        const int TIME_FOR_ONE_DIGIT = 10;
        public int[] numbers;
        public int guessedNumbers = 0;
        public int numberToGuess;

        public int s = 0;
        public int m = 0;

        public Form1()
        {
            InitializeComponent();
            numbers = new int[4];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int[] _domain1Items = { 4, 3, 2, 1 };
            domainUpDown1.Items.AddRange(_domain1Items);
            domainUpDown1.Text = "1";
            domainUpDown2.Items.Clear();
            domainUpDown2.Items.Add(1);
            domainUpDown2.Text = "1";
            timer1.Interval = 1000;
            _switchAreSettingEnabled(true);
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _applyChangedForChangedTextAndItsPanel(panel1, textBox1, numbers[0]);
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            _applyChangedForChangedTextAndItsPanel(panel2, textBox2, numbers[1]);

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            _applyChangedForChangedTextAndItsPanel(panel3, textBox3, numbers[2]);
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            _applyChangedForChangedTextAndItsPanel(panel4, textBox4, numbers[3]);
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            _adjustMamixumAllowedNeededRights();

        }



        private void domainUpDown2_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _generateRandomNumberToGuess();
            timer1.Start();
            _switchAreSettingEnabled(false);
     
            label6.Text = (int.Parse(domainUpDown2.Text) - guessedNumbers).ToString();

            s = TIME_FOR_ONE_DIGIT * int.Parse(domainUpDown1.Text);

        }

        private void _generateRandomNumberToGuess()
        {
            Random _rand = new Random();
            int _generatedNumbers = 0;
            int _charSpot = 1;
            numberToGuess = 0;
            while (_generatedNumbers < int.Parse(domainUpDown1.Text))
            {
                int _newNumber = _rand.Next(0, 9);
                if (!numbers.Contains(_newNumber) || checkBox1.Checked)
                {
                    Console.WriteLine("generated: " + _newNumber.ToString());
                    numbers[3 - _generatedNumbers] = _newNumber;
                    numberToGuess += _newNumber * _charSpot;
                    _charSpot *= 10;
                    Console.WriteLine("number to guess: " + numberToGuess.ToString());
                    _generatedNumbers++;
                }
            }
            foreach (int number in numbers)
            {
                Console.WriteLine("numbers: " + number.ToString());
            }
        }

        private void _adjustMamixumAllowedNeededRights()
        {
            domainUpDown2.Items.Clear();
            List<int> newRange = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                if (i >= int.Parse(domainUpDown1.Text))
                {

                    break;
                }
                newRange.Add(i + 1);
            }
            newRange.Reverse();
            domainUpDown2.Items.AddRange(newRange);
            domainUpDown2.Text = "1";
        }


        private void _switchAreSettingEnabled(bool areEnabled)
        {
            if (!areEnabled)
            {
                _enableFieldsBasedOnCurrentDomainValue();
            }
            else
            {
                _disableAreInputTextFields();
            }
            button1.Enabled = areEnabled;
            domainUpDown1.Enabled = areEnabled;
            domainUpDown2.Enabled = areEnabled;
            checkBox1.Enabled = areEnabled;
        }


        private void _disableAreInputTextFields()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            panel1.BackColor = Color.Orange;

            panel2.BackColor = Color.Orange;

            panel3.BackColor = Color.Orange;

            panel4.BackColor = Color.Orange;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
        private void _applyChangedForChangedTextAndItsPanel(Panel panel, TextBox textBox, int numberToGuess)
        {
            try
            {
                if (textBox.Text != "")
                {
                    _changePanelToApproptiateColorAndCheckWin(panel, textBox, numberToGuess);

                }

            }
            catch
            {
                panel.BackColor = Color.Orange;
                MessageBox.Show("Entered text could not be parsed into a valid number, please enter valid numbers into the text field.");
                textBox.Text = "";
            }
        }

        private void _changePanelToApproptiateColorAndCheckWin(Panel panel, TextBox textBox, int numberToGuess)
        {
            int _newNUmber = int.Parse(textBox.Text);

            if (_newNUmber == numberToGuess)
            {

                if (panel.BackColor != Color.Blue)
                {
                    guessedNumbers++;
                }
                panel.BackColor = Color.Blue;
                if (guessedNumbers == int.Parse(domainUpDown2.Text))
                {
                    _winGame();

                }

            }
            else
            {
                if (panel.BackColor == Color.Blue)
                {
                    guessedNumbers--;
                }

                if (numbers.Contains(_newNUmber))
                {
                    panel.BackColor = Color.Red;
                }
                else
                {
                    panel.BackColor = Color.Orange;
                }
            }
            label6.Text = (int.Parse(domainUpDown2.Text) - guessedNumbers).ToString();
        }

        private void _winGame()
        {
            timer1.Stop();
            _switchAreSettingEnabled(true);
            _showGameEndDialog("CONGRATULATIONS!");
        }

        private void _showGameEndDialog(String message)
        {
            DialogResult res = MessageBox.Show(message + "\nContinue game?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (res == DialogResult.No)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void _enableFieldsBasedOnCurrentDomainValue()
        {
            String _currentValue = domainUpDown1.Text;
            if (_currentValue == "1")
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = true;
            }
            else if (_currentValue == "2")
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = true;
                textBox4.Enabled = true;

            }
            else if (_currentValue == "3")
            {
                textBox1.Enabled = false;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;

            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            s--;
            label5.Text = m.ToString().PadLeft(2, '0') + " : " + s.ToString().PadLeft(2, '0');
            if (s == 0)
            {
                timer1.Stop();
                _showGameEndDialog("YOU DID NOT KNOW, GAME OVER - NUMBER: " + _showUserInputNumber() + " should have been " + numberToGuess.ToString() + ".");
                _switchAreSettingEnabled(true);
            }
        }

        String _showUserInputNumber()
        {
            String _userInput = "";
            if (textBox1.Text != "") _userInput += textBox1.Text;
            if (textBox2.Text != "") _userInput += textBox2.Text;
            if (textBox3.Text != "") _userInput += textBox3.Text;
            if (textBox4.Text != "") _userInput += textBox4.Text;
            return _userInput;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }



}
