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
    public class PdfFieldAttribute : Attribute
    {
        public string FieldName{ get; }
        public PdfFieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }                                                                                                                                                                                                                   	
    }

    public class PdfTools
    {
        public byte[] CreatePdf(dynamic recs, string templatePdfFile)
        {
            // create clone page for each user in users
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfDocument pdfDoc = new PdfDocument(new PdfWriter(memoryStream).SetSmartMode(true));
                pdfDoc.InitializeOutlines();
                PdfDocument srcDoc;
                foreach (var rec in recs)
                {                
                    MemoryStream m = new MemoryStream(FillForm(rec,templatePdfFile));
                    srcDoc = new PdfDocument(new PdfReader(m));
                    // copy content to the resulting PDF
                    srcDoc.CopyPagesTo(1, srcDoc.GetNumberOfPages(), pdfDoc);
                }
                pdfDoc.Close();

                return memoryStream.ToArray();
            }
        }

        internal static byte[] FillForm<T>(T rec, string templatePdfFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                PdfReader reader = new PdfReader(templatePdfFile); //Iput
                PdfWriter writer = new PdfWriter(memoryStream); //output
                PdfDocument pdfDoc = new PdfDocument(reader, writer);
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
                var fields = form.GetFormFields();
            // var fields = 
            //var fields = GetFormFieldsForTempalte(templatePdfFile);

                var properties = typeof(T).GetProperties().Where(x => x.GetCustomAttributes(typeof(PdfFieldAttribute),true).Any());
                foreach (var prop in properties)
                {
                    var attr = prop.GetCustomAttribute<PdfFieldAttribute>();
                    if (!fields.TryGetValue(attr.FieldName, out var pdfField))
                    continue;

                    pdfField.SetValue(prop.GetValue(rec)?.ToString() ?? String.Empty);
                }
                form.FlattenFields(); 
                pdfDoc.Close();            
                return memoryStream.ToArray();
            }
        }

        private static void discoverPDFFields(string pdf)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(pdf));
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
            StreamWriter sw = new StreamWriter(@"output\fields.txt");
            var fields = form.GetFormFields();
            var lines = fields.Select(kvp => kvp.Key);  //Grab PDF Fields from document
            foreach (var l in lines)  //Iterate through the fields to build the set value map
            {
                //Console.WriteLine($"fields[\"{l}\"].SetValue();");
                //Console.WriteLine(l);
                sw.WriteLine(l);
                //var fld = l.Replace("form1[0].#subform[0].","").Replace("[0]","");
                //sw.WriteLine($"fields.First(kvp => kvp.Key.Contains(\"{fld}\")).Value.SetValue()");

            }
            sw.Close();
        }
    }
}