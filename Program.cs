using System;
using System.IO;
using iText.Forms;
using iText.Forms.Fields;
using iText.IO;
using iText.Kernel.Pdf;
using System.Collections.Generic;
using System.Linq;
using ChoETL;
using Newtonsoft.Json;

namespace pdffillerdncore
{
    class Program
    { 
        static void Main(string[] args)
        {
            Console.WriteLine("Begin PDF Processing");
            //string srcPdf = @"resources\sf3101_template.pdf";  //Set template form as source
            string srcPdf = @"resources\SF1150-77.pdf";  //Set template form as source
            string destPdf = @"output\mergedsf1150_" + DateTime.Now.ToString("MMddyyyyHHmmss") + ".pdf";  //Set named output pdf                
            
            var myclass = new SF1150();
            myclass.GenerateSF1150(srcPdf,@"C:\DEV\PDFFormFillDemo-dotnet\resources\par166p1_revised.csv",destPdf);
            //var myclass = new testClass();
            //myclass.GenerateTest();

            //Program.discoverPDFFields(srcPdf);
            //Program.testFillSF1150(srcPdf,destPdf);
            //Program.createPocoFromPDF(srcPdf);
            //Program.FixFieldNamesPDF(srcPdf);
            //Read CSV File into POCO using ChoETL Lib
            /* 
            var recs = new ChoCSVReader<SF3101>(@"C:\DEV\pdffillerdncore\resources\sf3101_sample_data.csv").WithFirstLineHeader(); 
            byte[] result = createPdf(recs, srcPdf);  //Create multi-page pdf using the template for each record.
            File.WriteAllBytes(destPdf, result);  //Write the pdf in memory out to file.
             */    
            Console.WriteLine("End PDF Processing");
        }
        
        private static byte[] createPdf(dynamic recs, string templatePdfFile)
        {
            // create clone page for each user in users
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfDocument pdfDoc = new PdfDocument(new PdfWriter(memoryStream).SetSmartMode(true));
                pdfDoc.InitializeOutlines();
                PdfDocument srcDoc;
                recs.Configuration.MayContainEOLInData = true; //Handling for multi-line values in CSV
                foreach (var rec in recs)
                {                
                    MemoryStream m = new MemoryStream(fillFormSF3101(rec,templatePdfFile));
                    srcDoc = new PdfDocument(new PdfReader(m));
                    // copy content to the resulting PDF
                    srcDoc.CopyPagesTo(1, srcDoc.GetNumberOfPages(), pdfDoc);
                }
                pdfDoc.Close();

                return memoryStream.ToArray();
            }
        }

        private static byte[] fillFormSF3101(SF3101 rec, string templatePdfFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                PdfReader reader = new PdfReader(templatePdfFile); //Iput
                PdfWriter writer = new PdfWriter(memoryStream); //output
                PdfDocument pdfDoc = new PdfDocument(reader, writer);
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
                var fields = form.GetFormFields();

                form.RemoveField("SAVE"); //remove save button          
                form.RemoveField("PRINT");  //remove print button
                form.RemoveField("RESET");  //remove reset button
                //form.GetField("Reason").SetVisibility(3);
                
                //Since we control the template, we can directly set all the fields
                fields["SSN"].SetValue(rec.SSN);
                fields["Name"].SetValue(rec.Name);
                fields["DOB"].SetValue(rec.DOB);
                fields["Agency"].SetValue(rec.Agency);
                fields["Payroll Office No"].SetValue(rec.PayrollOfficeNo);
                if (!String.IsNullOrEmpty(rec.Location)) { fields["Location"].SetValue(rec.Location); }
                if (!String.IsNullOrEmpty(rec.Reason)) { fields["Reason"].SetValue(rec.Reason); }
                fields["Data on SF 3100"].SetValue(rec.DataOnSF3100);
                fields["Total culmulative"].SetValue(rec.TotalCulmulative);
                fields["Corrected Data"].SetValue(rec.CorrectedData);
                fields["Overstatement"].SetValue(rec.Overstatement);
                fields["Total culmulative deductions"].SetValue(rec.TotalCulmulativeDeductions);
                fields["SF 3100 Data 2"].SetValue(rec.SF3100Data2);
                fields["SF 3100 Corrected Data 2"].SetValue(rec.SF3100CorrectedData2);
                fields["SF 3100 Data"].SetValue(rec.SF3100Data);
                fields["SF 3100 Corrected Data"].SetValue(rec.SF3100CorrectedData);
                fields["SF 3100 Corrected Data 3"].SetValue(rec.SF3100CorrectedData3);
                fields["SF 2812 Number"].SetValue(rec.SF2812Number);
                fields["Register"].SetValue(rec.Register);
                fields["SF2812 - dated"].SetValue(rec.SF2812Dated);
                fields["SF 3100 Data 3"].SetValue(rec.SF3100Data3);
                fields["Title"].SetValue(rec.Title);
                fields["Telephone Number"].SetValue(rec.TelephoneNumber);
                fields["Register - dated"].SetValue(rec.RegisterDated);
                fields["Date signed"].SetValue(rec.DateSigned);

                form.FlattenFields();
                pdfDoc.Close();            
                return memoryStream.ToArray();
            }
        }    

        private static void createPocoFromPDF(string pdf)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(pdf));
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
            var fields = form.GetFormFields();
            var lines = fields.Select(kvp => kvp.Key);  //Grab PDF Fields from document
            foreach (var l in lines)  //Iterate through the fields to build the set value map
            {                
                Console.WriteLine($"public string {l.Replace(" ",string.Empty).Replace(".",string.Empty)} {{ get; set; }}");
            }
        }

        private static void FixFieldNamesPDF(string pdf)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(pdf),new PdfWriter(@"output\3100_new.pdf"));
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
            var fields = form.GetFormFields();
            var lines = fields.Select(kvp => kvp.Key);  //Grab PDF Fields from document
            //int count = 0;
            foreach (var l in lines)  //Iterate through the fields to build the set value map
            {                
                //Console.WriteLine($"public string {l.Replace(" ",string.Empty).Replace(".",string.Empty)} {{ get; set; }}");
                if (l.Contains("Year", StringComparison.OrdinalIgnoreCase))
                {
                    
                    //form.GetFormFields().Put($"EffectiveDate{count}", f);
                    //fields[l].SetFieldName($"EffectiveDate{count}");
                    //count++;
                }
                
                
            }
            pdfDoc.Close();            
        }
    }
}
