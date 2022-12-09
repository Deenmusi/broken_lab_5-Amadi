using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    
    

    public partial class Form1 : Form
    {    //Name: Uchechi Amadi
         //Due Date: 06/12/2022
         //Description:  A program showing various events like generating random numbers, creating functions which send
         //and return values, validating user inputs and calculating values using loops.

        //declare class level variable
        const string PROGRAMMER = "Uchechi";

        //declare class level variable for attempts
        int attempts = 3;


        //create GetRandom function
        private int GetRandom(int min, int max)
        {
            Random rand = new Random();
            int ranNum = rand.Next(min, max);
            return ranNum;      
        }
            
        public Form1()
        {
            InitializeComponent();
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
            //add name to end of form title
            this.Text = Text + " " + PROGRAMMER;
            //hide the Text groupbox
            grpText.Hide();
            //hide the Choose groupbox
            grpChoose.Hide();
            //hide the Stats groupbox
            grpStats.Hide();
            //set cursor to textbox
            txtCode.Focus();
            //call the function GetRandom
            lblCode.Text = GetRandom(100000, 200000).ToString();
        }

       
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //make login button work on [Enter]
            this.AcceptButton = btnLogin;
            
            //validate passcode entry
            if (lblCode.Text == txtCode.Text)
            {
                grpChoose.Show();
                grpLogin.Enabled = false;
            }

            //validate user entry attempts
            do
            {
                attempts++;
                if (txtCode.Text == lblCode.Text)
                {
                    grpChoose.Show();
                    grpLogin.Enabled = false;
                    break;
                }
                else if (attempts == 3)
                {
                    MessageBox.Show(attempts.ToString() + " Attempts to login. \n Account locked - Closing programm.", PROGRAMMER);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(attempts.ToString() + " incorrect code entered. \n Try again - only 3 attempts allowed", PROGRAMMER);
                    txtCode.SelectAll();
                    break;
                }
            } while (attempts < 4);
        }

        //create ResetTextGrp function
        private void ResetTextGrp()
        {
            //make join button work on [Enter]
            this.AcceptButton = btnJoin;

            //make cancel button work on [Esc]
            this.CancelButton = btnReset;

            txtString1.Clear();
            txtString1.Focus();
            txtString2.Clear();
            chkSwap.Checked = false;
            lblResults.Text = ""
        }

        //create ResetStatsGrp function
        private void ResetStatsGrp()
        {
            //make join button work on [Enter]
            this.AcceptButton = btnGenerate;

            //make cancel button work on [Esc]
            this.CancelButton = btnClear;

            lblDisplaySum.Text = "";
            lblDisplayMean.Text = "";
            lblDisplayOdd.Text = "";
            lstNumbers.Items.Clear();
           
        }
        
        //create SetupOption function
        private void SetupOption()
        {
            if (radText.Checked)
            {
                grpText.Show();
                grpStats.Hide();
                //call function
                ResetTextGrp();

            }
            else if (radStats.Checked)
            {
                grpStats.Show();
                grpText.Hide();
                //call function
                ResetStatsGrp();
            }
        }

        private void radText_CheckedChanged(object sender, EventArgs e)
        {   //call SetupOption function
            SetupOption();
        }

        private void radStats_CheckedChanged(object sender, EventArgs e)
        {   //call SetupOption funtion
            SetupOption();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {   //call ResetTextGrp function
            ResetTextGrp();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {   //call ResetStatsGrp function
            ResetStatsGrp();
        }

        //create swap function
        private void Swap(string first, string second)
        {
            first = txtString1.Text;
            second = txtString2.Text;
            string temp;

            temp = first;
            txtString1.Text = second;
            txtString2.Text = temp;
           
        }

        //create CheckInput function
        private Boolean CheckInput()
        {   
            //declare variables
            string first, second;
            
            //validate input in textboxes
            bool.TryParse(txtString1.Text, out first);
            bool.TryParse(txtString2.Text, out second);

            if (first && second)
            {
                return true;
            }
            else
                return false;
        }

        private void chkSwap_CheckedChanged(object sender, EventArgs e)
        {
            //call CheckInput function
            CheckInput();

            //call Swap function
            Swap(txtString1.Text, txtString2.Text);
            lblResults.Text = "Strings have been swapped";

           
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            //declare variable with assignment
            string first = txtString1.Text.Trim();
            string second = txtString2.Text.Trim();

            //display results in label
            lblResults.Text = "First string" + " = " + first + "\n" +
                              "Second string" + " = " + second + "\n" +
                               "Joined" + " = " + first + "-->" + second;         
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            //declare variable with assignment
            string first = txtString1.Text.Trim();
            string second = txtString2.Text.Trim();

            //display results in label
            lblResults.Text = "First string" + " = " + first + "\n" +
                             "\t" + "Characters" + " = " + first.Length + "\n" +
                              "Second string" + " = " + second + "\n" +
                           "\t" +  "Characters" + " = " + second.Length;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //generate random object with seed value
            Random r = new Random(533);
            
            for (int i = 0; i < nudHowMany.Value; i+= 1)
            {
                int ranNum = r.Next(1000, 5001);
                lstNumbers.Items.Add(ranNum.ToString());
            }

            //call function 
             AddList();

            //call function
            CountOdd();

             //calculate mean
            double mean = AddList() / (double)lstNumbers.Items.Count;
            lblDisplayMean.Text = mean.ToString("N2");
        }

        //create funtion AddList
        private int AddList()
        {
            int i = 0, result = 0;
            while (i < lstNumbers.Items.Count)
            {
                result += Convert.ToInt32(lstNumbers.Items[i++]);
            }
            lblDisplaySum.Text = result.ToString("N0");
            return result;
        }
        //create function CountOdd
         private int CountOdd()
         {
            int oddNum = 0, i = 0;

            do
            {
                if (Convert.ToInt32(lstNumbers.Items[i]) % 2 != 0)
                    oddNum++;
                i++;
            } while (i < lstNumbers.Items.Count);
            lblDisplayOdd.Text = oddNum.ToString(); 
            return oddNum;
         }
    }
}
