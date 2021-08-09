using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace Readability_Calculator.Models.Readability
{
    public class SyllableDictionary
    {
        private int syllables { get;  set; }
        public int GetSyllableCount(string word)
        {

            string word1 = word;
            string sql = @"select Word, SyllableCount 
                           from Syllables 
                           where Word = '"+word1+"'";

            SqlConnection connection = new SqlConnection(
                @"Server=198.30.71.41\COURSESQLEXPRESS,22433; 
                Database=CSCI2630; User Id=queryonly; Password=Wanderer2020!");

            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    syllables = reader.GetInt32(1);
                    string test = reader.GetString(0);
                }
                reader.Close();
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }

            return syllables;


            
        }
    }
}
