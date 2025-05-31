using TempriDomain.Interfaces;

namespace SpreadSheetLibrary.DataTransferObject
{
    public class QaSheetObject : IQuestionMasterEntity
    {
        public int QuestionCount {  get; set; }
        public int WorksheetNumber {  get; set; }
        public int QuestionNumber { get; set; }
        public string Value0 { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public string Value4 { get; set; }
        public string Value5 { get; set; }
        public string Value6 { get; set; }
        public string Value7 { get; set; }
        public string Value8 { get; set; }
        public string Value9 { get; set; }
        public string Value10 { get; set; }
        public string Value11 { get; set; }
        public string Value12 { get; set; }
        public string Value13 { get; set; }
        public string Value14 { get; set; }
        public string Value15 { get; set; }
        public string Value16 { get; set; }
        public string Value17 { get; set; }
        public string Value18 { get; set; }
        public string Value19 { get; set; }
        public string Value20 { get; set; }
        public IPrintMasterEntity print { get; set; }
        public IWorksheetMasterEntity worksheet { get; set; }
    }
}
