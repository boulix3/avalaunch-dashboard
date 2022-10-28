using System.Collections.Concurrent;
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
        "0x4c858df3BebBa1CDB73f49B002f095bB15Df4542", //345
        "0x23a391BFc5599f8c02AE121125536E3D72D19179", //421
        "0x0e5505404c0bfC6FC9F70bb1E7D015B7daAC2FC6",
        "0x86F0942d25859f0791cc3D568bA0A099bbe1Ee69",
        "0x9eB3fEF2963b359562694c391A6DAf18322FB2c6",
        "0xd124d278Ad66E383Dc789D593FC719f7D416D172",
        "0x29F351cdd647195553263924Cc3Abb017CB7fC7b"
    };
    public async Task<SaleData> GetSalesData(string[] factoryContracts)
    {
        var tasks = new List<Task<Dictionary<string, SaleInfo>>>();
        foreach (var factoryContract in factoryContracts)
        {
            tasks.Add(GetSalesData(factoryContract));
        }
        var result = new SaleData();
        result.LastUpdated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var dictionaryList = await Task.WhenAll(tasks);
        foreach (var dictionary in dictionaryList)
        {
            foreach (var item in dictionary)
            {
                if (!result.Items.ContainsKey(item.Key))
                {
                    result.Items.Add(item.Key, item.Value);
                }
            }
        }
        return result;
    }
    public async Task<Dictionary<string, SaleInfo>> GetSalesData(string factoryContract)
    {
        var tasks = new List<Task<KeyValuePair<string, SaleInfo>?>>();
        var service = new Avalaunch.SalesFactory.SalesFactoryService(web3, factoryContract);
        var nbSales = await service.GetNumberOfSalesDeployedQueryAsync();
        var salesContracts = await service.GetAllSalesQueryAsync(0, nbSales);
        foreach (var saleContract in salesContracts)
        {
            tasks.Add(GetSaleInfo(saleContract.ToLower()));
        }
        var salesInfos = await Task.WhenAll(tasks);
        var result = salesInfos.WhereNotNull().ToDictionary(x => x.Key, y => y.Value);
        return result;
    }
    public async Task<KeyValuePair<string, SaleInfo>?> GetSaleInfo(string address)
    {
        var info = await GetInfo(address);
        if (info != null)
        {
            return new KeyValuePair<string, SaleInfo>(address, info);
        }
        return null;
    }

    public async Task<KeyValuePair<string, UserVestingInfo>?> GetUserData(string address, SaleInfo saleInfo)
    {
        UserVestingInfo? vestingInfo = null;
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
            vestingInfo = new UserVestingInfo(withdrawnPortions.ToArray(), totalTokens, totalAvax);
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
            vestingInfo = new UserVestingInfo(withdrawnPortions.ToArray(), totalTokens, totalAvax);
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
            vestingInfo = new UserVestingInfo(withdrawnPortions.ToArray(), totalTokens, totalAvax);
        }
        catch (Exception)
        {
            // try next contract type
        }
        if (vestingInfo != null)
        {
            return new KeyValuePair<string, UserVestingInfo>(saleInfo.Address, vestingInfo);
        }
        return null;
    }
    public async Task<UserData> GetUserData(string address, SaleData saleInfos)
    {
        var tasks = new List<Task<KeyValuePair<string, UserVestingInfo>?>>();
        foreach (var item in saleInfos.Items.Values)
        {
            tasks.Add(GetUserData(address, item));
        }
        var vestingInfos = await Task.WhenAll(tasks);
        var dictionary = vestingInfos.WhereNotNull()
            .ToDictionary(x => x.Key, y => y.Value);
        var result = new UserData(DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            dictionary);
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

    async Task<SaleInfo?> GetInfo(string saleContractAddress, string tokenAddress, long saleEnd, IEnumerable<BigInteger> vestingTimes,
            IEnumerable<BigInteger> vestingPortions, BigInteger vestingPortionPrecision)
    {
        try
        {
            var erc20Contract = new Avalaunch.Erc20.Erc20Service(web3, tokenAddress);
            var tokenName = erc20Contract.NameQueryAsync();
            var tokenSymbol = erc20Contract.SymbolQueryAsync();
            var tokenDecimals = erc20Contract.DecimalsQueryAsync();
            var saleInfo = new SaleInfo(saleContractAddress.ToLower(), await tokenName, await tokenSymbol, await tokenDecimals, tokenAddress.ToLower(), saleEnd,
                    vestingTimes.ToLongArray(), vestingPortions.ToLongArray(), vestingPortionPrecision.ToLong());
            return saleInfo;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while getting erc20 contract info {tokenAddress} - {e.Message}");
        }
        return null;
    }

}
