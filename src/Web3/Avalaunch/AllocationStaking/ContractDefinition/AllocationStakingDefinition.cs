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

namespace Avalaunch.AllocationStaking.ContractDefinition
{


    public partial class AllocationStakingDeployment : AllocationStakingDeploymentBase
    {
        public AllocationStakingDeployment() : base(BYTECODE) { }
        public AllocationStakingDeployment(string byteCode) : base(byteCode) { }
    }

    public class AllocationStakingDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = """
        [{"anonymous":false,"inputs":[{"indexed":true,"internalType":"address","name":"user","type":"address"},{"indexed":true,"internalType":"uint256","name":"pid","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"amountAdded","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"totalDeposited","type":"uint256"}],"name":"CompoundedEarnings","type":"event"},{"anonymous":false,"inputs":[{"indexed":true,"internalType":"address","name":"user","type":"address"},{"indexed":true,"internalType":"uint256","name":"pid","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"amount","type":"uint256"}],"name":"Deposit","type":"event"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"uint256","name":"depositFeePercent","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"depositFeePrecision","type":"uint256"}],"name":"DepositFeeSet","type":"event"},{"anonymous":false,"inputs":[{"indexed":true,"internalType":"address","name":"user","type":"address"},{"indexed":true,"internalType":"uint256","name":"pid","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"amount","type":"uint256"}],"name":"EmergencyWithdraw","type":"event"},{"anonymous":false,"inputs":[{"indexed":true,"internalType":"address","name":"user","type":"address"},{"indexed":true,"internalType":"uint256","name":"pid","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"amount","type":"uint256"}],"name":"FeeTaken","type":"event"},{"anonymous":false,"inputs":[{"indexed":true,"internalType":"address","name":"previousOwner","type":"address"},{"indexed":true,"internalType":"address","name":"newOwner","type":"address"}],"name":"OwnershipTransferred","type":"event"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"address","name":"user","type":"address"},{"indexed":false,"internalType":"uint256","name":"amountStake","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"amountRewards","type":"uint256"}],"name":"PostSaleWithdrawFeeCharged","type":"event"},{"anonymous":false,"inputs":[{"indexed":true,"internalType":"address","name":"user","type":"address"},{"indexed":true,"internalType":"uint256","name":"pid","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"amount","type":"uint256"}],"name":"Withdraw","type":"event"},{"inputs":[{"internalType":"uint256","name":"_allocPoint","type":"uint256"},{"internalType":"contract IERC20","name":"_lpToken","type":"address"},{"internalType":"bool","name":"_withUpdate","type":"bool"}],"name":"add","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256","name":"_pid","type":"uint256"}],"name":"compound","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256","name":"_pid","type":"uint256"},{"internalType":"uint256","name":"_amount","type":"uint256"}],"name":"deposit","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"depositFeePercent","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"depositFeePrecision","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"_pid","type":"uint256"},{"internalType":"address","name":"_user","type":"address"}],"name":"deposited","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"_pid","type":"uint256"}],"name":"emergencyWithdraw","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"endTimestamp","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"erc20","outputs":[{"internalType":"contract IERC20","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"_amount","type":"uint256"}],"name":"fund","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address[]","name":"users","type":"address[]"},{"internalType":"uint256","name":"pid","type":"uint256"}],"name":"getPendingAndDepositedForUsers","outputs":[{"internalType":"uint256[]","name":"","type":"uint256[]"},{"internalType":"uint256[]","name":"","type":"uint256[]"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"userAddress","type":"address"},{"internalType":"uint256","name":"amountToWithdraw","type":"uint256"},{"internalType":"uint256","name":"_pid","type":"uint256"}],"name":"getWithdrawFee","outputs":[{"internalType":"uint256","name":"","type":"uint256"},{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"contract IERC20","name":"_erc20","type":"address"},{"internalType":"uint256","name":"_rewardPerSecond","type":"uint256"},{"internalType":"uint256","name":"_startTimestamp","type":"uint256"},{"internalType":"address","name":"_salesFactory","type":"address"},{"internalType":"uint256","name":"_depositFeePercent","type":"uint256"},{"internalType":"uint256","name":"_depositFeePrecision","type":"uint256"}],"name":"initialize","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"massUpdatePools","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"owner","outputs":[{"internalType":"address","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"paidOut","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"_pid","type":"uint256"},{"internalType":"address","name":"_user","type":"address"}],"name":"pending","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"","type":"uint256"}],"name":"poolInfo","outputs":[{"internalType":"contract IERC20","name":"lpToken","type":"address"},{"internalType":"uint256","name":"allocPoint","type":"uint256"},{"internalType":"uint256","name":"lastRewardTimestamp","type":"uint256"},{"internalType":"uint256","name":"accERC20PerShare","type":"uint256"},{"internalType":"uint256","name":"totalDeposits","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"poolLength","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"postSaleWithdrawPenaltyLength","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"postSaleWithdrawPenaltyPercent","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"postSaleWithdrawPenaltyPrecision","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"_pid","type":"uint256"},{"internalType":"address","name":"_user","type":"address"},{"internalType":"uint256","name":"_amountToRedistribute","type":"uint256"}],"name":"redistributeXava","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"renounceOwnership","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"rewardPerSecond","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"salesFactory","outputs":[{"internalType":"contract ISalesFactory","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"_pid","type":"uint256"},{"internalType":"uint256","name":"_allocPoint","type":"uint256"},{"internalType":"bool","name":"_withUpdate","type":"bool"}],"name":"set","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256","name":"_depositFeePercent","type":"uint256"},{"internalType":"uint256","name":"_depositFeePrecision","type":"uint256"}],"name":"setDepositFee","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256","name":"_postSaleWithdrawPenaltyPercent","type":"uint256"},{"internalType":"uint256","name":"_postSaleWithdrawPenaltyLength","type":"uint256"},{"internalType":"uint256","name":"_postSaleWithdrawPenaltyPrecision","type":"uint256"}],"name":"setPostSaleWithdrawPenaltyPercentAndLength","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"_salesFactory","type":"address"}],"name":"setSalesFactory","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256","name":"_pid","type":"uint256"},{"internalType":"address","name":"_user","type":"address"},{"internalType":"uint256","name":"_tokensUnlockTime","type":"uint256"}],"name":"setTokensUnlockTime","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"startTimestamp","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"totalAllocPoint","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"","type":"address"}],"name":"totalBurnedFromUser","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"totalPending","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"totalRewards","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"totalXavaRedistributed","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"newOwner","type":"address"}],"name":"transferOwnership","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256","name":"_pid","type":"uint256"}],"name":"updatePool","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256","name":"","type":"uint256"},{"internalType":"address","name":"","type":"address"}],"name":"userInfo","outputs":[{"internalType":"uint256","name":"amount","type":"uint256"},{"internalType":"uint256","name":"rewardDebt","type":"uint256"},{"internalType":"uint256","name":"tokensUnlockTime","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"_pid","type":"uint256"},{"internalType":"uint256","name":"_amount","type":"uint256"}],"name":"withdraw","outputs":[],"stateMutability":"nonpayable","type":"function"}]
        """;
        public AllocationStakingDeploymentBase() : base(BYTECODE) { }
        public AllocationStakingDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AddFunction : AddFunctionBase { }

    [Function("add")]
    public class AddFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_allocPoint", 1)]
        public virtual BigInteger AllocPoint { get; set; }
        [Parameter("address", "_lpToken", 2)]
        public virtual string LpToken { get; set; }
        [Parameter("bool", "_withUpdate", 3)]
        public virtual bool WithUpdate { get; set; }
    }

    public partial class CompoundFunction : CompoundFunctionBase { }

    [Function("compound")]
    public class CompoundFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_pid", 1)]
        public virtual BigInteger Pid { get; set; }
    }

    public partial class DepositFunction : DepositFunctionBase { }

    [Function("deposit")]
    public class DepositFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_pid", 1)]
        public virtual BigInteger Pid { get; set; }
        [Parameter("uint256", "_amount", 2)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class DepositFeePercentFunction : DepositFeePercentFunctionBase { }

    [Function("depositFeePercent", "uint256")]
    public class DepositFeePercentFunctionBase : FunctionMessage
    {

    }

    public partial class DepositFeePrecisionFunction : DepositFeePrecisionFunctionBase { }

    [Function("depositFeePrecision", "uint256")]
    public class DepositFeePrecisionFunctionBase : FunctionMessage
    {

    }

    public partial class DepositedFunction : DepositedFunctionBase { }

    [Function("deposited", "uint256")]
    public class DepositedFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_pid", 1)]
        public virtual BigInteger Pid { get; set; }
        [Parameter("address", "_user", 2)]
        public virtual string User { get; set; }
    }

    public partial class EmergencyWithdrawFunction : EmergencyWithdrawFunctionBase { }

    [Function("emergencyWithdraw")]
    public class EmergencyWithdrawFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_pid", 1)]
        public virtual BigInteger Pid { get; set; }
    }

    public partial class EndTimestampFunction : EndTimestampFunctionBase { }

    [Function("endTimestamp", "uint256")]
    public class EndTimestampFunctionBase : FunctionMessage
    {

    }

    public partial class Erc20Function : Erc20FunctionBase { }

    [Function("erc20", "address")]
    public class Erc20FunctionBase : FunctionMessage
    {

    }

    public partial class FundFunction : FundFunctionBase { }

    [Function("fund")]
    public class FundFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_amount", 1)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class GetPendingAndDepositedForUsersFunction : GetPendingAndDepositedForUsersFunctionBase { }

    [Function("getPendingAndDepositedForUsers", typeof(GetPendingAndDepositedForUsersOutputDTO))]
    public class GetPendingAndDepositedForUsersFunctionBase : FunctionMessage
    {
        [Parameter("address[]", "users", 1)]
        public virtual List<string> Users { get; set; }
        [Parameter("uint256", "pid", 2)]
        public virtual BigInteger Pid { get; set; }
    }

    public partial class GetWithdrawFeeFunction : GetWithdrawFeeFunctionBase { }

    [Function("getWithdrawFee", typeof(GetWithdrawFeeOutputDTO))]
    public class GetWithdrawFeeFunctionBase : FunctionMessage
    {
        [Parameter("address", "userAddress", 1)]
        public virtual string UserAddress { get; set; }
        [Parameter("uint256", "amountToWithdraw", 2)]
        public virtual BigInteger AmountToWithdraw { get; set; }
        [Parameter("uint256", "_pid", 3)]
        public virtual BigInteger Pid { get; set; }
    }

    public partial class InitializeFunction : InitializeFunctionBase { }

    [Function("initialize")]
    public class InitializeFunctionBase : FunctionMessage
    {
        [Parameter("address", "_erc20", 1)]
        public virtual string Erc20 { get; set; }
        [Parameter("uint256", "_rewardPerSecond", 2)]
        public virtual BigInteger RewardPerSecond { get; set; }
        [Parameter("uint256", "_startTimestamp", 3)]
        public virtual BigInteger StartTimestamp { get; set; }
        [Parameter("address", "_salesFactory", 4)]
        public virtual string SalesFactory { get; set; }
        [Parameter("uint256", "_depositFeePercent", 5)]
        public virtual BigInteger DepositFeePercent { get; set; }
        [Parameter("uint256", "_depositFeePrecision", 6)]
        public virtual BigInteger DepositFeePrecision { get; set; }
    }

    public partial class MassUpdatePoolsFunction : MassUpdatePoolsFunctionBase { }

    [Function("massUpdatePools")]
    public class MassUpdatePoolsFunctionBase : FunctionMessage
    {

    }

    public partial class OwnerFunction : OwnerFunctionBase { }

    [Function("owner", "address")]
    public class OwnerFunctionBase : FunctionMessage
    {

    }

    public partial class PaidOutFunction : PaidOutFunctionBase { }

    [Function("paidOut", "uint256")]
    public class PaidOutFunctionBase : FunctionMessage
    {

    }

    public partial class PendingFunction : PendingFunctionBase { }

    [Function("pending", "uint256")]
    public class PendingFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_pid", 1)]
        public virtual BigInteger Pid { get; set; }
        [Parameter("address", "_user", 2)]
        public virtual string User { get; set; }
    }

    public partial class PoolInfoFunction : PoolInfoFunctionBase { }

    [Function("poolInfo", typeof(PoolInfoOutputDTO))]
    public class PoolInfoFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class PoolLengthFunction : PoolLengthFunctionBase { }

    [Function("poolLength", "uint256")]
    public class PoolLengthFunctionBase : FunctionMessage
    {

    }

    public partial class PostSaleWithdrawPenaltyLengthFunction : PostSaleWithdrawPenaltyLengthFunctionBase { }

    [Function("postSaleWithdrawPenaltyLength", "uint256")]
    public class PostSaleWithdrawPenaltyLengthFunctionBase : FunctionMessage
    {

    }

    public partial class PostSaleWithdrawPenaltyPercentFunction : PostSaleWithdrawPenaltyPercentFunctionBase { }

    [Function("postSaleWithdrawPenaltyPercent", "uint256")]
    public class PostSaleWithdrawPenaltyPercentFunctionBase : FunctionMessage
    {

    }

    public partial class PostSaleWithdrawPenaltyPrecisionFunction : PostSaleWithdrawPenaltyPrecisionFunctionBase { }

    [Function("postSaleWithdrawPenaltyPrecision", "uint256")]
    public class PostSaleWithdrawPenaltyPrecisionFunctionBase : FunctionMessage
    {

    }

    public partial class RedistributeXavaFunction : RedistributeXavaFunctionBase { }

    [Function("redistributeXava")]
    public class RedistributeXavaFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_pid", 1)]
        public virtual BigInteger Pid { get; set; }
        [Parameter("address", "_user", 2)]
        public virtual string User { get; set; }
        [Parameter("uint256", "_amountToRedistribute", 3)]
        public virtual BigInteger AmountToRedistribute { get; set; }
    }

    public partial class RenounceOwnershipFunction : RenounceOwnershipFunctionBase { }

    [Function("renounceOwnership")]
    public class RenounceOwnershipFunctionBase : FunctionMessage
    {

    }

    public partial class RewardPerSecondFunction : RewardPerSecondFunctionBase { }

    [Function("rewardPerSecond", "uint256")]
    public class RewardPerSecondFunctionBase : FunctionMessage
    {

    }

    public partial class SalesFactoryFunction : SalesFactoryFunctionBase { }

    [Function("salesFactory", "address")]
    public class SalesFactoryFunctionBase : FunctionMessage
    {

    }

    public partial class SetFunction : SetFunctionBase { }

    [Function("set")]
    public class SetFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_pid", 1)]
        public virtual BigInteger Pid { get; set; }
        [Parameter("uint256", "_allocPoint", 2)]
        public virtual BigInteger AllocPoint { get; set; }
        [Parameter("bool", "_withUpdate", 3)]
        public virtual bool WithUpdate { get; set; }
    }

    public partial class SetDepositFeeFunction : SetDepositFeeFunctionBase { }

    [Function("setDepositFee")]
    public class SetDepositFeeFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_depositFeePercent", 1)]
        public virtual BigInteger DepositFeePercent { get; set; }
        [Parameter("uint256", "_depositFeePrecision", 2)]
        public virtual BigInteger DepositFeePrecision { get; set; }
    }

    public partial class SetPostSaleWithdrawPenaltyPercentAndLengthFunction : SetPostSaleWithdrawPenaltyPercentAndLengthFunctionBase { }

    [Function("setPostSaleWithdrawPenaltyPercentAndLength")]
    public class SetPostSaleWithdrawPenaltyPercentAndLengthFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_postSaleWithdrawPenaltyPercent", 1)]
        public virtual BigInteger PostSaleWithdrawPenaltyPercent { get; set; }
        [Parameter("uint256", "_postSaleWithdrawPenaltyLength", 2)]
        public virtual BigInteger PostSaleWithdrawPenaltyLength { get; set; }
        [Parameter("uint256", "_postSaleWithdrawPenaltyPrecision", 3)]
        public virtual BigInteger PostSaleWithdrawPenaltyPrecision { get; set; }
    }

    public partial class SetSalesFactoryFunction : SetSalesFactoryFunctionBase { }

    [Function("setSalesFactory")]
    public class SetSalesFactoryFunctionBase : FunctionMessage
    {
        [Parameter("address", "_salesFactory", 1)]
        public virtual string SalesFactory { get; set; }
    }

    public partial class SetTokensUnlockTimeFunction : SetTokensUnlockTimeFunctionBase { }

    [Function("setTokensUnlockTime")]
    public class SetTokensUnlockTimeFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_pid", 1)]
        public virtual BigInteger Pid { get; set; }
        [Parameter("address", "_user", 2)]
        public virtual string User { get; set; }
        [Parameter("uint256", "_tokensUnlockTime", 3)]
        public virtual BigInteger TokensUnlockTime { get; set; }
    }

    public partial class StartTimestampFunction : StartTimestampFunctionBase { }

    [Function("startTimestamp", "uint256")]
    public class StartTimestampFunctionBase : FunctionMessage
    {

    }

    public partial class TotalAllocPointFunction : TotalAllocPointFunctionBase { }

    [Function("totalAllocPoint", "uint256")]
    public class TotalAllocPointFunctionBase : FunctionMessage
    {

    }

    public partial class TotalBurnedFromUserFunction : TotalBurnedFromUserFunctionBase { }

    [Function("totalBurnedFromUser", "uint256")]
    public class TotalBurnedFromUserFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class TotalPendingFunction : TotalPendingFunctionBase { }

    [Function("totalPending", "uint256")]
    public class TotalPendingFunctionBase : FunctionMessage
    {

    }

    public partial class TotalRewardsFunction : TotalRewardsFunctionBase { }

    [Function("totalRewards", "uint256")]
    public class TotalRewardsFunctionBase : FunctionMessage
    {

    }

    public partial class TotalXavaRedistributedFunction : TotalXavaRedistributedFunctionBase { }

    [Function("totalXavaRedistributed", "uint256")]
    public class TotalXavaRedistributedFunctionBase : FunctionMessage
    {

    }

    public partial class TransferOwnershipFunction : TransferOwnershipFunctionBase { }

    [Function("transferOwnership")]
    public class TransferOwnershipFunctionBase : FunctionMessage
    {
        [Parameter("address", "newOwner", 1)]
        public virtual string NewOwner { get; set; }
    }

    public partial class UpdatePoolFunction : UpdatePoolFunctionBase { }

    [Function("updatePool")]
    public class UpdatePoolFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_pid", 1)]
        public virtual BigInteger Pid { get; set; }
    }

    public partial class UserInfoFunction : UserInfoFunctionBase { }

    [Function("userInfo", typeof(UserInfoOutputDTO))]
    public class UserInfoFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
        [Parameter("address", "", 2)]
        public virtual string ReturnValue2 { get; set; }
    }

    public partial class WithdrawFunction : WithdrawFunctionBase { }

    [Function("withdraw")]
    public class WithdrawFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_pid", 1)]
        public virtual BigInteger Pid { get; set; }
        [Parameter("uint256", "_amount", 2)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class CompoundedEarningsEventDTO : CompoundedEarningsEventDTOBase { }

    [Event("CompoundedEarnings")]
    public class CompoundedEarningsEventDTOBase : IEventDTO
    {
        [Parameter("address", "user", 1, true )]
        public virtual string User { get; set; }
        [Parameter("uint256", "pid", 2, true )]
        public virtual BigInteger Pid { get; set; }
        [Parameter("uint256", "amountAdded", 3, false )]
        public virtual BigInteger AmountAdded { get; set; }
        [Parameter("uint256", "totalDeposited", 4, false )]
        public virtual BigInteger TotalDeposited { get; set; }
    }

    public partial class DepositEventDTO : DepositEventDTOBase { }

    [Event("Deposit")]
    public class DepositEventDTOBase : IEventDTO
    {
        [Parameter("address", "user", 1, true )]
        public virtual string User { get; set; }
        [Parameter("uint256", "pid", 2, true )]
        public virtual BigInteger Pid { get; set; }
        [Parameter("uint256", "amount", 3, false )]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class DepositFeeSetEventDTO : DepositFeeSetEventDTOBase { }

    [Event("DepositFeeSet")]
    public class DepositFeeSetEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "depositFeePercent", 1, false )]
        public virtual BigInteger DepositFeePercent { get; set; }
        [Parameter("uint256", "depositFeePrecision", 2, false )]
        public virtual BigInteger DepositFeePrecision { get; set; }
    }

    public partial class EmergencyWithdrawEventDTO : EmergencyWithdrawEventDTOBase { }

    [Event("EmergencyWithdraw")]
    public class EmergencyWithdrawEventDTOBase : IEventDTO
    {
        [Parameter("address", "user", 1, true )]
        public virtual string User { get; set; }
        [Parameter("uint256", "pid", 2, true )]
        public virtual BigInteger Pid { get; set; }
        [Parameter("uint256", "amount", 3, false )]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class FeeTakenEventDTO : FeeTakenEventDTOBase { }

    [Event("FeeTaken")]
    public class FeeTakenEventDTOBase : IEventDTO
    {
        [Parameter("address", "user", 1, true )]
        public virtual string User { get; set; }
        [Parameter("uint256", "pid", 2, true )]
        public virtual BigInteger Pid { get; set; }
        [Parameter("uint256", "amount", 3, false )]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class OwnershipTransferredEventDTO : OwnershipTransferredEventDTOBase { }

    [Event("OwnershipTransferred")]
    public class OwnershipTransferredEventDTOBase : IEventDTO
    {
        [Parameter("address", "previousOwner", 1, true )]
        public virtual string PreviousOwner { get; set; }
        [Parameter("address", "newOwner", 2, true )]
        public virtual string NewOwner { get; set; }
    }

    public partial class PostSaleWithdrawFeeChargedEventDTO : PostSaleWithdrawFeeChargedEventDTOBase { }

    [Event("PostSaleWithdrawFeeCharged")]
    public class PostSaleWithdrawFeeChargedEventDTOBase : IEventDTO
    {
        [Parameter("address", "user", 1, false )]
        public virtual string User { get; set; }
        [Parameter("uint256", "amountStake", 2, false )]
        public virtual BigInteger AmountStake { get; set; }
        [Parameter("uint256", "amountRewards", 3, false )]
        public virtual BigInteger AmountRewards { get; set; }
    }

    public partial class WithdrawEventDTO : WithdrawEventDTOBase { }

    [Event("Withdraw")]
    public class WithdrawEventDTOBase : IEventDTO
    {
        [Parameter("address", "user", 1, true )]
        public virtual string User { get; set; }
        [Parameter("uint256", "pid", 2, true )]
        public virtual BigInteger Pid { get; set; }
        [Parameter("uint256", "amount", 3, false )]
        public virtual BigInteger Amount { get; set; }
    }







    public partial class DepositFeePercentOutputDTO : DepositFeePercentOutputDTOBase { }

    [FunctionOutput]
    public class DepositFeePercentOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class DepositFeePrecisionOutputDTO : DepositFeePrecisionOutputDTOBase { }

    [FunctionOutput]
    public class DepositFeePrecisionOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class DepositedOutputDTO : DepositedOutputDTOBase { }

    [FunctionOutput]
    public class DepositedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class EndTimestampOutputDTO : EndTimestampOutputDTOBase { }

    [FunctionOutput]
    public class EndTimestampOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class Erc20OutputDTO : Erc20OutputDTOBase { }

    [FunctionOutput]
    public class Erc20OutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }



    public partial class GetPendingAndDepositedForUsersOutputDTO : GetPendingAndDepositedForUsersOutputDTOBase { }

    [FunctionOutput]
    public class GetPendingAndDepositedForUsersOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256[]", "", 1)]
        public virtual List<BigInteger> ReturnValue1 { get; set; }
        [Parameter("uint256[]", "", 2)]
        public virtual List<BigInteger> ReturnValue2 { get; set; }
    }

    public partial class GetWithdrawFeeOutputDTO : GetWithdrawFeeOutputDTOBase { }

    [FunctionOutput]
    public class GetWithdrawFeeOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
        [Parameter("uint256", "", 2)]
        public virtual BigInteger ReturnValue2 { get; set; }
    }





    public partial class OwnerOutputDTO : OwnerOutputDTOBase { }

    [FunctionOutput]
    public class OwnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class PaidOutOutputDTO : PaidOutOutputDTOBase { }

    [FunctionOutput]
    public class PaidOutOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class PendingOutputDTO : PendingOutputDTOBase { }

    [FunctionOutput]
    public class PendingOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class PoolInfoOutputDTO : PoolInfoOutputDTOBase { }

    [FunctionOutput]
    public class PoolInfoOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "lpToken", 1)]
        public virtual string LpToken { get; set; }
        [Parameter("uint256", "allocPoint", 2)]
        public virtual BigInteger AllocPoint { get; set; }
        [Parameter("uint256", "lastRewardTimestamp", 3)]
        public virtual BigInteger LastRewardTimestamp { get; set; }
        [Parameter("uint256", "accERC20PerShare", 4)]
        public virtual BigInteger AccERC20PerShare { get; set; }
        [Parameter("uint256", "totalDeposits", 5)]
        public virtual BigInteger TotalDeposits { get; set; }
    }

    public partial class PoolLengthOutputDTO : PoolLengthOutputDTOBase { }

    [FunctionOutput]
    public class PoolLengthOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class PostSaleWithdrawPenaltyLengthOutputDTO : PostSaleWithdrawPenaltyLengthOutputDTOBase { }

    [FunctionOutput]
    public class PostSaleWithdrawPenaltyLengthOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class PostSaleWithdrawPenaltyPercentOutputDTO : PostSaleWithdrawPenaltyPercentOutputDTOBase { }

    [FunctionOutput]
    public class PostSaleWithdrawPenaltyPercentOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class PostSaleWithdrawPenaltyPrecisionOutputDTO : PostSaleWithdrawPenaltyPrecisionOutputDTOBase { }

    [FunctionOutput]
    public class PostSaleWithdrawPenaltyPrecisionOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }





    public partial class RewardPerSecondOutputDTO : RewardPerSecondOutputDTOBase { }

    [FunctionOutput]
    public class RewardPerSecondOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class SalesFactoryOutputDTO : SalesFactoryOutputDTOBase { }

    [FunctionOutput]
    public class SalesFactoryOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }











    public partial class StartTimestampOutputDTO : StartTimestampOutputDTOBase { }

    [FunctionOutput]
    public class StartTimestampOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class TotalAllocPointOutputDTO : TotalAllocPointOutputDTOBase { }

    [FunctionOutput]
    public class TotalAllocPointOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class TotalBurnedFromUserOutputDTO : TotalBurnedFromUserOutputDTOBase { }

    [FunctionOutput]
    public class TotalBurnedFromUserOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class TotalPendingOutputDTO : TotalPendingOutputDTOBase { }

    [FunctionOutput]
    public class TotalPendingOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class TotalRewardsOutputDTO : TotalRewardsOutputDTOBase { }

    [FunctionOutput]
    public class TotalRewardsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class TotalXavaRedistributedOutputDTO : TotalXavaRedistributedOutputDTOBase { }

    [FunctionOutput]
    public class TotalXavaRedistributedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }





    public partial class UserInfoOutputDTO : UserInfoOutputDTOBase { }

    [FunctionOutput]
    public class UserInfoOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "amount", 1)]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "rewardDebt", 2)]
        public virtual BigInteger RewardDebt { get; set; }
        [Parameter("uint256", "tokensUnlockTime", 3)]
        public virtual BigInteger TokensUnlockTime { get; set; }
    }


}
