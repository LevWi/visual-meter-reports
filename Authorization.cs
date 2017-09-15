using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace WindowsFormsApplication1
{
    public delegate void StateChange();
    public class Authorization
    {
        public event StateChange LogInGhanged; 
        public string login {private set;get;}
        public string password {private set;get;}
        bool isloggedin_ = false;
        public bool isLoggedIn { 
            set { 
                    isloggedin_ = value; 
                }
            get { return isloggedin_; }
        }
        public double timerInterval
        {
            set
            {
                timer.Interval = value;
            }
            get
            {
                return timer.Interval;
            }
        }

        System.Timers.Timer timer;

        public Authorization()
        {
            login = "";
            password = "";
            timer = new System.Timers.Timer();
            timer.AutoReset = false;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
        }
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            LogOut();
        }
        //public void Login(string log, string pas)
        //{
        //    login = log;
        //    password = pas;
        //    isLoggedIn = true;
        //}
        public void Login(string log, string pas, bool autologout = false, double interval = 120)
        {
            login = log;
            password = pas;
            isLoggedIn = true;
            if (autologout)
            {
                timer.Stop();
                timerInterval = interval * 1000;
                timer.Start();
            }
            if (LogInGhanged != null)
                        LogInGhanged();
        }
        public void LogOut()
        {
            login = "";
            password = "";
            isLoggedIn = false;
            timer.Stop();
            if (LogInGhanged != null)
                        LogInGhanged();
        }
    }
}
