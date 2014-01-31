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
using System.ComponentModel.Composition;

namespace Medius.DummyERP.Platform.MasterDataImportHandler
{
    
    [Export(typeof(IMasterDataHandler))]
    public class AccountImport : SPCSImportHandlerBase, IMasterDataHandler
    {
        public override Core.Entities.MasterData CreateNewInstance()
        {
            var dimensionValue = new Medius.Enterprise.Entities.Accounting.Dimensions.DimensionValue();
            return dimensionValue;
        }

        protected override MediusTransformer.ConnectionType.Source.Source PrepareSource()
        {
            var source = base.PrepareSource();
            source.SelectionStatement = "ADK_DB_ACCOUNT";
            return source;
        }

        public override System.Type MasterDataType
        {
            get
            {
                return typeof(Medius.Enterprise.Entities.Accounting.Dimensions.DimensionValue);
            }
        }
    }
}

/*

<SourceId>SPCS_Account</SourceId>
			<ConnectionType>Custom</ConnectionType>
			<ConnectionSubType>0</ConnectionSubType>
			<AdapterNameSpace>Adapters.ERP.SPCS_500</AdapterNameSpace>
			<AssemblyPath>E:\Temp\EmilWXIStuff\MIG\SPCSv1.3\SPCS_500.dll</AssemblyPath>
			<Remote />
			<ConnectionString>\\deepblue-wm1\SPCS_Administration\Gemensamma filer;\\deepblue-wm1\SPCS_Administration\Företag\Ovnbol2000</ConnectionString>
			<SelectionStatement>ADK_DB_ACCOUNT</SelectionStatement>
			<Multidimensional>false</Multidimensional>
			<Parameters>
				<Parameter>
					<Name>DataStructurePath</Name>
					<Value>E:\Temp\EmilWXIStuff\MIG\SPCSv1.3\Other\SPCS_Structure.xml</Value>
				</Parameter>
				<Parameter>
					<Name>AdkNetWrapperPath</Name>
					<Value>C:\Program Files\SPCS\SPCS Administration\AdkNet4Wrapper.dll</Value>
				</Parameter>
				<Parameter>
					<Name>AdkNetWrapperType</Name>
					<Value>AdkNet4Wrapper.Api</Value>
				</Parameter>
			</Parameters>
			<StaticColumns>
				<StaticColumn>
					<ColumnName>CompanyId</ColumnName>
					<DefaultValue>[[CompanyId]]</DefaultValue>
				</StaticColumn>
				<StaticColumn>
					<ColumnName>AccountingType</ColumnName>
					<DefaultValue>Dimension1</DefaultValue>
				</StaticColumn>
				<StaticColumn>
					<ColumnName>Dim3Required</ColumnName>
					<DefaultValue>1</DefaultValue>
				</StaticColumn>
			</StaticColumns>
			<Functions>
				<Function>
					<FunctionName>ReplaceNull</FunctionName>
					<FunctionDestination>ADK_ACCOUNT_PROFIT_CENTRE_ON_ACCOUNT</FunctionDestination>
					<FunctionPriority>0</FunctionPriority>
					<Parameters>
						<Parameter>
							<Name>Value</Name>
							<Value>0</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>						
					</Parameters>
				</Function>
				<Function>
					<FunctionName>Replace</FunctionName>
					<FunctionDestination>ADK_ACCOUNT_PROFIT_CENTRE_ON_ACCOUNT</FunctionDestination>
					<FunctionPriority>0</FunctionPriority>
					<Parameters>
						<Parameter>
							<Name>Value1</Name>
							<Value>E</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
						<Parameter>
							<Name>Value2</Name>
							<Value>1</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
					</Parameters>
				</Function>
				<Function>
					<FunctionName>Replace</FunctionName>
					<FunctionDestination>ADK_ACCOUNT_PROFIT_CENTRE_ON_ACCOUNT</FunctionDestination>
					<FunctionPriority>0</FunctionPriority>
					<Parameters>
						<Parameter>
							<Name>Value1</Name>
							<Value>O</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
						<Parameter>
							<Name>Value2</Name>
							<Value>0</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
					</Parameters>
				</Function>
				<Function>
					<FunctionName>Replace</FunctionName>
					<FunctionDestination>ADK_ACCOUNT_PROFIT_CENTRE_ON_ACCOUNT</FunctionDestination>
					<FunctionPriority>0</FunctionPriority>
					<Parameters>
						<Parameter>
							<Name>Value1</Name>
							<Value>A</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
						<Parameter>
							<Name>Value2</Name>
							<Value>2</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
					</Parameters>
				</Function>
				<Function>
					<FunctionName>Replace</FunctionName>
					<FunctionDestination>ADK_ACCOUNT_PROFIT_CENTRE_ON_ACCOUNT</FunctionDestination>
					<FunctionPriority>0</FunctionPriority>
					<Parameters>
						<Parameter>
							<Name>Value1</Name>
							<Value>S</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
						<Parameter>
							<Name>Value2</Name>
							<Value>0</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
					</Parameters>
				</Function>
				<Function>
					<FunctionName>RightTrimChar</FunctionName>
					<FunctionDestination>ADK_ACCOUNT_PROFIT_CENTRE_ON_ACCOUNT</FunctionDestination>
					<FunctionPriority>0</FunctionPriority>
					<Parameters>
						<Parameter>
							<Name>ColumnName</Name>
							<Value>ADK_ACCOUNT_PROFIT_CENTRE_ON_ACCOUNT</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
						<Parameter>
							<Name>Character</Name>
							<Value>?</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
					</Parameters>
				</Function>
			<!--	<Function>
					<FunctionName>ReplaceNull</FunctionName>
					<FunctionDestination>ADK_ACCOUNT_PROJECT_ON_ACCOUNT (4)</FunctionDestination>
					<FunctionPriority>0</FunctionPriority>
					<Parameters>
						<Parameter>
							<Name>Value</Name>
							<Value>0</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>						
					</Parameters>
				</Function>
				<Function>
					<FunctionName>Replace</FunctionName>
					<FunctionDestination>ADK_ACCOUNT_PROJECT_ON_ACCOUNT (4)</FunctionDestination>
					<FunctionPriority>0</FunctionPriority>
					<Parameters>
						<Parameter>
							<Name>Value1</Name>
							<Value>E</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
						<Parameter>
							<Name>Value2</Name>
							<Value>1</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
					</Parameters>
				</Function>
				<Function>
					<FunctionName>Replace</FunctionName>
					<FunctionDestination>ADK_ACCOUNT_PROJECT_ON_ACCOUNT (4)</FunctionDestination>
					<FunctionPriority>0</FunctionPriority>
					<Parameters>
						<Parameter>
							<Name>Value1</Name>
							<Value>O</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
						<Parameter>
							<Name>Value2</Name>
							<Value>0</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
					</Parameters>
				</Function>
				<Function>
					<FunctionName>Replace</FunctionName>
					<FunctionDestination>ADK_ACCOUNT_PROJECT_ON_ACCOUNT (4)</FunctionDestination>
					<FunctionPriority>0</FunctionPriority>
					<Parameters>
						<Parameter>
							<Name>Value1</Name>
							<Value>A</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
						<Parameter>
							<Name>Value2</Name>
							<Value>2</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
					</Parameters>
				</Function>
				<Function>
					<FunctionName>Replace</FunctionName>
					<FunctionDestination>ADK_ACCOUNT_PROJECT_ON_ACCOUNT (4)</FunctionDestination>
					<FunctionPriority>0</FunctionPriority>
					<Parameters>
						<Parameter>
							<Name>Value1</Name>
							<Value>S</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
						<Parameter>
							<Name>Value2</Name>
							<Value>0</Value>
							<UseLookUp>false</UseLookUp>
						</Parameter>
					</Parameters>
				</Function> -->
			</Functions>
			<Filter/>
		</Source*/