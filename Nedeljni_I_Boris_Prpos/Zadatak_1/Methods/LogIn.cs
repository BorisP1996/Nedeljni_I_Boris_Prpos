using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.Methods
{
    class LogIn
    {
        Entity context = new Entity();

        public int Admin(string username,string password)
        {
            try
            {
                List<tblUser> userList = context.tblUsers.ToList();

                foreach (tblUser item in userList)
                {
                    if (item.Username==username && item.Pasword==password)
                    {
                        List<tblAdmin> adminList = context.tblAdmins.ToList();

                        foreach (tblAdmin itemAdmin in adminList)
                        {
                            if (itemAdmin.UserID==item.UserId)
                            {
                                //team admin
                                if (itemAdmin.AdminTypeID==1)
                                {
                                    return 1;
                                }
                                //system
                                else if (itemAdmin.AdminTypeID == 2)
                                {
                                    return 2;
                                }
                                //local
                                else if(itemAdmin.AdminTypeID==3)
                                {
                                    return 3;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                return 0;
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
                return 0;
            }
        }
    }
}
