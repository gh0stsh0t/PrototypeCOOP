using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMC
{
    public sealed class User
    {
        private static User name = null;
        public readonly string id, person;
        private User (string id, string person)
        {
            this.id = id;
            this.person = person;
        }

        public static User Name
        {
            get
            {
                if (name == null)
                    using (var login = new Login())
                        if (login.ShowDialog() == DialogResult.OK)
                            name = new User(login.uid, login.uname);
                return name;
            }
        }
        public bool Logout()
        {
            name = null;
            return true;
        }
    }
}
