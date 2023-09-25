using Caliburn.Micro;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System.Linq;
using ThreeNH.i18N;

namespace ReagentStripTest.Utils
{
    public class ReportHelper
    {


    }

    public enum ReportType
    {
        Chinese,
        English
    }

    public static class XWPFDocumentExtension
    {
        private static string Color_Green = "#008000";
        private static string Color_Orange = "#FFA500";
        private static string Color_Red = "#FF0000";
        private static string Color_Black = "#000000";
        private static ITranslater _translater => IoC.Get<ITranslater>();
        public static XWPFTableCell SetBackgroud(this XWPFTableCell cell)
        {
            cell.SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
            cell.SetColor("#D3D3D3");
            return cell;
        }

        public static void SetCellText(this XWPFTableRow row, int cellIndex, string text, bool? isPassed = null)
        {
            var cell = row.GetCell(cellIndex);
            if (cell == null)
                cell = row.AddNewTableCell();
            cell.SetContent(text, isPassed);
        }

        public static XWPFTableCell SetContent(this XWPFTableCell cell, string content, bool? isPassed = null)
        {
            var paragraph = cell.Paragraphs.FirstOrDefault();
            paragraph.Alignment = ParagraphAlignment.CENTER;
            var run = paragraph.CreateRun();
            run.FontSize = 7;
            if (string.IsNullOrEmpty(content))
                content = "";
            run.SetText(content);
            var brush = isPassed == null ? Color_Black
                : isPassed == false ? Color_Red
                : isPassed == true ? Color_Green
                : Color_Black;

            run.SetColor(brush);

            return cell;
        }


        public static void MergeColumnCells(this XWPFTable table, int rowIndex, int fromCol, int toCol)
        {
            XWPFTableCell rowcell = table.GetRow(rowIndex).GetCell(fromCol);
            CT_Tc cttc = rowcell.GetCTTc();
            if (cttc.tcPr == null)
            {
                cttc.AddNewTcPr();
            }

            if (fromCol <= toCol)
            {
                var count = toCol - fromCol + 1;
                rowcell.GetCTTc().tcPr.gridSpan = new CT_DecimalNumber();
                rowcell.GetCTTc().tcPr.gridSpan.val = count.ToString();
            }
        }


        public static void MergeCells(this XWPFTable table, int fromCol, int toCol, int fromRow, int toRow)
        {
            for (int columnIndex = fromCol; columnIndex <= toCol; columnIndex++)
            {
                XWPFTableCell rowcell = table.GetRow(fromRow).GetCell(columnIndex);
                CT_Tc cttc = rowcell.GetCTTc();
                if (cttc.tcPr == null)
                {
                    cttc.AddNewTcPr();
                }

                if (fromCol == toCol)
                {
                    rowcell.GetCTTc().tcPr.AddNewHMerge().val = ST_Merge.restart;
                }
                else
                {
                    rowcell.GetCTTc().tcPr.AddNewHMerge().val = ST_Merge.@continue;
                }
            }

            for (int rowIndex = fromRow; rowIndex <= toRow; rowIndex++)
            {
                XWPFTableCell rowcell = table.GetRow(rowIndex).GetCell(fromCol);
                CT_Tc cttc = rowcell.GetCTTc();
                if (cttc.tcPr == null)
                {
                    cttc.AddNewTcPr();
                }


                if (rowIndex == fromRow)
                {
                    // The first merged cell is set with RESTART merge value  
                    rowcell.GetCTTc().tcPr.AddNewVMerge().val = ST_Merge.restart;
                }
                else
                {
                    // Cells which join (merge) the first one, are set with CONTINUE  
                    rowcell.GetCTTc().tcPr.AddNewVMerge().val = ST_Merge.@continue;
                }
            }
        }
    }
}
