using System.Data;
using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using PagedList;

namespace Example2
{
    class ContactDB : DataAccessLayer, IDisposable
    {
        SQLiteConnection con = default(SQLiteConnection);
        string cs = @"URI=file:test.db";
        public ContactDB()
        {
            con = new SQLiteConnection(cs);
            con.Open();
            PrepareDB();
        }

        public void Dispose()
        {
            con.Close();
        }

        private void ExecuteNonQuery(string commandText)
        {
            var cmd = new SQLiteCommand(con);
            cmd.CommandText = commandText;
            cmd.ExecuteNonQuery();
        }
        private void PrepareDB()
        {
            //SQLiteConnection.CreateFile("test.db");
            ExecuteNonQuery("DROP TABLE IF EXISTS contacts");
            ExecuteNonQuery("CREATE TABLE contacts(id STRING PRIMARY KEY, name TEXT, phone TEXT, address TEXT)");
        }

        public string CreateContact(ContactDTO contact)
        {
            string text = string.Format("INSERT INTO contacts(id, name, phone, address) VALUES('{0}', '{1}', '{2}', '{3}')"
                , contact.Id,
                contact.Name,
                contact.Phone,
                contact.Address);

            ExecuteNonQuery(text);
            return contact.Id;
        }

        public bool DelecteContactById(string id)
        {
            ExecuteNonQuery(string.Format("DELETE FROM contacts WHERE id = '{0}'", id));
            return false;
        }

        public List<ContactDTO> GetAllContacts(bool sorted)
        {
            List<ContactDTO> res = new List<ContactDTO>();

            string selectSql = @"SELECT * FROM contacts";
            if (sorted)
            {
                selectSql = @"SELECT * FROM contacts ORDER BY name";
            }
            using (SQLiteCommand command = new SQLiteCommand(selectSql, con))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ContactDTO
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Phone = reader.GetString(2),
                        Address = reader.GetString(3)
                    };

                    res.Add(item);
                }
            }
            return res;
        }

        public ContactDTO GetContactById(string id)
        {
            var cmd = new SQLiteCommand(con);
            cmd.CommandText = string.Format("SELECT * FROM contacts WHERE id = '{0}'", id);
            var reader = cmd.ExecuteReader();
            reader.Read();
            return new ContactDTO
            {
                Id = reader.GetString(0),
                Name = reader.GetString(1),
                Phone = reader.GetString(2),
                Address = reader.GetString(3)
            };
        }

        public List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        public ContactDTO GetContactByRowIndex(int rowIndex)
        {
            var cmd = new SQLiteCommand(con);
            cmd.CommandText = string.Format("SELECT * FROM contacts LIMIT 1 OFFSET {0}", rowIndex);
            var reader = cmd.ExecuteReader();
            reader.Read();
            return new ContactDTO
            {
                Id = reader.GetString(0),
                Name = reader.GetString(1),
                Phone = reader.GetString(2),
                Address = reader.GetString(3)
            };
        }

        public void EditContactById(string id, string name, string phone, string address)
        {
            ExecuteNonQuery(string.Format("UPDATE contacts SET name = '{1}', phone = '{2}', address = '{3}' WHERE id = '{0}'", id, name, phone, address));
        }

        public List<ContactDTO> GetAllContacts(string name)
        {
            List<ContactDTO> res = new List<ContactDTO>();

            //string selectSql = @"select * from contacts";
            string selectSql = string.Format("SELECT * FROM contacts WHERE name LIKE '{0}%'", name);
            using (SQLiteCommand command = new SQLiteCommand(selectSql, con))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ContactDTO
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Phone = reader.GetString(2),
                        Address = reader.GetString(3)
                    };

                    res.Add(item);
                }
            }
            return res;
        }
    }
}
