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

namespace Avalaunch.OldSale.ContractDefinition
{


    public partial class OldSaleDeployment : OldSaleDeploymentBase
    {
        public OldSaleDeployment() : base(BYTECODE) { }
        public OldSaleDeployment(string byteCode) : base(byteCode) { }
    }

    public class OldSaleDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = """
        [{"inputs":[{"internalType":"address","name":"_admin","type":"address"},{"internalType":"address","name":"_allocationStaking","type":"address"}],"stateMutability":"nonpayable","type":"constructor"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"uint256","name":"roundId","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"maxParticipation","type":"uint256"}],"name":"MaxParticipationSet","type":"event"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"uint256","name":"registrationTimeStarts","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"registrationTimeEnds","type":"uint256"}],"name":"RegistrationTimeSet","type":"event"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"uint256","name":"roundId","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"startTime","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"maxParticipation","type":"uint256"}],"name":"RoundAdded","type":"event"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"address","name":"saleOwner","type":"address"},{"indexed":false,"internalType":"uint256","name":"tokenPriceInAVAX","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"amountOfTokensToSell","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"saleEnd","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"tokensUnlockTime","type":"uint256"}],"name":"SaleCreated","type":"event"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"uint256","name":"newPrice","type":"uint256"}],"name":"TokenPriceSet","type":"event"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"address","name":"user","type":"address"},{"indexed":false,"internalType":"uint256","name":"amount","type":"uint256"}],"name":"TokensSold","type":"event"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"address","name":"user","type":"address"},{"indexed":false,"internalType":"uint256","name":"amount","type":"uint256"}],"name":"TokensWithdrawn","type":"event"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"address","name":"user","type":"address"},{"indexed":false,"internalType":"uint256","name":"roundId","type":"uint256"}],"name":"UserRegistered","type":"event"},{"inputs":[{"internalType":"address","name":"","type":"address"}],"name":"addressToRoundRegisteredFor","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"admin","outputs":[{"internalType":"contract IAdmin","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"allocationStakingContract","outputs":[{"internalType":"contract IAllocationStaking","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"bytes","name":"signature","type":"bytes"},{"internalType":"address","name":"user","type":"address"},{"internalType":"uint256","name":"amount","type":"uint256"},{"internalType":"uint256","name":"amountXavaToBurn","type":"uint256"},{"internalType":"uint256","name":"round","type":"uint256"}],"name":"checkParticipationSignature","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"bytes","name":"signature","type":"bytes"},{"internalType":"address","name":"user","type":"address"},{"internalType":"uint256","name":"roundId","type":"uint256"}],"name":"checkRegistrationSignature","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"depositTokens","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256","name":"timeToAdd","type":"uint256"}],"name":"extendRegistrationPeriod","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"factory","outputs":[{"internalType":"contract ISalesFactory","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"getCurrentRound","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"getNumberOfRegisteredUsers","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"_user","type":"address"}],"name":"getParticipation","outputs":[{"internalType":"uint256","name":"","type":"uint256"},{"internalType":"uint256","name":"","type":"uint256"},{"internalType":"uint256","name":"","type":"uint256"},{"internalType":"uint256","name":"","type":"uint256"},{"internalType":"bool[]","name":"","type":"bool[]"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"bytes","name":"signature","type":"bytes"},{"internalType":"address","name":"user","type":"address"},{"internalType":"uint256","name":"amount","type":"uint256"},{"internalType":"uint256","name":"amountXavaToBurn","type":"uint256"},{"internalType":"uint256","name":"roundId","type":"uint256"}],"name":"getParticipationSigner","outputs":[{"internalType":"address","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"getVestingInfo","outputs":[{"internalType":"uint256[]","name":"","type":"uint256[]"},{"internalType":"uint256[]","name":"","type":"uint256[]"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"","type":"address"}],"name":"isParticipated","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"maxVestingTimeShift","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"numberOfParticipants","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"one","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"bytes","name":"signature","type":"bytes"},{"internalType":"uint256","name":"amount","type":"uint256"},{"internalType":"uint256","name":"amountXavaToBurn","type":"uint256"},{"internalType":"uint256","name":"roundId","type":"uint256"}],"name":"participate","outputs":[],"stateMutability":"payable","type":"function"},{"inputs":[],"name":"portionVestingPrecision","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"timeToShift","type":"uint256"}],"name":"postponeSale","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"bytes","name":"signature","type":"bytes"},{"internalType":"uint256","name":"roundId","type":"uint256"}],"name":"registerForSale","outputs":[],"stateMutability":"payable","type":"function"},{"inputs":[],"name":"registration","outputs":[{"internalType":"uint256","name":"registrationTimeStarts","type":"uint256"},{"internalType":"uint256","name":"registrationTimeEnds","type":"uint256"},{"internalType":"uint256","name":"numberOfRegistrants","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"registrationDepositAVAX","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"registrationFees","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"","type":"uint256"}],"name":"roundIdToRound","outputs":[{"internalType":"uint256","name":"startTime","type":"uint256"},{"internalType":"uint256","name":"maxParticipation","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"","type":"uint256"}],"name":"roundIds","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"sale","outputs":[{"internalType":"contract IERC20","name":"token","type":"address"},{"internalType":"bool","name":"isCreated","type":"bool"},{"internalType":"bool","name":"earningsWithdrawn","type":"bool"},{"internalType":"bool","name":"tokensDeposited","type":"bool"},{"internalType":"address","name":"saleOwner","type":"address"},{"internalType":"uint256","name":"tokenPriceInAVAX","type":"uint256"},{"internalType":"uint256","name":"amountOfTokensToSell","type":"uint256"},{"internalType":"uint256","name":"totalTokensSold","type":"uint256"},{"internalType":"uint256","name":"totalAVAXRaised","type":"uint256"},{"internalType":"uint256","name":"saleEnd","type":"uint256"},{"internalType":"uint256","name":"tokensUnlockTime","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256[]","name":"rounds","type":"uint256[]"},{"internalType":"uint256[]","name":"caps","type":"uint256[]"}],"name":"setCapPerRound","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256","name":"_registrationTimeStarts","type":"uint256"},{"internalType":"uint256","name":"_registrationTimeEnds","type":"uint256"}],"name":"setRegistrationTime","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256[]","name":"startTimes","type":"uint256[]"},{"internalType":"uint256[]","name":"maxParticipations","type":"uint256[]"}],"name":"setRounds","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"_token","type":"address"},{"internalType":"address","name":"_saleOwner","type":"address"},{"internalType":"uint256","name":"_tokenPriceInAVAX","type":"uint256"},{"internalType":"uint256","name":"_amountOfTokensToSell","type":"uint256"},{"internalType":"uint256","name":"_saleEnd","type":"uint256"},{"internalType":"uint256","name":"_tokensUnlockTime","type":"uint256"},{"internalType":"uint256","name":"_portionVestingPrecision","type":"uint256"},{"internalType":"uint256","name":"_stakingRoundId","type":"uint256"},{"internalType":"uint256","name":"_registrationDepositAVAX","type":"uint256"}],"name":"setSaleParams","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"saleToken","type":"address"}],"name":"setSaleToken","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256[]","name":"_unlockingTimes","type":"uint256[]"},{"internalType":"uint256[]","name":"_percents","type":"uint256[]"},{"internalType":"uint256","name":"_maxVestingTimeShift","type":"uint256"}],"name":"setVestingParams","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256","name":"timeToShift","type":"uint256"}],"name":"shiftVestingUnlockingTimes","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"stakingRoundId","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"price","type":"uint256"}],"name":"updateTokenPriceInAVAX","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"","type":"address"}],"name":"userToParticipation","outputs":[{"internalType":"uint256","name":"amountBought","type":"uint256"},{"internalType":"uint256","name":"amountAVAXPaid","type":"uint256"},{"internalType":"uint256","name":"timeParticipated","type":"uint256"},{"internalType":"uint256","name":"roundId","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"","type":"uint256"}],"name":"vestingPercentPerPortion","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"uint256","name":"","type":"uint256"}],"name":"vestingPortionsUnlockTime","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"bool","name":"withBurn","type":"bool"}],"name":"withdrawEarningsAndLeftover","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"withdrawRegistrationFees","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256","name":"portionId","type":"uint256"}],"name":"withdrawTokens","outputs":[],"stateMutability":"nonpayable","type":"function"}]
        """;
        public OldSaleDeploymentBase() : base(BYTECODE) { }
        public OldSaleDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "_admin", 1)]
        public virtual string Admin { get; set; }
        [Parameter("address", "_allocationStaking", 2)]
        public virtual string AllocationStaking { get; set; }
    }

    public partial class AddressToRoundRegisteredForFunction : AddressToRoundRegisteredForFunctionBase { }

    [Function("addressToRoundRegisteredFor", "uint256")]
    public class AddressToRoundRegisteredForFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class AdminFunction : AdminFunctionBase { }

    [Function("admin", "address")]
    public class AdminFunctionBase : FunctionMessage
    {

    }

    public partial class AllocationStakingContractFunction : AllocationStakingContractFunctionBase { }

    [Function("allocationStakingContract", "address")]
    public class AllocationStakingContractFunctionBase : FunctionMessage
    {

    }

    public partial class CheckParticipationSignatureFunction : CheckParticipationSignatureFunctionBase { }

    [Function("checkParticipationSignature", "bool")]
    public class CheckParticipationSignatureFunctionBase : FunctionMessage
    {
        [Parameter("bytes", "signature", 1)]
        public virtual byte[] Signature { get; set; }
        [Parameter("address", "user", 2)]
        public virtual string User { get; set; }
        [Parameter("uint256", "amount", 3)]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "amountXavaToBurn", 4)]
        public virtual BigInteger AmountXavaToBurn { get; set; }
        [Parameter("uint256", "round", 5)]
        public virtual BigInteger Round { get; set; }
    }

    public partial class CheckRegistrationSignatureFunction : CheckRegistrationSignatureFunctionBase { }

    [Function("checkRegistrationSignature", "bool")]
    public class CheckRegistrationSignatureFunctionBase : FunctionMessage
    {
        [Parameter("bytes", "signature", 1)]
        public virtual byte[] Signature { get; set; }
        [Parameter("address", "user", 2)]
        public virtual string User { get; set; }
        [Parameter("uint256", "roundId", 3)]
        public virtual BigInteger RoundId { get; set; }
    }

    public partial class DepositTokensFunction : DepositTokensFunctionBase { }

    [Function("depositTokens")]
    public class DepositTokensFunctionBase : FunctionMessage
    {

    }

    public partial class ExtendRegistrationPeriodFunction : ExtendRegistrationPeriodFunctionBase { }

    [Function("extendRegistrationPeriod")]
    public class ExtendRegistrationPeriodFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "timeToAdd", 1)]
        public virtual BigInteger TimeToAdd { get; set; }
    }

    public partial class FactoryFunction : FactoryFunctionBase { }

    [Function("factory", "address")]
    public class FactoryFunctionBase : FunctionMessage
    {

    }

    public partial class GetCurrentRoundFunction : GetCurrentRoundFunctionBase { }

    [Function("getCurrentRound", "uint256")]
    public class GetCurrentRoundFunctionBase : FunctionMessage
    {

    }

    public partial class GetNumberOfRegisteredUsersFunction : GetNumberOfRegisteredUsersFunctionBase { }

    [Function("getNumberOfRegisteredUsers", "uint256")]
    public class GetNumberOfRegisteredUsersFunctionBase : FunctionMessage
    {

    }

    public partial class GetParticipationFunction : GetParticipationFunctionBase { }

    [Function("getParticipation", typeof(GetParticipationOutputDTO))]
    public class GetParticipationFunctionBase : FunctionMessage
    {
        [Parameter("address", "_user", 1)]
        public virtual string User { get; set; }
    }

    public partial class GetParticipationSignerFunction : GetParticipationSignerFunctionBase { }

    [Function("getParticipationSigner", "address")]
    public class GetParticipationSignerFunctionBase : FunctionMessage
    {
        [Parameter("bytes", "signature", 1)]
        public virtual byte[] Signature { get; set; }
        [Parameter("address", "user", 2)]
        public virtual string User { get; set; }
        [Parameter("uint256", "amount", 3)]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "amountXavaToBurn", 4)]
        public virtual BigInteger AmountXavaToBurn { get; set; }
        [Parameter("uint256", "roundId", 5)]
        public virtual BigInteger RoundId { get; set; }
    }

    public partial class GetVestingInfoFunction : GetVestingInfoFunctionBase { }

    [Function("getVestingInfo", typeof(GetVestingInfoOutputDTO))]
    public class GetVestingInfoFunctionBase : FunctionMessage
    {

    }

    public partial class IsParticipatedFunction : IsParticipatedFunctionBase { }

    [Function("isParticipated", "bool")]
    public class IsParticipatedFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class MaxVestingTimeShiftFunction : MaxVestingTimeShiftFunctionBase { }

    [Function("maxVestingTimeShift", "uint256")]
    public class MaxVestingTimeShiftFunctionBase : FunctionMessage
    {

    }

    public partial class NumberOfParticipantsFunction : NumberOfParticipantsFunctionBase { }

    [Function("numberOfParticipants", "uint256")]
    public class NumberOfParticipantsFunctionBase : FunctionMessage
    {

    }

    public partial class OneFunction : OneFunctionBase { }

    [Function("one", "uint256")]
    public class OneFunctionBase : FunctionMessage
    {

    }

    public partial class ParticipateFunction : ParticipateFunctionBase { }

    [Function("participate")]
    public class ParticipateFunctionBase : FunctionMessage
    {
        [Parameter("bytes", "signature", 1)]
        public virtual byte[] Signature { get; set; }
        [Parameter("uint256", "amount", 2)]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "amountXavaToBurn", 3)]
        public virtual BigInteger AmountXavaToBurn { get; set; }
        [Parameter("uint256", "roundId", 4)]
        public virtual BigInteger RoundId { get; set; }
    }

    public partial class PortionVestingPrecisionFunction : PortionVestingPrecisionFunctionBase { }

    [Function("portionVestingPrecision", "uint256")]
    public class PortionVestingPrecisionFunctionBase : FunctionMessage
    {

    }

    public partial class PostponeSaleFunction : PostponeSaleFunctionBase { }

    [Function("postponeSale")]
    public class PostponeSaleFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "timeToShift", 1)]
        public virtual BigInteger TimeToShift { get; set; }
    }

    public partial class RegisterForSaleFunction : RegisterForSaleFunctionBase { }

    [Function("registerForSale")]
    public class RegisterForSaleFunctionBase : FunctionMessage
    {
        [Parameter("bytes", "signature", 1)]
        public virtual byte[] Signature { get; set; }
        [Parameter("uint256", "roundId", 2)]
        public virtual BigInteger RoundId { get; set; }
    }

    public partial class RegistrationFunction : RegistrationFunctionBase { }

    [Function("registration", typeof(RegistrationOutputDTO))]
    public class RegistrationFunctionBase : FunctionMessage
    {

    }

    public partial class RegistrationDepositAVAXFunction : RegistrationDepositAVAXFunctionBase { }

    [Function("registrationDepositAVAX", "uint256")]
    public class RegistrationDepositAVAXFunctionBase : FunctionMessage
    {

    }

    public partial class RegistrationFeesFunction : RegistrationFeesFunctionBase { }

    [Function("registrationFees", "uint256")]
    public class RegistrationFeesFunctionBase : FunctionMessage
    {

    }

    public partial class RoundIdToRoundFunction : RoundIdToRoundFunctionBase { }

    [Function("roundIdToRound", typeof(RoundIdToRoundOutputDTO))]
    public class RoundIdToRoundFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class RoundIdsFunction : RoundIdsFunctionBase { }

    [Function("roundIds", "uint256")]
    public class RoundIdsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class SaleFunction : SaleFunctionBase { }

    [Function("sale", typeof(SaleOutputDTO))]
    public class SaleFunctionBase : FunctionMessage
    {

    }

    public partial class SetCapPerRoundFunction : SetCapPerRoundFunctionBase { }

    [Function("setCapPerRound")]
    public class SetCapPerRoundFunctionBase : FunctionMessage
    {
        [Parameter("uint256[]", "rounds", 1)]
        public virtual List<BigInteger> Rounds { get; set; }
        [Parameter("uint256[]", "caps", 2)]
        public virtual List<BigInteger> Caps { get; set; }
    }

    public partial class SetRegistrationTimeFunction : SetRegistrationTimeFunctionBase { }

    [Function("setRegistrationTime")]
    public class SetRegistrationTimeFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_registrationTimeStarts", 1)]
        public virtual BigInteger RegistrationTimeStarts { get; set; }
        [Parameter("uint256", "_registrationTimeEnds", 2)]
        public virtual BigInteger RegistrationTimeEnds { get; set; }
    }

    public partial class SetRoundsFunction : SetRoundsFunctionBase { }

    [Function("setRounds")]
    public class SetRoundsFunctionBase : FunctionMessage
    {
        [Parameter("uint256[]", "startTimes", 1)]
        public virtual List<BigInteger> StartTimes { get; set; }
        [Parameter("uint256[]", "maxParticipations", 2)]
        public virtual List<BigInteger> MaxParticipations { get; set; }
    }

    public partial class SetSaleParamsFunction : SetSaleParamsFunctionBase { }

    [Function("setSaleParams")]
    public class SetSaleParamsFunctionBase : FunctionMessage
    {
        [Parameter("address", "_token", 1)]
        public virtual string Token { get; set; }
        [Parameter("address", "_saleOwner", 2)]
        public virtual string SaleOwner { get; set; }
        [Parameter("uint256", "_tokenPriceInAVAX", 3)]
        public virtual BigInteger TokenPriceInAVAX { get; set; }
        [Parameter("uint256", "_amountOfTokensToSell", 4)]
        public virtual BigInteger AmountOfTokensToSell { get; set; }
        [Parameter("uint256", "_saleEnd", 5)]
        public virtual BigInteger SaleEnd { get; set; }
        [Parameter("uint256", "_tokensUnlockTime", 6)]
        public virtual BigInteger TokensUnlockTime { get; set; }
        [Parameter("uint256", "_portionVestingPrecision", 7)]
        public virtual BigInteger PortionVestingPrecision { get; set; }
        [Parameter("uint256", "_stakingRoundId", 8)]
        public virtual BigInteger StakingRoundId { get; set; }
        [Parameter("uint256", "_registrationDepositAVAX", 9)]
        public virtual BigInteger RegistrationDepositAVAX { get; set; }
    }

    public partial class SetSaleTokenFunction : SetSaleTokenFunctionBase { }

    [Function("setSaleToken")]
    public class SetSaleTokenFunctionBase : FunctionMessage
    {
        [Parameter("address", "saleToken", 1)]
        public virtual string SaleToken { get; set; }
    }

    public partial class SetVestingParamsFunction : SetVestingParamsFunctionBase { }

    [Function("setVestingParams")]
    public class SetVestingParamsFunctionBase : FunctionMessage
    {
        [Parameter("uint256[]", "_unlockingTimes", 1)]
        public virtual List<BigInteger> UnlockingTimes { get; set; }
        [Parameter("uint256[]", "_percents", 2)]
        public virtual List<BigInteger> Percents { get; set; }
        [Parameter("uint256", "_maxVestingTimeShift", 3)]
        public virtual BigInteger MaxVestingTimeShift { get; set; }
    }

    public partial class ShiftVestingUnlockingTimesFunction : ShiftVestingUnlockingTimesFunctionBase { }

    [Function("shiftVestingUnlockingTimes")]
    public class ShiftVestingUnlockingTimesFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "timeToShift", 1)]
        public virtual BigInteger TimeToShift { get; set; }
    }

    public partial class StakingRoundIdFunction : StakingRoundIdFunctionBase { }

    [Function("stakingRoundId", "uint256")]
    public class StakingRoundIdFunctionBase : FunctionMessage
    {

    }

    public partial class UpdateTokenPriceInAVAXFunction : UpdateTokenPriceInAVAXFunctionBase { }

    [Function("updateTokenPriceInAVAX")]
    public class UpdateTokenPriceInAVAXFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "price", 1)]
        public virtual BigInteger Price { get; set; }
    }

    public partial class UserToParticipationFunction : UserToParticipationFunctionBase { }

    [Function("userToParticipation", typeof(UserToParticipationOutputDTO))]
    public class UserToParticipationFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class VestingPercentPerPortionFunction : VestingPercentPerPortionFunctionBase { }

    [Function("vestingPercentPerPortion", "uint256")]
    public class VestingPercentPerPortionFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class VestingPortionsUnlockTimeFunction : VestingPortionsUnlockTimeFunctionBase { }

    [Function("vestingPortionsUnlockTime", "uint256")]
    public class VestingPortionsUnlockTimeFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class WithdrawEarningsAndLeftoverFunction : WithdrawEarningsAndLeftoverFunctionBase { }

    [Function("withdrawEarningsAndLeftover")]
    public class WithdrawEarningsAndLeftoverFunctionBase : FunctionMessage
    {
        [Parameter("bool", "withBurn", 1)]
        public virtual bool WithBurn { get; set; }
    }

    public partial class WithdrawRegistrationFeesFunction : WithdrawRegistrationFeesFunctionBase { }

    [Function("withdrawRegistrationFees")]
    public class WithdrawRegistrationFeesFunctionBase : FunctionMessage
    {

    }

    public partial class WithdrawTokensFunction : WithdrawTokensFunctionBase { }

    [Function("withdrawTokens")]
    public class WithdrawTokensFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "portionId", 1)]
        public virtual BigInteger PortionId { get; set; }
    }

    public partial class MaxParticipationSetEventDTO : MaxParticipationSetEventDTOBase { }

    [Event("MaxParticipationSet")]
    public class MaxParticipationSetEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "roundId", 1, false )]
        public virtual BigInteger RoundId { get; set; }
        [Parameter("uint256", "maxParticipation", 2, false )]
        public virtual BigInteger MaxParticipation { get; set; }
    }

    public partial class RegistrationTimeSetEventDTO : RegistrationTimeSetEventDTOBase { }

    [Event("RegistrationTimeSet")]
    public class RegistrationTimeSetEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "registrationTimeStarts", 1, false )]
        public virtual BigInteger RegistrationTimeStarts { get; set; }
        [Parameter("uint256", "registrationTimeEnds", 2, false )]
        public virtual BigInteger RegistrationTimeEnds { get; set; }
    }

    public partial class RoundAddedEventDTO : RoundAddedEventDTOBase { }

    [Event("RoundAdded")]
    public class RoundAddedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "roundId", 1, false )]
        public virtual BigInteger RoundId { get; set; }
        [Parameter("uint256", "startTime", 2, false )]
        public virtual BigInteger StartTime { get; set; }
        [Parameter("uint256", "maxParticipation", 3, false )]
        public virtual BigInteger MaxParticipation { get; set; }
    }

    public partial class SaleCreatedEventDTO : SaleCreatedEventDTOBase { }

    [Event("SaleCreated")]
    public class SaleCreatedEventDTOBase : IEventDTO
    {
        [Parameter("address", "saleOwner", 1, false )]
        public virtual string SaleOwner { get; set; }
        [Parameter("uint256", "tokenPriceInAVAX", 2, false )]
        public virtual BigInteger TokenPriceInAVAX { get; set; }
        [Parameter("uint256", "amountOfTokensToSell", 3, false )]
        public virtual BigInteger AmountOfTokensToSell { get; set; }
        [Parameter("uint256", "saleEnd", 4, false )]
        public virtual BigInteger SaleEnd { get; set; }
        [Parameter("uint256", "tokensUnlockTime", 5, false )]
        public virtual BigInteger TokensUnlockTime { get; set; }
    }

    public partial class TokenPriceSetEventDTO : TokenPriceSetEventDTOBase { }

    [Event("TokenPriceSet")]
    public class TokenPriceSetEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "newPrice", 1, false )]
        public virtual BigInteger NewPrice { get; set; }
    }

    public partial class TokensSoldEventDTO : TokensSoldEventDTOBase { }

    [Event("TokensSold")]
    public class TokensSoldEventDTOBase : IEventDTO
    {
        [Parameter("address", "user", 1, false )]
        public virtual string User { get; set; }
        [Parameter("uint256", "amount", 2, false )]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class TokensWithdrawnEventDTO : TokensWithdrawnEventDTOBase { }

    [Event("TokensWithdrawn")]
    public class TokensWithdrawnEventDTOBase : IEventDTO
    {
        [Parameter("address", "user", 1, false )]
        public virtual string User { get; set; }
        [Parameter("uint256", "amount", 2, false )]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class UserRegisteredEventDTO : UserRegisteredEventDTOBase { }

    [Event("UserRegistered")]
    public class UserRegisteredEventDTOBase : IEventDTO
    {
        [Parameter("address", "user", 1, false )]
        public virtual string User { get; set; }
        [Parameter("uint256", "roundId", 2, false )]
        public virtual BigInteger RoundId { get; set; }
    }

    public partial class AddressToRoundRegisteredForOutputDTO : AddressToRoundRegisteredForOutputDTOBase { }

    [FunctionOutput]
    public class AddressToRoundRegisteredForOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class AdminOutputDTO : AdminOutputDTOBase { }

    [FunctionOutput]
    public class AdminOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class AllocationStakingContractOutputDTO : AllocationStakingContractOutputDTOBase { }

    [FunctionOutput]
    public class AllocationStakingContractOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class CheckParticipationSignatureOutputDTO : CheckParticipationSignatureOutputDTOBase { }

    [FunctionOutput]
    public class CheckParticipationSignatureOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class CheckRegistrationSignatureOutputDTO : CheckRegistrationSignatureOutputDTOBase { }

    [FunctionOutput]
    public class CheckRegistrationSignatureOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }





    public partial class FactoryOutputDTO : FactoryOutputDTOBase { }

    [FunctionOutput]
    public class FactoryOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetCurrentRoundOutputDTO : GetCurrentRoundOutputDTOBase { }

    [FunctionOutput]
    public class GetCurrentRoundOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetNumberOfRegisteredUsersOutputDTO : GetNumberOfRegisteredUsersOutputDTOBase { }

    [FunctionOutput]
    public class GetNumberOfRegisteredUsersOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetParticipationOutputDTO : GetParticipationOutputDTOBase { }

    [FunctionOutput]
    public class GetParticipationOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
        [Parameter("uint256", "", 2)]
        public virtual BigInteger ReturnValue2 { get; set; }
        [Parameter("uint256", "", 3)]
        public virtual BigInteger ReturnValue3 { get; set; }
        [Parameter("uint256", "", 4)]
        public virtual BigInteger ReturnValue4 { get; set; }
        [Parameter("bool[]", "", 5)]
        public virtual List<bool> ReturnValue5 { get; set; }
    }

    public partial class GetParticipationSignerOutputDTO : GetParticipationSignerOutputDTOBase { }

    [FunctionOutput]
    public class GetParticipationSignerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetVestingInfoOutputDTO : GetVestingInfoOutputDTOBase { }

    [FunctionOutput]
    public class GetVestingInfoOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256[]", "", 1)]
        public virtual List<BigInteger> ReturnValue1 { get; set; }
        [Parameter("uint256[]", "", 2)]
        public virtual List<BigInteger> ReturnValue2 { get; set; }
    }

    public partial class IsParticipatedOutputDTO : IsParticipatedOutputDTOBase { }

    [FunctionOutput]
    public class IsParticipatedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class MaxVestingTimeShiftOutputDTO : MaxVestingTimeShiftOutputDTOBase { }

    [FunctionOutput]
    public class MaxVestingTimeShiftOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class NumberOfParticipantsOutputDTO : NumberOfParticipantsOutputDTOBase { }

    [FunctionOutput]
    public class NumberOfParticipantsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class OneOutputDTO : OneOutputDTOBase { }

    [FunctionOutput]
    public class OneOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class PortionVestingPrecisionOutputDTO : PortionVestingPrecisionOutputDTOBase { }

    [FunctionOutput]
    public class PortionVestingPrecisionOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }





    public partial class RegistrationOutputDTO : RegistrationOutputDTOBase { }

    [FunctionOutput]
    public class RegistrationOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "registrationTimeStarts", 1)]
        public virtual BigInteger RegistrationTimeStarts { get; set; }
        [Parameter("uint256", "registrationTimeEnds", 2)]
        public virtual BigInteger RegistrationTimeEnds { get; set; }
        [Parameter("uint256", "numberOfRegistrants", 3)]
        public virtual BigInteger NumberOfRegistrants { get; set; }
    }

    public partial class RegistrationDepositAVAXOutputDTO : RegistrationDepositAVAXOutputDTOBase { }

    [FunctionOutput]
    public class RegistrationDepositAVAXOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class RegistrationFeesOutputDTO : RegistrationFeesOutputDTOBase { }

    [FunctionOutput]
    public class RegistrationFeesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class RoundIdToRoundOutputDTO : RoundIdToRoundOutputDTOBase { }

    [FunctionOutput]
    public class RoundIdToRoundOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "startTime", 1)]
        public virtual BigInteger StartTime { get; set; }
        [Parameter("uint256", "maxParticipation", 2)]
        public virtual BigInteger MaxParticipation { get; set; }
    }

    public partial class RoundIdsOutputDTO : RoundIdsOutputDTOBase { }

    [FunctionOutput]
    public class RoundIdsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class SaleOutputDTO : SaleOutputDTOBase { }

    [FunctionOutput]
    public class SaleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "token", 1)]
        public virtual string Token { get; set; }
        [Parameter("bool", "isCreated", 2)]
        public virtual bool IsCreated { get; set; }
        [Parameter("bool", "earningsWithdrawn", 3)]
        public virtual bool EarningsWithdrawn { get; set; }
        [Parameter("bool", "tokensDeposited", 4)]
        public virtual bool TokensDeposited { get; set; }
        [Parameter("address", "saleOwner", 5)]
        public virtual string SaleOwner { get; set; }
        [Parameter("uint256", "tokenPriceInAVAX", 6)]
        public virtual BigInteger TokenPriceInAVAX { get; set; }
        [Parameter("uint256", "amountOfTokensToSell", 7)]
        public virtual BigInteger AmountOfTokensToSell { get; set; }
        [Parameter("uint256", "totalTokensSold", 8)]
        public virtual BigInteger TotalTokensSold { get; set; }
        [Parameter("uint256", "totalAVAXRaised", 9)]
        public virtual BigInteger TotalAVAXRaised { get; set; }
        [Parameter("uint256", "saleEnd", 10)]
        public virtual BigInteger SaleEnd { get; set; }
        [Parameter("uint256", "tokensUnlockTime", 11)]
        public virtual BigInteger TokensUnlockTime { get; set; }
    }















    public partial class StakingRoundIdOutputDTO : StakingRoundIdOutputDTOBase { }

    [FunctionOutput]
    public class StakingRoundIdOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class UserToParticipationOutputDTO : UserToParticipationOutputDTOBase { }

    [FunctionOutput]
    public class UserToParticipationOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "amountBought", 1)]
        public virtual BigInteger AmountBought { get; set; }
        [Parameter("uint256", "amountAVAXPaid", 2)]
        public virtual BigInteger AmountAVAXPaid { get; set; }
        [Parameter("uint256", "timeParticipated", 3)]
        public virtual BigInteger TimeParticipated { get; set; }
        [Parameter("uint256", "roundId", 4)]
        public virtual BigInteger RoundId { get; set; }
    }

    public partial class VestingPercentPerPortionOutputDTO : VestingPercentPerPortionOutputDTOBase { }

    [FunctionOutput]
    public class VestingPercentPerPortionOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class VestingPortionsUnlockTimeOutputDTO : VestingPortionsUnlockTimeOutputDTOBase { }

    [FunctionOutput]
    public class VestingPortionsUnlockTimeOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }






}
