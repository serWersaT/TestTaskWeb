using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using TestTask.DAL.Interfaces;
using TestTask.DAL.Models;
using System.IO;

namespace TestTask.DAL.DB
{
    public class GeneralTable : IDisposable, Icruid<GeneralTableModel>
    {
        private SQLiteConnection conn;
        public GeneralTable()
        {
            string baseName = Directory.GetCurrentDirectory() + "/GeneralTable.db3";
            //Dispose();
#if DEBUG
            baseName = @"D:\С# примеры\TestTask\TestTask.Web\TestTask.DAL\DBSQL\GeneralTable.db3";
#endif
            if (!File.Exists(baseName))
            {

                using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = " + baseName))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"CREATE TABLE [GeneralTable] (
                                [Id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            [RowNumber] Integer NOT NULL,
                                            [Code] char(100) NOT NULL,
                                            [Description] char(100) NOT NULL
                                );";
                        command.CommandType = System.Data.CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
            }
            conn = new SQLiteConnection($"Data Source = " + baseName);
            conn.Open();
        }


        public bool Insert(List<GeneralTableModel> model)
        {
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand($@"Delete FROM GeneralTable"))
                {
                    cmd.Connection = conn;
                    SQLiteDataReader reader = cmd.ExecuteReader();
                }


                using (SQLiteCommand command = new SQLiteCommand())
                {
                    model = model.OrderBy(x => x.Code).ToList();
                    foreach (var x in model)
                    {
                        command.CommandText = $@"INSERT INTO GeneralTable(RowNumber, Code, Description)
                                        VALUES ({x.RowNumber}, '{x.Code}', '{x.Description}')";
                        command.CommandType = System.Data.CommandType.Text;
                        command.Connection = conn;
                        command.ExecuteNonQuery();
                    }                    
                }
                return true;
            }
            catch(Exception ex) {
                return false;
            }
        }

        public void Delete(int id)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"DELETE FROM GeneralTable
                                         WHERE Id = {id}"))
            {
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
        }

        public bool Update(GeneralTableModel model)
        {

            using (SQLiteCommand command = new SQLiteCommand($@"UPDATE GeneralTable 
                                                                SET 
                                                                RowNumber = {model.RowNumber},
                                                                Code = '{model.Code}',
                                                                Description = '{model.Description}'
                                                            WHERE Id = {model.Id}"))
            {
                try
                {
                    command.Connection = conn;
                    command.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }

        public int Count()
        {
            int count = 0;
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM GeneralTable"))
            {
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                count = (reader.Read()) ? reader.GetInt32(0) : 0;
                reader.Close();
            }
            return count;
        }

        public List<GeneralTableModel> SelectWhere(string where)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM GeneralTable b  WHERE a.id >= b.id) AS cnt,
                                                                RowNumber,
                                                                Code,
                                                                Description
                                                                FROM GeneralTable a   
                                                                where " + where +
                                                                "order by Id"))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }


        public List<GeneralTableModel> Select(int? topmin, int? topmax)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM GeneralTable b  WHERE a.id >= b.id) AS cntRow,
                                                                RowNumber,
                                                                Code,
                                                                Description
                                                                FROM GeneralTable a   
                                                                where cntRow >= {topmin}
                                                                and cntRow <= {topmax}
                                                                order by Id"))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }

        private List<GeneralTableModel> Reader(SQLiteDataReader reader)
        {
            var ListGeneral = new List<GeneralTableModel>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var wd = new GeneralTableModel();
                    wd.Id = reader.GetInt32(0);
                    wd.cntNumber = reader.GetInt32(1);
                    wd.RowNumber = reader.GetInt32(2);
                    wd.Code = reader.GetString(3);
                    wd.Description = reader.GetString(4);
                    ListGeneral.Add(wd);
                }
                reader.Close();
            }
            return ListGeneral;
        }


        public void Dispose()
        {
            conn.Close();
            conn.Dispose();
        }

    }
}
