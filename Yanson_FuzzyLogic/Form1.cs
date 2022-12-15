using DotFuzzy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yanson_FuzzyLogic
{
    public partial class Form1 : Form
    {
        FuzzyEngine fuzzyEngine;
        MembershipFunctionCollection liter, speed, hour;
        LinguisticVariable myliter, myspeed, myhour;
        FuzzyRuleCollection myrules;

        public Form1()
        {
            InitializeComponent();
            SetMembers();
            SetRules();
            SetFuzzyEngine();
        }

        public void SetMembers()
        {

            speed = new MembershipFunctionCollection();
            speed.Add(new MembershipFunction("Slow", 1, 1, 1000, 1500));
            speed.Add(new MembershipFunction("Medium", 1250, 1500, 2500, 3000));
            speed.Add(new MembershipFunction("Fast", 2750, 3000, 4000, 4500));
            myspeed = new LinguisticVariable("Speed", speed);

            liter = new MembershipFunctionCollection();
            liter.Add(new MembershipFunction("Low", 1, 1, 2, 4));
            liter.Add(new MembershipFunction("Medium", 2, 4, 8, 10));
            liter.Add(new MembershipFunction("High", 8, 12, 15, 24));
            myliter = new LinguisticVariable("Liter", liter);

            hour = new MembershipFunctionCollection();
            hour.Add(new MembershipFunction("VeryShort", 1, 1, 4, 5));
            hour.Add(new MembershipFunction("Short", 4, 5, 6, 7));
            hour.Add(new MembershipFunction("Medium", 7, 7, 8, 9));
            hour.Add(new MembershipFunction("Long", 8, 9, 10, 11));
            hour.Add(new MembershipFunction("VeryLong", 10, 11, 12, 12));
            myhour = new LinguisticVariable("Hour", hour);

        }

        public void SetRules()
        {
            myrules = new FuzzyRuleCollection();
            myrules.Add(new FuzzyRule("IF (Speed IS Slow) AND (Liter IS Low) THEN Hour IS VeryLong"));
            myrules.Add(new FuzzyRule("IF (Speed IS Slow) AND (Liter IS Medium) THEN Hour IS VeryLong"));
            myrules.Add(new FuzzyRule("IF (Speed IS Slow) AND (Liter IS High) THEN Hour IS VeryLong"))
                ;
            myrules.Add(new FuzzyRule("IF (Speed IS Medium) AND (Liter IS Low) THEN Hour IS VeryShort"));
            myrules.Add(new FuzzyRule("IF (Speed IS Medium) AND (Liter IS Medium) THEN Hour IS Medium"));
            myrules.Add(new FuzzyRule("IF (Speed IS Medium) AND (Liter IS High) THEN Hour IS Long"));

            myrules.Add(new FuzzyRule("IF (Speed IS Fast) AND (Liter IS Low) THEN Hour IS VeryShort"));
            myrules.Add(new FuzzyRule("IF (Speed IS Fast) AND (Liter IS Medium) THEN Hour IS Short"));
            myrules.Add(new FuzzyRule("IF (Speed IS Fast) AND (Liter IS High) THEN Hour IS Medium"));
        }

        public void SetFuzzyEngine()
        {
            fuzzyEngine = new FuzzyEngine();
            fuzzyEngine.LinguisticVariableCollection.Add(myspeed);
            fuzzyEngine.LinguisticVariableCollection.Add(myliter);
            fuzzyEngine.LinguisticVariableCollection.Add(myhour);
            fuzzyEngine.FuzzyRuleCollection = myrules;
        }
        private void button2_Click(object sender, System.EventArgs e)
        {
            myliter.InputValue = (Convert.ToDouble(textBox1.Text));
            myliter.Fuzzify("Medium");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetFuzzyEngine();
            fuzzyEngine.Consequent = "Hour";
            textBox3.Text = fuzzyEngine.Defuzzify() + "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ComputeNewSpeed();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FuziffyValues();
            Defuzzy();
            ComputeNewSpeed();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = trackBar2.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            myspeed.InputValue = (Convert.ToDouble(textBox2.Text));
            myspeed.Fuzzify("Medium");
        }

        public void FuziffyValues()
        {
            myliter.InputValue = (Convert.ToDouble(textBox1.Text));
            myliter.Fuzzify("Low");
            myspeed.InputValue = (Convert.ToDouble(textBox2.Text));
            myspeed.Fuzzify("Fast");
        }

        public void Defuzzy()
        {
            SetFuzzyEngine();
            fuzzyEngine.Consequent = "Hour";
            textBox3.Text = "" + fuzzyEngine.Defuzzify();
        }
        public void ComputeNewSpeed()
        {

            double oldSpeed = Convert.ToDouble(textBox2.Text);
            double oldHour = Convert.ToDouble(textBox3.Text);
            double oldLiter = Convert.ToDouble(textBox1.Text);
            double newSpeed = ((1 - 0.1) * (oldSpeed)) + (oldHour - (0.1 * oldLiter));
            textBox2.Text = "" + newSpeed;
        }

    }
}
