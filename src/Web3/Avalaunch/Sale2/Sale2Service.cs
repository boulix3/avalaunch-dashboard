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
using Avalaunch.Sale2.ContractDefinition;

namespace Avalaunch.Sale2
{
    public partial class Sale2Service
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, Sale2Deployment sale2Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<Sale2Deployment>().SendRequestAndWaitForReceiptAsync(sale2Deployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, Sale2Deployment sale2Deployment)
        {
            return web3.Eth.GetContractDeploymentHandler<Sale2Deployment>().SendRequestAsync(sale2Deployment);
        }

        public static async Task<Sale2Service> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, Sale2Deployment sale2Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, sale2Deployment, cancellationTokenSource);
            return new Sale2Service(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public Sale2Service(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Sale2Service(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> AddressToRoundRegisteredForQueryAsync(AddressToRoundRegisteredForFunction addressToRoundRegisteredForFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressToRoundRegisteredForFunction, BigInteger>(addressToRoundRegisteredForFunction, blockParameter);
        }

        
        public Task<BigInteger> AddressToRoundRegisteredForQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var addressToRoundRegisteredForFunction = new AddressToRoundRegisteredForFunction();
                addressToRoundRegisteredForFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<AddressToRoundRegisteredForFunction, BigInteger>(addressToRoundRegisteredForFunction, blockParameter);
        }

        public Task<string> AdminQueryAsync(AdminFunction adminFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AdminFunction, string>(adminFunction, blockParameter);
        }

        
        public Task<string> AdminQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AdminFunction, string>(null, blockParameter);
        }

        public Task<string> AllocationStakingContractQueryAsync(AllocationStakingContractFunction allocationStakingContractFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AllocationStakingContractFunction, string>(allocationStakingContractFunction, blockParameter);
        }

        
        public Task<string> AllocationStakingContractQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AllocationStakingContractFunction, string>(null, blockParameter);
        }

        public Task<bool> CheckParticipationSignatureQueryAsync(CheckParticipationSignatureFunction checkParticipationSignatureFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CheckParticipationSignatureFunction, bool>(checkParticipationSignatureFunction, blockParameter);
        }

        
        public Task<bool> CheckParticipationSignatureQueryAsync(byte[] signature, string user, BigInteger amount, BigInteger amountXavaToBurn, BigInteger round, BlockParameter blockParameter = null)
        {
            var checkParticipationSignatureFunction = new CheckParticipationSignatureFunction();
                checkParticipationSignatureFunction.Signature = signature;
                checkParticipationSignatureFunction.User = user;
                checkParticipationSignatureFunction.Amount = amount;
                checkParticipationSignatureFunction.AmountXavaToBurn = amountXavaToBurn;
                checkParticipationSignatureFunction.Round = round;
            
            return ContractHandler.QueryAsync<CheckParticipationSignatureFunction, bool>(checkParticipationSignatureFunction, blockParameter);
        }

        public Task<bool> CheckRegistrationSignatureQueryAsync(CheckRegistrationSignatureFunction checkRegistrationSignatureFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CheckRegistrationSignatureFunction, bool>(checkRegistrationSignatureFunction, blockParameter);
        }

        
        public Task<bool> CheckRegistrationSignatureQueryAsync(byte[] signature, string user, BigInteger roundId, BlockParameter blockParameter = null)
        {
            var checkRegistrationSignatureFunction = new CheckRegistrationSignatureFunction();
                checkRegistrationSignatureFunction.Signature = signature;
                checkRegistrationSignatureFunction.User = user;
                checkRegistrationSignatureFunction.RoundId = roundId;
            
            return ContractHandler.QueryAsync<CheckRegistrationSignatureFunction, bool>(checkRegistrationSignatureFunction, blockParameter);
        }

        public Task<string> DepositTokensRequestAsync(DepositTokensFunction depositTokensFunction)
        {
             return ContractHandler.SendRequestAsync(depositTokensFunction);
        }

        public Task<string> DepositTokensRequestAsync()
        {
             return ContractHandler.SendRequestAsync<DepositTokensFunction>();
        }

        public Task<TransactionReceipt> DepositTokensRequestAndWaitForReceiptAsync(DepositTokensFunction depositTokensFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(depositTokensFunction, cancellationToken);
        }

        public Task<TransactionReceipt> DepositTokensRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<DepositTokensFunction>(null, cancellationToken);
        }

        public Task<string> ExtendRegistrationPeriodRequestAsync(ExtendRegistrationPeriodFunction extendRegistrationPeriodFunction)
        {
             return ContractHandler.SendRequestAsync(extendRegistrationPeriodFunction);
        }

        public Task<TransactionReceipt> ExtendRegistrationPeriodRequestAndWaitForReceiptAsync(ExtendRegistrationPeriodFunction extendRegistrationPeriodFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(extendRegistrationPeriodFunction, cancellationToken);
        }

        public Task<string> ExtendRegistrationPeriodRequestAsync(BigInteger timeToAdd)
        {
            var extendRegistrationPeriodFunction = new ExtendRegistrationPeriodFunction();
                extendRegistrationPeriodFunction.TimeToAdd = timeToAdd;
            
             return ContractHandler.SendRequestAsync(extendRegistrationPeriodFunction);
        }

        public Task<TransactionReceipt> ExtendRegistrationPeriodRequestAndWaitForReceiptAsync(BigInteger timeToAdd, CancellationTokenSource cancellationToken = null)
        {
            var extendRegistrationPeriodFunction = new ExtendRegistrationPeriodFunction();
                extendRegistrationPeriodFunction.TimeToAdd = timeToAdd;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(extendRegistrationPeriodFunction, cancellationToken);
        }

        public Task<string> FactoryQueryAsync(FactoryFunction factoryFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FactoryFunction, string>(factoryFunction, blockParameter);
        }

        
        public Task<string> FactoryQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FactoryFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> GetCurrentRoundQueryAsync(GetCurrentRoundFunction getCurrentRoundFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetCurrentRoundFunction, BigInteger>(getCurrentRoundFunction, blockParameter);
        }

        
        public Task<BigInteger> GetCurrentRoundQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetCurrentRoundFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> GetNumberOfRegisteredUsersQueryAsync(GetNumberOfRegisteredUsersFunction getNumberOfRegisteredUsersFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetNumberOfRegisteredUsersFunction, BigInteger>(getNumberOfRegisteredUsersFunction, blockParameter);
        }

        
        public Task<BigInteger> GetNumberOfRegisteredUsersQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetNumberOfRegisteredUsersFunction, BigInteger>(null, blockParameter);
        }

        public Task<GetParticipationOutputDTO> GetParticipationQueryAsync(GetParticipationFunction getParticipationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetParticipationFunction, GetParticipationOutputDTO>(getParticipationFunction, blockParameter);
        }

        public Task<GetParticipationOutputDTO> GetParticipationQueryAsync(string user, BlockParameter blockParameter = null)
        {
            var getParticipationFunction = new GetParticipationFunction();
                getParticipationFunction.User = user;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetParticipationFunction, GetParticipationOutputDTO>(getParticipationFunction, blockParameter);
        }

        public Task<string> GetParticipationSignerQueryAsync(GetParticipationSignerFunction getParticipationSignerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetParticipationSignerFunction, string>(getParticipationSignerFunction, blockParameter);
        }

        
        public Task<string> GetParticipationSignerQueryAsync(byte[] signature, string user, BigInteger amount, BigInteger amountXavaToBurn, BigInteger roundId, BlockParameter blockParameter = null)
        {
            var getParticipationSignerFunction = new GetParticipationSignerFunction();
                getParticipationSignerFunction.Signature = signature;
                getParticipationSignerFunction.User = user;
                getParticipationSignerFunction.Amount = amount;
                getParticipationSignerFunction.AmountXavaToBurn = amountXavaToBurn;
                getParticipationSignerFunction.RoundId = roundId;
            
            return ContractHandler.QueryAsync<GetParticipationSignerFunction, string>(getParticipationSignerFunction, blockParameter);
        }

        public Task<GetVestingInfoOutputDTO> GetVestingInfoQueryAsync(GetVestingInfoFunction getVestingInfoFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetVestingInfoFunction, GetVestingInfoOutputDTO>(getVestingInfoFunction, blockParameter);
        }

        public Task<GetVestingInfoOutputDTO> GetVestingInfoQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetVestingInfoFunction, GetVestingInfoOutputDTO>(null, blockParameter);
        }

        public Task<bool> IsParticipatedQueryAsync(IsParticipatedFunction isParticipatedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsParticipatedFunction, bool>(isParticipatedFunction, blockParameter);
        }

        
        public Task<bool> IsParticipatedQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var isParticipatedFunction = new IsParticipatedFunction();
                isParticipatedFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<IsParticipatedFunction, bool>(isParticipatedFunction, blockParameter);
        }

        public Task<BigInteger> MaxVestingTimeShiftQueryAsync(MaxVestingTimeShiftFunction maxVestingTimeShiftFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MaxVestingTimeShiftFunction, BigInteger>(maxVestingTimeShiftFunction, blockParameter);
        }

        
        public Task<BigInteger> MaxVestingTimeShiftQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MaxVestingTimeShiftFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> NumberOfParticipantsQueryAsync(NumberOfParticipantsFunction numberOfParticipantsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NumberOfParticipantsFunction, BigInteger>(numberOfParticipantsFunction, blockParameter);
        }

        
        public Task<BigInteger> NumberOfParticipantsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NumberOfParticipantsFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> OneQueryAsync(OneFunction oneFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OneFunction, BigInteger>(oneFunction, blockParameter);
        }

        
        public Task<BigInteger> OneQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OneFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> ParticipateRequestAsync(ParticipateFunction participateFunction)
        {
             return ContractHandler.SendRequestAsync(participateFunction);
        }

        public Task<TransactionReceipt> ParticipateRequestAndWaitForReceiptAsync(ParticipateFunction participateFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(participateFunction, cancellationToken);
        }

        public Task<string> ParticipateRequestAsync(byte[] signature, BigInteger amount, BigInteger amountXavaToBurn, BigInteger roundId)
        {
            var participateFunction = new ParticipateFunction();
                participateFunction.Signature = signature;
                participateFunction.Amount = amount;
                participateFunction.AmountXavaToBurn = amountXavaToBurn;
                participateFunction.RoundId = roundId;
            
             return ContractHandler.SendRequestAsync(participateFunction);
        }

        public Task<TransactionReceipt> ParticipateRequestAndWaitForReceiptAsync(byte[] signature, BigInteger amount, BigInteger amountXavaToBurn, BigInteger roundId, CancellationTokenSource cancellationToken = null)
        {
            var participateFunction = new ParticipateFunction();
                participateFunction.Signature = signature;
                participateFunction.Amount = amount;
                participateFunction.AmountXavaToBurn = amountXavaToBurn;
                participateFunction.RoundId = roundId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(participateFunction, cancellationToken);
        }

        public Task<BigInteger> PortionVestingPrecisionQueryAsync(PortionVestingPrecisionFunction portionVestingPrecisionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PortionVestingPrecisionFunction, BigInteger>(portionVestingPrecisionFunction, blockParameter);
        }

        
        public Task<BigInteger> PortionVestingPrecisionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PortionVestingPrecisionFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> PostponeSaleRequestAsync(PostponeSaleFunction postponeSaleFunction)
        {
             return ContractHandler.SendRequestAsync(postponeSaleFunction);
        }

        public Task<TransactionReceipt> PostponeSaleRequestAndWaitForReceiptAsync(PostponeSaleFunction postponeSaleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(postponeSaleFunction, cancellationToken);
        }

        public Task<string> PostponeSaleRequestAsync(BigInteger timeToShift)
        {
            var postponeSaleFunction = new PostponeSaleFunction();
                postponeSaleFunction.TimeToShift = timeToShift;
            
             return ContractHandler.SendRequestAsync(postponeSaleFunction);
        }

        public Task<TransactionReceipt> PostponeSaleRequestAndWaitForReceiptAsync(BigInteger timeToShift, CancellationTokenSource cancellationToken = null)
        {
            var postponeSaleFunction = new PostponeSaleFunction();
                postponeSaleFunction.TimeToShift = timeToShift;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(postponeSaleFunction, cancellationToken);
        }

        public Task<string> RegisterForSaleRequestAsync(RegisterForSaleFunction registerForSaleFunction)
        {
             return ContractHandler.SendRequestAsync(registerForSaleFunction);
        }

        public Task<TransactionReceipt> RegisterForSaleRequestAndWaitForReceiptAsync(RegisterForSaleFunction registerForSaleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerForSaleFunction, cancellationToken);
        }

        public Task<string> RegisterForSaleRequestAsync(byte[] signature, BigInteger roundId)
        {
            var registerForSaleFunction = new RegisterForSaleFunction();
                registerForSaleFunction.Signature = signature;
                registerForSaleFunction.RoundId = roundId;
            
             return ContractHandler.SendRequestAsync(registerForSaleFunction);
        }

        public Task<TransactionReceipt> RegisterForSaleRequestAndWaitForReceiptAsync(byte[] signature, BigInteger roundId, CancellationTokenSource cancellationToken = null)
        {
            var registerForSaleFunction = new RegisterForSaleFunction();
                registerForSaleFunction.Signature = signature;
                registerForSaleFunction.RoundId = roundId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerForSaleFunction, cancellationToken);
        }

        public Task<RegistrationOutputDTO> RegistrationQueryAsync(RegistrationFunction registrationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<RegistrationFunction, RegistrationOutputDTO>(registrationFunction, blockParameter);
        }

        public Task<RegistrationOutputDTO> RegistrationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<RegistrationFunction, RegistrationOutputDTO>(null, blockParameter);
        }

        public Task<BigInteger> RegistrationDepositAVAXQueryAsync(RegistrationDepositAVAXFunction registrationDepositAVAXFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RegistrationDepositAVAXFunction, BigInteger>(registrationDepositAVAXFunction, blockParameter);
        }

        
        public Task<BigInteger> RegistrationDepositAVAXQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RegistrationDepositAVAXFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> RegistrationFeesQueryAsync(RegistrationFeesFunction registrationFeesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RegistrationFeesFunction, BigInteger>(registrationFeesFunction, blockParameter);
        }

        
        public Task<BigInteger> RegistrationFeesQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RegistrationFeesFunction, BigInteger>(null, blockParameter);
        }

        public Task<RoundIdToRoundOutputDTO> RoundIdToRoundQueryAsync(RoundIdToRoundFunction roundIdToRoundFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<RoundIdToRoundFunction, RoundIdToRoundOutputDTO>(roundIdToRoundFunction, blockParameter);
        }

        public Task<RoundIdToRoundOutputDTO> RoundIdToRoundQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var roundIdToRoundFunction = new RoundIdToRoundFunction();
                roundIdToRoundFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<RoundIdToRoundFunction, RoundIdToRoundOutputDTO>(roundIdToRoundFunction, blockParameter);
        }

        public Task<BigInteger> RoundIdsQueryAsync(RoundIdsFunction roundIdsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RoundIdsFunction, BigInteger>(roundIdsFunction, blockParameter);
        }

        
        public Task<BigInteger> RoundIdsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var roundIdsFunction = new RoundIdsFunction();
                roundIdsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<RoundIdsFunction, BigInteger>(roundIdsFunction, blockParameter);
        }

        public Task<SaleOutputDTO> SaleQueryAsync(SaleFunction saleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<SaleFunction, SaleOutputDTO>(saleFunction, blockParameter);
        }

        public Task<SaleOutputDTO> SaleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<SaleFunction, SaleOutputDTO>(null, blockParameter);
        }

        public Task<string> SetCapPerRoundRequestAsync(SetCapPerRoundFunction setCapPerRoundFunction)
        {
             return ContractHandler.SendRequestAsync(setCapPerRoundFunction);
        }

        public Task<TransactionReceipt> SetCapPerRoundRequestAndWaitForReceiptAsync(SetCapPerRoundFunction setCapPerRoundFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setCapPerRoundFunction, cancellationToken);
        }

        public Task<string> SetCapPerRoundRequestAsync(List<BigInteger> rounds, List<BigInteger> caps)
        {
            var setCapPerRoundFunction = new SetCapPerRoundFunction();
                setCapPerRoundFunction.Rounds = rounds;
                setCapPerRoundFunction.Caps = caps;
            
             return ContractHandler.SendRequestAsync(setCapPerRoundFunction);
        }

        public Task<TransactionReceipt> SetCapPerRoundRequestAndWaitForReceiptAsync(List<BigInteger> rounds, List<BigInteger> caps, CancellationTokenSource cancellationToken = null)
        {
            var setCapPerRoundFunction = new SetCapPerRoundFunction();
                setCapPerRoundFunction.Rounds = rounds;
                setCapPerRoundFunction.Caps = caps;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setCapPerRoundFunction, cancellationToken);
        }

        public Task<string> SetRegistrationTimeRequestAsync(SetRegistrationTimeFunction setRegistrationTimeFunction)
        {
             return ContractHandler.SendRequestAsync(setRegistrationTimeFunction);
        }

        public Task<TransactionReceipt> SetRegistrationTimeRequestAndWaitForReceiptAsync(SetRegistrationTimeFunction setRegistrationTimeFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setRegistrationTimeFunction, cancellationToken);
        }

        public Task<string> SetRegistrationTimeRequestAsync(BigInteger registrationTimeStarts, BigInteger registrationTimeEnds)
        {
            var setRegistrationTimeFunction = new SetRegistrationTimeFunction();
                setRegistrationTimeFunction.RegistrationTimeStarts = registrationTimeStarts;
                setRegistrationTimeFunction.RegistrationTimeEnds = registrationTimeEnds;
            
             return ContractHandler.SendRequestAsync(setRegistrationTimeFunction);
        }

        public Task<TransactionReceipt> SetRegistrationTimeRequestAndWaitForReceiptAsync(BigInteger registrationTimeStarts, BigInteger registrationTimeEnds, CancellationTokenSource cancellationToken = null)
        {
            var setRegistrationTimeFunction = new SetRegistrationTimeFunction();
                setRegistrationTimeFunction.RegistrationTimeStarts = registrationTimeStarts;
                setRegistrationTimeFunction.RegistrationTimeEnds = registrationTimeEnds;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setRegistrationTimeFunction, cancellationToken);
        }

        public Task<string> SetRoundsRequestAsync(SetRoundsFunction setRoundsFunction)
        {
             return ContractHandler.SendRequestAsync(setRoundsFunction);
        }

        public Task<TransactionReceipt> SetRoundsRequestAndWaitForReceiptAsync(SetRoundsFunction setRoundsFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setRoundsFunction, cancellationToken);
        }

        public Task<string> SetRoundsRequestAsync(List<BigInteger> startTimes, List<BigInteger> maxParticipations)
        {
            var setRoundsFunction = new SetRoundsFunction();
                setRoundsFunction.StartTimes = startTimes;
                setRoundsFunction.MaxParticipations = maxParticipations;
            
             return ContractHandler.SendRequestAsync(setRoundsFunction);
        }

        public Task<TransactionReceipt> SetRoundsRequestAndWaitForReceiptAsync(List<BigInteger> startTimes, List<BigInteger> maxParticipations, CancellationTokenSource cancellationToken = null)
        {
            var setRoundsFunction = new SetRoundsFunction();
                setRoundsFunction.StartTimes = startTimes;
                setRoundsFunction.MaxParticipations = maxParticipations;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setRoundsFunction, cancellationToken);
        }

        public Task<string> SetSaleParamsRequestAsync(SetSaleParamsFunction setSaleParamsFunction)
        {
             return ContractHandler.SendRequestAsync(setSaleParamsFunction);
        }

        public Task<TransactionReceipt> SetSaleParamsRequestAndWaitForReceiptAsync(SetSaleParamsFunction setSaleParamsFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSaleParamsFunction, cancellationToken);
        }

        public Task<string> SetSaleParamsRequestAsync(string token, string saleOwner, BigInteger tokenPriceInAVAX, BigInteger amountOfTokensToSell, BigInteger saleEnd, BigInteger tokensUnlockTime, BigInteger portionVestingPrecision, BigInteger stakingRoundId, BigInteger registrationDepositAVAX)
        {
            var setSaleParamsFunction = new SetSaleParamsFunction();
                setSaleParamsFunction.Token = token;
                setSaleParamsFunction.SaleOwner = saleOwner;
                setSaleParamsFunction.TokenPriceInAVAX = tokenPriceInAVAX;
                setSaleParamsFunction.AmountOfTokensToSell = amountOfTokensToSell;
                setSaleParamsFunction.SaleEnd = saleEnd;
                setSaleParamsFunction.TokensUnlockTime = tokensUnlockTime;
                setSaleParamsFunction.PortionVestingPrecision = portionVestingPrecision;
                setSaleParamsFunction.StakingRoundId = stakingRoundId;
                setSaleParamsFunction.RegistrationDepositAVAX = registrationDepositAVAX;
            
             return ContractHandler.SendRequestAsync(setSaleParamsFunction);
        }

        public Task<TransactionReceipt> SetSaleParamsRequestAndWaitForReceiptAsync(string token, string saleOwner, BigInteger tokenPriceInAVAX, BigInteger amountOfTokensToSell, BigInteger saleEnd, BigInteger tokensUnlockTime, BigInteger portionVestingPrecision, BigInteger stakingRoundId, BigInteger registrationDepositAVAX, CancellationTokenSource cancellationToken = null)
        {
            var setSaleParamsFunction = new SetSaleParamsFunction();
                setSaleParamsFunction.Token = token;
                setSaleParamsFunction.SaleOwner = saleOwner;
                setSaleParamsFunction.TokenPriceInAVAX = tokenPriceInAVAX;
                setSaleParamsFunction.AmountOfTokensToSell = amountOfTokensToSell;
                setSaleParamsFunction.SaleEnd = saleEnd;
                setSaleParamsFunction.TokensUnlockTime = tokensUnlockTime;
                setSaleParamsFunction.PortionVestingPrecision = portionVestingPrecision;
                setSaleParamsFunction.StakingRoundId = stakingRoundId;
                setSaleParamsFunction.RegistrationDepositAVAX = registrationDepositAVAX;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSaleParamsFunction, cancellationToken);
        }

        public Task<string> SetSaleTokenRequestAsync(SetSaleTokenFunction setSaleTokenFunction)
        {
             return ContractHandler.SendRequestAsync(setSaleTokenFunction);
        }

        public Task<TransactionReceipt> SetSaleTokenRequestAndWaitForReceiptAsync(SetSaleTokenFunction setSaleTokenFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSaleTokenFunction, cancellationToken);
        }

        public Task<string> SetSaleTokenRequestAsync(string saleToken)
        {
            var setSaleTokenFunction = new SetSaleTokenFunction();
                setSaleTokenFunction.SaleToken = saleToken;
            
             return ContractHandler.SendRequestAsync(setSaleTokenFunction);
        }

        public Task<TransactionReceipt> SetSaleTokenRequestAndWaitForReceiptAsync(string saleToken, CancellationTokenSource cancellationToken = null)
        {
            var setSaleTokenFunction = new SetSaleTokenFunction();
                setSaleTokenFunction.SaleToken = saleToken;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSaleTokenFunction, cancellationToken);
        }

        public Task<string> SetVestingParamsRequestAsync(SetVestingParamsFunction setVestingParamsFunction)
        {
             return ContractHandler.SendRequestAsync(setVestingParamsFunction);
        }

        public Task<TransactionReceipt> SetVestingParamsRequestAndWaitForReceiptAsync(SetVestingParamsFunction setVestingParamsFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setVestingParamsFunction, cancellationToken);
        }

        public Task<string> SetVestingParamsRequestAsync(List<BigInteger> unlockingTimes, List<BigInteger> percents, BigInteger maxVestingTimeShift)
        {
            var setVestingParamsFunction = new SetVestingParamsFunction();
                setVestingParamsFunction.UnlockingTimes = unlockingTimes;
                setVestingParamsFunction.Percents = percents;
                setVestingParamsFunction.MaxVestingTimeShift = maxVestingTimeShift;
            
             return ContractHandler.SendRequestAsync(setVestingParamsFunction);
        }

        public Task<TransactionReceipt> SetVestingParamsRequestAndWaitForReceiptAsync(List<BigInteger> unlockingTimes, List<BigInteger> percents, BigInteger maxVestingTimeShift, CancellationTokenSource cancellationToken = null)
        {
            var setVestingParamsFunction = new SetVestingParamsFunction();
                setVestingParamsFunction.UnlockingTimes = unlockingTimes;
                setVestingParamsFunction.Percents = percents;
                setVestingParamsFunction.MaxVestingTimeShift = maxVestingTimeShift;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setVestingParamsFunction, cancellationToken);
        }

        public Task<string> ShiftVestingUnlockingTimesRequestAsync(ShiftVestingUnlockingTimesFunction shiftVestingUnlockingTimesFunction)
        {
             return ContractHandler.SendRequestAsync(shiftVestingUnlockingTimesFunction);
        }

        public Task<TransactionReceipt> ShiftVestingUnlockingTimesRequestAndWaitForReceiptAsync(ShiftVestingUnlockingTimesFunction shiftVestingUnlockingTimesFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(shiftVestingUnlockingTimesFunction, cancellationToken);
        }

        public Task<string> ShiftVestingUnlockingTimesRequestAsync(BigInteger timeToShift)
        {
            var shiftVestingUnlockingTimesFunction = new ShiftVestingUnlockingTimesFunction();
                shiftVestingUnlockingTimesFunction.TimeToShift = timeToShift;
            
             return ContractHandler.SendRequestAsync(shiftVestingUnlockingTimesFunction);
        }

        public Task<TransactionReceipt> ShiftVestingUnlockingTimesRequestAndWaitForReceiptAsync(BigInteger timeToShift, CancellationTokenSource cancellationToken = null)
        {
            var shiftVestingUnlockingTimesFunction = new ShiftVestingUnlockingTimesFunction();
                shiftVestingUnlockingTimesFunction.TimeToShift = timeToShift;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(shiftVestingUnlockingTimesFunction, cancellationToken);
        }

        public Task<BigInteger> StakingRoundIdQueryAsync(StakingRoundIdFunction stakingRoundIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StakingRoundIdFunction, BigInteger>(stakingRoundIdFunction, blockParameter);
        }

        
        public Task<BigInteger> StakingRoundIdQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StakingRoundIdFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> UpdateTokenPriceInAVAXRequestAsync(UpdateTokenPriceInAVAXFunction updateTokenPriceInAVAXFunction)
        {
             return ContractHandler.SendRequestAsync(updateTokenPriceInAVAXFunction);
        }

        public Task<TransactionReceipt> UpdateTokenPriceInAVAXRequestAndWaitForReceiptAsync(UpdateTokenPriceInAVAXFunction updateTokenPriceInAVAXFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updateTokenPriceInAVAXFunction, cancellationToken);
        }

        public Task<string> UpdateTokenPriceInAVAXRequestAsync(BigInteger price)
        {
            var updateTokenPriceInAVAXFunction = new UpdateTokenPriceInAVAXFunction();
                updateTokenPriceInAVAXFunction.Price = price;
            
             return ContractHandler.SendRequestAsync(updateTokenPriceInAVAXFunction);
        }

        public Task<TransactionReceipt> UpdateTokenPriceInAVAXRequestAndWaitForReceiptAsync(BigInteger price, CancellationTokenSource cancellationToken = null)
        {
            var updateTokenPriceInAVAXFunction = new UpdateTokenPriceInAVAXFunction();
                updateTokenPriceInAVAXFunction.Price = price;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updateTokenPriceInAVAXFunction, cancellationToken);
        }

        public Task<UserToParticipationOutputDTO> UserToParticipationQueryAsync(UserToParticipationFunction userToParticipationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<UserToParticipationFunction, UserToParticipationOutputDTO>(userToParticipationFunction, blockParameter);
        }

        public Task<UserToParticipationOutputDTO> UserToParticipationQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var userToParticipationFunction = new UserToParticipationFunction();
                userToParticipationFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<UserToParticipationFunction, UserToParticipationOutputDTO>(userToParticipationFunction, blockParameter);
        }

        public Task<BigInteger> VestingPercentPerPortionQueryAsync(VestingPercentPerPortionFunction vestingPercentPerPortionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VestingPercentPerPortionFunction, BigInteger>(vestingPercentPerPortionFunction, blockParameter);
        }

        
        public Task<BigInteger> VestingPercentPerPortionQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var vestingPercentPerPortionFunction = new VestingPercentPerPortionFunction();
                vestingPercentPerPortionFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<VestingPercentPerPortionFunction, BigInteger>(vestingPercentPerPortionFunction, blockParameter);
        }

        public Task<BigInteger> VestingPortionsUnlockTimeQueryAsync(VestingPortionsUnlockTimeFunction vestingPortionsUnlockTimeFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VestingPortionsUnlockTimeFunction, BigInteger>(vestingPortionsUnlockTimeFunction, blockParameter);
        }

        
        public Task<BigInteger> VestingPortionsUnlockTimeQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var vestingPortionsUnlockTimeFunction = new VestingPortionsUnlockTimeFunction();
                vestingPortionsUnlockTimeFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<VestingPortionsUnlockTimeFunction, BigInteger>(vestingPortionsUnlockTimeFunction, blockParameter);
        }

        public Task<string> WithdrawEarningsRequestAsync(WithdrawEarningsFunction withdrawEarningsFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawEarningsFunction);
        }

        public Task<string> WithdrawEarningsRequestAsync()
        {
             return ContractHandler.SendRequestAsync<WithdrawEarningsFunction>();
        }

        public Task<TransactionReceipt> WithdrawEarningsRequestAndWaitForReceiptAsync(WithdrawEarningsFunction withdrawEarningsFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawEarningsFunction, cancellationToken);
        }

        public Task<TransactionReceipt> WithdrawEarningsRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<WithdrawEarningsFunction>(null, cancellationToken);
        }

        public Task<string> WithdrawEarningsAndLeftoverRequestAsync(WithdrawEarningsAndLeftoverFunction withdrawEarningsAndLeftoverFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawEarningsAndLeftoverFunction);
        }

        public Task<string> WithdrawEarningsAndLeftoverRequestAsync()
        {
             return ContractHandler.SendRequestAsync<WithdrawEarningsAndLeftoverFunction>();
        }

        public Task<TransactionReceipt> WithdrawEarningsAndLeftoverRequestAndWaitForReceiptAsync(WithdrawEarningsAndLeftoverFunction withdrawEarningsAndLeftoverFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawEarningsAndLeftoverFunction, cancellationToken);
        }

        public Task<TransactionReceipt> WithdrawEarningsAndLeftoverRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<WithdrawEarningsAndLeftoverFunction>(null, cancellationToken);
        }

        public Task<string> WithdrawLeftoverRequestAsync(WithdrawLeftoverFunction withdrawLeftoverFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawLeftoverFunction);
        }

        public Task<string> WithdrawLeftoverRequestAsync()
        {
             return ContractHandler.SendRequestAsync<WithdrawLeftoverFunction>();
        }

        public Task<TransactionReceipt> WithdrawLeftoverRequestAndWaitForReceiptAsync(WithdrawLeftoverFunction withdrawLeftoverFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawLeftoverFunction, cancellationToken);
        }

        public Task<TransactionReceipt> WithdrawLeftoverRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<WithdrawLeftoverFunction>(null, cancellationToken);
        }

        public Task<string> WithdrawMultiplePortionsRequestAsync(WithdrawMultiplePortionsFunction withdrawMultiplePortionsFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawMultiplePortionsFunction);
        }

        public Task<TransactionReceipt> WithdrawMultiplePortionsRequestAndWaitForReceiptAsync(WithdrawMultiplePortionsFunction withdrawMultiplePortionsFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawMultiplePortionsFunction, cancellationToken);
        }

        public Task<string> WithdrawMultiplePortionsRequestAsync(List<BigInteger> portionIds)
        {
            var withdrawMultiplePortionsFunction = new WithdrawMultiplePortionsFunction();
                withdrawMultiplePortionsFunction.PortionIds = portionIds;
            
             return ContractHandler.SendRequestAsync(withdrawMultiplePortionsFunction);
        }

        public Task<TransactionReceipt> WithdrawMultiplePortionsRequestAndWaitForReceiptAsync(List<BigInteger> portionIds, CancellationTokenSource cancellationToken = null)
        {
            var withdrawMultiplePortionsFunction = new WithdrawMultiplePortionsFunction();
                withdrawMultiplePortionsFunction.PortionIds = portionIds;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawMultiplePortionsFunction, cancellationToken);
        }

        public Task<string> WithdrawRegistrationFeesRequestAsync(WithdrawRegistrationFeesFunction withdrawRegistrationFeesFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawRegistrationFeesFunction);
        }

        public Task<string> WithdrawRegistrationFeesRequestAsync()
        {
             return ContractHandler.SendRequestAsync<WithdrawRegistrationFeesFunction>();
        }

        public Task<TransactionReceipt> WithdrawRegistrationFeesRequestAndWaitForReceiptAsync(WithdrawRegistrationFeesFunction withdrawRegistrationFeesFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawRegistrationFeesFunction, cancellationToken);
        }

        public Task<TransactionReceipt> WithdrawRegistrationFeesRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<WithdrawRegistrationFeesFunction>(null, cancellationToken);
        }

        public Task<string> WithdrawTokensRequestAsync(WithdrawTokensFunction withdrawTokensFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawTokensFunction);
        }

        public Task<TransactionReceipt> WithdrawTokensRequestAndWaitForReceiptAsync(WithdrawTokensFunction withdrawTokensFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawTokensFunction, cancellationToken);
        }

        public Task<string> WithdrawTokensRequestAsync(BigInteger portionId)
        {
            var withdrawTokensFunction = new WithdrawTokensFunction();
                withdrawTokensFunction.PortionId = portionId;
            
             return ContractHandler.SendRequestAsync(withdrawTokensFunction);
        }

        public Task<TransactionReceipt> WithdrawTokensRequestAndWaitForReceiptAsync(BigInteger portionId, CancellationTokenSource cancellationToken = null)
        {
            var withdrawTokensFunction = new WithdrawTokensFunction();
                withdrawTokensFunction.PortionId = portionId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawTokensFunction, cancellationToken);
        }

        public Task<string> WithdrawUnusedFundsRequestAsync(WithdrawUnusedFundsFunction withdrawUnusedFundsFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawUnusedFundsFunction);
        }

        public Task<string> WithdrawUnusedFundsRequestAsync()
        {
             return ContractHandler.SendRequestAsync<WithdrawUnusedFundsFunction>();
        }

        public Task<TransactionReceipt> WithdrawUnusedFundsRequestAndWaitForReceiptAsync(WithdrawUnusedFundsFunction withdrawUnusedFundsFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawUnusedFundsFunction, cancellationToken);
        }

        public Task<TransactionReceipt> WithdrawUnusedFundsRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<WithdrawUnusedFundsFunction>(null, cancellationToken);
        }
    }
}
