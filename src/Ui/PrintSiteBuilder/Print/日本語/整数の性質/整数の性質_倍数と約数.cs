using PrintSiteBuilder.Models.Print;
using PrintSiteBuilder.Utilities;
using SpreadSheetLibrary.Archive;
using SpreadSheetLibrary.Config;
using SpreadSheetLibrary.DataTransferObject;
using SpreadSheetLibrary.Repository.Tempri;

namespace PrintSiteBuilder.Print.日本語.整数の性質
{
    public class 整数の性質_倍数と約数 : IPrint
    {
        private List<int> answerColumnIndex = new List<int> { 3 };
        private List<int> answerRowIndex = new List<int> { 2, 5, 8 };
        private List<List<List<int>>> Coordinates = new List<List<List<int>>>
        {
            new List<List<int>>{ new List<int> { 0, 1 }, new List<int> { 1, 1 }, new List<int>{2,3} },
            new List<List<int>>{ new List<int> { 3, 1 }, new List<int> { 4, 1 }, new List<int>{5,3} },
            new List<List<int>>{ new List<int> { 6, 1 }, new List<int> { 7, 1 }, new List<int>{8,3} },
        };
        private SheetConnecter sheetConnecter = new SheetConnecter();
        private _QaSheetRepository qaSheetRepository;
        private ProductSheetRepository productSheetRepository;
        private SlidePrintSheetRepository slidePrintSheetRepository;
        private ProductSheetObject productObject;
        private SlidePrintSheetObject slidePrintObject;
        private List<_QaSheetObject> qaSheetObjectList;
        private int PrintId;
        private int QuestionNumber;
        public int PagesCount { get; private set; }
        public string PresentationId { get; private set; }
        public 整数の性質_倍数と約数()
        {
            PrintId = 100067;
            qaSheetRepository = new _QaSheetRepository(sheetConnecter);
            productSheetRepository = new ProductSheetRepository(sheetConnecter);
            slidePrintSheetRepository = new SlidePrintSheetRepository(sheetConnecter);
            productObject = productSheetRepository.GetProductSheetObject(PrintId);
            slidePrintObject = slidePrintSheetRepository.GetPrintSheetObject(PrintId);
            PagesCount = productObject.PagesCount;
            PresentationId = slidePrintObject.PresentationId;
            qaSheetObjectList = qaSheetRepository.GetQaSheetObjects(productObject.SheetId);
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

            for (var i = 0; i < productObject.PagesCount; i++)
            {
                // Question Config
                var questionPrintType = "問題";
                var questionPrintName = $"{productObject.PrintName}-{(i + 1).ToString("D3")}";
                var questionPrintNumber = i;
                var questionScore = Coordinates.Count;
                var questionPageIndex = i;
                HeaderConfig questionConfig = new HeaderConfig(questionPrintType, questionPrintName, questionPrintNumber, questionScore, questionPageIndex);
                headerConfigs.Add(questionConfig);

                // Answer Config
                var answerPrintType = "回答";
                var answerPrintName = $"{productObject.PrintName}-{(i + 1).ToString("D3")}";
                var answerPrintNumber = i;
                var answerScore = Coordinates.Count;
                var answerPageIndex = i + productObject.PagesCount;
                HeaderConfig answerConfig = new HeaderConfig(answerPrintType, answerPrintName, answerPrintNumber, answerScore, answerPageIndex);
                headerConfigs.Add(answerConfig);
            }

            return headerConfigs;
        }
        public List<CellConfig> GetCellConfigs()
        {
            var CellConfigs = new List<CellConfig>();
            if (qaSheetObjectList.Count < Coordinates.Count) MessageBox.Show("問題数が足りません。");
            for (var i = 0; i < Coordinates.Count(); i++)
            {
                for (var j = 0; j < Coordinates[i].Count(); j++)
                {
                    var qaObject = qaSheetObjectList[QuestionNumber];
                    var values = new List<string>() { qaObject.Question, qaObject.MidStep1, qaObject.Answer };
                    var RowNumber = Coordinates[i][j][0];
                    var ColumnNumber = Coordinates[i][j][1];
                    var Value = values[j];
                    var answerColumn = answerColumnIndex;
                    var answerRow = answerRowIndex;
                    var cellConfig = new CellConfig(RowNumber, ColumnNumber, Value, answerColumn, answerRow);
                    CellConfigs.Add(cellConfig);
                }
                QuestionNumber++;
            }
            return CellConfigs;
        }
    }
}
