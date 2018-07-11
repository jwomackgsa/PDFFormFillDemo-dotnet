using System;
using System.IO;
using System.Text.RegularExpressions;
using iText.Forms;
using iText.Forms.Fields;
using iText.IO;
using iText.Kernel.Pdf;
using System.Collections.Generic;
using System.Linq;
using ChoETL;

namespace pdffillerdncore
{
    public class SF1150
    {
        public bool GenerateSF1150(string template, string csvData, string output)
        {
            ChoCSVRecordConfiguration etlConfig = new ChoCSVRecordConfiguration();
            etlConfig.Delimiter = ";";
            etlConfig.MayContainEOLInData = true; //Handling for multi-line values in CSV
            var reader = new ChoCSVReader(csvData,etlConfig).WithFirstLineHeader();
            byte[] result = createPdf(reader, template);  //Create multi-page pdf using the template for each record.
            File.WriteAllBytes(output, result);  //Write the pdf in memory out to file.
            return true;
        }      
       private static byte[] createPdf(dynamic recs, string templatePdfFile)
        {
            // create clone page for each user in users
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfDocument pdfDoc = new PdfDocument(new PdfWriter(memoryStream).SetSmartMode(true));
                pdfDoc.InitializeOutlines();
                PdfDocument srcDoc;
                foreach (var rec in recs)
                {                
                    MemoryStream m = new MemoryStream(fillForm(rec,templatePdfFile));
                    srcDoc = new PdfDocument(new PdfReader(m));
                    // copy content to the resulting PDF
                    srcDoc.CopyPagesTo(1, srcDoc.GetNumberOfPages(), pdfDoc);
                }
                pdfDoc.Close();

                return memoryStream.ToArray();
            }
        }

        private static byte[] fillForm(dynamic rec, string templatePdfFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                PdfReader reader = new PdfReader(templatePdfFile); //Iput
                PdfWriter writer = new PdfWriter(memoryStream); //output
                PdfDocument pdfDoc = new PdfDocument(reader, writer);
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
                var fields = form.GetFormFields();

            
                //form.GetField("Reason").SetVisibility(3);
                
                //Since we control the template, we can directly set all the fields 
                fields.First(kvp => kvp.Key.Contains("NAME")).Value.SetValue(rec.NAME);    
                fields.First(kvp => kvp.Key.Contains("SSN")).Value.SetValue(rec.SSN);
                fields.First(kvp => kvp.Key.Contains("AGENCYUSE")).Value.SetValue(rec.BLOCK_CURR);
                fields.First(kvp => kvp.Key.Contains("DateOFSEP")).Value.SetValue(rec.SEP_DATE);
                fields.First(kvp => kvp.Key.Contains("Nature")).Value.SetValue(Regex.Replace(rec.SEP_DESC, @"\r\n?|\\n|\n", Environment.NewLine));
                fields.First(kvp => kvp.Key.Contains("MONTH[0]")).Value.SetValue(rec.PRIOR_LV_DATE_MM); //Box 7
                fields.First(kvp => kvp.Key.Contains("DAY[0]")).Value.SetValue(rec.PRIOR_LV_DATE_DD);  //Box 7
                fields.First(kvp => kvp.Key.Contains("YEAR[0]")).Value.SetValue(rec.PRIOR_LV_DATE_YYYY); //Box 7
                fields.First(kvp => kvp.Key.Contains("ANNUAL")).Value.SetValue(rec.AL_CO); //Box 7
                fields.First(kvp => kvp.Key.Contains("SICK")).Value.SetValue(rec.SL_CO); //Box 7
                fields.First(kvp => kvp.Key.Contains("RESTORED")).Value.SetValue(rec.RL_CO);  //Box 7
                fields.First(kvp => kvp.Key.Contains("MONTH2")).Value.SetValue(rec.CURR_LY_ACCRUAL_MM); //Box 8
                fields.First(kvp => kvp.Key.Contains("DAY2")).Value.SetValue(rec.CURR_LY_ACCRUAL_DD); //Box 8
                fields.First(kvp => kvp.Key.Contains("YEAR2")).Value.SetValue(rec.CURR_LY_ACCRUAL_YYYY); //Box 8
                fields.First(kvp => kvp.Key.Contains("HOURS_1")).Value.SetValue(rec.CURR_LY_ACCRUAL_AL); //Box 8
                fields.First(kvp => kvp.Key.Contains("SICK_1")).Value.SetValue(rec.CURR_LY_ACCRUAL_SL); //Box 8
                fields.First(kvp => kvp.Key.Contains("RESTORED_1")).Value.SetValue(rec.CURR_LY_ACCRUAL_RL); // Box 8
                fields.First(kvp => kvp.Key.Contains("TOTALA")).Value.SetValue(rec.TOTAL_AL);  //Box 9
                fields.First(kvp => kvp.Key.Contains("TOTALS")).Value.SetValue(rec.TOTAL_SL); //Box 9
                fields.First(kvp => kvp.Key.Contains("TOTALR")).Value.SetValue(rec.TOTAL_RS); //Box 9
                fields.First(kvp => kvp.Key.Contains("ANNUALREDUC")).Value.SetValue(rec.REDUCT_AL); //Box 10
                fields.First(kvp => kvp.Key.Contains("SICKREDUC")).Value.SetValue(rec.REDUCT_SL); //Box 10
                fields.First(kvp => kvp.Key.Contains("TOTALTAKEN")).Value.SetValue(rec.TOT_LV_USED_AL ?? String.Empty); //Box 11
                fields.First(kvp => kvp.Key.Contains("TOTALLSICK")).Value.SetValue(rec.TOT_LV_SED_SL ?? String.Empty); //Box 11
                fields.First(kvp => kvp.Key.Contains("TOTALRESTORTED")).Value.SetValue(rec.TOT_LV_USED_RS ?? String.Empty); //Box 11
                fields.First(kvp => kvp.Key.Contains("BALANCEA")).Value.SetValue(rec.AL_BAL); //Box 12
                fields.First(kvp => kvp.Key.Contains("BALANCES")).Value.SetValue(rec.SL_BAL); //Box 12
                fields.First(kvp => kvp.Key.Contains("BALANCER")).Value.SetValue(rec.RS_BAL); //Box 12
                		
                

                /*
                
                
                fields.First(kvp => kvp.Key.Contains("YES")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("NO")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("LASTDATE")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("NumericField1")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("MORETHAN")).Value.SetValue() //Box 6
                fields.First(kvp => kvp.Key.Contains("LESSTHAN")).Value.SetValue() //Box 6
                fields.First(kvp => kvp.Key.Contains("YEARS")).Value.SetValue() //Box 6
                fields.First(kvp => kvp.Key.Contains("MONTHS")).Value.SetValue() //Box 6
                fields.First(kvp => kvp.Key.Contains("DAYS")).Value.SetValue() //Box 6
                                
                fields.First(kvp => kvp.Key.Contains("RESTOREDREDUC")).Value.SetValue()  //Box 10
                
                
                fields.First(kvp => kvp.Key.Contains("TOTALAHOURS")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("TOTALRHOURS")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("FROMMO")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("CTHRUMO")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("CFROM")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("BTHURMO")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("BFROMMO")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("ATHRUMO")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("AFROMMO")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("THURMO")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("FROMDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("CTHRUDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("CFROMDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("BTHURDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("BFROMDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("ATHURDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("AFROMDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("THRUDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("FROMYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("CTHRUYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("CFROMYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("BTHURYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("BFROMYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("ATHRUYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("AFROMYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("THRUYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("HOURS1")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("HOURS8")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("HOURS7")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("HOURS6")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("HOURS5")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("HOURS4")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("HOURS3")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("HOURS2")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("NumericField2")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("ABSENCEMONTH")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("ABSENCEDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("ABSENCEYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("TOTALLWOPHOURS")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("STARTMONTH")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("STARTYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("STARTDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("COMPLETEYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("COMPLETEDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("COMPLETEDATE")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("BEGANMONTH")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("BEGANYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("BEGANDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("ABSENTWOPAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("ACCRUALMONTH")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("ACCRUALYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("ACCURALDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("NUMOFDAYS")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("ACCRUALSEPDAT")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATEFROMMO")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATETOYEAR4")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATETODAY4")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATETOMO4")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATEFROMYEAR4")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATEFROMDAY4")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATEFROMMO4")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATETOYEAR3")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATETODAY3")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATETOMO3")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATEFROMYEAR3")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATEFROMDAY3")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATEFROMMO3")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATETOYEAR2")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATETODAY2")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATETOMO2")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATEFROMYEAR2")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATEFROMDAY2")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATEFROMMO2")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATETOYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATETODAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATETOMO")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATEFROMYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("DATEFROMDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("REGMO")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("TOREGYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("TOREGDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("TOREGMO")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("REGYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("REGDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("SPECIALMO")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("SPECIALYEAR2")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("SPECIALDAY2")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("SPECIALMO2")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("SPECIALYEAR")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("SPECIALDAY")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("REMARKS")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("TitleAgency")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("Date")).Value.SetValue()
                
                fields.First(kvp => kvp.Key.Contains("SALARYRATE")).Value.SetValue()
                
                fields.First(kvp => kvp.Key.Contains("TextField1")).Value.SetValue()
                fields.First(kvp => kvp.Key.Contains("SignatureField1")).Value.SetValue()
                 */

                form.FlattenFields();
                pdfDoc.Close();            
                return memoryStream.ToArray();
            }
        }
    }
}