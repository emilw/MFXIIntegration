using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medius.Core.Platform.BusinessRuleEngine.Perspectives;
using MediusTransformer.ConnectionType.Source;
using MediusTransformer.ConnectionType.Source.Function;
using MediusTransformer.ConnectionType.Source.StaticColumn;
using MediusTransformer.Parameters;

namespace Medius.Integration.Platform.BusinessRulePerspectives
{
    public class GenericMasterDataSourcePerspective : CorePerspective
    {
        public Source Source {get;set;}

        public void CreateSource()
        {
            Source = new Source();
            Source.Parameters = new MediusTransformer.Parameters.ParameterCollection();
            Source.StaticColumns = new StaticColumnCollection();
            Source.Functions = new FunctionCollection();
            Source.ConnectionType = MediusTransformer.Connection.Type.Custom;
        }

        public void AddParameter(string name, string value)
        {
            Source.Parameters.Add(new MediusTransformer.Parameters.Parameter() { Name = name, Value = value });
        }

        public void AddStaticColumns(string columnName, string defaultValue)
        {
            Source.StaticColumns.Add(new StaticColumn() { ColumnName = columnName, DefaultValue = defaultValue });
        }

        private Function createFunction(string functionName)
        {
            var func = new Function();
            func.Parameters = new MediusTransformer.Parameters.ParameterCollection();
            return func;
        }

        private Parameter createParameter(string name, string value, bool useLookup = false)
        {
            var par = new Parameter();
            par.Name = name;
            par.Value = value;
            par.UseLookUp = useLookup;
            return par;
        }

        public void AddReplaceNullFunction(string columnName, string replaceWith)
        {
            var func = createFunction("ReplaceNull");
            func.Parameters.Add(createParameter("Value", replaceWith, false));
            Source.Functions.Add(func);
        }

        public void AddReplaceFunction(string columnName, string valueToReplace, string replaceWith)
        {
            var func = createFunction("Replace");
            func.Parameters.Add(createParameter("Value1", valueToReplace));
            func.Parameters.Add(createParameter("Value2", replaceWith));

            Source.Functions.Add(func);
        }

        public void AddRightTrimCharFunction(string columnName, string character)
        {
            var func = createFunction("RightTrimChar");
            func.Parameters.Add(createParameter("ColumnName", columnName));
            func.Parameters.Add(createParameter("Character", character));
            Source.Functions.Add(func);
        }


            /*
             * var function = AddFunction("ReplaceNull", "ADK_ACCOUNT_PROFIT_CENTRE_ON_ACCOUNT", null);
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
             * */
    }
}
