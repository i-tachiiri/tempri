using PrintSiteBuilder.Models.Print;
using PrintSiteBuilder.Utilities;

namespace PrintSiteBuilder.Print.日本語.足し算
{
    public class 筆算_二桁の足し算_10枚 : IPrint
    {
        private string PrintName;
        private List<int> answerColumnIndex = new List<int> { 1, 2, 3, 5, 6, 7, 9, 10, 11, 13, 14, 15 };
        private List<int> answerRowIndex = new List<int> { 3, 7, 11 };
        private List<List<List<int>>> Coordinates = new List<List<List<int>>>
            {
                new List<List<int>>{ new List<int> { 1, 2 }, new List<int> { 1, 3 }, new List<int>{2,1}, new List<int> { 2, 2 }, new List<int>{2,3}, new List<int> { 3, 1 }, new List<int> { 3, 2 },new List<int>{3,3} },
                new List<List<int>>{ new List<int> { 1, 6 }, new List<int> { 1, 7 }, new List<int>{2,5}, new List<int> { 2, 6 }, new List<int>{2,7}, new List<int> { 3, 5 }, new List<int> { 3, 6 },new List<int>{3,7} },
                new List<List<int>>{ new List<int> { 1, 10 }, new List<int> { 1, 11 }, new List<int>{2,9}, new List<int> { 2, 10 }, new List<int>{2,11}, new List<int> { 3, 9 }, new List<int> { 3, 10 },new List<int>{3,11} },
                new List<List<int>>{ new List<int> { 1, 14 }, new List<int> { 1, 15 }, new List<int>{2,13}, new List<int> { 2, 14 }, new List<int>{2,15}, new List<int> { 3, 13 }, new List<int> { 3, 14 },new List<int>{3,15} },
                new List<List<int>>{ new List<int> { 5, 2 }, new List<int> { 5, 3 }, new List<int>{6,1}, new List<int> { 6, 2 }, new List<int>{6,3}, new List<int> { 7, 1 }, new List<int> { 7, 2 },new List<int>{7,3} },
                new List<List<int>>{ new List<int> { 5, 6 }, new List<int> { 5, 7 }, new List<int>{6,5}, new List<int> { 6, 6 }, new List<int>{6,7}, new List<int> { 7, 5 }, new List<int> { 7, 6 },new List<int>{7,7} },
                new List<List<int>>{ new List<int> { 5, 10 }, new List<int> { 5, 11 }, new List<int>{6,9}, new List<int> { 6, 10 }, new List<int>{6,11}, new List<int> { 7, 9 }, new List<int> { 7, 10 },new List<int>{7,11} },
                new List<List<int>>{ new List<int> { 5, 14 }, new List<int> { 5, 15 }, new List<int>{6,13}, new List<int> { 6, 14 }, new List<int>{6,15}, new List<int> { 7, 13 }, new List<int> { 7, 14 },new List<int>{7,15} },
                new List<List<int>>{ new List<int> { 9, 2 }, new List<int> { 9, 3 }, new List<int>{10,1}, new List<int> { 10, 2 }, new List<int>{10,3}, new List<int> { 11, 1 }, new List<int> { 11, 2 },new List<int>{11,3} },
                new List<List<int>>{ new List<int> { 9, 6 }, new List<int> { 9, 7 }, new List<int>{10,5}, new List<int> { 10, 6 }, new List<int>{10,7}, new List<int> { 11, 5 }, new List<int> { 11, 6 },new List<int>{11,7} },
                new List<List<int>>{ new List<int> { 9, 10 }, new List<int> { 9, 11 }, new List<int>{10,9}, new List<int> { 10, 10 }, new List<int>{10,11}, new List<int> { 11, 9 }, new List<int> { 11, 10 },new List<int>{11,11} },
                new List<List<int>>{ new List<int> { 9, 14 }, new List<int> { 9, 15 }, new List<int>{10,13}, new List<int> { 10, 14 }, new List<int>{10,15}, new List<int> { 11, 13 }, new List<int> { 11, 14 },new List<int>{11,15} },

            };
        private Calcs calcs = new Calcs();
        public int PagesCount { get; private set; }
        public string PresentationId { get; private set; }
        private int Score;
        public List<List<string>> QaPairs;
        public List<List<string>> RandomPairs;
        public 筆算_二桁の足し算_10枚()
        {
            PresentationId = "1TgoELYSRXav9ta2KcZ-3Ic9am2zNN8nzlBtBUC3C4d4";
            PrintName = "ふたけたのたしざん（ひっさん）";
            PagesCount = 10;
            Score = Coordinates.Count;
        }
        public List<PrintConfig> GetPrintConfigs()
        {
            var headerConfigs = GetHeaderConfigs();
            var printConfigs = new List<PrintConfig>();
            foreach (var headerConfigGroup in headerConfigs.GroupBy(item => item.PrintNumber))   //プリント番号別にグループ化して問題と回答をセットに
            {
                var cellConfigs = GetCellConfigs();
                foreach (var headerConfig in headerConfigGroup)
                {
                    var printConfig = new PrintConfig(headerConfig, cellConfigs);
                    printConfigs.Add(printConfig);
                }
            }
            return printConfigs;
        }
        public List<HeaderConfig> GetHeaderConfigs()
        {
            var headerConfigs = new List<HeaderConfig>();

            for (var i = 0; i < PagesCount; i++)
            {
                // Question Config
                var questionPrintType = "問題";
                var questionPrintName = $"{PrintName}-{(i + 1).ToString("D3")}";
                var questionPrintNumber = i;
                var questionScore = Score;
                var questionPageIndex = i;
                HeaderConfig questionConfig = new HeaderConfig(questionPrintType, questionPrintName, questionPrintNumber, questionScore, questionPageIndex);
                headerConfigs.Add(questionConfig);

                // Answer Config
                var answerPrintType = "回答";
                var answerPrintName = $"{PrintName}-{(i + 1).ToString("D3")}";
                var answerPrintNumber = i;
                var answerScore = Score;
                var answerPageIndex = i + PagesCount;
                HeaderConfig answerConfig = new HeaderConfig(answerPrintType, answerPrintName, answerPrintNumber, answerScore, answerPageIndex);
                headerConfigs.Add(answerConfig);
            }

            return headerConfigs;
        }
        public List<CellConfig> GetCellConfigs()
        {
            var CellConfigs = new List<CellConfig>();
            var Questions = GetQaPairs();
            if (Questions.Count < Score) MessageBox.Show("問題数が足りません。");
            for (var i = 0; i < Coordinates.Count(); i++)
            {
                for (var j = 0; j < Coordinates[i].Count(); j++)
                {
                    var RowNumber = Coordinates[i][j][0];
                    var ColumnNumber = Coordinates[i][j][1];
                    var Value = j < Questions[i].Count ? Questions[i][j] : "";
                    var answerColumn = answerColumnIndex;
                    var answerRow = answerRowIndex;
                    var cellConfig = new CellConfig(RowNumber, ColumnNumber, Value, answerColumn, answerRow);
                    CellConfigs.Add(cellConfig);
                }
            }
            return CellConfigs;
        }
        public List<List<string>> GetQaPairs()
        {
            var QuestionsList = new List<List<List<string>>>()
            {
                GetQuestions1(),
            };

            var Questions = new List<List<string>>();
            foreach (var question in QuestionsList)
            {
                Questions = Questions.Concat(question).ToList();
            }

            return GetRandomPairs(Questions);
        }
        public List<List<string>> GetRandomPairs(List<List<string>> questions)
        {
            var random = new Random();
            var questionIndex = new HashSet<string>();
            var randomPairs = new List<List<string>>();

            var shuffledQuestions = questions.OrderBy(x => random.Next()).ToList();

            foreach (var question in shuffledQuestions)
            {
                // Find "+" and extract answer (skip '+' and the digit after it)
                int plusIndex = question.IndexOf("+");
                if (plusIndex == -1 || plusIndex + 2 >= question.Count) continue;

                var answer = string.Join("", question.Skip(plusIndex + 2));

                if (questionIndex.Add(answer))
                {
                    randomPairs.Add(question);
                }
            }

            return randomPairs;
        }


        public List<List<string>> GetQuestions1()
        {
            var questions = new List<List<string>>();
            for (var i = 11; i < 100; i++)
            {
                for (var j = 11; j < 100; j++)
                {
                    var question = new List<string>();

                    // Digits of the first number
                    foreach (var digit in i.ToString())
                    {
                        question.Add(digit.ToString());
                    }

                    // Operator and second number
                    question.Add("+");
                    // Digits of the first number
                    foreach (var digit in j.ToString())
                    {
                        question.Add(digit.ToString());
                    }
                    // Digits of the answer (ensuring space padding for two-digit results)
                    var sumStr = (i + j).ToString();
                    if (sumStr.Length == 2)
                    {
                        question.Add(" ");
                    }
                    foreach (var digit in sumStr)
                    {
                        question.Add(digit.ToString());
                    }

                    questions.Add(question);
                }
            }
            return questions;
        }


    }
}
