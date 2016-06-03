using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Persons;

namespace Brokers
{
    public class Broker
    {
        SqlConnection _connection;
        SqlConnectionStringBuilder _connectionStringBuilder;

        private void ConnectTo()
        {
            _connectionStringBuilder = new SqlConnectionStringBuilder();
            _connectionStringBuilder.DataSource = "DESKTOP-2JT2JT9";
            _connectionStringBuilder.InitialCatalog = "UPP-1";
            _connectionStringBuilder.IntegratedSecurity = true;

            _connection = new SqlConnection(_connectionStringBuilder.ToString());
        }

        public Broker()
        {
            ConnectTo();
        }

        public void Insert(Person p)
        {
            try
            {
                var cmdText =
                    "INSERT INTO  [UPP-1].[dbo].[Persons](Имя, Фамилия, Класс) VALUES(@Имя, @Фамилия, @Класс)";
                 

                var cmd = new SqlCommand(cmdText, _connection);
                cmd.Parameters.AddWithValue("@Имя", p.FirstName);
                cmd.Parameters.AddWithValue("@Фамилия", p.LastName);
                cmd.Parameters.AddWithValue("@Класс", p.Class);

                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }

            try
            {
                var cmdText =
                "INSERT INTO  [UPP-1].[dbo].[Extended](Успеваемость, Пропуски) VALUES(@Успеваемость, @Пропуски)";

                var cmd = new SqlCommand(cmdText, _connection);
                
                cmd.Parameters.AddWithValue("@Успеваемость", p.Progress);
                cmd.Parameters.AddWithValue("@Пропуски", p.Omissions);

                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
        }

        public void Insert_Control(Persons.Control c)
        {
            try
            {
                var cmdText =
                    "INSERT INTO [UPP-1].[dbo].[Control](Дата, Урок, Оценка, Присутствие, ID_Ученика) VALUES(@Дата, @Урок, @Оценка, @Присутствие, @ID_Ученика)";


                var cmd = new SqlCommand(cmdText, _connection);
                cmd.Parameters.AddWithValue("@Дата", c.Date);
                cmd.Parameters.AddWithValue("@Урок", c.Lesson);
                cmd.Parameters.AddWithValue("@Оценка", c.Mark);
                cmd.Parameters.AddWithValue("@Присутствие", c.Presence);
                cmd.Parameters.AddWithValue("@ID_Ученика", c.ID_student);

                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
        }

        public List<Person> FillComboBox()
        {
            var personsList = new List<Person>();

            try
            {
                var cmdText = "SELECT * FROM [UPP-1].[dbo].[Persons]";
                var cmd = new SqlCommand(cmdText, _connection);
                _connection.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var p = new Person();

                    p.ID = int.Parse(reader["ID"].ToString());
                    p.FirstName = reader["Имя"].ToString();
                    p.LastName = reader["Фамилия"].ToString();
                    p.Class = Convert.ToInt32(reader["Класс"].ToString());

                    personsList.Add(p);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
            return personsList;
        }

        public List<Persons.Control> FillComboBox_Control()
        {
            var controlList = new List<Persons.Control>();

            try
            {
                var cmdText = "SELECT * FROM [UPP-1].[dbo].[Control]";
                var cmd = new SqlCommand(cmdText, _connection);
                _connection.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var c = new Persons.Control();

                    c.ID = int.Parse(reader["ID"].ToString());
                    c.Date = Convert.ToDateTime(reader["Дата"].ToString());
                    c.Lesson = reader["Урок"].ToString();
                    c.Mark = Convert.ToInt32(reader["Оценка"].ToString());
                    c.Presence = Convert.ToBoolean(reader["Присутствие"].ToString());
                    c.ID_student = Convert.ToInt32(reader["ID_Ученика"].ToString());

                    controlList.Add(c);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
            return controlList;
        }

        public void Update(Person oldPerson, Person newPerson)
        {
            try
            {
                var cmdText =
                    "UPDATE [UPP-1].[dbo].[Persons] SET Имя =  @Имя, Фамилия = @Фамилия, Класс = @Класс WHERE ID = @ID";
                var cmd = new SqlCommand(cmdText, _connection);
                cmd.Parameters.AddWithValue("@Имя", newPerson.FirstName);
                cmd.Parameters.AddWithValue("@Фамилия", newPerson.LastName);
                cmd.Parameters.AddWithValue("@Класс", newPerson.Class);
                cmd.Parameters.AddWithValue("@ID", oldPerson.ID);

                _connection.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }

            try
            {
                var cmdText =
                    "UPDATE [UPP-1].[dbo].[Extended] SET Пропуски =  @Пропуски, Успеваемость = @Успеваемость WHERE ID = @ID";
                var cmd = new SqlCommand(cmdText, _connection);
                cmd.Parameters.AddWithValue("@Пропуски", newPerson.Omissions);
                cmd.Parameters.AddWithValue("@Успеваемость", newPerson.Progress);

                _connection.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
        }

        public void Update_Control(Persons.Control oldControl, Persons.Control newControl)
        {
            try
            {
                var cmdText =
                    "UPDATE [UPP-1].[dbo].[Control] SET Дата =  @Дата, Урок = @Урок, Оценка = @Оценка, Присутствие = @Присутствие, ID_Ученика = @ID_Ученика WHERE ID = @ID";

                var cmd = new SqlCommand(cmdText, _connection);
                cmd.Parameters.AddWithValue("@Дата", newControl.Date);
                cmd.Parameters.AddWithValue("@Урок", newControl.Lesson);
                cmd.Parameters.AddWithValue("@Оценка", newControl.Mark);
                cmd.Parameters.AddWithValue("@Присутствие", newControl.Presence);
                cmd.Parameters.AddWithValue("@ID_Ученика", newControl.ID_student);
                cmd.Parameters.AddWithValue("@ID", oldControl.ID);

                _connection.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
        }

        public void Delete(Person p)
        {
            try
            {

                var cmdText = "DELETE FROM  [UPP-1].[dbo].[Persons] WHERE ID = @ID";
                var cmd = new SqlCommand(cmdText, _connection);
                cmd.Parameters.AddWithValue("@ID", p.ID);

                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }

            try
            {

                var cmdText = "DELETE FROM  [UPP-1].[dbo].[Extended] WHERE ID = @ID";
                var cmd = new SqlCommand(cmdText, _connection);
                cmd.Parameters.AddWithValue("@ID", p.ID);

                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
        }

        public void Delete_Control(Persons.Control c)
        {
            try
            {

                var cmdText = "DELETE FROM  [UPP-1].[dbo].[Control] WHERE ID = @ID";
                var cmd = new SqlCommand(cmdText, _connection);
                cmd.Parameters.AddWithValue("@ID", c.ID);

                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
        }

        public List<Person> Clear()
        {
            var personsList = new List<Person>();

            try
            {
                var cmdText = "TRUNCATE TABLE [UPP-1].[dbo].[Persons]";
                var cmd = new SqlCommand(cmdText, _connection);
                _connection.Open();
                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var p = new Person();

                    p.ID = int.Parse(reader["ID"].ToString());
                    p.FirstName = reader["Имя"].ToString();
                    p.LastName = reader["Фамилия"].ToString();
                    p.Class = Convert.ToInt32(reader["Класс"].ToString());
                    personsList.Add(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }

            try
            {
                var cmdText = "TRUNCATE TABLE [UPP-1].[dbo].[Extended]";
                var cmd = new SqlCommand(cmdText, _connection);
                _connection.Open();
                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var p = new Person();

                    p.ID = int.Parse(reader["ID"].ToString());
                    p.Omissions = Convert.ToInt32(reader["Пропуски"].ToString());
                    p.Progress = reader["Успеваемость"].ToString();
                    personsList.Add(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
            return personsList;
        }

        public List<Persons.Control> Clear_Control()
        {
            var controlList = new List<Persons.Control>();

            try
            {
                var cmdText = "TRUNCATE TABLE [UPP-1].[dbo].[Control]";
                var cmd = new SqlCommand(cmdText, _connection);
                _connection.Open();
                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var c = new Persons.Control();

                    c.ID = int.Parse(reader["ID"].ToString());
                    c.Date = Convert.ToDateTime(reader["Дата"].ToString());
                    c.Lesson = reader["Урок"].ToString();
                    c.Mark = Convert.ToInt32(reader["Оценка"].ToString());
                    c.Presence = Convert.ToBoolean(reader["Присутствие"].ToString());
                    c.ID_student = Convert.ToInt32(reader["ID_Ученика"].ToString());
                    controlList.Add(c);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
           
            return controlList;
        }

        public object Rebuild()
        {
            var personsList = new List<Person>();

            try
            {
                var cmdText = "SELECT * FROM  [UPP-1].[dbo].[Persons]";
                var cmd = new SqlCommand(cmdText, _connection);
                _connection.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var p = new Person();

                    p.ID = int.Parse(reader["ID"].ToString());
                    p.FirstName = reader["Имя"].ToString();
                    p.LastName = reader["Фамилия"].ToString();
                    p.Class = Convert.ToInt32(reader["Класс"].ToString());
                    personsList.Add(p);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
            return personsList;
        }

        public DataTable JoinReader(int? id)
        {
            try
            {
                var cmdText =
                    "SELECT * from [UPP-1].[dbo].[Extended] E WHERE ID=" + id;
                    //"SELECT dbo.Extended.ID, Успеваемость, dbo.Extended.Пропуски FROM dbo.Extended RIGHT JOIN dbo.Persons ON dbo.Persons.ID = dbo.Extended.ID WHere dbo.Persons.ID = dbo.Extended.ID";
                var cmd = new SqlCommand(cmdText, _connection);
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
            return null;
        }

        public DataTable JoinReaderControl_1(int? id)
        {
            try
            {
                var cmdText =
                    "SELECT * from [UPP-1].[dbo].[Persons] P WHERE ID = " + id;
                //"SELECT dbo.Extended.ID, Успеваемость, dbo.Extended.Пропуски FROM dbo.Extended RIGHT JOIN dbo.Persons ON dbo.Persons.ID = dbo.Extended.ID WHere dbo.Persons.ID = dbo.Extended.ID";
                var cmd = new SqlCommand(cmdText, _connection);
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
            return null;
        }

        public DataTable JoinReaderControl_2(int? id)
        {
            try
            {
                var cmdText =
                    "SELECT * from [UPP-1].[dbo].[Extended] E WHERE ID = " + id;
                //"SELECT dbo.Extended.ID, Успеваемость, dbo.Extended.Пропуски FROM dbo.Extended RIGHT JOIN dbo.Persons ON dbo.Persons.ID = dbo.Extended.ID WHere dbo.Persons.ID = dbo.Extended.ID";
                var cmd = new SqlCommand(cmdText, _connection);
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
            return null;
        }

        public DataTable JoinReaderHistory(int? id)
        {
            try
            {
                var cmdText =
                    "SELECT * from [UPP-1].[dbo].[Control] E WHERE ID_Ученика =" + id;
                //"SELECT * FROM dbo.Control INNER JOIN dbo.Persons ON dbo.Control.ID_Ученика = dbo.Persons.ID WHere dbo.Persons.ID = dbo.Control.ID_Ученика";
                var cmd = new SqlCommand(cmdText, _connection);
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
            return null;
        }
    }
}
