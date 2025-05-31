using PrintSiteBuilder.Models.Print;
using PrintSiteBuilder.Utilities;

namespace PrintSiteBuilder.Print.日本語.足し算
{
    public class 文章題_一桁の足し算_30枚 : IPrint
    {
        private string PrintName;
        private List<int> answerColumnIndex = new List<int> { 3 };
        private List<int> answerRowIndex = new List<int> { 1, 3, 5 };
        private List<List<List<int>>> Coordinates = new List<List<List<int>>>
            {
                new List<List<int>>{ new List<int> { 0, 1 }, new List<int>{1,3} },
                new List<List<int>>{ new List<int> { 2, 1 }, new List<int>{3,3} },
                new List<List<int>>{ new List<int> { 4, 1 }, new List<int>{5,3} },
            };
        private Calcs calcs = new Calcs();
        public int PagesCount { get; private set; }
        public string PresentationId { get; private set; }
        private int Score;
        public List<List<string>> QaPairs;
        public List<List<string>> RandomPairs;
        public 文章題_一桁の足し算_30枚()
        {
            PresentationId = "1oM_VM4smU0-SYb4fTt6VE1p4gTJAZ-D6wN4B4TX9iQ4";
            PrintName = "ぶんしょうだい(ひとけたのたしざん)";
            PagesCount = 30;
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
            for (var i = 1; i < 10; i++)
            {
                for (var j = 1; j < 10; j++)
                {
                    if (i + j <= 10) continue;
                    var x = i + j;
                    var qaText = GetQaText();
                    qaText = ReplaceQaText(qaText, i, j, x);
                    var question = new List<string> { qaText.Item1, qaText.Item2 };
                    questions.Add(question);
                }
            }
            return questions;
        }
        private (string, string) GetQaText()
        {
            var questionTexts = new List<(string, string)>()
            {
                ("{個}が{i}あります。{友達}がもう{j}くれました。ぜんぶでなんこありますか？","{x}"),
                ("{本}が{i}あります。{友達}がもう{j}くれました。ぜんぶでなんほんありますか？","{x}"),
                ("{一人称}は{個}を {i} たべました。{家族}は {j} たべました。ぜんぶで なんこ たべたでしょう？","{x}"),
                ("{動物}が {i} あそんでいます。あとから {j} きました。ぜんぶでなんびきになりましたか？","{x}")
            };
            var random = new Random();
            int index = random.Next(questionTexts.Count);
            return questionTexts[index];
        }
        private string GetHikiReading(int number)
        {
            int lastDigit = number % 10;
            return lastDigit == 1 || lastDigit == 6 || lastDigit == 8 || lastDigit == 0 ? "ぴき"
                 : lastDigit == 3 ? "びき" : "ひき";
        }

        private string GetHonReading(int number)
        {
            int lastDigit = number % 10;
            return lastDigit == 1 || lastDigit == 6 || lastDigit == 8 || lastDigit == 0 ? "ぽん"
                 : lastDigit == 3 ? "ぼん" : "ほん";
        }

        private (string, string) ReplaceQaText((string, string) qaTemplate, int i, int j, int x)
        {
            var 個s = new List<string>() { "りんご", "みかん", "もも", "なし", "かき", "キウイ", "レモン", "じゃがいも", "さつまいも", "たまねぎ", "なす", "ピーマン", "パプリカ", "からあげ", "たこやき", "ぎょうざ" };
            var 本s = new List<string>() { "えんぴつ", "クレヨン", "ストロー", "バナナ", "きゅうり", "にんじん", "アスパラガス", "ソーセージ" };
            var 友達s = new List<string>() { "はるとくん", "そうたくん", "ゆうまくん", "そうしくん", "れんくん", "いつきくん", "りくくん", "けんとくん", "こうきくん", "たいようくん", "はるきくん", "あおとくん", "ひなたくん", "たくみくん", "だいちくん", "けいしくん", "なおやくん", "けいとくん", "りょうたくん", "ひなたちゃん", "ここなちゃん", "さくらちゃん", "りこちゃん", "あかりちゃん", "ゆいちゃん", "こはるちゃん", "みおちゃん", "りおちゃん", "こころちゃん", "みゆちゃん", "まなちゃん", "ひよりちゃん", "いちかちゃん", "あおいちゃん", "はなちゃん", "すみれちゃん", "ほのかちゃん", "あんちゃん", "ななみちゃん", };
            var 家族s = new List<string>() { "おとうさん", "おかあさん", "おじいちゃん", "おばあちゃん", "おにいちゃん", "おねえちゃん" };
            var 一人称s = new List<string>() { "ぼく", "わたし" };
            var 動物s = new List<string>() { "いぬ", "ねこ", "うさぎ", "ハムスター", "リス", "さかな", "カブトムシ", "セミ", "バッタ", "てんとうむし" };

            var rand = new Random();
            string q = qaTemplate.Item1;
            string a = qaTemplate.Item2;

            // プレースホルダーの置換対象を記録
            bool hasHon = q.Contains("{本}") || a.Contains("{本}");
            bool hasHiki = q.Contains("{動物}") || a.Contains("{動物}");

            // ランダム値の置換
            if (q.Contains("{個}")) q = q.Replace("{個}", 個s[rand.Next(個s.Count)]);
            if (q.Contains("{本}")) q = q.Replace("{本}", 本s[rand.Next(本s.Count)]);
            if (q.Contains("{友達}")) q = q.Replace("{友達}", 友達s[rand.Next(友達s.Count)]);
            if (q.Contains("{家族}")) q = q.Replace("{家族}", 家族s[rand.Next(家族s.Count)]);
            if (q.Contains("{一人称}")) q = q.Replace("{一人称}", 一人称s[rand.Next(一人称s.Count)]);
            if (q.Contains("{動物}")) q = q.Replace("{動物}", 動物s[rand.Next(動物s.Count)]);

            // 読み方付きの数字に置換
            if (hasHon)
            {
                q = q.Replace("{i}", $"{i}{GetHonReading(i)}")
                     .Replace("{j}", $"{j}{GetHonReading(j)}");
                a = a.Replace("{x}", $"{x}{GetHonReading(x)}");
            }
            else if (hasHiki)
            {
                q = q.Replace("{i}", $"{i}{GetHikiReading(i)}")
                     .Replace("{j}", $"{j}{GetHikiReading(j)}");
                a = a.Replace("{x}", $"{x}{GetHikiReading(x)}");
            }
            else // 「こ」など読み方変化のないもの
            {
                q = q.Replace("{i}", $"{i}こ").Replace("{j}", $"{j}こ");
                a = a.Replace("{x}", $"{x}こ");
            }

            return (q, a);
        }
    }
}
