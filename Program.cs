using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

class Program
{
    private static XmlDocument _employeeXmlDoc;

    static void Main()
    {
        // Load XML document
        XmlDocument xmlDoc1 = new XmlDocument();
        xmlDoc1.Load("/Users/shahmeer/Desktop/csharptask3/data2.xml");

        // Select all "record" nodes
        XmlNodeList recordNodes = xmlDoc1.SelectNodes("//record");

        foreach (XmlNode recordNode in recordNodes)
        {
            string name = recordNode.SelectSingleNode("name")?.InnerText;
            string unit = recordNode.SelectSingleNode("unit")?.InnerText;
            string legalAddress = recordNode.SelectSingleNode("legaladdress")?.InnerText;
            string actualAddress = recordNode.SelectSingleNode("actualaddress")?.InnerText;

            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Unit: {unit}");
            Console.WriteLine($"Legal Address: {legalAddress}");
            Console.WriteLine($"Actual Address: {actualAddress}");
            Console.WriteLine();
        }

        // Call the new method to fetch data from the third XML file
        FetchCurrencyData("/Users/shahmeer/Desktop/csharptask3/data3.xml");
        FetchEmployeeListData("/Users/shahmeer/Desktop/csharptask3/data.xml");
    }
    

    // New method to fetch data from the third XML file
    static void FetchCurrencyData(string filePath)
    {
        XDocument xDoc = XDocument.Load(filePath);

        var currencyRecords = xDoc.Descendants("Record")
            .Select(record => new
            {
                Date = record.Attribute("Date")?.Value,
                Nominal = record.Element("Nominal")?.Value,
                Value = record.Element("Value")?.Value,
                VunitRate = record.Element("VunitRate")?.Value
            });

        foreach (var record in currencyRecords)
        {
            Console.WriteLine($"Date: {record.Date}");
            Console.WriteLine($"Nominal: {record.Nominal}");
            Console.WriteLine($"Value: {record.Value}");
            Console.WriteLine($"VunitRate: {record.VunitRate}");
            Console.WriteLine();
        }
    }
    

    // New method to fetch data from the third XML file
static void FetchEmployeeListData(string filePath)
{
    XDocument xDoc = XDocument.Load(filePath);

    var employees = xDoc.Descendants("Employee")
        .Select(employee => new
        {
            FullName = employee.Element("FullName")?.Value,
            BirthYear = employee.Element("BirthYear")?.Value,
            Address = employee.Element("Address")?.Value,
            Phone = employee.Element("Phone")?.Value,
            WorkList = employee.Element("WorkList")?.Elements()
                .Where(e => e.Name.LocalName.StartsWith("Work"))
                .Select(work => new
                {
                    Name = work.Element("Name")?.Value,
                    StartDate = work.Element("StartDate")?.Value,
                    EndDate = work.Element("EndDate")?.Value,
                    Department = work.Element("Department")?.Value
                })
                .Where(work => !string.IsNullOrWhiteSpace(work.Name))
                .ToList(),
            SalaryList = employee.Element("SalaryList")?.Elements()
                .Where(e => e.Name.LocalName.StartsWith("Salary"))
                .Select(salary => new
                {
                    Year = salary.Element("Year")?.Value,
                    Month = salary.Element("Mounth")?.Value,
                    Size = salary.Element("Size")?.Value
                })
                .Where(salary => !string.IsNullOrWhiteSpace(salary.Year))
                .ToList()
        });

    foreach (var employee in employees)
    {
        Console.WriteLine($"Full Name: {employee.FullName}");
        Console.WriteLine($"Birth Year: {employee.BirthYear}");
        Console.WriteLine($"Address: {employee.Address}");
        Console.WriteLine($"Phone: {employee.Phone}");

        foreach (var work in employee.WorkList)
        {
            Console.WriteLine($"--- Work ---");
            Console.WriteLine($"Name: {work.Name}");
            Console.WriteLine($"Start Date: {work.StartDate}");
            Console.WriteLine($"End Date: {work.EndDate}");
            Console.WriteLine($"Department: {work.Department}");
        }

        foreach (var salary in employee.SalaryList)
        {
            Console.WriteLine($"--- Salary ---");
            Console.WriteLine($"Year: {salary.Year}");
            Console.WriteLine($"Month: {salary.Month}");
            Console.WriteLine($"Size: {salary.Size}");
        }

        Console.WriteLine();
    }
    static void FetchCurrencyData(string filePath)
{
    XDocument xDoc = XDocument.Load(filePath);

    var currencies = xDoc.Descendants("Item")
        .Select(currency => new
        {
            ID = currency.Attribute("ID")?.Value,
            Name = currency.Element("Name")?.Value,
            EngName = currency.Element("EngName")?.Value,
            Nominal = currency.Element("Nominal")?.Value,
            ParentCode = currency.Element("ParentCode")?.Value
        });

    foreach (var currency in currencies)
    {
        Console.WriteLine($"ID: {currency.ID}");
        Console.WriteLine($"Name: {currency.Name}");
        Console.WriteLine($"EngName: {currency.EngName}");
        Console.WriteLine($"Nominal: {currency.Nominal}");
        Console.WriteLine($"ParentCode: {currency.ParentCode}");
        Console.WriteLine("data4");
    }
}

// Call the new method to fetch data from the fourth XML file
FetchCurrencyData("/Users/shahmeer/Desktop/csharptask3/data4.xml");

}



}