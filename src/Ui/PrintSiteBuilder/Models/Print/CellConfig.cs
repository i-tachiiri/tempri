using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintSiteBuilder.Models.Print
{
    public class CellConfig
    {
        public CellConfig() { }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public string Value { get; set; }
        public List<int> AnswerColumn { get; set; }
        public List<int> AnswerRow { get; set; }
        public CellConfig(int rowNumber,int columnNumber,string value,List<int> answerColumn)
        {
            RowNumber = rowNumber;
            ColumnNumber = columnNumber;
            Value = value;    
            AnswerColumn = answerColumn;
        }
        public CellConfig(int rowNumber, int columnNumber, string value, List<int> answerColumn, List<int> answerRow)
        {
            RowNumber = rowNumber;
            ColumnNumber = columnNumber;
            Value = value;
            AnswerColumn = answerColumn;
            AnswerRow = answerRow;
        }
    }
}
