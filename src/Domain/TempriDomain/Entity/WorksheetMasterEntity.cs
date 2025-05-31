using TempriDomain.Interfaces;

namespace TempriDomain.Entity
{
    public class WorksheetMasterEntity : IWorksheetMasterEntity
    {
        public IPrintMasterEntity print { get; set; }
        public List<IQuestionMasterEntity> questions { get; set; }
        public int worksheetNumber { get; set; }
        public int questionCount { get; set; }
    }
}
