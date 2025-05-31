﻿using PrintSiteBuilder.Models.Print;
using PrintSiteBuilder.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintSiteBuilder.Print.日本語.足し算
{
    public class 筆算_一桁の足し算_30枚 : IPrint
    {
        private string PrintName;
        private List<int> answerColumnIndex = new List<int> { 2, 3, 6, 7, 10, 11, 14, 15 };
        private List<int> answerRowIndex = new List<int> { 3, 7, 11 };
        private List<List<List<int>>> Coordinates = new List<List<List<int>>>
            {
                new List<List<int>>{ new List<int> { 1, 3 }, new List<int>{2,2},new List<int>{2,3}, new List<int> { 3, 2 },new List<int>{3,3} },
                new List<List<int>>{ new List<int> { 1, 7 }, new List<int>{2,6},new List<int>{2,7}, new List<int> { 3, 6 },new List<int>{3,7} },
                new List<List<int>>{ new List<int> { 1, 11 }, new List<int>{2,10},new List<int>{2,11}, new List<int> { 3, 10 },new List<int>{3,11} },
                new List<List<int>>{ new List<int> { 1, 15 }, new List<int>{2,14},new List<int>{2,15}, new List<int> { 3, 14 }, new List<int>{3,15} },
                new List<List<int>>{ new List<int> { 5, 3 }, new List<int>{6,2},new List<int>{6,3}, new List<int> { 7, 2 }, new List<int>{7,3} },
                new List<List<int>>{ new List<int> { 5, 7 }, new List<int>{6,6},new List<int>{6,7}, new List<int> { 7, 6 }, new List<int>{7,7} },
                new List<List<int>>{ new List<int> { 5, 11 }, new List<int>{6,10},new List<int>{6,11}, new List<int> { 7, 10 }, new List<int>{7,11} },
                new List<List<int>>{ new List<int> { 5, 15 }, new List<int>{6,14},new List<int>{6,15}, new List<int> { 7, 14 }, new List<int>{7,15} },
                new List<List<int>>{ new List<int> { 9, 3 }, new List<int>{10,2},new List<int>{ 10, 3}, new List<int> { 11, 2 }, new List<int>{11,3} },
                new List<List<int>>{ new List<int> { 9, 7 }, new List<int>{ 10, 6},new List<int>{ 10, 7}, new List<int> { 11, 6 }, new List<int>{11,7} },
                new List<List<int>>{ new List<int> { 9, 11 }, new List<int>{ 10, 10},new List<int>{ 10, 11}, new List<int> { 11, 10 }, new List<int>{11,11} },
                new List<List<int>>{ new List<int> { 9, 15 }, new List<int>{ 10, 14},new List<int>{ 10, 15}, new List<int> { 11, 14 }, new List<int>{11,15} },
            };
        private Calcs calcs = new Calcs();
        public int PagesCount { get; private set; }
        public string PresentationId { get; private set; }
        private int Score;
        public List<List<string>> QaPairs;
        public List<List<string>> RandomPairs;
        public 筆算_一桁の足し算_30枚()
        {
            PresentationId = "14S2-bokCG3smX8JCChD_2igzexdIK6Yzx-8dzETZIzI";
            PrintName = "ひっさん(ひとけたのたしざん)";
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
                    var sum = (i + j).ToString();
                    var question = new List<string> { i.ToString(), "+", j.ToString() };
                    if (sum.Length == 1)
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
