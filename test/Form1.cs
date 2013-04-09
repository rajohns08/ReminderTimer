using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        // minutesChosen is combobox variable. minutesTicking is initialized to minutesChosen but decremented to be 
        // displayed on screen as each minute passes
        decimal minutesChosen, minutesTicking;

        double minutesChoseDouble;

        // Variable for displaying seconds on screen
        decimal secondsTicking = 0;

        System.DateTime stopTime;
        System.TimeSpan s;

        // Optional custom reminder message
        string reminderMessage;

        // Flag for setting default minutes value to 5 in case it isn't changed
        bool minutesChanged = false;

        // Flag for testing if a timer has already been started (so we don't start another one if one is alraeady going)
        bool alreadyStarted = false;

        // Flag for testing if alarm file was found correctly
        bool alarmSoundFound = false;

        // Create a timer to call alerting event when minutes are up, and a countdown timer that calls a function 
        // every second for updating the countdown display
        //Timer timer = new Timer();
        Timer countdown = new Timer();

        // Sound file variable
        System.Media.SoundPlayer alarmSound;

        public Form1()
        {
            InitializeComponent();

            // Call setupAlarmSound() to set up alarm file
            setupAlarmSound();
        }

        /*****************************************************************************************************************
         * setupAlarmSound() checks to make sure the alarm file is found, so that a runtime error will not occur with
         * a file not found error.
         ****************************************************************************************************************/
        void setupAlarmSound()
        {
            if (File.Exists(@"C:\Program Files (x86)\ReminderTimer\sweet_wake_up.wav"))
            {
                alarmSound = new System.Media.SoundPlayer(@"C:\Program Files (x86)\ReminderTimer\sweet_wake_up.wav");
                alarmSoundFound = true;
            }
            else if (File.Exists(@"C:\Program Files\ReminderTimer\sweet_wake_up.wav"))
            {
                alarmSound = new System.Media.SoundPlayer(@"C:\Program Files\ReminderTimer\sweet_wake_up.wav");
                alarmSoundFound = true;
            }
            else
            {
                MessageBox.Show("Alarm sound file not found. Program will still work, but it won't play alarm sound", "ReminderTimer");
            }
        }

        /*****************************************************************************************************************
         * button1_Click is called when start timer button is pressed. It checks minutesChanged flag to determine whether
         * or not to use default 5 minute value. It also checks the alreadyStarted flag to ensure the timer code is not
         * called while a timer is already running. It sets up a timer_Tick and countdown_Tick functions, and creates time
         * intervals for both starts timer.
         ****************************************************************************************************************/
        private void button1_Click(object sender, EventArgs e)
        {
            // Don't allow user to change time while timer is running
            numericUpDown1.Enabled = false;

            if (alreadyStarted == false)
            {
                alreadyStarted = true;
                if (minutesChanged == false)
                {
                    minutesChosen = Convert.ToInt32(5);
                    minutesTicking = 5;
                }

                minutesChoseDouble = System.Convert.ToDouble(minutesChosen);
                stopTime = System.DateTime.Now.AddMinutes(minutesChoseDouble);
                s = System.DateTime.Now.TimeOfDay;

                // Everytime timer ticks, timer_Tick will be called.
                //timer.Tick += new EventHandler(timer_Tick);
                countdown.Tick += new EventHandler(countdown_Tick);

                // Convert ms to actual minutes
                //timer.Interval = Convert.ToInt32(minutesChosen) * (1000 * 60);

                // Countdown function called every second to update countdown timer on screen
                countdown.Interval = 1000;

                //timer.Enabled = true;
                countdown.Enabled = true;
                //timer.Start();
                countdown.Start();
            }

        }

        /********************************************************************************************** 
         * This function handles the timer expiration. Once the timer expires, the alarm sound begins
         * to play, a message box will pop up, and the timer will be stopped to avoid another timer 
         * interval going off. Also, stop the alarm sound when user clicks OK, and restart the program
         * to get back to initial state.
         **********************************************************************************************/
        void timerExpires()
        {
            // Only try to play the file if the file was found
            if (alarmSoundFound == true)
                alarmSound.Play();

            // Display default reminder message if the user doesn't enter a custom message
            string msg;
            if (reminderMessage == "" || reminderMessage == null)
            {
                msg = "Timer finished";
            }
            else
            {
                msg = reminderMessage;
            }

            DialogResult dlgResult = MessageBox.Show(new Form() { TopMost = true }, msg, "ReminderTimer");
            if (dlgResult == DialogResult.OK)
            {
                // Only try to stop the sound if alarm file was found
                if (alarmSoundFound == true)
                    alarmSound.Stop();
                Application.Restart();
            }
        }

        /****************************************************************************
         * countdown_Tick handles the countdown timer display updates on the screen. 
         * It is called every second to update the timer shown every second.
         ****************************************************************************/
        void countdown_Tick(object sender, EventArgs e)
        {
            if (System.DateTime.Now >= stopTime)
            {
                countdown.Stop();
                timerExpires();
            }
            //// New string variables for displaying the values to the screen
            //string secTicks = secondsTicking.ToString();
            //string minTicks = minutesTicking.ToString();

            //// Logic for making sure time is shown in xx:xx format
            //if (minutesTicking == 9)
            //    minTicks = "09";
            //if (minutesTicking == 8)
            //    minTicks = "08";
            //if (minutesTicking == 7)
            //    minTicks = "07";
            //if (minutesTicking == 6)
            //    minTicks = "06";
            //if (minutesTicking == 5)
            //    minTicks = "05";
            //if (minutesTicking == 4)
            //    minTicks = "04";
            //if (minutesTicking == 3)
            //    minTicks = "03";
            //if (minutesTicking == 2)
            //    minTicks = "02";
            //if (minutesTicking == 1)
            //    minTicks = "01";
            //if (minutesTicking == 0)
            //    minTicks = "00";

            //if (secondsTicking == 9)
            //    secTicks = "09";
            //if (secondsTicking == 8)
            //    secTicks = "08";
            //if (secondsTicking == 7)
            //    secTicks = "07";
            //if (secondsTicking == 6)
            //    secTicks = "06";
            //if (secondsTicking == 5)
            //    secTicks = "05";
            //if (secondsTicking == 4)
            //    secTicks = "04";
            //if (secondsTicking == 3)
            //    secTicks = "03";
            //if (secondsTicking == 2)
            //    secTicks = "02";
            //if (secondsTicking == 1)
            //    secTicks = "01";
            //if (secondsTicking == 0)
            //    secTicks = "00";

            //Display the time in label6
            //label6.Text = minTicks + ":" + secTicks;

            string stopTim = s.ToString();
            label6.Text = stopTim;

            //secondsTicking--;

            // Logic for resetting seconds and decrementing minutes
            //if (secondsTicking < 0)
            //{
            //    secondsTicking = 59;
            //    minutesTicking--;
            //}

            //// Timer has expired so stop the countdown and display 00:00
            //if (minutesTicking < 0)
            //{
            //    minutesTicking = 0;
            //    secondsTicking = 0;
            //    countdown.Stop();
            //    timerExpires();
            //}


        }

        /***************************************************************************************
         * When numeric value changed this function sets minutes to equal the value in the box.
         ***************************************************************************************/
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // This flag is used to tell button1_click to override the default value of 5 with 
            // the value received from numeric up/down box
            minutesChanged = true;
            minutesChosen = numericUpDown1.Value;

            // Initialize minutesTicking to what was chosen
            minutesTicking = minutesChosen;
        }

        /*************************************************************************************
         * Get custom reminder message from user (optional)
         *************************************************************************************/
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            reminderMessage = textBox1.Text;
        }

        /*************************************************************************************
         * Restart the application to return to init state if user cancels timer by clicking
         * "Cancel Timer" button (button2)
         *************************************************************************************/
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }



    }
}
