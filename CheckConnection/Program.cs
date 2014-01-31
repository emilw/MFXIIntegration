using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediusTransformer.ConnectionType.Source.StaticColumn;
using MediusTransformer.Parameters;

namespace CheckConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            runJob();
            Console.ReadLine();
        }


        private static void runJob()
        {

            Console.Write("In the loop");

            var parameters = new MediusTransformer.Parameters.ParameterCollection();
            var staticColumns = new StaticColumnCollection();

            parameters.Add(new Parameter() { Name = "DataStructurePath", Value = @"E:\Temp\EmilWXIStuff\MIG\SPCSv1.3\Other\SPCS_Structure.xml" });
            parameters.Add(new Parameter() { Name = "AdkNetWrapperPath", Value = @"C:\Program Files\SPCS\SPCS Administration\AdkNet4Wrapper.dll" });
            parameters.Add(new Parameter() { Name = "AdkNetWrapperType", Value = @"AdkNet4Wrapper.Api" });

            staticColumns.Add(new StaticColumn() { ColumnName = "CompanyId", DefaultValue = "[[CompanyId]]" });
            staticColumns.Add(new StaticColumn() { ColumnName = "AccountingType", DefaultValue = "Dimension1" });
            staticColumns.Add(new StaticColumn() { ColumnName = "Dim3Required", DefaultValue = "1" });

            var source = new MediusTransformer.ConnectionType.Source.Source()
            {
                Remote = "yes;medius-integration;integrations/000037;INTEGRATIONDEV_SEND;CXFXZPMIv3W6xGOzA36PY0d8VgGiohLWYdKZ6onVo7Q=;5;5;1000;1000000;1000000;1000000",
                ConnectionType = MediusTransformer.Connection.Type.Custom,
                ConnectionSubType = 0,
                AdapterNameSpace = "Adapters.ERP.SPCS_500",
                AssemblyPath = @"E:\Temp\EmilWXIStuff\MIG\SPCSv1.3\SPCS_500.dll",
                ConnectionString = @"\\deepblue-wm1\SPCS_Administration\Gemensamma filer;\\deepblue-wm1\SPCS_Administration\Företag\Ovnbol2000",
                SelectionStatement = "ADK_DB_ACCOUNT",
                Multidimensional = false,
                Parameters = parameters,
                StaticColumns = staticColumns
            };

            var result = source.getDataTable();

            Console.WriteLine(result.Rows.Count);
        }
    }
}
