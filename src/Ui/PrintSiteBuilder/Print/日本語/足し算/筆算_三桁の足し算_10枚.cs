using PrintSiteBuilder.Models.Print;
using PrintSiteBuilder.Utilities;


namespace PrintSiteBuilder.Print.日本語.足し算
{
    public class 筆算_三桁の足し算_10枚 : IPrint
    {
        private string PrintName;
        private List<int> answerColumnIndex = new List<int> { 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14 };
        private List<int> answerRowIndex = new List<int> { 3, 7, 11 };
        private List<List<List<int>>> Coordinates = new List<List<List<int>>>
            {
                new List<List<int>>{ new List<int> { 1, 2 }, new List<int> { 1, 3 }, new List<int> { 1, 4 }, new List<int> { 2,1 }, new List<int>{2,2},new List<int>{2,3}, new List<int> { 2, 4 }, new List<int> { 3, 1 }, new List<int> { 3,2 }, new List<int> { 3, 3 },new List<int>{3,4} },
                new List<List<int>>{ new List<int> { 1, 7 }, new List<int> { 1, 8 }, new List<int> { 1, 9 }, new List<int> { 2,6 }, new List<int>{2,7},new List<int>{2,8}, new List<int> { 2, 9 }, new List<int> { 3, 6 }, new List<int> { 3,7 }, new List<int> { 3, 8 },new List<int>{3,9} },
                new List<List<int>>{ new List<int> { 1, 12 }, new List<int> { 1, 13 }, new List<int> { 1, 14 }, new List<int> { 2,11 }, new List<int>{2,12},new List<int>{2,13}, new List<int> { 2, 14 }, new List<int> { 3, 11 }, new List<int> { 3,12 }, new List<int> { 3, 13 },new List<int>{3,14} },
                new List<List<int>>{ new List<int> { 5, 2 }, new List<int> { 5, 3 }, new List<int> { 5, 4 }, new List<int> { 6,1 }, new List<int>{6,2},new List<int>{6,3}, new List<int> { 6, 4 }, new List<int> { 7, 1 }, new List<int> { 7,2 }, new List<int> { 7, 3 },new List<int>{7,4} },
                new List<List<int>>{ new List<int> { 5, 7 }, new List<int> { 5, 8 }, new List<int> { 5, 9 }, new List<int> { 6,6 }, new List<int>{6,7},new List<int>{6,8}, new List<int> { 6, 9 }, new List<int> { 7, 6 }, new List<int> { 7,7 }, new List<int> { 7, 8 },new List<int>{7,9} },
                new List<List<int>>{ new List<int> { 5, 12 }, new List<int> { 5, 13 }, new List<int> { 5, 14 }, new List<int> { 6,11 }, new List<int>{6,12},new List<int>{6,13}, new List<int> { 6, 14 }, new List<int> { 7, 11 }, new List<int> { 7,12 }, new List<int> { 7, 13 },new List<int>{7,14} },
                new List<List<int>>{ new List<int> { 9, 2 }, new List<int> { 9, 3 }, new List<int> { 9, 4 }, new List<int> { 10,1 }, new List<int>{10,2},new List<int>{10,3}, new List<int> { 10, 4 }, new List<int> { 11, 1 }, new List<int> { 11,2 }, new List<int> { 11, 3 },new List<int>{11,4} },
                new List<List<int>>{ new List<int> { 9, 7 }, new List<int> { 9, 8 }, new List<int> { 9, 9 }, new List<int> { 10,6 }, new List<int>{10,7},new List<int>{10,8}, new List<int> { 10, 9 }, new List<int> { 11, 6 }, new List<int> { 11,7 }, new List<int> { 11, 8 },new List<int>{11,9} },
                new List<List<int>>{ new List<int> { 9, 12 }, new List<int> { 9, 13 }, new List<int> { 9, 14 }, new List<int> { 10,11 }, new List<int>{10,12},new List<int>{10,13}, new List<int> { 10, 14 }, new List<int> { 11, 11 }, new List<int> { 11,12 }, new List<int> { 11, 13 },new List<int>{11,14} },
            };
        private Calcs calcs = new Calcs();
        public int PagesCount { get; private set; }
        public string PresentationId { get; private set; }
        private int Score;
        public List<List<string>> QaPairs;
        public List<List<string>> RandomPairs;
        public 筆算_三桁の足し算_10枚()
        {
            PresentationId = "1Y5CycI4cQcD88yNnOQvXob6Sir1pfxteUCE87wi7Cco";
            PrintName = "ひっさん(さんけたのたしざん)";
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
                    var Value = Questions[i][j];
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
                if (questionIndex.Add(question[question.Count - 1]))
                {
                    randomPairs.Add(question);
                }
            }
            return randomPairs;
        }
        public List<List<string>> GetQuestions1()
        {
            var questions = new List<List<string>>();
            for (var i = 101; i < 1000; i++)
            {
                for (var j = 101; j < 1000; j++)
                {
                    var Number1 = i.ToString();
                    var Number2 = j.ToString();
                    var sum = (i + j).ToString();
                    var question = new List<string> { Number1[0].ToString(), Number1[1].ToString(), Number1[2].ToString(), "+", Number2[0].ToString(), Number2[1].ToString(), Number2[2].ToString() };
                    if (sum.Length == 3)
                    {
                        question.Add(" ");
                    }
                    foreach (var digit in sum)
                    {
                        question.Add(digit.ToString());
                    }
                    question.Add(Guid.NewGuid().ToString());
                    questions.Add(question);
                }
            }
            return questions;
        }
    }
}
