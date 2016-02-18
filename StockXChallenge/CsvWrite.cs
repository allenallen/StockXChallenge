using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PortfolioController
{
    public class CsvWrite
    {
        public static string data(Dealers dealers)
        {           
            string delimiter = ",";


            StringBuilder sb = new StringBuilder();

            Console.WriteLine("PORTFOLIO\tDATE\t\tSECURITY_ID\tPOSITION\tAVG_COST");

            sb.Append("PORTFOLIO" + delimiter);
            sb.Append("DATE" + delimiter);
            sb.Append("SECURITY_ID" + delimiter);
            sb.Append("POSITION" + delimiter);
            sb.Append("AVG_COST");
            sb.AppendLine("");

            foreach (PortfolioModel p in dealers.D1)
            {
                WriteToConsole(p);
                sb.Append(p.PortfolioName + delimiter);
                sb.Append(p.Date + delimiter);
                sb.Append(p.SecurityID + delimiter);
                sb.Append(p.Position + delimiter);
                sb.Append(p.AvgCost);
                sb.AppendLine("");
            }

            foreach (PortfolioModel p in dealers.Dasys)
            {
                WriteToConsole(p);
                sb.Append(p.PortfolioName + delimiter);
                sb.Append(p.Date + delimiter);
                sb.Append(p.SecurityID + delimiter);
                sb.Append(p.Position + delimiter);
                sb.Append(p.AvgCost);
                sb.AppendLine("");
            }

            foreach (PortfolioModel p in dealers.D4)
            {
                WriteToConsole(p);
                sb.Append(p.PortfolioName + delimiter);
                sb.Append(p.Date + delimiter);
                sb.Append(p.SecurityID + delimiter);
                sb.Append(p.Position + delimiter);
                sb.Append(p.AvgCost);
                sb.AppendLine("");
            }

            foreach (PortfolioModel p in dealers.Mm)
            {
                WriteToConsole(p);
                sb.Append(p.PortfolioName + delimiter);
                sb.Append(p.Date + delimiter);
                sb.Append(p.SecurityID + delimiter);
                sb.Append(p.Position + delimiter);
                sb.Append(p.AvgCost);
                sb.AppendLine("");
            }
            
            string toCsv = sb.ToString();          
            return toCsv;
        }

        private static void WriteToConsole(PortfolioModel p)
        {
       
            Console.Write(p.PortfolioName + "\t\t" + p.Date);
            if (!p.PortfolioName.Equals("DASYS"))
            {

                if (p.SecurityID.Equals("php Curncy"))
                {
                    Console.Write("\t" + p.SecurityID + "\t" + p.Position + "\t\t" + p.AvgCost);
                }
                else
                {
                    Console.Write("\t" + p.SecurityID + "\t\t" + p.Position + "\t\t" + p.AvgCost);
                }
            }
            else
            {
                if (p.SecurityID.Equals("php Curncy"))
                {
                    Console.Write("\t" + p.SecurityID + "\t" + p.Position + "\t\t" + p.AvgCost);
                }
                else
                {
                    Console.Write("\t" + p.SecurityID + "\t\t" + p.Position + "\t\t" + p.AvgCost);
                }
            }
            Console.WriteLine("");

        }
    }

    //public class Calculator
    //{
    //    public static List<model.Portfolio> calculateCurrencyPosCost(List<model.Portfolio> p)
    //    {
    //        double d1 = 0;
    //        double dasys = 0;
    //        double d4 = 0;
    //        double mm = 0;
    //        double er = 0;

    //        double d1Capital = 0;
    //        double.TryParse(ConfigurationManager.AppSettings["dealer1capital"].ToString(), out d1Capital);

    //        double dasysCapital = 0;
    //        double.TryParse(ConfigurationManager.AppSettings["dasyscapital"].ToString(), out dasysCapital);
            
    //        double d4Capital = 0;
    //        double.TryParse(ConfigurationManager.AppSettings["dealer4capital"].ToString(), out d4Capital);
            
    //        double mmCapital = 0;
    //        double.TryParse(ConfigurationManager.AppSettings["mmcapital"].ToString(), out mmCapital);


    //        foreach (model.Portfolio a in p)
    //        { 
    //            if (a.PortfolioName == "D1") d1 += pcost;
    //            if (a.PortfolioName == "DASYS") dasys += pcost;
    //            if (a.PortfolioName == "D4") d4 += pcost;
    //            if (a.PortfolioName == "MM") mm += pcost;
    //            if (a.PortfolioName == "ER") er += pcost;
    //        }

    //        foreach (model.Portfolio a in p)
    //        {
    //            if (a.PortfolioName == "D1" && a.SecurityID == "php Curncy")
    //            {
    //                a.Poscost = ((d1Capital) - d1).ToString();
    //            }
    //            else if (a.PortfolioName == "DASYS" && a.SecurityID == "php Curncy")
    //            {
    //                a.Poscost = ((dasysCapital) - dasys).ToString();
    //            } 
    //            else if (a.PortfolioName == "D4" && a.SecurityID == "php Curncy")
    //            {
    //                a.Poscost = ((d4Capital) - d4).ToString();
    //            } 
    //            else if (a.PortfolioName == "MM" && a.SecurityID == "php Curncy")
    //            {
    //                a.Poscost = ((mmCapital) - mm).ToString();
    //            }
    //        }

    //        return p;
    //    }
    }

