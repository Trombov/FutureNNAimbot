using Alturos.Yolo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace FutureNNAimbot
{

    public class Program
    {

        static void Main(string[] args)
        {
            if (PriorProcess() != null)
            {

                MessageBox.Show("Another instance of the app is already running.");
                return;
            }

            MainApp.Start();


        }

        public static Process PriorProcess()
        // Returns a System.Diagnostics.Process pointing to
        // a pre-existing process with the same name as the
        // current one, if any; or null if the current process
        // is unique.
        {
            Process curr = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcessesByName(curr.ProcessName);
            foreach (Process p in procs)
            {
                if ((p.Id != curr.Id) &&
                    (p.MainModule.FileName == curr.MainModule.FileName))
                    return p;
            }
            return null;
        }



    }
}
