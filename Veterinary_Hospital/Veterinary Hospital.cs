using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;    
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Veterinary_Hospital

{
    public partial class Form1 : Form
    {
        DataSet dataset;
        DataTable dt;
        SqlDataAdapter adapter;
        SqlCommand command;
        bool check = true;
        string connectionString = @"Data Source=DESKTOP-Q6NEM54\SQLEXPRESS;Initial Catalog=vetclinic;Integrated Security=True";

        public void Registration()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    adapter = new SqlDataAdapter("select r.ID_Register as '№',  p.Pet_Name  as 'Имя животного', p.Type_Of_Pet  as 'Вид животного', o.FIO_Owner  as 'ФИО владельца',d.FIO_Doctor  as 'ФИО врача',r.Est_Date_Of_Visit  as 'Дата',r.Est_Time_Of_Visit  as 'Время' from Registers r inner join Pets p on r.ID_Pet =p.ID_Pet  inner join Owners o on o.ID_Owner =p.ID_Owner  inner join Doctors d on r.ID_Doctor =d.ID_Doctor  ", connectionString);
                    dataset = new DataSet();
                    adapter.Fill(dataset);
                    dataGridView1.DataSource = dataset.Tables[0];
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        

        public void Owners()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    adapter = new SqlDataAdapter("select FIO_Owner as 'ФИО владельца ',Owner_Address as 'Адресс владельца ' , Telephone as 'Телефон владельца 'from Owners", connectionString);
                    dataset = new DataSet();
                    adapter.Fill(dataset);
                    dataGridView3.DataSource = dataset.Tables[0];
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void Pets()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    adapter = new SqlDataAdapter("select p.ID_Pet as '№', p.Pet_Name as 'Имя животного', p.Type_Of_Pet as 'Вид животного', o.FIO_Owner as 'ФИО владельца',p.Date_Of_Birth as 'Дата рождения',p.Breed as 'Порода', p.Male as 'Пол',p.Coat_Color as 'Цвет шерсти', p.Special_Sings as'Особые приметы'  from Pets p inner join Owners o on p.ID_Owner=o.ID_Owner", connectionString);
                    dataset = new DataSet();
                    adapter.Fill(dataset);
                    dataGridView4.DataSource = dataset.Tables[0];
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void Doctors()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    adapter = new SqlDataAdapter("select FIO_Doctor as 'ФИО доктора ',Speciality  as 'Специальность ' , Parlour as 'Стаж работы 'from Doctors", connectionString);
                    dataset = new DataSet();
                    adapter.Fill(dataset);
                    dataGridView5.DataSource = dataset.Tables[0];
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void Doctors_Combobox_FIO()
        {
            string Doctors = "Select FIO_Doctor From Doctors";
            adapter = new SqlDataAdapter(Doctors, connectionString);

            dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIO_DOC_CB_REG.Items.Add(dt.Rows[i]["FIO_Doctor"]);
                DEL_FIO_DOC_CB_DOC.Items.Add(dt.Rows[i]["FIO_Doctor"]);
                //FIO_DOC_CB_VIS.Items.Add(dt.Rows[i]["FIO_Doctor"]);
            }
        }

        public void Doctors_Combobox_Speciality()
        {
            FIO_OWN_CB_REG.Items.Clear();
            string Owners = $"Select FIO_Owner from Owners where ID_Owner =(select ID_Owner from Pets where Pet_Name =N'{PET_NAME_CB_REG.Text}')";
            adapter = new SqlDataAdapter(Owners, connectionString);

            dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIO_OWN_CB_REG.Items.Add(dt.Rows[i]["FIO_Owner"]);
            }
        }

        public void Owners_Combobox()
        {
            string Owners = "Select FIO_Owner From Owners";
            adapter = new SqlDataAdapter(Owners, connectionString);

            dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIO_OWN_CB_REG.Items.Add(dt.Rows[i]["FIO_Owner"]);
                //FIO_OWN_CB_VIS.Items.Add(dt.Rows[i]["FIO_Owner"]);
                DEL_FIO_OWN_CB_OWN.Items.Add(dt.Rows[i]["FIO_Owner"]);
                FIO_OWN_CB_PET.Items.Add(dt.Rows[i]["FIO_Owner"]);

            }
        }

        public void Registers_Combobox()
        {
            string ID_Register = "Select ID_Register From Registers";
            adapter = new SqlDataAdapter(ID_Register, connectionString);

            dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DEL_ID_REG_CB_REG.Items.Add(dt.Rows[i]["ID_Register"]);

            }
        }
      

        public void Pets_Combobox()
        {
            string ID_Pet = "Select ID_Pet From Pets";
            adapter = new SqlDataAdapter(ID_Pet, connectionString);

            dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DEL_ID_PET_CB_PET.Items.Add(dt.Rows[i]["ID_Pet"]);

            }
        }

        public void NamePets_Combobox()
        {
            string ID_Pet = "Select Pet_Name From Pets";
            adapter = new SqlDataAdapter(ID_Pet, connectionString);

            dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PET_NAME_CB_REG.Items.Add(dt.Rows[i]["Pet_Name"]);
                //PET_NAME_CB_VIS.Items.Add(dt.Rows[i]["Pet_Name"]);

            }
        }

        public Form1()
        {
            InitializeComponent();

            Registration();
          //  Vizits();
            Owners();
            Pets();
            Doctors();

            Doctors_Combobox_FIO();
            Doctors_Combobox_Speciality();
            Owners_Combobox();
            //Visitss_Combobox();
            Registers_Combobox();
            Pets_Combobox();
            NamePets_Combobox();
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            check = true;
            string queryForCheck = "Select Est_Date_Of_Visit, Est_Time_Of_Visit from Registers";
            adapter = new SqlDataAdapter(queryForCheck, connectionString);

            dt = new DataTable();

            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((dt.Rows[i][0].ToString() == monthCalendar1.Text) &&(dt.Rows[i][1].ToString() == dateTimePicker1.Text))
                {
                    check = false;
                }
            }
            if (PET_NAME_CB_REG.Text != ""  && monthCalendar1.Text != "" && dateTimePicker1.Text != "" && FIO_OWN_CB_REG.Text != "" && FIO_DOC_CB_REG.Text != "")
            {
                if (check == true)
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand($"select ID_Pet from Pets where Pet_Name = N'{PET_NAME_CB_REG.Text}'", connection);
                            var petid = Convert.ToInt32(cmd.ExecuteScalar());
                            cmd = new SqlCommand($"select ID_Owner from Owners where FIO_Owner = N'{FIO_OWN_CB_REG.Text}'", connection);
                            var ownid = Convert.ToInt32(cmd.ExecuteScalar());
                            cmd = new SqlCommand($"select ID_Doctor from Doctors where FIO_Doctor = N'{FIO_DOC_CB_REG.Text}'", connection);
                            var docid = Convert.ToInt32(cmd.ExecuteScalar());
                            cmd = new SqlCommand($"exec insertRegisters {docid},{ownid},{petid},'{monthCalendar1.Text}','{dateTimePicker1.Text}';", connection);
                            cmd.ExecuteNonQuery();
                            Registration();
                            MessageBox.Show("Добавлено");
                            DEL_ID_REG_CB_REG.Items.Clear();
                            Registers_Combobox();
                            PET_NAME_CB_REG.Text = "";
                            FIO_OWN_CB_REG.Text = "";
                            FIO_DOC_CB_REG.Text = "";
                            monthCalendar1.Text = "";
                            dateTimePicker1.Text = "";
                            Pets_Combobox();


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else MessageBox.Show("Запись на это время уже есть");
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            check = true;
            string queryForCheck = "Select FIO_Owner from Owners";
            adapter = new SqlDataAdapter(queryForCheck, connectionString);

            dt = new DataTable();

            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["FIO_Owner"].ToString() == FIO_OWN_TB_OWN.Text)
                {
                    check = false;
                }
            }
            if (FIO_OWN_TB_OWN.Text != "" && ADDRESS_TB_OWN.Text != "" && maskedTextBox5.Text != "")
            {
                if (check == true)
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            FIO_OWN_CB_PET.Items.Clear();

                            connection.Open();

                            string Sql1 = $"insert into Owners  values  (N'" + (FIO_OWN_TB_OWN.Text) + "',N'" + ADDRESS_TB_OWN.Text + "','" + maskedTextBox5.Text + "')";
                            SqlCommand cmd = new SqlCommand(Sql1, connection);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Добавлено");
                            Owners();
                            FIO_OWN_TB_OWN.Text = "";
                            ADDRESS_TB_OWN.Text = "";
                            maskedTextBox5.Text = "";
                            FIO_OWN_CB_REG.Items.Clear();
                           // FIO_OWN_CB_VIS.Items.Clear();
                            FIO_OWN_CB_PET.Items.Clear();
                            DEL_FIO_OWN_CB_OWN.Items.Clear();
                            Owners_Combobox();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else MessageBox.Show("Такой владелец уже есть");
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            check = true;
            string queryForCheck = "Select FIO_Doctor  from Doctors";
            adapter = new SqlDataAdapter(queryForCheck, connectionString);

            dt = new DataTable();

            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].ItemArray[0].ToString() == FIO_DOC_TB_DOC.Text)
                {
                    check = false;
                }
            }
            if (FIO_DOC_TB_DOC.Text != "" && SPEC_TB_DOC.Text != "" && EX_TB_DOC.Text != "")
            {
                if (check == true)
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            string Sql1 = $"insert into Doctors  values  (N'" + (FIO_DOC_TB_DOC.Text) + "',N'" + SPEC_TB_DOC.Text + "'," + EX_TB_DOC.Text + ")";
                            SqlCommand cmd = new SqlCommand(Sql1, connection);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Добавлено");
                            Doctors();
                            FIO_DOC_TB_DOC.Text = "";
                            EX_TB_DOC.Text = "";
                            SPEC_TB_DOC.Text = "";
                            FIO_DOC_CB_REG.Items.Clear();
                           // FIO_DOC_CB_VIS.Items.Clear();
                            DEL_FIO_DOC_CB_DOC.Items.Clear();
                            Doctors_Combobox_FIO();
                            Doctors_Combobox_Speciality();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else MessageBox.Show("Такой врач уже есть");
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }

       /* private void button8_Click(object sender, EventArgs e)
        {
            check = true;
            string queryForCheck = "Select Date_Of_Visit, Time_Of_Visit from Visits";
            adapter = new SqlDataAdapter(queryForCheck, connectionString);

            dt = new DataTable();

            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((dt.Rows[i]["Date_Of_Visit"].ToString() == maskedTextBox4.Text) && (dt.Rows[i]["Time_Of_Visit "].ToString() == maskedTextBox3.Text))
                {
                    check = false;
                }
            }
            if (PET_NAME_CB_VIS.Text != "" && FIO_OWN_CB_VIS.Text != "" && FIO_DOC_CB_VIS.Text != "" && maskedTextBox4.Text != "" && maskedTextBox3.Text != "" && DIAGNOSIS_TB_VIS.Text != "")
            {
                if (check == true)
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand($"select ID_Pet from Pets where Pet_Name = N'{PET_NAME_CB_VIS.Text}'", connection);
                            var petid = Convert.ToInt32(cmd.ExecuteScalar());
                            cmd = new SqlCommand($"select ID_Owner from Owners where FIO_Owner = N'{FIO_OWN_CB_VIS.Text}'", connection);
                            var ownid = Convert.ToInt32(cmd.ExecuteScalar());
                            cmd = new SqlCommand($"select ID_Doctor from Doctors where FIO_Doctor = N'{FIO_DOC_CB_VIS.Text}'", connection);
                            var docid = Convert.ToInt32(cmd.ExecuteScalar());
                            cmd = new SqlCommand($"exec insertVisites {docid},{ownid},{petid},'{maskedTextBox4.Text}','{maskedTextBox3.Text}','{DIAGNOSIS_TB_VIS.Text}';", connection);
                            cmd.ExecuteNonQuery();
                      
                            MessageBox.Show("Добавлено");
                            Vizits();
                            Owners_Combobox();
                         
                            FIO_OWN_CB_VIS.Items.Clear();
                            FIO_OWN_CB_VIS.Text = "";
                            Owners_Combobox();
                            DEL_ID_VIS_CB_VIS.Items.Clear();
                            Visitss_Combobox();



                            PET_NAME_CB_VIS.Text = "";
                            FIO_DOC_CB_VIS.Text = "";
                            maskedTextBox3.Text = "";
                            maskedTextBox4.Text = "";
                            DIAGNOSIS_TB_VIS.Text = "";

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else MessageBox.Show("Запись на это время уже есть");
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }*/

        private void button2_Click(object sender, EventArgs e)
        {
             if (DEL_ID_REG_CB_REG.SelectedIndex != -1)
            {
                DialogResult dialogResult = MessageBox.Show("Вы действительно хотите удалить данную запись?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {

                    using (var connection = new SqlConnection(connectionString))
                    {
                        try

                        {
                            string sql = $" delete from Registers where ID_Register = '{DEL_ID_REG_CB_REG.Text}' ";
                            SqlCommand cmd = new SqlCommand(sql, connection);

                            connection.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Удалено");
                            Registration();
                            DEL_ID_REG_CB_REG.Items.Clear();
                            Registers_Combobox();
                            DEL_ID_REG_CB_REG.Text = "";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else MessageBox.Show("Выберите элемент");
        }

       /* private void button7_Click(object sender, EventArgs e)
        {
            if (DEL_ID_VIS_CB_VIS.SelectedIndex != -1)
            {
                DialogResult dialogResult = MessageBox.Show("Вы действительно хотите удалить данный визит?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {

                    using (var connection = new SqlConnection(connectionString))
                    {
                        try

                        {
                            string sql = $" delete from Visits where ID_Visits  = '{DEL_ID_VIS_CB_VIS.Text}' ";
                            SqlCommand cmd = new SqlCommand(sql, connection);

                            connection.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Удалено");
                            Vizits();
                            DEL_ID_VIS_CB_VIS.Items.Clear();
                            Visitss_Combobox();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else MessageBox.Show("Выберите элемент");
        }*/

        private void button4_Click(object sender, EventArgs e)
        {
            if (DEL_FIO_OWN_CB_OWN.SelectedIndex != -1)
            {
                DialogResult dialogResult = MessageBox.Show("Вы действительно хотите удалить владельца?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {

                    using (var connection = new SqlConnection(connectionString))
                    {
                        try

                        {
                            string sql = $" delete from Owners where FIO_Owner   = N'{DEL_FIO_OWN_CB_OWN.Text}' ";
                            SqlCommand cmd = new SqlCommand(sql, connection);

                            connection.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Удалено");
                            DEL_FIO_OWN_CB_OWN.Text = "";
                            DEL_FIO_OWN_CB_OWN.Items.Clear();
                            Owners_Combobox();
                            FIO_OWN_CB_PET.Items.Clear();
                            Owners_Combobox();
                            Owners();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else MessageBox.Show("Выберите элемент");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (DEL_ID_PET_CB_PET.SelectedIndex != -1)
            {
                DialogResult dialogResult = MessageBox.Show("Вы действительно хотите удалить это животное?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {

                    using (var connection = new SqlConnection(connectionString))
                    {
                        try

                        {
                            connection.Open();
                            string sql = $"delete from Registers where ID_Pet = '{DEL_ID_PET_CB_PET.Text}' ";


                            SqlCommand cmd = new SqlCommand(sql, connection);
                            cmd.ExecuteNonQuery();

                            //cmd = new SqlCommand($"select Pet_Name from Pets where ID_Pet = '{comboBox10.Text}'", connection);
                            //var petid = Convert.ToInt32(cmd.ExecuteScalar());
                            cmd = new SqlCommand($" delete from Visits where ID_Pet  = '{DEL_ID_PET_CB_PET.Text}' ", connection);
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand($" delete from Pets where ID_Pet  = '{DEL_ID_PET_CB_PET.Text}' ", connection);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Удалено");
                            Pets();
                            //Vizits();
                            Registration();
                            DEL_ID_PET_CB_PET.Text = "";
                            DEL_ID_PET_CB_PET.Items.Clear();
                            Pets_Combobox();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                       connection.Close();
                    }
                }
            }
            else MessageBox.Show("Выберите элемент");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (DEL_FIO_DOC_CB_DOC.SelectedIndex != -1)
            {
                DialogResult dialogResult = MessageBox.Show("Вы действительно хотите удалить врача?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {

                    using (var connection = new SqlConnection(connectionString))
                    {
                        try

                        {
                            string sql = $" delete from Doctors where FIO_Doctor = N'{DEL_FIO_DOC_CB_DOC.Text}' ";
                            SqlCommand cmd = new SqlCommand(sql, connection);

                            connection.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Удалено");
                            DEL_FIO_DOC_CB_DOC.Text = "";
                            FIO_DOC_CB_REG.Items.Clear();
                           // FIO_DOC_CB_VIS.Items.Clear();
                            DEL_FIO_DOC_CB_DOC.Items.Clear();
                            Doctors_Combobox_FIO();
                            Doctors();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else MessageBox.Show("Выберите элемент");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (PET_NAME_TB_PET.Text != "" && TYPE_PET_TB_PET.Text != "" && FIO_OWN_CB_PET.Text != "" && maskedTextBox6.Text != "" && BREED_TB_PET.Text != "" && SEX_TB_PET.Text != "" && CC_TB_PET.Text != "" && ST_TB_PET.Text != "")
            {
                if (check == true)
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                           SqlCommand cmd = new SqlCommand($"select ID_Owner from Owners where FIO_Owner = N'{FIO_OWN_CB_PET .Text}'", connection);
                            var ownid = Convert.ToInt32(cmd.ExecuteScalar());
                            string Sql1 = $"exec insertPets N'{TYPE_PET_TB_PET.Text}', N'{PET_NAME_TB_PET.Text}', N'{maskedTextBox6.Text}', N'{BREED_TB_PET.Text}', N'{SEX_TB_PET.Text}', N'{CC_TB_PET.Text}', N'{ST_TB_PET.Text}',{ownid}";
                            cmd = new SqlCommand(Sql1, connection);
                            cmd.ExecuteNonQuery();
                            Pets();
                            MessageBox.Show("Добавлено");

                            PET_NAME_TB_PET.Text = "";
                            TYPE_PET_TB_PET.Text = "";
                            FIO_OWN_CB_PET.Text = "";
                            maskedTextBox6.Text = "";
                            BREED_TB_PET.Text = "";
                            SEX_TB_PET.Text = "";
                            CC_TB_PET.Text = "";
                            ST_TB_PET.Text = "";

                            FIO_OWN_CB_PET.Items.Clear();
                            DEL_ID_PET_CB_PET.Items.Clear();
                            PET_NAME_CB_REG.Items.Clear();
                            //PET_NAME_CB_VIS.Items.Clear();
                            NamePets_Combobox();
                            Owners_Combobox();
                            Pets_Combobox();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else MessageBox.Show("Запись на это время уже есть");
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            adapter = new SqlDataAdapter("select count(ID_Pet) as 'Количество пациентов за сегодня' from Visits where Date_Of_Visit='02.12.2020'", connectionString);
            dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView6.DataSource = dataset.Tables[0];

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("select FIO_Owner as 'Владельцы без животных' from Owners where ID_Owner not in (select ID_Owner from Pets)", connectionString);
            dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView6.DataSource = dataset.Tables[0];
        }

        private void button11_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("select Est_Date_Of_Visit from Registers where ID_Register=(select max(ID_Register) from Registers)", connectionString);
            dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView6.DataSource = dataset.Tables[0];
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("select Est_Date_Of_Visit from Registers where ID_Register=(select min(ID_Register) from Registers)", connectionString);
            dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView6.DataSource = dataset.Tables[0];
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("select Date_Of_Visit from Visits where ID_Visits = (select max(ID_Visits) from Visits)", connectionString);
            dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView6.DataSource = dataset.Tables[0];
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("select Date_Of_Visit from Visits where ID_Visits = (select min(ID_Visits) from Visits)", connectionString);
            dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView6.DataSource = dataset.Tables[0];
        }

        private void PET_NAME_CB_REG_SelectedIndexChanged(object sender, EventArgs e)
        {
            FIO_OWN_CB_REG.Items.Clear();
            string Owners = $"Select FIO_Owner from Owners where ID_Owner =(select ID_Owner from Pets where Pet_Name =N'{PET_NAME_CB_REG.Text}')";
            adapter = new SqlDataAdapter(Owners, connectionString);

            dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIO_OWN_CB_REG.Items.Add(dt.Rows[i]["FIO_Owner"]);
            }

            }

        private void DEL_ID_REG_CB_REG_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string month;
            month = monthCalendar1.SelectionRange.Start.ToString();
            label21.Text = month;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label19.Left = panel1.Width / 2 - label19.Width / 2;
            monthCalendar1.Left = panel1.Width / 2 - monthCalendar1.Width / 2;
        }
    }
}
