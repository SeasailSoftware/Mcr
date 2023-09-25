using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ReagentStripTest.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReagentStripTest.Utils
{
    public class ExcelHelper
    {

        public static void SaveRecords(IEnumerable<RecordModel> records, string fileName)
        {
            var workbook = CreateWorkbook(fileName);
            var sheet = workbook.CreateSheet("Datas");
            var maxColumn = records.Max(x => x.Samples.Count);
            var titleRow = sheet.CreateRow(0);
            titleRow.CreateCell(0).SetCellValue("Num.");
            titleRow.CreateCell(1).SetCellValue("Rdry");
            for (var colIndex = 0; colIndex < maxColumn; colIndex++)
            {
                titleRow.CreateCell(colIndex + 2).SetCellValue(colIndex + 1);
            }
            var rowIndex = 1;
            foreach(var record in records)
            {
                var row = sheet.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(rowIndex++);
                row.CreateCell(1).SetCellValue(record.Name);
                var colIndex =2;
                foreach(var sample in record.Samples)
                {
                    row.CreateCell(colIndex++).SetCellValue(sample.Data);
                }
            }

            using (var fileStream = new System.IO.FileStream(fileName, FileMode.Create))
            {
                workbook.Write(fileStream);
            }

        }
        public static IWorkbook CreateWorkbook(string fileName)
        {
            var prefix = fileName.Substring(fileName.LastIndexOf("."));
            if (prefix.ToUpper().Equals(".XLS"))
                return new HSSFWorkbook();
            else
                return new XSSFWorkbook();
        }
    }
}
