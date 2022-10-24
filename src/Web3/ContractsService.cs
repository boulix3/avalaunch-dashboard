using System.Numerics;
using AvalaunchDashboard.Shared;

namespace AvalaunchDashboard.Web3;
public class ContractsService
{
    private Nethereum.Web3.Web3 web3;
    private ContractsService()
    {
        web3 = new Nethereum.Web3.Web3("https://api.avax.network/ext/bc/C/rpc");
    }
    public static ContractsService Init()
    {
        return new ContractsService();
    }
    public static readonly string[] factories = new string[]{
        "0x4c858df3BebBa1CDB73f49B002f095bB15Df4542",
        "0x23a391BFc5599f8c02AE121125536E3D72D19179",
        "0x0e5505404c0bfC6FC9F70bb1E7D015B7daAC2FC6",
        "0x86F0942d25859f0791cc3D568bA0A099bbe1Ee69",
        "0x9eB3fEF2963b359562694c391A6DAf18322FB2c6",
        "0xd124d278Ad66E383Dc789D593FC719f7D416D172",
        "0x29F351cdd647195553263924Cc3Abb017CB7fC7b"
    };
    public async Task<SaleInfo[]> GetSalesInfos(string[] factoryContracts)
    {
        var result = new List<SaleInfo>();
        foreach (var factoryContract in factoryContracts)
        {
            var service = new Avalaunch.SalesFactory.SalesFactoryService(web3, factoryContract);
            var nbSales = await service.GetNumberOfSalesDeployedQueryAsync();
            var salesContracts = await service.GetAllSalesQueryAsync(0, nbSales);
            foreach (var saleContract in salesContracts)
            {
                try
                {
                    var info = await GetInfo(saleContract);
                    if (info != null)
                    {
                        result.Add(info);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"exception in contract {saleContract} - {e.Message}");
                }
            }
        }
        return result.OrderByDescending(x => x.Time).ToArray();
    }

    public async Task<UserData> GetUserData(string address, SaleInfo[] saleInfos)
    {
        var vestingInfos = new Dictionary<string, UserVestingInfo>();
        foreach (var saleInfo in saleInfos)
        {
            try
            {
                // extract from contract (solidity)
                //return (
                //     p.amountBought,
                //     p.amountAVAXPaid,
                //     p.timeParticipated,
                //     p.roundId,
                //     p.isPortionWithdrawn
                // );
                var contract = new Avalaunch.Sale0.Sale0Service(web3, saleInfo.Address);
                var participation = await contract.GetParticipationQueryAsync(address);
                var totalTokens = participation.ReturnValue1;
                var totalAvax = participation.ReturnValue2;
                var withdrawnPortions = participation.ReturnValue5;
                vestingInfos.Add(saleInfo.Address,
                    new UserVestingInfo(withdrawnPortions.ToArray(), totalTokens, totalAvax));
            }
            catch (Exception)
            {
                // try next contract type
            }
            try
            {
                // extract from contract (solidity)
                // return (
                //     p.amountBought,
                //     p.amountAVAXPaid,
                //     p.timeParticipated,
                //     p.roundId,
                //     p.isPortionWithdrawn,
                //     p.isPortionWithdrawnToDexalot,
                //     p.isParticipationBoosted,
                //     p.boostedAmountBought,
                //     p.boostedAmountAVAXPaid
                // );
                var contract = new Avalaunch.Sale1.Sale1Service(web3, saleInfo.Address);
                var participation = await contract.GetParticipationQueryAsync(address);
                var totalTokens = participation.ReturnValue1;
                var totalAvax = participation.ReturnValue2;
                var withdrawnPortions = participation.ReturnValue5;
                var dexalotWithdrawnPortions = participation.ReturnValue6;
                vestingInfos.Add(saleInfo.Address,
                    new UserVestingInfo(AndArray(withdrawnPortions, dexalotWithdrawnPortions), totalTokens, totalAvax));
            }
            catch (Exception)
            {
                // try next contract type
            }
            try
            {
                // extract from contract (solidity)
                //return (
                //     p.amountBought,
                //     p.amountAVAXPaid,
                //     p.timeParticipated,
                //     p.roundId,
                //     p.isPortionWithdrawn
                // );
                var contract = new Avalaunch.Sale2.Sale2Service(web3, saleInfo.Address);
                var participation = await contract.GetParticipationQueryAsync(address);
                var totalTokens = participation.ReturnValue1;
                var totalAvax = participation.ReturnValue2;
                var withdrawnPortions = participation.ReturnValue5;
                vestingInfos.Add(saleInfo.Address,
                    new UserVestingInfo(withdrawnPortions.ToArray(), totalTokens, totalAvax));
            }
            catch (Exception)
            {
                // try next contract type
            }
        }
        var result = new UserData(DateTimeOffset.UtcNow.ToUnixTimeSeconds(), vestingInfos);
        return result;
    }
    public bool[] AndArray(List<bool> array1, List<bool> array2)
    {
        var result = array1.ToArray();
        for (var i = 0; i < array2.Count; i++)
        {
            if (array2[i])
            {
                result[i] = true;
            }
        }
        return result;
    }

    async Task<SaleInfo?> GetInfo(string contractAddress)
    {
        try
        {
            var contract = new Avalaunch.Sale1.Sale1Service(web3, contractAddress);
            var info = await contract.SaleQueryAsync();
            if (info.IsCreated && info.SaleEnd > 0 && info.TotalTokensSold > 0)
            {
                var vestingPrecision = contract.PortionVestingPrecisionQueryAsync();
                var vesting = await contract.GetVestingInfoQueryAsync();
                return await GetInfo(contractAddress, info.Token, (long)info.SaleEnd, vesting.ReturnValue1, vesting.ReturnValue2, await vestingPrecision);
            }
        }
        catch (Exception)
        {
            // we try next sale contract type
        }
        try
        {
            var contract = new Avalaunch.Sale2.Sale2Service(web3, contractAddress);
            var info = await contract.SaleQueryAsync();
            if (info.IsCreated && info.SaleEnd > 0 && info.TotalTokensSold > 0)
            {
                var vestingPrecision = contract.PortionVestingPrecisionQueryAsync();
                var vesting = await contract.GetVestingInfoQueryAsync();
                return await GetInfo(contractAddress, info.Token, (long)info.SaleEnd, vesting.ReturnValue1, vesting.ReturnValue2, await vestingPrecision);
            }
        }
        catch (Exception)
        {
            // we try next sale contract type
        }
        try
        {
            var contract = new Avalaunch.Sale0.Sale0Service(web3, contractAddress);
            var info = await contract.SaleQueryAsync();
            if (info.IsCreated && info.SaleEnd > 0 && info.TotalTokensSold > 0)
            {
                var vestingPrecision = contract.PortionVestingPrecisionQueryAsync();
                var vesting = await contract.GetVestingInfoQueryAsync();
                return await GetInfo(contractAddress, info.Token, (long)info.SaleEnd, vesting.ReturnValue1, vesting.ReturnValue2, await vestingPrecision);
            }
        }
        catch (Exception)
        {
            // we try next sale contract type
        }
        return null;
    }

    async Task<SaleInfo?> GetInfo(string saleContractAdress, string tokenAdress, long saleEnd, IEnumerable<BigInteger> vestingTimes,
            IEnumerable<BigInteger> vestingPortions, BigInteger vestingPortionPrecision)
    {
        try
        {
            var erc20Contract = new Avalaunch.Erc20.Erc20Service(web3, tokenAdress);
            var tokenName = erc20Contract.NameQueryAsync();
            var tokenSymbol = erc20Contract.SymbolQueryAsync();
            var tokenDecimals = erc20Contract.DecimalsQueryAsync();
            var saleInfo = new SaleInfo(saleContractAdress, await tokenName, await tokenSymbol, await tokenDecimals, tokenAdress, saleEnd,
                    vestingTimes.ToLongArray(), vestingPortions.ToLongArray(), vestingPortionPrecision.ToLong());
            return saleInfo;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while getting erc20 contract info {tokenAdress} - {e.Message}");
        }
        return null;
    }

}
