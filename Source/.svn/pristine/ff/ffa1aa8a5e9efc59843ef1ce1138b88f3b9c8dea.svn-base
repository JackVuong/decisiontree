using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDecisionTree_GameDevelopment_A1
{
    class ID3
    {
        List<List<string>> Examples;
        List<Feature> Features;
        Node tree;
        int depth;

        internal Node Tree
        {
            get { return tree; }
            set { tree = value; }
        }

        public int Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        public ID3(List<List<string>> Examples, List<Feature> Features)
        {
            this.Examples = Examples;
            this.Features = Features;
            this.Tree = null;
            Depth = 0;
        }

        // tính entroypy

        private double GetEntropy(int Positives, int Negatives)
        {
            if (Positives == 0)
                return 0;
            if (Negatives == 0)
                return 0;
            double Entropy;
            int total = Negatives + Positives;
            double RatePositves = (double)Positives / total;
            double RateNegatives = (double)Negatives / total;
            Entropy = -RatePositves * Math.Log(RatePositves, 2) - RateNegatives * Math.Log(RateNegatives, 2);
            return Entropy;
        }

        // tính Gain(bestat,A);

        private double Gain(List<List<string>> Examples, Feature A, string bestat)
        {
            double result; // dap so
            int CountPositives = 0;
            int[] CountPositivesA = new int[A.Values.Count]; //tao ra mang dem yes cua tung Feature
            int[] CountNegativeA = new int[A.Values.Count]; // -
            int Col = Features.IndexOf(A);
            for (int i = 0; i < A.Values.Count; i++)
            {
                CountPositivesA[i] = 0;
                CountNegativeA[i] = 0;
            }
            for (int i = 0; i < Examples.Count; i++)
            {
                int j = A.Values.IndexOf(Examples[i][Col].ToString());
                if (Examples[i][Examples[0].Count - 1] == "yes")
                {
                    CountPositives++;
                    CountPositivesA[j]++;
                }
                else
                {
                    CountNegativeA[j]++;
                }
            }
            result = GetEntropy(CountPositives, Examples.Count - CountPositives);
            for (int i = 0; i < A.Values.Count; i++)
            {
                double RateValues = (double)(CountPositivesA[i] + CountNegativeA[i]) / Examples.Count;
                result = result - RateValues * GetEntropy(CountPositivesA[i], CountNegativeA[i]);
            }
            return result;
        }

        // giải thuật ID3

        private Node ID3algorithm(List<List<string>> Examples, List<Feature> Feature, string bestat)
        {           
            if (CheckAllPositive(Examples))
            {
                return new Node(new Feature("Yes"));
            }
            if (CheckAllNegative(Examples))
            {
                return new Node(new Feature("No"));
            }
            if (Feature.Count == 0)
            {
                return new Node(new Feature(GetMostCommonValue(Examples)));
            }
            Feature BestFeature = GetBestFeature(Examples, Feature, bestat);
            int LocationBA = Features.IndexOf(BestFeature);
            Node Root = new Node(BestFeature);
            for (int i = 0; i < BestFeature.Values.Count; i++)
            {
                List<List<string>> Examplesvi = new List<List<string>>();
                for (int j = 0; j < Examples.Count; j++)
                {
                    if (Examples[j][LocationBA].ToString() == BestFeature.Values[i].ToString())
                        Examplesvi.Add(Examples[j]);
                }
                if (Examplesvi.Count == 0)
                {
                    return new Node(new Feature(GetMostCommonValue(Examplesvi)));
                }
                else
                {
                    Feature.Remove(BestFeature);
                    Root.AddNode(ID3algorithm(Examplesvi, Feature, BestFeature.Values[i]));
                }
            }
            return Root;
        }

        // lấy thuật tính có Gain cao nhất

        private Feature GetBestFeature(List<List<string>> Examples, List<Feature> Features, string bestat)
        {
            double MaxGain = Gain(Examples, Features[0], bestat);
            int MaxPos = 0;
            for (int i = 1; i < Features.Count; i++)
            {
                double GainCurrent = Gain(Examples, Features[i], bestat);
                if (MaxGain < GainCurrent)
                {
                    MaxGain = GainCurrent;
                    MaxPos = i;
                }
            }
            return Features[MaxPos];
        }

        // lấy giá trị phổ biến nhất của tập đích
        // xem yes hay no nhiều hơn, cái nào nhiều thì chọn cái đó
        private string GetMostCommonValue(List<List<string>> Examples)
        {
            int CountPositive = 0;
            for (int i = 0; i < Examples.Count; i++)
            {
                if (Examples[i][Examples[0].Count - 1] == "yes")
                    CountPositive++;
            }
            int CountNegative = Examples.Count - CountPositive;
            string Label;
            if (CountPositive > CountNegative)
                Label = "Yes";
            else
                Label = "No";
            return Label;
        }

        // kiểm tra xem tất cả tập có phải là positive không

        private bool CheckAllPositive(List<List<string>> Examples)
        {
            for (int i = 0; i < Examples.Count; i++)
            {
                if (Examples[i][Examples[0].Count - 1].ToString() == "no")
                    return false;
            }
            return true;
        }

        // kiểm tra xem tất cả tập có phải là Negative không

        private bool CheckAllNegative(List<List<string>> Examples)
        {
            for (int i = 0; i < Examples.Count; i++)
            {
                if (Examples[i][Examples[0].Count - 1] == "yes")
                    return false;
            }
            return true;
        }

        // xây dựng cây

        public void GetTree()
        {
            List<Feature> at = new List<Feature>();
            for (int i = 0; i < Features.Count; i++)
            {
                at.Add(Features[i]);
            }
            Tree = ID3algorithm(Examples, at, "S");
            Depth = GetDepth(Tree);
        }

        // lấy độ sâu của cây

        private int GetDepth(Node tree)
        {
            int depth;
            if (tree.Childs.Length == 0)
                return 1;
            else
            {
                depth = GetDepth(tree.Childs[0]);
                for (int i = 1; i < tree.Childs.Length; i++)
                {
                    int depthchild = GetDepth(tree.Childs[i]);
                    if (depth < depthchild)
                        depth = depthchild;
                }
                depth++;
            }
            return depth;
        }
    }
}
