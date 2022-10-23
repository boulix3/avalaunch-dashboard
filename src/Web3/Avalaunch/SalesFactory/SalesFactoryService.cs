using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Avalaunch.SalesFactory.ContractDefinition;

namespace Avalaunch.SalesFactory
{
    public partial class SalesFactoryService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, SalesFactoryDeployment salesFactoryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<SalesFactoryDeployment>().SendRequestAndWaitForReceiptAsync(salesFactoryDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, SalesFactoryDeployment salesFactoryDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<SalesFactoryDeployment>().SendRequestAsync(salesFactoryDeployment);
        }

        public static async Task<SalesFactoryService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, SalesFactoryDeployment salesFactoryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, salesFactoryDeployment, cancellationTokenSource);
            return new SalesFactoryService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public SalesFactoryService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public SalesFactoryService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AdminQueryAsync(AdminFunction adminFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AdminFunction, string>(adminFunction, blockParameter);
        }

        
        public Task<string> AdminQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AdminFunction, string>(null, blockParameter);
        }

        public Task<string> AllSalesQueryAsync(AllSalesFunction allSalesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AllSalesFunction, string>(allSalesFunction, blockParameter);
        }

        
        public Task<string> AllSalesQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var allSalesFunction = new AllSalesFunction();
                allSalesFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<AllSalesFunction, string>(allSalesFunction, blockParameter);
        }

        public Task<string> AllocationStakingQueryAsync(AllocationStakingFunction allocationStakingFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AllocationStakingFunction, string>(allocationStakingFunction, blockParameter);
        }

        
        public Task<string> AllocationStakingQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AllocationStakingFunction, string>(null, blockParameter);
        }

        public Task<string> CollateralQueryAsync(CollateralFunction collateralFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CollateralFunction, string>(collateralFunction, blockParameter);
        }

        
        public Task<string> CollateralQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CollateralFunction, string>(null, blockParameter);
        }

        public Task<string> DeploySaleRequestAsync(DeploySaleFunction deploySaleFunction)
        {
             return ContractHandler.SendRequestAsync(deploySaleFunction);
        }

        public Task<string> DeploySaleRequestAsync()
        {
             return ContractHandler.SendRequestAsync<DeploySaleFunction>();
        }

        public Task<TransactionReceipt> DeploySaleRequestAndWaitForReceiptAsync(DeploySaleFunction deploySaleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(deploySaleFunction, cancellationToken);
        }

        public Task<TransactionReceipt> DeploySaleRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<DeploySaleFunction>(null, cancellationToken);
        }

        public Task<List<string>> GetAllSalesQueryAsync(GetAllSalesFunction getAllSalesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetAllSalesFunction, List<string>>(getAllSalesFunction, blockParameter);
        }

        
        public Task<List<string>> GetAllSalesQueryAsync(BigInteger startIndex, BigInteger endIndex, BlockParameter blockParameter = null)
        {
            var getAllSalesFunction = new GetAllSalesFunction();
                getAllSalesFunction.StartIndex = startIndex;
                getAllSalesFunction.EndIndex = endIndex;
            
            return ContractHandler.QueryAsync<GetAllSalesFunction, List<string>>(getAllSalesFunction, blockParameter);
        }

        public Task<string> GetLastDeployedSaleQueryAsync(GetLastDeployedSaleFunction getLastDeployedSaleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetLastDeployedSaleFunction, string>(getLastDeployedSaleFunction, blockParameter);
        }

        
        public Task<string> GetLastDeployedSaleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetLastDeployedSaleFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> GetNumberOfSalesDeployedQueryAsync(GetNumberOfSalesDeployedFunction getNumberOfSalesDeployedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetNumberOfSalesDeployedFunction, BigInteger>(getNumberOfSalesDeployedFunction, blockParameter);
        }

        
        public Task<BigInteger> GetNumberOfSalesDeployedQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetNumberOfSalesDeployedFunction, BigInteger>(null, blockParameter);
        }

        public Task<bool> IsSaleCreatedThroughFactoryQueryAsync(IsSaleCreatedThroughFactoryFunction isSaleCreatedThroughFactoryFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsSaleCreatedThroughFactoryFunction, bool>(isSaleCreatedThroughFactoryFunction, blockParameter);
        }

        
        public Task<bool> IsSaleCreatedThroughFactoryQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var isSaleCreatedThroughFactoryFunction = new IsSaleCreatedThroughFactoryFunction();
                isSaleCreatedThroughFactoryFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<IsSaleCreatedThroughFactoryFunction, bool>(isSaleCreatedThroughFactoryFunction, blockParameter);
        }

        public Task<string> SetAllocationStakingRequestAsync(SetAllocationStakingFunction setAllocationStakingFunction)
        {
             return ContractHandler.SendRequestAsync(setAllocationStakingFunction);
        }

        public Task<TransactionReceipt> SetAllocationStakingRequestAndWaitForReceiptAsync(SetAllocationStakingFunction setAllocationStakingFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setAllocationStakingFunction, cancellationToken);
        }

        public Task<string> SetAllocationStakingRequestAsync(string allocationStaking)
        {
            var setAllocationStakingFunction = new SetAllocationStakingFunction();
                setAllocationStakingFunction.AllocationStaking = allocationStaking;
            
             return ContractHandler.SendRequestAsync(setAllocationStakingFunction);
        }

        public Task<TransactionReceipt> SetAllocationStakingRequestAndWaitForReceiptAsync(string allocationStaking, CancellationTokenSource cancellationToken = null)
        {
            var setAllocationStakingFunction = new SetAllocationStakingFunction();
                setAllocationStakingFunction.AllocationStaking = allocationStaking;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setAllocationStakingFunction, cancellationToken);
        }

        public Task<string> SetImplementationRequestAsync(SetImplementationFunction setImplementationFunction)
        {
             return ContractHandler.SendRequestAsync(setImplementationFunction);
        }

        public Task<TransactionReceipt> SetImplementationRequestAndWaitForReceiptAsync(SetImplementationFunction setImplementationFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setImplementationFunction, cancellationToken);
        }

        public Task<string> SetImplementationRequestAsync(string implementation)
        {
            var setImplementationFunction = new SetImplementationFunction();
                setImplementationFunction.Implementation = implementation;
            
             return ContractHandler.SendRequestAsync(setImplementationFunction);
        }

        public Task<TransactionReceipt> SetImplementationRequestAndWaitForReceiptAsync(string implementation, CancellationTokenSource cancellationToken = null)
        {
            var setImplementationFunction = new SetImplementationFunction();
                setImplementationFunction.Implementation = implementation;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setImplementationFunction, cancellationToken);
        }
    }
}
