using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace DemoDecisionTree_GameDevelopment_A1
{
    public partial class Form1 : Form
    {
        List<Feature> Features = new List<Feature>();
        ID3 DTID3;
        List<List<string>> Examples = new List<List<string>>();
        int height, width = 0;
        DecisionTreeView dt;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnLearn.Enabled = false;
            dgwTest.Enabled = false;
            //dgwTest.Enabled = false;
            btnResult.Enabled = false;           
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            dataset.Columns.Clear();
            Features.Clear();
            OpenFileDialog OpenDiag = new OpenFileDialog();
            OpenDiag.DefaultExt = ".txt";
            OpenDiag.Filter = "Text documents (.txt)|*.txt";
            if (OpenDiag.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(OpenDiag.FileName);
                string line = "";
                if ((line = sr.ReadLine()) != null)
                {
                    string[] FeatureName = line.Trim().ToLower().Split(' ');

                    for (int i = 0; i < FeatureName.Length - 1; i++)
                    {
                        Feature temp = new Feature();
                        temp.Name = FeatureName[i];
                        Features.Add(temp);
                        DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                        column.HeaderText = FeatureName[i].ToUpper();
                        column.Name = FeatureName[i].ToUpper();
                        dataset.Columns.Add(column);
                    }
                    DataGridViewTextBoxColumn columnend = new DataGridViewTextBoxColumn();
                    columnend.HeaderText = FeatureName[FeatureName.Length - 1].ToUpper();
                    columnend.Name = FeatureName[FeatureName.Length - 1].ToUpper();
                    dataset.Columns.Add(columnend);

                    btnLearn.Enabled = true;
                }
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    string[] value = line.Trim().ToLower().Split(' ');

                    DataGridViewRow dgvr = new DataGridViewRow();
                    for (int i = 0; i < value.Length - 1; i++)
                    {
                        Features[i].AddValue(value[i]);
                    }
                    string[] value2 = line.Trim().ToLower().Split(' ');
                    dataset.Rows.Add(value2);
                }
                sr.Close();
            }
        }

        private void btnLearn_Click(object sender, EventArgs e)
        {
            dt = new DecisionTreeView();
            Examples.Clear();
            dgwTest.DataSource = null;
            dgwTest.Refresh();
            for (int i = 0; i < dataset.Rows.Count - 1; i++)
            {
                List<string> example = new List<string>();
                for (int j = 0; j < dataset.Columns.Count; j++)
                {
                    example.Add(dataset.Rows[i].Cells[j].Value.ToString().ToLower());
                }
                Examples.Add(example);
            }
            List<Feature> at = new List<Feature>();
            for (int i = 0; i < Features.Count; i++)
            {
                at.Add(Features[i]);
            }
            DTID3 = new ID3(Examples, at);
            DTID3.GetTree();
            height = DTID3.Depth * 300;
            width = DTID3.Tree.NumberLabel * 100;
            dt.w = width;
            dt.h = height;
            dt.Tree = DTID3.Tree;
            //picTree.Invalidate();
            dgwTest.Enabled = true;
            btnResult.Enabled = true;
            for (int i = 0; i < Features.Count; i++)
            {
                DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
                cmb.Name = Features[i].Name.ToUpper(); ;
                cmb.MaxDropDownItems = Features[i].Values.Count;
                for(int j = 0; j < Features[i].Values.Count;j++ )
                {
                    cmb.Items.Add(Features[i].Values[j]);
                }
                dgwTest.Columns.Add(cmb);
            }
            dt.Show();

        }

        private void dataset_Paint(object sender, PaintEventArgs e)
        {
            //Do Nothing
        }

        private void picTree_PaddingChanged(object sender, EventArgs e)
        {
            //
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            List<TestCase> testcase = new List<TestCase>();
            DataGridViewRow row = dgwTest.Rows[0];
            for(int i = 0; i < Features.Count; i++)
            {
                if(row.Cells[i].Value.ToString().Equals(""))
                {
                    break;
                }
                TestCase test = new TestCase(Features[i].Name, row.Cells[i].Value.ToString());
                testcase.Add(test);
            }
            makeDecision(DTID3.Tree, testcase);

        }

        private void makeDecision(Node tree, List<TestCase> testcase)
        {
            if (tree.Features.Name.Equals(""))
            {
                MessageBox.Show(tree.Features.Label);
                return;
            }
            else
            {
                foreach (TestCase t in testcase)
                {
                    if (t.featureName.Equals(tree.Features.Name))
                    {
                        for(int i = 0; i< tree.Features.Values.Count;i++)
                        {
                            if(t.value.Equals(tree.Features.Values[i]))
                            {
                                makeDecision(tree.Childs[i], testcase);
                            }
                        }
                    }
                }
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgwTest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
