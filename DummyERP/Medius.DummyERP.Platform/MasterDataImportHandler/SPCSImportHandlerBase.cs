using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Integration.Platform.MasterDataHandler;
using Medius.Integration.Platform.SchedulerAction;
using MediusTransformer.ConnectionType.Source.Function;
using MediusTransformer.ConnectionType.Source.StaticColumn;
using MediusTransformer.Parameters;

namespace Medius.DummyERP.Platform.MasterDataImportHandler
{
    public abstract class SPCSImportHandlerBase : BaseMasterDataHandler, IMasterDataHandler
    {
        protected Function AddFunction(string functionName, string functionDestination, ParameterCollection parameters, int functionPriority = 0)
        {
            if (parameters == null)
                parameters = new ParameterCollection();

            var function = new Function() { FunctionName = functionName, FunctionDestination = functionDestination, FunctionPriority = functionPriority, Parameters = parameters };
            return function;
        }

        protected override MediusTransformer.ConnectionType.Source.Source PrepareSource()
        {
            var parameters = new MediusTransformer.Parameters.ParameterCollection();
            var staticColumns = new StaticColumnCollection();
            var functions = new FunctionCollection();

            parameters.Add(new Parameter() { Name = "DataStructurePath", Value = @"E:\Temp\EmilWXIStuff\MIG\SPCSv1.3\Other\SPCS_Structure.xml" });
            parameters.Add(new Parameter() { Name = "AdkNetWrapperPath", Value = @"C:\Program Files\SPCS\SPCS Administration\AdkNet4Wrapper.dll" });
            parameters.Add(new Parameter() { Name = "AdkNetWrapperType", Value = @"AdkNet4Wrapper.Api" });

            staticColumns.Add(new StaticColumn() { ColumnName = "CompanyId", DefaultValue = "[[CompanyId]]" });
            staticColumns.Add(new StaticColumn() { ColumnName = "AccountingType", DefaultValue = "Dimension1" });
            staticColumns.Add(new StaticColumn() { ColumnName = "Dim3Required", DefaultValue = "1" });
            
            var function = AddFunction("ReplaceNull", "ADK_ACCOUNT_PROFIT_CENTRE_ON_ACCOUNT", null);
            function.Parameters.Add(new Parameter() { Name = "Value", Value = "0", UseLookUp = false });
            functions.Add(function);

            function = AddFunction("Replace", "ADK_ACCOUNT_PROFIT_CENTRE_ON_ACCOUNT", null);
            function.Parameters.Add(new Parameter() { Name = "Value1", Value = "E", UseLookUp = false });
            function.Parameters.Add(new Parameter() { Name = "Value2", Value = "1", UseLookUp = false });
            functions.Add(function);

            function = AddFunction("Replace", "ADK_ACCOUNT_PROFIT_CENTRE_ON_ACCOUNT", null);
            function.Parameters.Add(new Parameter() { Name = "Value1", Value = "O", UseLookUp = false });
            function.Parameters.Add(new Parameter() { Name = "Value2", Value = "0", UseLookUp = false });
            functions.Add(function);

            function = AddFunction("Replace", "ADK_ACCOUNT_PROFIT_CENTRE_ON_ACCOUNT", null);
            function.Parameters.Add(new Parameter() { Name = "Value1", Value = "A", UseLookUp = false });
            function.Parameters.Add(new Parameter() { Name = "Value2", Value = "2", UseLookUp = false });
            functions.Add(function);

            function = AddFunction("Replace", "ADK_ACCOUNT_PROFIT_CENTRE_ON_ACCOUNT", null);
            function.Parameters.Add(new Parameter() { Name = "Value1", Value = "S", UseLookUp = false });
            function.Parameters.Add(new Parameter() { Name = "Value2", Value = "0", UseLookUp = false });
            functions.Add(function);

            function = AddFunction("RightTrimChar", "ADK_ACCOUNT_PROFIT_CENTRE_ON_ACCOUNT", null);
            function.Parameters.Add(new Parameter() { Name = "ColumnName", Value = "ADK_ACCOUNT_PROFIT_CENTRE_ON_ACCOUNT", UseLookUp = false });
            function.Parameters.Add(new Parameter() { Name = "Character", Value = "?", UseLookUp = false });
            functions.Add(function);

            var source = new MediusTransformer.ConnectionType.Source.Source()
            {
                Remote = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                ConnectionType = MediusTransformer.Connection.Type.Custom,
                ConnectionSubType = 0,
                AdapterNameSpace = "Adapters.ERP.SPCS_500",
                AssemblyPath = @"E:\Temp\EmilWXIStuff\MIG\SPCSv1.3\SPCS_500.dll",
                ConnectionString = @"\\deepblue-wm1\SPCS_Administration\Gemensamma filer;\\deepblue-wm1\SPCS_Administration\Företag\Ovnbol2000",
                SelectionStatement = "ADK_DB_ACCOUNT",
                Multidimensional = false,
                Parameters = parameters,
                StaticColumns = staticColumns,
                Functions = functions
            };

            return source;
        }
    }
}
