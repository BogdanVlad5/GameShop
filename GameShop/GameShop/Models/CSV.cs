using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using GameShop.Factory;
using Microsoft.Ajax.Utilities;

namespace GameShop.Factory
{
    public class CSV
    {

        /// To keep the ordered list of column names

        List<string> fields = new List<string>();


        /// The list of rows

        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();


        /// The current row

        Dictionary<string, object> currentRow { get { return rows[rows.Count - 1]; } }


        /// Set a value on this column

        public object this[string field]
        {
            set
            {
                // Keep track of the field names, because the dictionary loses the ordering
                if (!fields.Contains(field)) fields.Add(field);
                currentRow[field] = value;
            }
        }


        /// Call this before setting any fields on a row

        public void AddRow()
        {
            rows.Add(new Dictionary<string, object>());
        }


        /// Add a list of typed objects, maps object properties to CsvFields

        public void AddRows<T>(IEnumerable<T> list)
        {
            if (list.Any())
            {
                foreach (var obj in list)
                {
                    AddRow();
                    var values = obj.GetType().GetProperties();
                    foreach (var value in values)
                    {
                        this[value.Name] = value.GetValue(obj, null);
                    }
                }
            }
        }


        /// Converts a value to how it should output in a csv file
        /// If it has a comma, it needs surrounding with double quotes
        /// Eg Sydney, Australia -> "Sydney, Australia"
        /// Also if it contains any double quotes ("), then they need to be replaced with quad quotes[sic] ("")
        /// Eg "Dangerous Dan" McGrew -> """Dangerous Dan"" McGrew"

        string MakeValueCsvFriendly(object value)
        {
            if (value == null) return "";
            if (value is INullable && ((INullable)value).IsNull) return "";
            if (value is DateTime)
            {
                if (((DateTime)value).TimeOfDay.TotalSeconds == 0)
                    return ((DateTime)value).ToString("yyyy-MM-dd");
                return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string output = value.ToString();
            if (output.Contains(",") || output.Contains("\"") || output.Contains("\n") || output.Contains("\r"))
                output = '"' + output.Replace("\"", "\"\"") + '"';

            return output.Length <= 32767 ? output : output.Substring(0, 32767);
        }



        /// Output all rows as a CSV returning a string

        public string Export()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("sep=,");

            // The header
            sb.Append(string.Join(",", fields.ToArray()));
            sb.AppendLine();

            // The rows
            foreach (Dictionary<string, object> row in rows)
            {
                fields.Where(f => !row.ContainsKey(f)).ToList().ForEach(k =>
                {
                    row[k] = null;
                });
                sb.Append(string.Join(",", fields.Select(field => MakeValueCsvFriendly(row[field])).ToArray()));
                sb.AppendLine();
            }

            return sb.ToString();
        }


        /// Exports to a file

        public void ExportToFile(string path)
        {
            File.WriteAllText(path, Export());
        }


    }
}