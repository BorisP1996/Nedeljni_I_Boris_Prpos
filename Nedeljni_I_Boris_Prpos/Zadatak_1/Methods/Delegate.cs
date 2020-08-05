using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zadatak_1.Methods
{
    class Delegate
    {
        public delegate void Notification();
        public event Notification OnNotification;

        /// <summary>
        /// Method that will input random 8 letter string into file every time when app is started
        /// </summary>
        /// <param name="carNum"></param>
        /// <param name="list"></param>
        public void GenerateKey()
        {
            try
            {
                string path = @"../../ManagerAccess.txt";
                Random random = new Random();
                int length = 8;
                var rString = "";
                for (var i = 0; i < length; i++)
                {
                    rString += ((char)(random.Next(1, 26) + 64)).ToString().ToLower();
                }
                OnNotification += () =>
                {
                    StreamWriter sw = new StreamWriter(path);
                    sw.Write(rString);
                    sw.Close();
                };
                OnNotification.Invoke();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
