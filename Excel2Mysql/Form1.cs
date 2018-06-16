using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Excel2Mysql
{
    public partial class Form1 : Form
    {
        private OpenFileDialog openFileDialog1;
        private DBConnect dbmanager;
        private string folder;
        private string outputPath;

        public Form1()
        {
            InitializeComponent();
            CenterToScreen();

            textServer.Text = "localhost";
            textPort.Text = "3306";
            textUser.Text = "root";
            textPassword.Text = "";

            buttonConvert.Enabled = false;
            buttonQuery.Enabled = false;
            comboBoxQuery.SelectedIndex = 0;

            dateTickerBegin.Value = dateTickerEnd.Value.AddDays(-7);

            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog1.DefaultExt = "xls";
            this.openFileDialog1.Filter = "Excel files (*.xls)|*.xls|(*.xlsx)|*.xlsx|(*.csv)|*.csv";

            this.dbmanager = new DBConnect();            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit really?", "Exit Program",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (!result.Equals(DialogResult.OK))
            {
                e.Cancel = true;
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (dbmanager.Initialize(textServer.Text, textPort.Text, "dbals", "itemtable", textUser.Text, textPassword.Text))
            {
                buttonConnect.Text = "Connected";
                buttonConnect.Enabled = false;
                buttonConvert.Enabled = true;
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                listBoxExcel.Items.Clear();

                textFolder.Text = folder = Path.GetDirectoryName(openFileDialog1.FileName);

                IEnumerable<string> files = Directory.EnumerateFiles(folder, "*.xls");

                if (files.Count() > 0)
                {
                    foreach (string filepath in files)
                    {
                        string filename = Path.GetFileName(filepath);
                        listBoxExcel.Items.Add(filename);
                    }
                }

                buttonQuery.Enabled = true;
            }
        }

        private static IList<string> GetTablenames(DataTableCollection tables)
        {
            var tableList = new List<string>();
            foreach (var table in tables)
            {
                tableList.Add(table.ToString());
            }

            return tableList;
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            IEnumerable<string> files;
            DataSet ds;
            try
            {
                files = Directory.EnumerateFiles(folder, "*.xls");
            }
            catch
            {
                return;
            }

            if (files.Count() > 0)
            {
                foreach (string filepath in files)
                {
                    using (var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        IExcelDataReader reader = null;
                        textCurrentFile.Text = Path.GetFileName(filepath);
                        var extension = Path.GetExtension(filepath).ToLower();
                        if (extension == ".xls")
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else if (extension == ".xlsx")
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }
                        else if (extension == ".csv")
                        {
                            reader = ExcelReaderFactory.CreateCsvReader(stream);
                        }

                        if (reader == null)
                            return;

                        var sw = new Stopwatch();
                        sw.Start();
                        using (reader)
                        {
                            ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = false
                                }
                            });
                        }

                        string title = Path.GetFileNameWithoutExtension(filepath);                        
                        if (title.Length < 8)
                        {
                            MessageBox.Show("Invalid Filename Format.");
                            return;
                        }
                        string date = title.Substring(0, 4) + "-" + title.Substring(4, 2) + "-" + title.Substring(6, 2);

                        if (!dbmanager.ExistDate(date))
                        {
                            progressBar1.Maximum = ds.Tables[0].Rows.Count;
                            for (int ncols = 2; ncols < ds.Tables[0].Rows.Count; ncols++)
                            {
                                dbmanager.Insert(ds.Tables[0].Rows[ncols][0].ToString().Trim(),
                                    ds.Tables[0].Rows[ncols][1].ToString().Trim(),
                                    ds.Tables[0].Rows[ncols][2].ToString().Trim(),
                                    ds.Tables[0].Rows[ncols][3].ToString().Trim(),
                                    ds.Tables[0].Rows[ncols][4].ToString().Trim(),
                                    ds.Tables[0].Rows[ncols][5].ToString().Trim(),
                                    ds.Tables[0].Rows[ncols][6].ToString().Trim(),
                                    date);

                                progressBar1.Value = ncols;
                            }
                            progressBar1.Value = ds.Tables[0].Rows.Count;
                        }
                    }
                }

                MessageBox.Show("Converting of Excel files finished successfully.");
            }
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            int type = comboBoxQuery.SelectedIndex;
            if (dateTickerBegin.Value > dateTickerEnd.Value)
            {
                MessageBox.Show("Begin Date must be earlier than End Date.");
                return;
            }
            string begin = dateTickerBegin.Value.ToString("yyyy-MM-dd");
            string end = dateTickerEnd.Value.ToString("yyyy-MM-dd");

            List<string>[] result = dbmanager.CustomizedQuery(type, dateTickerBegin.Value, dateTickerEnd.Value);

            if (result != null)
            {
                outputPath = folder + @"\Query_" + type.ToString() + "(" + begin + "_" + end + ")" + ".xlsx";

                try
                {
                    Excel.Application excel = new Excel.Application();
                    Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);
                    Excel.Worksheet sheet = (Excel.Worksheet)workbook.ActiveSheet;

                    ((Excel.Range)sheet.Cells[1, 1]).Value = "Item";
                    ((Excel.Range)sheet.Cells[1, 2]).Value = "Description";
                    int date_count = (int)(dateTickerEnd.Value - dateTickerBegin.Value).TotalDays + 1;
                    for (int nid = 0; nid < date_count; nid++)
                        ((Excel.Range)sheet.Cells[1, nid + 3]).Value = dateTickerBegin.Value.AddDays(nid).ToString("yyyyMMdd");

                    for (int ncols = 0; ncols < result.Count(); ncols++)
                    {
                        List<string> recode = result[ncols];
                        for (int nrows = 0; nrows < recode.Count(); nrows++)
                        {
                            string value = recode[nrows];
                            ((Excel.Range)sheet.Cells[nrows + 2, ncols + 1]).Value = value;
                        }
                    }

                    workbook.SaveAs(outputPath);
                    workbook.Close();
                    excel.Quit();

                    MessageBox.Show("Executed query and saved result in xlsx successfully.");
                }
                catch
                {
                    MessageBox.Show("Did not save into Excel document.");
                }                
            }
        }
    }
}
