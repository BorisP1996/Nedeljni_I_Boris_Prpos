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
        public bool EmployeLoged(string username,string pasword)
        {
            try
            {
                List<tblUser> userList = context.tblUsers.ToList();
                List<tblEmploye> employeList = context.tblEmployes.ToList();

                foreach (tblUser item in userList)
                {
                    if (item.Username==username && item.Pasword==pasword)
                    {
                        foreach (tblEmploye item1 in employeList)
                        {
                            if (item1.UserID==item.UserId)
                            {
                                return true;
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
                return false;
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
                return false;
            }
        }
        public bool ManagerLoged(string username, string pasword)
        {
            try
            {
                List<tblUser> userList = new List<tblUser>();
                userList=context.tblUsers.ToList();
                List<tblManager> managerList = new List<tblManager>();
                managerList=context.tblManagers.ToList();

                foreach (tblUser item in userList)
                {
                    if (item.Username == username && item.Pasword == pasword)
                    {
                        foreach (tblManager item1 in managerList)
                        {
                            if (item1.UserID == item.UserId)
                            {
                                return true;
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
                return false;
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
                return false;
            }
        }
    }
}
