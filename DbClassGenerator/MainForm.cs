using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static DbClassGenerator.MainForm;

namespace DbClassGenerator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            userIdTextBox.Enabled = passwordTextBox.Enabled = userIdCheckBox.Checked;
            if (userIdCheckBox.Checked)
                userIdTextBox.Focus();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(GetConnectionString("Master")))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM sys.databases", connection);
                var reader = command.ExecuteReader();
                databaseComboBox.Items.Clear();
                while (reader.Read())
                {
                    databaseComboBox.Items.Add(reader["name"]);
                }
                databaseComboBox.Enabled = true;
            }
        }

        private string GetConnectionString(string databaseName)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = dataSourceTextBox.Text;
            connectionStringBuilder.InitialCatalog = databaseName;
            connectionStringBuilder.MultipleActiveResultSets = true;
            if (userIdCheckBox.Checked)
            {
                connectionStringBuilder.IntegratedSecurity = false;
                connectionStringBuilder.UserID = userIdTextBox.Text;
                connectionStringBuilder.Password = passwordTextBox.Text;
            }
            else
            {
                connectionStringBuilder.IntegratedSecurity = true;
            }
            return connectionStringBuilder.ConnectionString;
        }

        private void databaseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var connectionString = GetConnectionString(databaseComboBox.SelectedItem.ToString());
            using(var connection= new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.TABLES", connection);
                var reader=command.ExecuteReader();
                checkedListBox1.Items.Clear();
                while (reader.Read())
                {
                    var schema = reader["TABLE_SCHEMA"];
                    var table = reader["TABLE_NAME"];
                    checkedListBox1.Items.Add(schema + "." + table);
                }
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() != DialogResult.OK)
                return;
            
            foreach (var item in checkedListBox1.CheckedItems)
            {
                var text = item.ToString();
                var schema = text.Split('.')[0];
                var table = text.Split('.')[1];
                var columns = GetTableColumns(schema, table);
                
                GenerateEntities(folderBrowser.SelectedPath,schema,table,columns);
                GenerateRepositoryInterfaces(folderBrowser.SelectedPath,schema,table,columns);
                GenerateRepositories(folderBrowser.SelectedPath,schema,table,columns);

                Application.Exit();
            }
            
        }
        private void GenerateEntities(string generatePath, string schema,string table,List<ColumnModel> columns)
        {
            var entitesFolder = Path.Combine(generatePath, "Entities");
            if (!Directory.Exists(entitesFolder))
                Directory.CreateDirectory(entitesFolder);
            List<string> classLine = new List<string>
                {
                    "using System;",
                    "",
                    "namespace " + rootNamespaceTextBox.Text + ".Entities",
                    "{",
                    "    [DataLayer.Table(\"" + schema + "\",\"" + table + "\")]",
                    "    public class " + GetSingularName(table),
                    "    {"
                };
            foreach (var column in columns)
            {
                if (column.IsPrimaryKey)
                {
                    classLine.Add("        [DataLayer.PrimaryKey]");
                }
                if (column.IsComputed)
                {
                    classLine.Add("        [DataLayer.ComputedColumn]");
                }
                classLine.Add("        public " + ConvertSqlTypeToCLR(column.DataType, column.IsNullable) + " " + column.Name + " {get; set;}");
            }
            classLine.Add("    }");
            classLine.Add("}");

            File.WriteAllLines(Path.Combine(entitesFolder, GetSingularName(table) + ".cs"), classLine);
        }

        private void GenerateRepositoryInterfaces(string generatePath, string schema, string table, List<ColumnModel> columns)
        {
            var entitesFolder = Path.Combine(generatePath, "Abstracts");
            if (!Directory.Exists(entitesFolder))
                Directory.CreateDirectory(entitesFolder);
            List<string> classLine = new List<string>
                {
                    "using System;",
                    "using "+ rootNamespaceTextBox.Text +".Entities;",
                    "using "+ rootNamespaceTextBox.Text +".DataLayer;",
                    "using System.Collections.Generic;",
                    "",
                    "namespace " + rootNamespaceTextBox.Text + ".RepositoryAbstracts",
                    "{",
                    "    public interface I" + table + "Repository : IRepository<" + GetSingularName(table) + ">",
                    "    {"
                };
            foreach (var column in columns)
            {
                classLine.Add("        List<Entities." + GetSingularName(table) + "> GetBy" + column.Name + "(" + ConvertSqlTypeToCLR(column.DataType, column.IsNullable) + " value );");

            }
            classLine.Add("    }");
            classLine.Add("}");

            File.WriteAllLines(Path.Combine(entitesFolder, GetSingularName(table) + ".cs"), classLine);
        }

        private void GenerateRepositories(string generatePath, string schema, string table, List<ColumnModel> columns)
        {
            var entitesFolder = Path.Combine(generatePath, "Repositories");
            if (!Directory.Exists(entitesFolder))
                Directory.CreateDirectory(entitesFolder);
            List<string> classLine = new List<string>
                {
                    "using System;",
                    "using "+ rootNamespaceTextBox.Text +".Entities;",
                    "using "+ rootNamespaceTextBox.Text +".DataLayer;",
                    "using "+ rootNamespaceTextBox.Text +".RepositoryAbstracts;",
                    "using System.Collections.Generic;",
                    "using System.Data.SqlClient;",
                    "",
                    "namespace " + rootNamespaceTextBox.Text + ".Repositories",
                    "{",
                    "    public class " + table + "Repository : GenericRepository<" + GetSingularName(table) + ">,I" + table + "Repository",
                "    {"
                };
            classLine.Add("        public " + table + "Repository() : base(\"name=DbConnectionString\"){ }");
            foreach (var column in columns)
            {
                var dataType = ConvertSqlTypeToCLR(column.DataType, column.IsNullable);
                classLine.Add("        public List<Entities." + GetSingularName(table) + "> GetBy" + column.Name + "(" + ConvertSqlTypeToCLR(column.DataType, column.IsNullable) + " value )");
                classLine.Add("        {");
                if (dataType == "string")
                {
                    classLine.Add("            return RunQuery(\"SELECT * FROM [" + schema + "].[" + table + "] WHERE [" + column.Name + "] LIKE @Value\", new SqlParameter(\"Value\", value));");
                }
                else
                {
                    classLine.Add("            return RunQuery(\"SELECT * FROM [" + schema + "].[" + table + "] WHERE [" + column.Name + "] = @Value\", new SqlParameter(\"Value\", value));");
                }
                classLine.Add("        }");
            }
            classLine.Add("    }");
            classLine.Add("}");

            File.WriteAllLines(Path.Combine(entitesFolder, GetSingularName(table) + ".cs"), classLine);
        }

        private string ConvertSqlTypeToCLR(string type,bool nullable)
        {
            switch(type)
            {
                case "int":
                    return nullable ? "int?" : "int";
                case "bigint":
                    return nullable ? "long?" : "long";
                case "datetime":
                case "datetime2":
                case "date":
                    return nullable ? "DateTime?" : "DateTime";
                case "nvarchar":
                case "varchar":
                case "nchar":
                case "char":
                    return "string";
                case "bit":
                    return nullable ? "bool?" : "bool";
                case "binary":
                case "image":
                    return "byte[]";
                case "decimal":
                    return nullable ? "decimal?" : "decimal";
                case "float":
                    return nullable ? "fload?" : "fload";
            }
            return "object";
        }
        
        private string GetSingularName(string name)
        {
            if (name.EndsWith("ies"))
            {
                return name.Substring(0, name.Length - 3) + "y";
            }
            else
                return name.Substring(0, name.Length - 1);
        }

        private List<ColumnModel> GetTableColumns(string schema,string tableName)
        {
            var columns = new List<ColumnModel>();
            using (var connection = new SqlConnection(GetConnectionString(databaseComboBox.SelectedItem.ToString())))
            {
                connection.Open();
                List<string> primaryKeyColumns = new List<string>();
                var keysCommand = new SqlCommand("SELECT tc.TABLE_SCHEMA,tc.TABLE_NAME,ccu.COLUMN_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu ON ccu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME WHERE tc.TABLE_SCHEMA = N'" + schema + "' AND tc.TABLE_NAME = N'" + tableName + "' AND tc.CONSTRAINT_TYPE = N'PRIMARY KEY'", connection);
                var keysReader=keysCommand.ExecuteReader();
                while(keysReader.Read())
                {
                    primaryKeyColumns.Add(keysReader["COLUMN_NAME"].ToString());
                }

                List<string> computedColumns = new List<string>();
                var computedcommand = new SqlCommand("SELECT [name] FROM sys.columns WHERE object_id = object_id('" + schema + "." + tableName + "') AND (is_identity = 1 OR is_computed = 1)", connection);
                var computedreader = computedcommand.ExecuteReader();
                while(computedreader.Read())
                {
                    computedColumns.Add(computedreader["name"].ToString());
                }
                var command = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=N'" + schema + "' AND TABLE_NAME=N'" + tableName + "'", connection);
                var reader=command.ExecuteReader();
                while (reader.Read())
                {
                    var columnModer = new ColumnModel()
                    {
                        IsPrimaryKey = primaryKeyColumns.Any(col => col.Equals(reader["COLUMN_NAME"])),
                        IsComputed=computedColumns.Any(col => col.Equals(reader["COLUMN_NAME"])),
                        Name = reader["COLUMN_NAME"].ToString(),
                        DataType = reader["DATA_TYPE"].ToString(),
                        IsNullable = reader["IS_NULLABLE"].ToString()=="YES"
                    };
                    columns.Add(columnModer);
                }
            }
            return columns;
        }

        public class ColumnModel
        {
            public string Name { get; set; }
            public string DataType { get; set; }
            public bool IsIdentity { get; set; }
            public bool IsPrimaryKey { get; set; }
            public bool IsComputed { get; set; }
            public bool IsNullable { get; set; }
        }
    }
}
