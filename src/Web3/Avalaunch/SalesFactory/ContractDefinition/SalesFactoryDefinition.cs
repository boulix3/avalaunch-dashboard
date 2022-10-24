using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Avalaunch.SalesFactory.ContractDefinition
{


    public partial class SalesFactoryDeployment : SalesFactoryDeploymentBase
    {
        public SalesFactoryDeployment() : base(BYTECODE) { }
        public SalesFactoryDeployment(string byteCode) : base(byteCode) { }
    }

    public class SalesFactoryDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = """
        [{"inputs":[{"internalType":"address","name":"_adminContract","type":"address"},{"internalType":"address","name":"_allocationStaking","type":"address"},{"internalType":"address","name":"_collateral","type":"address"}],"stateMutability":"nonpayable","type":"constructor"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"address","name":"allocationStaking","type":"address"}],"name":"AllocationStakingSet","type":"event"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"address","name":"implementation","type":"address"}],"name":"ImplementationChanged","type":"event"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"address","name":"saleContract","type":"address"}],"name":"SaleDeployed","type":"event"},{"inputs":[],"name":"admin","outputs":[{"internalType":"contract IAdmin","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"","type":"uint256"}],"name":"allSales","outputs":[{"internalType":"address","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"allocationStaking","outputs":[{"internalType":"address","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"collateral","outputs":[{"internalType":"address","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"deploySale","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256","name":"startIndex","type":"uint256"},{"internalType":"uint256","name":"endIndex","type":"uint256"}],"name":"getAllSales","outputs":[{"internalType":"address[]","name":"","type":"address[]"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"getLastDeployedSale","outputs":[{"internalType":"address","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"getNumberOfSalesDeployed","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"","type":"address"}],"name":"isSaleCreatedThroughFactory","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"_allocationStaking","type":"address"}],"name":"setAllocationStaking","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"_implementation","type":"address"}],"name":"setImplementation","outputs":[],"stateMutability":"nonpayable","type":"function"}]
        """;
        public SalesFactoryDeploymentBase() : base(BYTECODE) { }
        public SalesFactoryDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "_adminContract", 1)]
        public virtual string AdminContract { get; set; }
        [Parameter("address", "_allocationStaking", 2)]
        public virtual string AllocationStaking { get; set; }
        [Parameter("address", "_collateral", 3)]
        public virtual string Collateral { get; set; }
    }

    public partial class AdminFunction : AdminFunctionBase { }

    [Function("admin", "address")]
    public class AdminFunctionBase : FunctionMessage
    {

    }

    public partial class AllSalesFunction : AllSalesFunctionBase { }

    [Function("allSales", "address")]
    public class AllSalesFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class AllocationStakingFunction : AllocationStakingFunctionBase { }

    [Function("allocationStaking", "address")]
    public class AllocationStakingFunctionBase : FunctionMessage
    {

    }

    public partial class CollateralFunction : CollateralFunctionBase { }

    [Function("collateral", "address")]
    public class CollateralFunctionBase : FunctionMessage
    {

    }

    public partial class DeploySaleFunction : DeploySaleFunctionBase { }

    [Function("deploySale")]
    public class DeploySaleFunctionBase : FunctionMessage
    {

    }

    public partial class GetAllSalesFunction : GetAllSalesFunctionBase { }

    [Function("getAllSales", "address[]")]
    public class GetAllSalesFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "startIndex", 1)]
        public virtual BigInteger StartIndex { get; set; }
        [Parameter("uint256", "endIndex", 2)]
        public virtual BigInteger EndIndex { get; set; }
    }

    public partial class GetLastDeployedSaleFunction : GetLastDeployedSaleFunctionBase { }

    [Function("getLastDeployedSale", "address")]
    public class GetLastDeployedSaleFunctionBase : FunctionMessage
    {

    }

    public partial class GetNumberOfSalesDeployedFunction : GetNumberOfSalesDeployedFunctionBase { }

    [Function("getNumberOfSalesDeployed", "uint256")]
    public class GetNumberOfSalesDeployedFunctionBase : FunctionMessage
    {

    }

    public partial class IsSaleCreatedThroughFactoryFunction : IsSaleCreatedThroughFactoryFunctionBase { }

    [Function("isSaleCreatedThroughFactory", "bool")]
    public class IsSaleCreatedThroughFactoryFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class SetAllocationStakingFunction : SetAllocationStakingFunctionBase { }

    [Function("setAllocationStaking")]
    public class SetAllocationStakingFunctionBase : FunctionMessage
    {
        [Parameter("address", "_allocationStaking", 1)]
        public virtual string AllocationStaking { get; set; }
    }

    public partial class SetImplementationFunction : SetImplementationFunctionBase { }

    [Function("setImplementation")]
    public class SetImplementationFunctionBase : FunctionMessage
    {
        [Parameter("address", "_implementation", 1)]
        public virtual string Implementation { get; set; }
    }

    public partial class AllocationStakingSetEventDTO : AllocationStakingSetEventDTOBase { }

    [Event("AllocationStakingSet")]
    public class AllocationStakingSetEventDTOBase : IEventDTO
    {
        [Parameter("address", "allocationStaking", 1, false)]
        public virtual string AllocationStaking { get; set; }
    }

    public partial class ImplementationChangedEventDTO : ImplementationChangedEventDTOBase { }

    [Event("ImplementationChanged")]
    public class ImplementationChangedEventDTOBase : IEventDTO
    {
        [Parameter("address", "implementation", 1, false)]
        public virtual string Implementation { get; set; }
    }

    public partial class SaleDeployedEventDTO : SaleDeployedEventDTOBase { }

    [Event("SaleDeployed")]
    public class SaleDeployedEventDTOBase : IEventDTO
    {
        [Parameter("address", "saleContract", 1, false)]
        public virtual string SaleContract { get; set; }
    }

    public partial class AdminOutputDTO : AdminOutputDTOBase { }

    [FunctionOutput]
    public class AdminOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class AllSalesOutputDTO : AllSalesOutputDTOBase { }

    [FunctionOutput]
    public class AllSalesOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class AllocationStakingOutputDTO : AllocationStakingOutputDTOBase { }

    [FunctionOutput]
    public class AllocationStakingOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class CollateralOutputDTO : CollateralOutputDTOBase { }

    [FunctionOutput]
    public class CollateralOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }



    public partial class GetAllSalesOutputDTO : GetAllSalesOutputDTOBase { }

    [FunctionOutput]
    public class GetAllSalesOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address[]", "", 1)]
        public virtual List<string> ReturnValue1 { get; set; }
    }

    public partial class GetLastDeployedSaleOutputDTO : GetLastDeployedSaleOutputDTOBase { }

    [FunctionOutput]
    public class GetLastDeployedSaleOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetNumberOfSalesDeployedOutputDTO : GetNumberOfSalesDeployedOutputDTOBase { }

    [FunctionOutput]
    public class GetNumberOfSalesDeployedOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class IsSaleCreatedThroughFactoryOutputDTO : IsSaleCreatedThroughFactoryOutputDTOBase { }

    [FunctionOutput]
    public class IsSaleCreatedThroughFactoryOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }




}
