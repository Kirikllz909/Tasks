using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3SubdCars
{
    internal class Excel
    {
        public static byte[] GenerateExcel(Car[] cars)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo("Cars.xlsx")))
            {
                var sheet = package.Workbook.Worksheets.Add("Cars Report");
                //Draw head
                sheet.Cells[2, 2].Value = "Name";
                sheet.Cells[2, 3].Value = "Cost";
                sheet.Cells[2, 4].Value = "Production Country";
                sheet.Cells[2, 5].Value = "Count";
                
                //Print data in cars
                int row = 3;
                int column = 2;
                foreach (var car in cars)
                {
                    sheet.Cells[row, column].Value = car.Name;
                    sheet.Cells[row, column + 1].Value = car.Cost;
                    sheet.Cells[row, column + 2].Value = car.ProductionCountry;
                    sheet.Cells[row, column + 3].Value = car.Count;
                    row++;                    
                }

                //Set autowidth
                sheet.Cells[2, 2, row, column + 3].AutoFitColumns();

                sheet.Cells[2, 2, row, column + 3].Style.Border.BorderAround(ExcelBorderStyle.Double);
                sheet.Cells[2, 2, 2, column + 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                return package.GetAsByteArray();
            }
        }
    }
}
