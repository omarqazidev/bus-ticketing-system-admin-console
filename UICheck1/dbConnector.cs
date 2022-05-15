using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace UICheck1 {
    class dbConnector {
        String connectionString;
        MySqlConnection connection;
        MySqlCommand command;
        String textArea = "";


        private void startConnection() {

            connectionString = "Server=localhost;Port=3306;Database=dbBooking;UID=root;Password=";//12345678
            connection = new MySqlConnection(connectionString);

        }

        public String readFromDB(String tableName, String columnName) {
            startConnection();
            command = connection.CreateCommand();
            command.CommandText = "select * From " + tableName;

            try {
                connection.Open();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                textArea = textArea + "\n" + reader[columnName].ToString();
            }
            return textArea;
        }

        public Boolean checkCredentials(String enteredUsername, String enteredPassword) {

            startConnection();
            command = connection.CreateCommand();
            command.CommandText = "select * From " + "administrator";
            Boolean result = false;

            List<String> usernames = new List<String>();
            List<String> passwords = new List<String>();

            startConnection();
            command = connection.CreateCommand();
            command.CommandText = "select * from administrator";

            try {
                connection.Open();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

           
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                usernames.Add(reader.GetString(1));
                passwords.Add(reader.GetString(2));
            }

            for(int i = 0; i < usernames.Count; i++) {
                if (enteredUsername == usernames.ElementAt(i)) {
                    if (enteredPassword == passwords.ElementAt(i)) {
                        result = true;
                    }
                }
            }

            return result;
        }

        public int countRecords(String tableName) {

            startConnection();
            command = connection.CreateCommand();
            command.CommandText = "select * From " + tableName;

            try {
                connection.Open();
                int result = int.Parse(command.ExecuteScalar().ToString());
                return result;

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            } finally {

                connection.Close();
            }

            return 1;
        }



        public DataSet getInGrid(String tableName) {

            if (countRecords(tableName) > 0) {
                startConnection();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("select * from " + tableName, connection)) {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            } else {
                DataSet ds = new DataSet();
                return ds;
            }

        }

        public List<String> getTableNames() {

            List<String> tableNames = new List<String>();
            startConnection();
            command = connection.CreateCommand();
            command.CommandText = "show full tables where Table_Type = 'BASE TABLE'";

            try {
                connection.Open();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }


            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                tableNames.Add(reader.GetString(0));
            }
            return tableNames;

        }

        public List<String> getAttributeNames(String tableName) {

            List<String> attributeNames = new List<String>();
            startConnection();
            command = connection.CreateCommand();
            command.CommandText = "SHOW COLUMNS from " + tableName;

            try {
                connection.Open();

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }


            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                attributeNames.Add(reader.GetString(0));
            }
            return attributeNames;

        }



        public Boolean insertData(String tableName, List<String> dataToInsert) {

            Boolean result = false;
            String insertionQuery = "";
            String valuesToInsert = "";


            for (int i = 0; i < dataToInsert.Count; i++) {

                if (i == (dataToInsert.Count - 1)) {

                    if (dataToInsert.ElementAt(i) == "") {
                        valuesToInsert = valuesToInsert + "NULL";
                    } else {
                        valuesToInsert = valuesToInsert + "'" + dataToInsert.ElementAt(i) + "'";
                    }

                } else {

                    if (dataToInsert.ElementAt(i) == "") {
                        valuesToInsert = valuesToInsert + "NULL" + ", ";
                    } else {
                        valuesToInsert = valuesToInsert + "'" + dataToInsert.ElementAt(i) + "', ";
                    }

                }
            }

            insertionQuery = "INSERT INTO " + tableName + " VALUES (" + valuesToInsert + ");";


            startConnection();
            command = connection.CreateCommand();
            command.CommandText = insertionQuery;

            try {
                connection.Open();
                command.ExecuteNonQuery();
                result = true;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }

            return result;
        }




        public Boolean updateData(String tableName, List<String> attributes, List<String> updatedData, List<String> beforeUpdation) {

            Boolean result = false;
            String updationQuery = "";

            updationQuery = "Update " + tableName + " SET ";

            for (int i = 0; i < updatedData.Count; i++) {

                String toAppend = "";
                toAppend = attributes.ElementAt(i) + " = ";

                if (updatedData.ElementAt(i) == "") {

                    toAppend = toAppend + "NULL";

                } else if (updatedData.ElementAt(i) != "") {

                    toAppend = toAppend + "'" + updatedData.ElementAt(i) + "'";
                }
                updationQuery = updationQuery + toAppend + ", ";
            }

            updationQuery = updationQuery.Substring(0, updationQuery.Length - 2);
            updationQuery = updationQuery + " WHERE " +
                attributes.ElementAt(0) + " = '" + beforeUpdation.ElementAt(0) + "'";
           
            startConnection();
            command = connection.CreateCommand();
            command.CommandText = updationQuery;

            try {
                connection.Open();
                command.ExecuteNonQuery();
                result = true;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }

            return result;
           
        }



        public Boolean deleteData(String tableName, String attributeName, String attributeValue) {

            Boolean result = false;
            String deletionQuery = "";

            deletionQuery = "DELETE FROM " + tableName + " WHERE " +
                attributeName + " = '" + attributeValue + "'";

            MessageBox.Show(deletionQuery);

            startConnection();
            command = connection.CreateCommand();
            command.CommandText = deletionQuery;

            try {
                connection.Open();
                command.ExecuteNonQuery();
                result = true;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }


            return result;
        }



        public DataSet getCustomTable(String nameOfTable) {

            if (countRecords(nameOfTable) > 0) {
                startConnection();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("select * from " + nameOfTable, connection)) {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            } else {
                DataSet ds = new DataSet();
                return ds;
            }
        }

        public Boolean callDiscountProcedure(String discountPercentage, String trip_id, String trip_fare) {
            Boolean result = false;
            String discountQuery = "";

            discountQuery = "Call procedureDiscountFare(" + discountPercentage + ", " +
                trip_id + ", " + trip_fare + ");";

            //MessageBox.Show(discountQuery);

            startConnection();
            command = connection.CreateCommand();
            command.CommandText = discountQuery;

            try {
                connection.Open();
                command.ExecuteNonQuery();
                result = true;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }



            return result;
        }

            

    }
}







