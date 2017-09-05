using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMC
{
    public class ComboboxContent
    {
        public int id;
        public string content;
        public string content2;
        public string content3;
        public string content4;
        public string content5 { get; set; }
        public ComboboxContent(int id, string content)
        {
            this.id = id;
            this.content = content;
        }
        public ComboboxContent(int id, string content, string content2)
        {
            this.id = id;
            this.content = content;
            this.content2 = content2;
        }
        public ComboboxContent(int id, string content, string content2, string content3, string content4)
        {
            this.id = id;
            this.content = content;
            this.content2 = content2;
            this.content3 = content3;
            this.content4 = content4;
        }
        public ComboboxContent(int id, string content, string content2, string content3, string content4, string content5)
        {
            this.id = id;
            this.content = content;
            this.content2 = content2;
            this.content3 = content3;
            this.content4 = content4;
            this.content5 = content5;
        }
        public ComboboxContent(int id, string content, string content2, string content3)
        {
            this.id = id;
            this.content = content;
            this.content2 = content2;
            this.content3 = content3;

        }
        public override string ToString()
        {
            return content.ToString();
        }
        public string ToString(int a)
        {
            if (a == 0)
                return content;
            else if (a == 1)
                return content2;
            else if (a == 2)
                return content3;
            else
                return content4;
        }

        public int ID
        {
            get
            {
                return id;
            }
        }
        public string Content
        {
            get
            {
                return content;

            }
        }
        public string Content2
        {
            get
            {
                return content2;

            }
        }
        public string Content3
        {
            get
            {
                return content3;

            }
        }
        public string Content4
        {
            get
            {
                return content4;

            }
        }


    }
}


/*
private void SacramentForm_Load(object sender, EventArgs e)
{
    legitimacyCBox.DataSource = Enum.GetValues(typeof(Legitimacy));
    DataTable dt = dh.getMinisterWithStatus(MinisterStatus.Active);

    foreach (DataRow r in dt.Rows)
    {

        ComboboxContent cc = new ComboboxContent(int.Parse(r["ministerID"].ToString()), r["name"].ToString());
        //MessageBox.Show(cc.ToString());
        MinisterCBox.Items.Add(cc);
    }

    loadParents();

}*/