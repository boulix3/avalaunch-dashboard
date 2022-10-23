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
using Avalaunch.AllocationStaking.ContractDefinition;

namespace Avalaunch.AllocationStaking
{
    public partial class AllocationStakingService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, AllocationStakingDeployment allocationStakingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<AllocationStakingDeployment>().SendRequestAndWaitForReceiptAsync(allocationStakingDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, AllocationStakingDeployment allocationStakingDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<AllocationStakingDeployment>().SendRequestAsync(allocationStakingDeployment);
        }

        public static async Task<AllocationStakingService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, AllocationStakingDeployment allocationStakingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, allocationStakingDeployment, cancellationTokenSource);
            return new AllocationStakingService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public AllocationStakingService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public AllocationStakingService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AddRequestAsync(AddFunction addFunction)
        {
             return ContractHandler.SendRequestAsync(addFunction);
        }

        public Task<TransactionReceipt> AddRequestAndWaitForReceiptAsync(AddFunction addFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addFunction, cancellationToken);
        }

        public Task<string> AddRequestAsync(BigInteger allocPoint, string lpToken, bool withUpdate)
        {
            var addFunction = new AddFunction();
                addFunction.AllocPoint = allocPoint;
                addFunction.LpToken = lpToken;
                addFunction.WithUpdate = withUpdate;
            
             return ContractHandler.SendRequestAsync(addFunction);
        }

        public Task<TransactionReceipt> AddRequestAndWaitForReceiptAsync(BigInteger allocPoint, string lpToken, bool withUpdate, CancellationTokenSource cancellationToken = null)
        {
            var addFunction = new AddFunction();
                addFunction.AllocPoint = allocPoint;
                addFunction.LpToken = lpToken;
                addFunction.WithUpdate = withUpdate;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addFunction, cancellationToken);
        }

        public Task<string> CompoundRequestAsync(CompoundFunction compoundFunction)
        {
             return ContractHandler.SendRequestAsync(compoundFunction);
        }

        public Task<TransactionReceipt> CompoundRequestAndWaitForReceiptAsync(CompoundFunction compoundFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(compoundFunction, cancellationToken);
        }

        public Task<string> CompoundRequestAsync(BigInteger pid)
        {
            var compoundFunction = new CompoundFunction();
                compoundFunction.Pid = pid;
            
             return ContractHandler.SendRequestAsync(compoundFunction);
        }

        public Task<TransactionReceipt> CompoundRequestAndWaitForReceiptAsync(BigInteger pid, CancellationTokenSource cancellationToken = null)
        {
            var compoundFunction = new CompoundFunction();
                compoundFunction.Pid = pid;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(compoundFunction, cancellationToken);
        }

        public Task<string> DepositRequestAsync(DepositFunction depositFunction)
        {
             return ContractHandler.SendRequestAsync(depositFunction);
        }

        public Task<TransactionReceipt> DepositRequestAndWaitForReceiptAsync(DepositFunction depositFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(depositFunction, cancellationToken);
        }

        public Task<string> DepositRequestAsync(BigInteger pid, BigInteger amount)
        {
            var depositFunction = new DepositFunction();
                depositFunction.Pid = pid;
                depositFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(depositFunction);
        }

        public Task<TransactionReceipt> DepositRequestAndWaitForReceiptAsync(BigInteger pid, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var depositFunction = new DepositFunction();
                depositFunction.Pid = pid;
                depositFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(depositFunction, cancellationToken);
        }

        public Task<BigInteger> DepositFeePercentQueryAsync(DepositFeePercentFunction depositFeePercentFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DepositFeePercentFunction, BigInteger>(depositFeePercentFunction, blockParameter);
        }

        
        public Task<BigInteger> DepositFeePercentQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DepositFeePercentFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> DepositFeePrecisionQueryAsync(DepositFeePrecisionFunction depositFeePrecisionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DepositFeePrecisionFunction, BigInteger>(depositFeePrecisionFunction, blockParameter);
        }

        
        public Task<BigInteger> DepositFeePrecisionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DepositFeePrecisionFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> DepositedQueryAsync(DepositedFunction depositedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DepositedFunction, BigInteger>(depositedFunction, blockParameter);
        }

        
        public Task<BigInteger> DepositedQueryAsync(BigInteger pid, string user, BlockParameter blockParameter = null)
        {
            var depositedFunction = new DepositedFunction();
                depositedFunction.Pid = pid;
                depositedFunction.User = user;
            
            return ContractHandler.QueryAsync<DepositedFunction, BigInteger>(depositedFunction, blockParameter);
        }

        public Task<string> EmergencyWithdrawRequestAsync(EmergencyWithdrawFunction emergencyWithdrawFunction)
        {
             return ContractHandler.SendRequestAsync(emergencyWithdrawFunction);
        }

        public Task<TransactionReceipt> EmergencyWithdrawRequestAndWaitForReceiptAsync(EmergencyWithdrawFunction emergencyWithdrawFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(emergencyWithdrawFunction, cancellationToken);
        }

        public Task<string> EmergencyWithdrawRequestAsync(BigInteger pid)
        {
            var emergencyWithdrawFunction = new EmergencyWithdrawFunction();
                emergencyWithdrawFunction.Pid = pid;
            
             return ContractHandler.SendRequestAsync(emergencyWithdrawFunction);
        }

        public Task<TransactionReceipt> EmergencyWithdrawRequestAndWaitForReceiptAsync(BigInteger pid, CancellationTokenSource cancellationToken = null)
        {
            var emergencyWithdrawFunction = new EmergencyWithdrawFunction();
                emergencyWithdrawFunction.Pid = pid;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(emergencyWithdrawFunction, cancellationToken);
        }

        public Task<BigInteger> EndTimestampQueryAsync(EndTimestampFunction endTimestampFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EndTimestampFunction, BigInteger>(endTimestampFunction, blockParameter);
        }

        
        public Task<BigInteger> EndTimestampQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EndTimestampFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> Erc20QueryAsync(Erc20Function erc20Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Erc20Function, string>(erc20Function, blockParameter);
        }

        
        public Task<string> Erc20QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Erc20Function, string>(null, blockParameter);
        }

        public Task<string> FundRequestAsync(FundFunction fundFunction)
        {
             return ContractHandler.SendRequestAsync(fundFunction);
        }

        public Task<TransactionReceipt> FundRequestAndWaitForReceiptAsync(FundFunction fundFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(fundFunction, cancellationToken);
        }

        public Task<string> FundRequestAsync(BigInteger amount)
        {
            var fundFunction = new FundFunction();
                fundFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(fundFunction);
        }

        public Task<TransactionReceipt> FundRequestAndWaitForReceiptAsync(BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var fundFunction = new FundFunction();
                fundFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(fundFunction, cancellationToken);
        }

        public Task<GetPendingAndDepositedForUsersOutputDTO> GetPendingAndDepositedForUsersQueryAsync(GetPendingAndDepositedForUsersFunction getPendingAndDepositedForUsersFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetPendingAndDepositedForUsersFunction, GetPendingAndDepositedForUsersOutputDTO>(getPendingAndDepositedForUsersFunction, blockParameter);
        }

        public Task<GetPendingAndDepositedForUsersOutputDTO> GetPendingAndDepositedForUsersQueryAsync(List<string> users, BigInteger pid, BlockParameter blockParameter = null)
        {
            var getPendingAndDepositedForUsersFunction = new GetPendingAndDepositedForUsersFunction();
                getPendingAndDepositedForUsersFunction.Users = users;
                getPendingAndDepositedForUsersFunction.Pid = pid;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetPendingAndDepositedForUsersFunction, GetPendingAndDepositedForUsersOutputDTO>(getPendingAndDepositedForUsersFunction, blockParameter);
        }

        public Task<GetWithdrawFeeOutputDTO> GetWithdrawFeeQueryAsync(GetWithdrawFeeFunction getWithdrawFeeFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetWithdrawFeeFunction, GetWithdrawFeeOutputDTO>(getWithdrawFeeFunction, blockParameter);
        }

        public Task<GetWithdrawFeeOutputDTO> GetWithdrawFeeQueryAsync(string userAddress, BigInteger amountToWithdraw, BigInteger pid, BlockParameter blockParameter = null)
        {
            var getWithdrawFeeFunction = new GetWithdrawFeeFunction();
                getWithdrawFeeFunction.UserAddress = userAddress;
                getWithdrawFeeFunction.AmountToWithdraw = amountToWithdraw;
                getWithdrawFeeFunction.Pid = pid;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetWithdrawFeeFunction, GetWithdrawFeeOutputDTO>(getWithdrawFeeFunction, blockParameter);
        }

        public Task<string> InitializeRequestAsync(InitializeFunction initializeFunction)
        {
             return ContractHandler.SendRequestAsync(initializeFunction);
        }

        public Task<TransactionReceipt> InitializeRequestAndWaitForReceiptAsync(InitializeFunction initializeFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(initializeFunction, cancellationToken);
        }

        public Task<string> InitializeRequestAsync(string erc20, BigInteger rewardPerSecond, BigInteger startTimestamp, string salesFactory, BigInteger depositFeePercent, BigInteger depositFeePrecision)
        {
            var initializeFunction = new InitializeFunction();
                initializeFunction.Erc20 = erc20;
                initializeFunction.RewardPerSecond = rewardPerSecond;
                initializeFunction.StartTimestamp = startTimestamp;
                initializeFunction.SalesFactory = salesFactory;
                initializeFunction.DepositFeePercent = depositFeePercent;
                initializeFunction.DepositFeePrecision = depositFeePrecision;
            
             return ContractHandler.SendRequestAsync(initializeFunction);
        }

        public Task<TransactionReceipt> InitializeRequestAndWaitForReceiptAsync(string erc20, BigInteger rewardPerSecond, BigInteger startTimestamp, string salesFactory, BigInteger depositFeePercent, BigInteger depositFeePrecision, CancellationTokenSource cancellationToken = null)
        {
            var initializeFunction = new InitializeFunction();
                initializeFunction.Erc20 = erc20;
                initializeFunction.RewardPerSecond = rewardPerSecond;
                initializeFunction.StartTimestamp = startTimestamp;
                initializeFunction.SalesFactory = salesFactory;
                initializeFunction.DepositFeePercent = depositFeePercent;
                initializeFunction.DepositFeePrecision = depositFeePrecision;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(initializeFunction, cancellationToken);
        }

        public Task<string> MassUpdatePoolsRequestAsync(MassUpdatePoolsFunction massUpdatePoolsFunction)
        {
             return ContractHandler.SendRequestAsync(massUpdatePoolsFunction);
        }

        public Task<string> MassUpdatePoolsRequestAsync()
        {
             return ContractHandler.SendRequestAsync<MassUpdatePoolsFunction>();
        }

        public Task<TransactionReceipt> MassUpdatePoolsRequestAndWaitForReceiptAsync(MassUpdatePoolsFunction massUpdatePoolsFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(massUpdatePoolsFunction, cancellationToken);
        }

        public Task<TransactionReceipt> MassUpdatePoolsRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<MassUpdatePoolsFunction>(null, cancellationToken);
        }

        public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
        }

        
        public Task<string> OwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> PaidOutQueryAsync(PaidOutFunction paidOutFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PaidOutFunction, BigInteger>(paidOutFunction, blockParameter);
        }

        
        public Task<BigInteger> PaidOutQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PaidOutFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> PendingQueryAsync(PendingFunction pendingFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PendingFunction, BigInteger>(pendingFunction, blockParameter);
        }

        
        public Task<BigInteger> PendingQueryAsync(BigInteger pid, string user, BlockParameter blockParameter = null)
        {
            var pendingFunction = new PendingFunction();
                pendingFunction.Pid = pid;
                pendingFunction.User = user;
            
            return ContractHandler.QueryAsync<PendingFunction, BigInteger>(pendingFunction, blockParameter);
        }

        public Task<PoolInfoOutputDTO> PoolInfoQueryAsync(PoolInfoFunction poolInfoFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<PoolInfoFunction, PoolInfoOutputDTO>(poolInfoFunction, blockParameter);
        }

        public Task<PoolInfoOutputDTO> PoolInfoQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var poolInfoFunction = new PoolInfoFunction();
                poolInfoFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<PoolInfoFunction, PoolInfoOutputDTO>(poolInfoFunction, blockParameter);
        }

        public Task<BigInteger> PoolLengthQueryAsync(PoolLengthFunction poolLengthFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoolLengthFunction, BigInteger>(poolLengthFunction, blockParameter);
        }

        
        public Task<BigInteger> PoolLengthQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoolLengthFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> PostSaleWithdrawPenaltyLengthQueryAsync(PostSaleWithdrawPenaltyLengthFunction postSaleWithdrawPenaltyLengthFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PostSaleWithdrawPenaltyLengthFunction, BigInteger>(postSaleWithdrawPenaltyLengthFunction, blockParameter);
        }

        
        public Task<BigInteger> PostSaleWithdrawPenaltyLengthQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PostSaleWithdrawPenaltyLengthFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> PostSaleWithdrawPenaltyPercentQueryAsync(PostSaleWithdrawPenaltyPercentFunction postSaleWithdrawPenaltyPercentFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PostSaleWithdrawPenaltyPercentFunction, BigInteger>(postSaleWithdrawPenaltyPercentFunction, blockParameter);
        }

        
        public Task<BigInteger> PostSaleWithdrawPenaltyPercentQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PostSaleWithdrawPenaltyPercentFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> PostSaleWithdrawPenaltyPrecisionQueryAsync(PostSaleWithdrawPenaltyPrecisionFunction postSaleWithdrawPenaltyPrecisionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PostSaleWithdrawPenaltyPrecisionFunction, BigInteger>(postSaleWithdrawPenaltyPrecisionFunction, blockParameter);
        }

        
        public Task<BigInteger> PostSaleWithdrawPenaltyPrecisionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PostSaleWithdrawPenaltyPrecisionFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> RedistributeXavaRequestAsync(RedistributeXavaFunction redistributeXavaFunction)
        {
             return ContractHandler.SendRequestAsync(redistributeXavaFunction);
        }

        public Task<TransactionReceipt> RedistributeXavaRequestAndWaitForReceiptAsync(RedistributeXavaFunction redistributeXavaFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(redistributeXavaFunction, cancellationToken);
        }

        public Task<string> RedistributeXavaRequestAsync(BigInteger pid, string user, BigInteger amountToRedistribute)
        {
            var redistributeXavaFunction = new RedistributeXavaFunction();
                redistributeXavaFunction.Pid = pid;
                redistributeXavaFunction.User = user;
                redistributeXavaFunction.AmountToRedistribute = amountToRedistribute;
            
             return ContractHandler.SendRequestAsync(redistributeXavaFunction);
        }

        public Task<TransactionReceipt> RedistributeXavaRequestAndWaitForReceiptAsync(BigInteger pid, string user, BigInteger amountToRedistribute, CancellationTokenSource cancellationToken = null)
        {
            var redistributeXavaFunction = new RedistributeXavaFunction();
                redistributeXavaFunction.Pid = pid;
                redistributeXavaFunction.User = user;
                redistributeXavaFunction.AmountToRedistribute = amountToRedistribute;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(redistributeXavaFunction, cancellationToken);
        }

        public Task<string> RenounceOwnershipRequestAsync(RenounceOwnershipFunction renounceOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(renounceOwnershipFunction);
        }

        public Task<string> RenounceOwnershipRequestAsync()
        {
             return ContractHandler.SendRequestAsync<RenounceOwnershipFunction>();
        }

        public Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(RenounceOwnershipFunction renounceOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceOwnershipFunction, cancellationToken);
        }

        public Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<RenounceOwnershipFunction>(null, cancellationToken);
        }

        public Task<BigInteger> RewardPerSecondQueryAsync(RewardPerSecondFunction rewardPerSecondFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RewardPerSecondFunction, BigInteger>(rewardPerSecondFunction, blockParameter);
        }

        
        public Task<BigInteger> RewardPerSecondQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RewardPerSecondFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> SalesFactoryQueryAsync(SalesFactoryFunction salesFactoryFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SalesFactoryFunction, string>(salesFactoryFunction, blockParameter);
        }

        
        public Task<string> SalesFactoryQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SalesFactoryFunction, string>(null, blockParameter);
        }

        public Task<string> SetRequestAsync(SetFunction setFunction)
        {
             return ContractHandler.SendRequestAsync(setFunction);
        }

        public Task<TransactionReceipt> SetRequestAndWaitForReceiptAsync(SetFunction setFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setFunction, cancellationToken);
        }

        public Task<string> SetRequestAsync(BigInteger pid, BigInteger allocPoint, bool withUpdate)
        {
            var setFunction = new SetFunction();
                setFunction.Pid = pid;
                setFunction.AllocPoint = allocPoint;
                setFunction.WithUpdate = withUpdate;
            
             return ContractHandler.SendRequestAsync(setFunction);
        }

        public Task<TransactionReceipt> SetRequestAndWaitForReceiptAsync(BigInteger pid, BigInteger allocPoint, bool withUpdate, CancellationTokenSource cancellationToken = null)
        {
            var setFunction = new SetFunction();
                setFunction.Pid = pid;
                setFunction.AllocPoint = allocPoint;
                setFunction.WithUpdate = withUpdate;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setFunction, cancellationToken);
        }

        public Task<string> SetDepositFeeRequestAsync(SetDepositFeeFunction setDepositFeeFunction)
        {
             return ContractHandler.SendRequestAsync(setDepositFeeFunction);
        }

        public Task<TransactionReceipt> SetDepositFeeRequestAndWaitForReceiptAsync(SetDepositFeeFunction setDepositFeeFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setDepositFeeFunction, cancellationToken);
        }

        public Task<string> SetDepositFeeRequestAsync(BigInteger depositFeePercent, BigInteger depositFeePrecision)
        {
            var setDepositFeeFunction = new SetDepositFeeFunction();
                setDepositFeeFunction.DepositFeePercent = depositFeePercent;
                setDepositFeeFunction.DepositFeePrecision = depositFeePrecision;
            
             return ContractHandler.SendRequestAsync(setDepositFeeFunction);
        }

        public Task<TransactionReceipt> SetDepositFeeRequestAndWaitForReceiptAsync(BigInteger depositFeePercent, BigInteger depositFeePrecision, CancellationTokenSource cancellationToken = null)
        {
            var setDepositFeeFunction = new SetDepositFeeFunction();
                setDepositFeeFunction.DepositFeePercent = depositFeePercent;
                setDepositFeeFunction.DepositFeePrecision = depositFeePrecision;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setDepositFeeFunction, cancellationToken);
        }

        public Task<string> SetPostSaleWithdrawPenaltyPercentAndLengthRequestAsync(SetPostSaleWithdrawPenaltyPercentAndLengthFunction setPostSaleWithdrawPenaltyPercentAndLengthFunction)
        {
             return ContractHandler.SendRequestAsync(setPostSaleWithdrawPenaltyPercentAndLengthFunction);
        }

        public Task<TransactionReceipt> SetPostSaleWithdrawPenaltyPercentAndLengthRequestAndWaitForReceiptAsync(SetPostSaleWithdrawPenaltyPercentAndLengthFunction setPostSaleWithdrawPenaltyPercentAndLengthFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPostSaleWithdrawPenaltyPercentAndLengthFunction, cancellationToken);
        }

        public Task<string> SetPostSaleWithdrawPenaltyPercentAndLengthRequestAsync(BigInteger postSaleWithdrawPenaltyPercent, BigInteger postSaleWithdrawPenaltyLength, BigInteger postSaleWithdrawPenaltyPrecision)
        {
            var setPostSaleWithdrawPenaltyPercentAndLengthFunction = new SetPostSaleWithdrawPenaltyPercentAndLengthFunction();
                setPostSaleWithdrawPenaltyPercentAndLengthFunction.PostSaleWithdrawPenaltyPercent = postSaleWithdrawPenaltyPercent;
                setPostSaleWithdrawPenaltyPercentAndLengthFunction.PostSaleWithdrawPenaltyLength = postSaleWithdrawPenaltyLength;
                setPostSaleWithdrawPenaltyPercentAndLengthFunction.PostSaleWithdrawPenaltyPrecision = postSaleWithdrawPenaltyPrecision;
            
             return ContractHandler.SendRequestAsync(setPostSaleWithdrawPenaltyPercentAndLengthFunction);
        }

        public Task<TransactionReceipt> SetPostSaleWithdrawPenaltyPercentAndLengthRequestAndWaitForReceiptAsync(BigInteger postSaleWithdrawPenaltyPercent, BigInteger postSaleWithdrawPenaltyLength, BigInteger postSaleWithdrawPenaltyPrecision, CancellationTokenSource cancellationToken = null)
        {
            var setPostSaleWithdrawPenaltyPercentAndLengthFunction = new SetPostSaleWithdrawPenaltyPercentAndLengthFunction();
                setPostSaleWithdrawPenaltyPercentAndLengthFunction.PostSaleWithdrawPenaltyPercent = postSaleWithdrawPenaltyPercent;
                setPostSaleWithdrawPenaltyPercentAndLengthFunction.PostSaleWithdrawPenaltyLength = postSaleWithdrawPenaltyLength;
                setPostSaleWithdrawPenaltyPercentAndLengthFunction.PostSaleWithdrawPenaltyPrecision = postSaleWithdrawPenaltyPrecision;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPostSaleWithdrawPenaltyPercentAndLengthFunction, cancellationToken);
        }

        public Task<string> SetSalesFactoryRequestAsync(SetSalesFactoryFunction setSalesFactoryFunction)
        {
             return ContractHandler.SendRequestAsync(setSalesFactoryFunction);
        }

        public Task<TransactionReceipt> SetSalesFactoryRequestAndWaitForReceiptAsync(SetSalesFactoryFunction setSalesFactoryFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSalesFactoryFunction, cancellationToken);
        }

        public Task<string> SetSalesFactoryRequestAsync(string salesFactory)
        {
            var setSalesFactoryFunction = new SetSalesFactoryFunction();
                setSalesFactoryFunction.SalesFactory = salesFactory;
            
             return ContractHandler.SendRequestAsync(setSalesFactoryFunction);
        }

        public Task<TransactionReceipt> SetSalesFactoryRequestAndWaitForReceiptAsync(string salesFactory, CancellationTokenSource cancellationToken = null)
        {
            var setSalesFactoryFunction = new SetSalesFactoryFunction();
                setSalesFactoryFunction.SalesFactory = salesFactory;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSalesFactoryFunction, cancellationToken);
        }

        public Task<string> SetTokensUnlockTimeRequestAsync(SetTokensUnlockTimeFunction setTokensUnlockTimeFunction)
        {
             return ContractHandler.SendRequestAsync(setTokensUnlockTimeFunction);
        }

        public Task<TransactionReceipt> SetTokensUnlockTimeRequestAndWaitForReceiptAsync(SetTokensUnlockTimeFunction setTokensUnlockTimeFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setTokensUnlockTimeFunction, cancellationToken);
        }

        public Task<string> SetTokensUnlockTimeRequestAsync(BigInteger pid, string user, BigInteger tokensUnlockTime)
        {
            var setTokensUnlockTimeFunction = new SetTokensUnlockTimeFunction();
                setTokensUnlockTimeFunction.Pid = pid;
                setTokensUnlockTimeFunction.User = user;
                setTokensUnlockTimeFunction.TokensUnlockTime = tokensUnlockTime;
            
             return ContractHandler.SendRequestAsync(setTokensUnlockTimeFunction);
        }

        public Task<TransactionReceipt> SetTokensUnlockTimeRequestAndWaitForReceiptAsync(BigInteger pid, string user, BigInteger tokensUnlockTime, CancellationTokenSource cancellationToken = null)
        {
            var setTokensUnlockTimeFunction = new SetTokensUnlockTimeFunction();
                setTokensUnlockTimeFunction.Pid = pid;
                setTokensUnlockTimeFunction.User = user;
                setTokensUnlockTimeFunction.TokensUnlockTime = tokensUnlockTime;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setTokensUnlockTimeFunction, cancellationToken);
        }

        public Task<BigInteger> StartTimestampQueryAsync(StartTimestampFunction startTimestampFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StartTimestampFunction, BigInteger>(startTimestampFunction, blockParameter);
        }

        
        public Task<BigInteger> StartTimestampQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StartTimestampFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> TotalAllocPointQueryAsync(TotalAllocPointFunction totalAllocPointFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalAllocPointFunction, BigInteger>(totalAllocPointFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalAllocPointQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalAllocPointFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> TotalBurnedFromUserQueryAsync(TotalBurnedFromUserFunction totalBurnedFromUserFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalBurnedFromUserFunction, BigInteger>(totalBurnedFromUserFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalBurnedFromUserQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var totalBurnedFromUserFunction = new TotalBurnedFromUserFunction();
                totalBurnedFromUserFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<TotalBurnedFromUserFunction, BigInteger>(totalBurnedFromUserFunction, blockParameter);
        }

        public Task<BigInteger> TotalPendingQueryAsync(TotalPendingFunction totalPendingFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalPendingFunction, BigInteger>(totalPendingFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalPendingQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalPendingFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> TotalRewardsQueryAsync(TotalRewardsFunction totalRewardsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalRewardsFunction, BigInteger>(totalRewardsFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalRewardsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalRewardsFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> TotalXavaRedistributedQueryAsync(TotalXavaRedistributedFunction totalXavaRedistributedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalXavaRedistributedFunction, BigInteger>(totalXavaRedistributedFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalXavaRedistributedQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalXavaRedistributedFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> TransferOwnershipRequestAsync(TransferOwnershipFunction transferOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(TransferOwnershipFunction transferOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }

        public Task<string> TransferOwnershipRequestAsync(string newOwner)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(string newOwner, CancellationTokenSource cancellationToken = null)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }

        public Task<string> UpdatePoolRequestAsync(UpdatePoolFunction updatePoolFunction)
        {
             return ContractHandler.SendRequestAsync(updatePoolFunction);
        }

        public Task<TransactionReceipt> UpdatePoolRequestAndWaitForReceiptAsync(UpdatePoolFunction updatePoolFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updatePoolFunction, cancellationToken);
        }

        public Task<string> UpdatePoolRequestAsync(BigInteger pid)
        {
            var updatePoolFunction = new UpdatePoolFunction();
                updatePoolFunction.Pid = pid;
            
             return ContractHandler.SendRequestAsync(updatePoolFunction);
        }

        public Task<TransactionReceipt> UpdatePoolRequestAndWaitForReceiptAsync(BigInteger pid, CancellationTokenSource cancellationToken = null)
        {
            var updatePoolFunction = new UpdatePoolFunction();
                updatePoolFunction.Pid = pid;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updatePoolFunction, cancellationToken);
        }

        public Task<UserInfoOutputDTO> UserInfoQueryAsync(UserInfoFunction userInfoFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<UserInfoFunction, UserInfoOutputDTO>(userInfoFunction, blockParameter);
        }

        public Task<UserInfoOutputDTO> UserInfoQueryAsync(BigInteger returnValue1, string returnValue2, BlockParameter blockParameter = null)
        {
            var userInfoFunction = new UserInfoFunction();
                userInfoFunction.ReturnValue1 = returnValue1;
                userInfoFunction.ReturnValue2 = returnValue2;
            
            return ContractHandler.QueryDeserializingToObjectAsync<UserInfoFunction, UserInfoOutputDTO>(userInfoFunction, blockParameter);
        }

        public Task<string> WithdrawRequestAsync(WithdrawFunction withdrawFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawFunction);
        }

        public Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(WithdrawFunction withdrawFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawFunction, cancellationToken);
        }

        public Task<string> WithdrawRequestAsync(BigInteger pid, BigInteger amount)
        {
            var withdrawFunction = new WithdrawFunction();
                withdrawFunction.Pid = pid;
                withdrawFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(withdrawFunction);
        }

        public Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(BigInteger pid, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var withdrawFunction = new WithdrawFunction();
                withdrawFunction.Pid = pid;
                withdrawFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawFunction, cancellationToken);
        }
    }
}
