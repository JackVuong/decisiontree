using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoDecisionTree_GameDevelopment_A1
{
    public partial class DecisionTreeView : Form
    {
        public int w, h;
        public Node Tree;

        public DecisionTreeView()
        {
            InitializeComponent();
        }


        private void DecisionTree_Load(object sender, EventArgs e)
        {

        }

        private void picTree_Paint(object sender, PaintEventArgs e)
        {
            if (w > 0)
            {
                picTree.Width = w;
                picTree.Height = h;
                PaintTree(Tree, e, 0, 50);
            }
        }
        private void PaintTree(Node tree, PaintEventArgs e, int X, int Y)
        {

            int XStart = X;
            X = (tree.NumberLabel * 100 + 2 * X) / 2 - 50;
            if (tree.Features.Name.ToString() != "")
            {
                //e.Graphics.FillEllipse(Brushes.Yellow, X-30, Y, tree.Features.Name.Length * 20, 30);
                e.Graphics.DrawString(tree.Features.Name.ToString().ToUpper(), new Font("Arial", 20), Brushes.Black, new PointF(X, Y));
            }
            else
            {
                e.Graphics.FillEllipse(Brushes.White, X + 20, Y, tree.Features.Label.Length * 25, 35);
                e.Graphics.DrawString(tree.Features.Label, new Font("Arial", 20), Brushes.DeepPink, new PointF(X + 25, Y));
            }
            int XEndA;
            for (int i = 0; i < tree.Features.Values.Count; i++)
            {
                XEndA = tree.Childs[i].NumberLabel * 100 + XStart;
                int XA = (XStart + XEndA) / 2 - 50;
                e.Graphics.DrawLine(new Pen(Brushes.Black, 2), X + 50, Y + 30, XA + 50, Y + 100);
                e.Graphics.DrawString(tree.Features.Values[i].ToString(), new Font("Arial", 20), Brushes.BlueViolet, new PointF(XA, Y + 100));
                e.Graphics.DrawLine(new Pen(Brushes.Black, 2), XA + 50, Y + 130, XA + 50, Y + 200);
                PaintTree(tree.Childs[i], e, XStart, Y + 200);
                XStart = XEndA;
            }
        }
    }
}
