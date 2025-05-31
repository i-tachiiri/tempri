
using TempriDomain.Entity;

namespace PrintExecutionService.Services;
public class PrintMasterGetter
{
    private readonly PrintMasterRepository printMasterRepository;
    private readonly QaSheetRepository qaSheetRepository;

    public PrintMasterGetter(PrintMasterRepository printMasterRepository, QaSheetRepository qaSheetRepository)
    {
        this.printMasterRepository = printMasterRepository;
        this.qaSheetRepository = qaSheetRepository;
    }
    public async Task<PrintMasterEntity> GetPrintEntity(int printId)
    {
        var printMasterObject = await printMasterRepository.GetPrintMasterEntity(printId);
        if(!string.IsNullOrEmpty(printMasterObject.QaSheetId))
        {
            printMasterObject.questions = await qaSheetRepository.GetQaSheetObjectsAsync(printMasterObject.QaSheetId);//printSlideRepository.GetPageEntities(printMasterObject.PrintSlideId);
            printMasterObject.worksheets = mapPrint2Worksheet(printMasterObject);
            foreach (var question in printMasterObject.questions)
            {
                question.print = printMasterObject;
                question.worksheet = printMasterObject.worksheets.First(worksheet => worksheet.worksheetNumber == question.WorksheetNumber);
            }
        }
        return printMasterObject;
    }
    private List<IWorksheetMasterEntity> mapPrint2Worksheet(IPrintMasterEntity print)
    {
        var entities = new List<IWorksheetMasterEntity>();
        var groups = print.questions.GroupBy(printMasterObject => printMasterObject.WorksheetNumber);
        for(var i=0;i<groups.Count();i++)
        {
            var entity = new WorksheetMasterEntity();
            entity.print = print;
            entity.questions = groups.First(group => group.Key == i+1).ToList();
            entity.worksheetNumber = entity.questions[0].WorksheetNumber;
            entity.questionCount = entity.questions[0].QuestionCount;
            entities.Add(entity);
        }
        return entities;
    }
    /*
    public async Task<List<IPageMasterEntity>> GetPageEntities(string presentationId)
    {
        var pageList = new List<IPageMasterEntity>(); 
        var pages = await pageService.GetPages(presentationId);
        int total = pages.Count;
        int logicalPageCount = total >= 2 ? total / 2 : 1;

        for (int i = 0; i < total; i++)
        {
            var entity = new PageMasterEntity
            {
                PageObjectId = pages[i].ObjectId,
                PageNumber = i % logicalPageCount + 1,
                PageIndex = i,
                IsAnswerPage = i >= logicalPageCount
            };
            pageList.Add(entity); 
        }

        return pageList;
    }
     */
}
