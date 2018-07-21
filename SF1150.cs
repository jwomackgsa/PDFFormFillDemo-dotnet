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
        [PdfFieldAttribute("form1[0].#subform[0].NAME[0]")]
        [ChoCSVRecordField(1, FieldName = "NAME")]
        public string Name {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].SSN[0]")]
        [ChoCSVRecordField(2, FieldName = "SSN")]
        public string SSN {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].AGENCYUSE[0]")]
        [ChoCSVRecordField(3, FieldName = "BLOCK_CURR")]
        public string AgencyUse {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].DateOFSEP[0]")]
        [ChoCSVRecordField(4, FieldName = "SEP_DATE")]
        public string DateOfSeparation {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].Nature[0]")]
        [ChoCSVRecordField(5, FieldName = "NOA_DESC")]
        public string NatureOfSeparation {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].MONTH[0]")]
        [ChoCSVRecordField(6, FieldName = "PRIOR_LV_DATE_MM")]
        public int? PriorLeaveDataMonth {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].DAY[0]")]
        [ChoCSVRecordField(7, FieldName = "PRIOR_LV_DATE_DD")]
        public int? PriorLeaveDateDay {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].YEAR[0]")]
        [ChoCSVRecordField(8, FieldName = "PRIOR_LV_DATE_YYYY")]
        public int? PriorLeaveDateYear {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].ANNUAL[0]")]
        [ChoCSVRecordField(9, FieldName = "AL_CO")]
        public float? CarryoverAnnualLeaveBalance {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].SICK[0]")]
        [ChoCSVRecordField(10, FieldName = "SL_CO")]
        public float? CarryoverSickLeaveBalance {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].RESTORED[0]")]
        [ChoCSVRecordField(11, FieldName = "RL_CO")]
        public float? CarryoverRestoredLeaveBalance {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].STARTMONTH[0]")]
        [ChoCSVRecordField(12, FieldName = "24_MTHS_BEG_MM")]
        public int? TwentyFourMonthsServiceAboardStartMonth {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].STARTYEAR[0]")]
        [ChoCSVRecordField(13, FieldName = "24_MTHS_BEG_DD")]
        public int? TwentyFourMonthsServiceAboardStartDay {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].STARTDAY[0]")]
        [ChoCSVRecordField(14, FieldName = "24_MTHS_BEG_YYYY")]
        public int? TwentyFourMonthsServiceAboardStartYear {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].COMPLETEYEAR[0]")]
        [ChoCSVRecordField(15, FieldName = "24_MTHS_END_MM")]
        public int? TwentyFourMonthsServiceAboardEndMonth {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].COMPLETEDAY[0]")]
        [ChoCSVRecordField(16, FieldName = "24_MTHS_END_DD")]
        public int? TwentyFourMonthsServiceAboardEndDay {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].COMPLETEDATE[0]")]
        [ChoCSVRecordField(17, FieldName = "24_MTHS_END_YYYY")]
        public int? TwentyFourMonthsServiceAboardEndYear {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].MONTH2[0]")]
        [ChoCSVRecordField(18, FieldName = "CURR_LY_ACCRUAL_MM")]
        public int? CurrLeaveYrAccrualPPEMonth {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].DAY2[0]")]
        [ChoCSVRecordField(19, FieldName = "CURR_LY_ACCRUAL_DD")]
        public int? CurrLeaveYrAccrualPPEDay {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].YEAR2[0]")]
        [ChoCSVRecordField(20, FieldName = "CURR_LY_ACCRUAL_YYYY")]
        public int? CurrLeaveYrAccrualPPEYear {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].HOURS_1[0]")]
        [ChoCSVRecordField(21, FieldName = "CURR_LY_ACCRUAL_AL")]
        public float? CurrLeaveYrAccrualAL {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].SICK_1[0]")]
        [ChoCSVRecordField(22, FieldName = "CURR_LY_ACCRUAL_SL")]
        public float? CurrLeaveYrAccrualSL {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].RESTORED_1[0]")]
        [ChoCSVRecordField(23, FieldName = "CURR_LY_ACCRUAL_RL")]
        public float? CurrLeaveYrAccrualRL {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].BEGANMONTH[0]")]
        [ChoCSVRecordField(24, FieldName = "12_MTH_BEG_MM")]
        public int? TweleveMonthAccuralBeganMonth {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].BEGANDAY[0]")]
        [ChoCSVRecordField(25, FieldName = "12_MTH_BEG_DD")]
        public int? TweleveMonthAccuralBeganDay {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].BEGANYEAR[0]")]
        [ChoCSVRecordField(26, FieldName = "12_MTH_BEG_YYYY")]
        public int? TweleveMonthAccuralBeganYear {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].TOTALA[0]")]
        [ChoCSVRecordField(27, FieldName = "TOTAL_AL")]
        public float? TotalAnnualLeave {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].TOTALS[0]")]
        [ChoCSVRecordField(28, FieldName = "TOTAL_SL")]
        public float? TotalSickLeave {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].TOTALR[0]")]
        [ChoCSVRecordField(29, FieldName = "TOTAL_RS")]
        public float? TotalRestoredLeave {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].ANNUALREDUC[0]")]
        [ChoCSVRecordField(30, FieldName = "REDUCT_AL")]
        public float? ReductionCreditAnnualLeave {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].SICKREDUC[0]")]
        [ChoCSVRecordField(31, FieldName = "REDUCT_SL")]
        public float? ReductionCreditSickLeave {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].ABSENTWOPAY[0]")]
        [ChoCSVRecordField(32, FieldName = "HRS_AWOL")]
        public float? AWOLHours {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].TOTALTAKEN[0]")]
        [ChoCSVRecordField(33, FieldName = "TOT_LV_USED_AL")]
        public float? TotalAnnualLeaveUsed {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].TOTALLSICK[0]")]
        [ChoCSVRecordField(34, FieldName = "TOT_LV_SED_SL")]
        public float? TotalSickLeaveUsed {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].TOTALRESTORTED[0]")]
        [ChoCSVRecordField(35, FieldName = "TOT_LV_USED_RS")]
        public float? TotalRestoredLeaveUsed {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].ACCRUALMONTH[0]")]
        [ChoCSVRecordField(36, FieldName = "CUR_BAL_DATE_MM")]
        public int? CurrentBalanceDateMonth {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].ACCURALDAY[0]")]
        [ChoCSVRecordField(37, FieldName = "CUR_BAL_DATE_DD")]
        public int? CurrentBalanceDateDay {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].ACCRUALYEAR[0]")]
        [ChoCSVRecordField(38, FieldName = "CURR_BAL_DATE_YYYY")]
        public int? CurrentBalanceDateYear {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].BALANCEA[0]")]
        [ChoCSVRecordField(39, FieldName = "AL_BAL")]
        public float? AnnualLeaveBalance {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].BALANCES[0]")]
        [ChoCSVRecordField(40, FieldName = "SL_BAL")]
        public float? SickLeaveBalance {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].BALANCER[0]")]
        [ChoCSVRecordField(41, FieldName = "RS_BAL")]
        public float? RestoredLeaveBalance {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].TOTALAHOURS[0]")]
        [ChoCSVRecordField(42, FieldName = "LUMP_SUM_PAY_AL")]
        public float? LumpSumPaymentAnnualLeave {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].TOTALRHOURS[0]")]
        [ChoCSVRecordField(43, FieldName = "LUMP_SUM_PAY_RS")]
        public float? LumpSumPaymentRestoredLeave {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].NUMOFDAYS[0]")]
        [ChoCSVRecordField(44, FieldName = "CUR_BAL_DAYS")]
        public float? CurrentBalanceNumberOfDays {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].SALARYRATE[0]")]
        [ChoCSVRecordField(45, FieldName = "HOURLY_RATE")]
        public float? HourlyRate {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].ACCRUALSEPDAT[0]")]
        [ChoCSVRecordField(46, FieldName = "12_MTH_ACCRUAL")]
        public float? TweleveMonthsAccuralSeparationDateHours {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].AFROMMO[0]")]
        [ChoCSVRecordField(47, FieldName = "LUMP_SUM_RS_BEG_MM")]
        public int? LumpSumLeaveDatesRestoredBeganMonth {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].AFROMDAY[0]")]
        [ChoCSVRecordField(48, FieldName = "LUMP_SUM_RS_BEG_DD")]
        public int? LumpSumLeaveDatesRestoredBeganDay {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].AFROMYEAR[0]")]
        [ChoCSVRecordField(49, FieldName = "LUMP_SUM_RS_BEG_YYYY")]
        public int? LumpSumLeaveDatesRestoredBeganYear {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].DATEFROMMO[0]")]
        [ChoCSVRecordField(50, FieldName = "PRIOR_24_MTHS_BEG_MM")]
        public int? PriorLeaveDatesTwentyFourMonthsBeganMonth {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].DATEFROMDAY[0]")]
        [ChoCSVRecordField(51, FieldName = "PRIOR_24_MTHS_BEG_DD")]
        public int? PriorLeaveDatesTwentyFourMonthsBeganDay {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].DATEFROMYEAR[0]")]
        [ChoCSVRecordField(52, FieldName = "PRIOR_24_MTHS_BEG_YYYY")]
        public int? PriorLeaveDatesTwentyFourMonthsBeganYear {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].DATETOMO[0]")]
        [ChoCSVRecordField(53, FieldName = "PRIOR_24_MTHS_END_MM")]
        public int? PriorLeaveDatesTwentyFourMonthsEndMonth {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].DATETODAY[0]")]
        [ChoCSVRecordField(54, FieldName = "PRIOR_24_MTHS_END_DD")]
        public int? PriorLeaveDatesTwentyFourMonthsEndDay {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].DATETOYEAR[0]")]
        [ChoCSVRecordField(55, FieldName = "PRIOR_24_MTHS_END_YYYY")]
        public int? PriorLeaveDatesTwentyFourMonthsEndYear {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].ATHRUMO[0]")]
        [ChoCSVRecordField(56, FieldName = "LUMP_SUM_RS_END_MM")]
        public int? LumpSumLeaveDatesRestoredThruMonth {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].ATHURDAY[0]")]
        [ChoCSVRecordField(57, FieldName = "LUMP_SUM_RS_END_DD")]
        public int? LumpSumLeaveDatesRestoredThruDay {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].ATHRUYEAR[0]")]
        [ChoCSVRecordField(58, FieldName = "LUMP_SUM_RS_RND_YYYY")]
        public int? LumpSumLeaveDatesRestoredThruYear {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].HOURS4[0]")]
        [ChoCSVRecordField(59, FieldName = "LUMP_SUM_RS_HRS")]
        public float? LumpSumRestoredHours {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].BFROMMO[0]")]
        [ChoCSVRecordField(60, FieldName = "LUMP_SUM_AL_BEG_MM")]
        public int? LumpSumLeaveDatesAnnualBeganMonth {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].BFROMDAY[0]")]
        [ChoCSVRecordField(61, FieldName = "LUMP_SUM_AL_BEG_DD")]
        public int? LumpSumLeaveDatesAnnualBeganDay {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].BFROMYEAR[0]")]
        [ChoCSVRecordField(62, FieldName = "LUMP_SUM_AL_BEG_YYYY")]
        public int? LumpSumLeaveDatesAnnualBeganYear {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].BTHURMO[0]")]
        [ChoCSVRecordField(63, FieldName = "LUMP_SUM_AL_END_MM")]
        public int? LumpSumLeaveDatesAnnualEndMonth {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].BTHURDAY[0]")]
        [ChoCSVRecordField(64, FieldName = "LUMP_SUM_AL_END_DD")]
        public int? LumpSumLeaveDatesAnnualEndDay {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].BTHURYEAR[0]")]
        [ChoCSVRecordField(65, FieldName = "LUMP_SUM_AL_END_YYYY")]
        public int? LumpSumLeaveDatesAnnualEndYear {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].HOURS6[0]")]
        [ChoCSVRecordField(66, FieldName = "LUMP_SUM_AL_HRS")]
        public float? LumpSumAnnualLeaveHours {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].NumericField2[0]")]
        [ChoCSVRecordField(67, FieldName = "HRS_LWOP")]
        public float? LeaveWithoutPayHours {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].ABSENCEMONTH[0]")]
        [ChoCSVRecordField(68, FieldName = "LAST_WIG_MM")]
        public int? LastWIGMonth {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].ABSENCEDAY[0]")]
        [ChoCSVRecordField(69, FieldName = "LAST_WIG_DD")]
        public int? LastWIGDay {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].ABSENCEYEAR[0]")]
        [ChoCSVRecordField(70, FieldName = "LAST_WIG_YYYY")]
        public int? LastWIGYear {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].REGMO[0]")]
        [ChoCSVRecordField(71, FieldName = "MIL_LV_BEG_MM")]
        public int? MilitaryLeaveBeganMonth {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].REGDAY[0]")]
        [ChoCSVRecordField(72, FieldName = "MIL_LV_BEG_DD")]
        public int? MilitaryLeaveBeganDay {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].REGYEAR[0]")]
        [ChoCSVRecordField(73, FieldName = "MIL_LV_BEG_YYYY")]
        public int? MilitaryLeaveBeganYear {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].TOREGMO[0]")]
        [ChoCSVRecordField(74, FieldName = "MIL_LV_END_MM")]
        public int? MilitaryLeaveEndMonth {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].TOREGDAY[0]")]
        [ChoCSVRecordField(75, FieldName = "MIL_LV_END_DD")]
        public int? MilitaryLeaveEndDay {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].TOREGYEAR[0]")]
        [ChoCSVRecordField(76, FieldName = "MIL_LV_END_YYYY")]
        public int? MilitaryLeaveEndYear {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].REMARKS[0]")]
        [ChoCSVRecordField(77, FieldName = "REMARK")]
        public string Remark {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].SignatureField1[0]")]
        [ChoCSVRecordField(78, FieldName = "CERTIFIED_BY")]
        public string CertifiedBy {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].TitleAgency[0]")]
        [ChoCSVRecordField(79, FieldName = "TITLE_LOCATION")]
        public string TitleAgencyLocation {get; set;}


        [PdfFieldAttribute("form1[0].#subform[0].Date[0]")]
        [ChoCSVRecordField(80, FieldName = "DATE")]
        public string Date {get; set;}

        public void GenerateSF1150(string template, string csvData, string output)
        {
            ChoCSVRecordConfiguration etlConfig = new ChoCSVRecordConfiguration();
            //etlConfig.Delimiter = ";";
            etlConfig.MayContainEOLInData = true; //Handling for multi-line values in CSV        
            var reader = new ChoCSVReader<SF1150>(csvData,etlConfig).WithFirstLineHeader();            
            var processor = new PdfTools();
            byte[] result = processor.CreatePdf(reader, template);  //Create multi-page pdf using the template for each record.
            File.WriteAllBytes(output, result);  //Write the pdf in memory out to file. */        
        }       
        
    }

}