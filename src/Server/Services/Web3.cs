using System.Numerics;

using AvalaunchDashboard.Shared;

namespace AvalaunchDashboard.Server.Services;
public class Web3
{
    private Nethereum.Web3.Web3 web3;
    private Web3()
    {
        web3 = new Nethereum.Web3.Web3("https://api.avax.network/ext/bc/C/rpc");
    }
    public static Web3 Init()
    {
        return new Web3();
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
    internal async Task<SaleInfo[]> GetSalesInfos(string[] factoryContracts)
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
    async Task<SaleInfo?> GetInfo(string contractAddress)
    {
        try
        {
            try
            {
                var contract = new Avalaunch.Sale.SaleService(web3, contractAddress);
                var info = await contract.SaleQueryAsync();
                if (info.IsCreated && info.SaleEnd > 0 && info.TotalTokensSold > 0)
                {
                    var vestingPrecision = contract.PortionVestingPrecisionQueryAsync();
                    var vesting = await contract.GetVestingInfoQueryAsync();
                    return await GetInfo(contractAddress, info.Token, (long)info.SaleEnd, false, vesting.ReturnValue1, vesting.ReturnValue2, await vestingPrecision);
                }
            }
            catch (Exception)
            {
                var contract = new Avalaunch.OldSale.OldSaleService(web3, contractAddress);
                var info = await contract.SaleQueryAsync();
                if (info.IsCreated && info.SaleEnd > 0 && info.TotalTokensSold > 0)
                {
                    var vestingPrecision = contract.PortionVestingPrecisionQueryAsync();
                    var vesting = await contract.GetVestingInfoQueryAsync();
                    return await GetInfo(contractAddress, info.Token, (long)info.SaleEnd, true, vesting.ReturnValue1, vesting.ReturnValue2, await vestingPrecision);
                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while getting contract info {contractAddress} - {e.Message}");
        }
        return null;
    }

    async Task<SaleInfo?> GetInfo(string saleContractAdress, string tokenAdress, long saleEnd, bool isOldContract, IEnumerable<BigInteger> vestingTimes,
            IEnumerable<BigInteger> vestingPortions, BigInteger vestingPortionPrecision)
    {
        try
        {
            var erc20Contract = new Avalaunch.Erc20.Erc20Service(web3, tokenAdress);
            var tokenName = erc20Contract.NameQueryAsync();
            var tokenSymbol = erc20Contract.SymbolQueryAsync();
            var tokenDecimals = erc20Contract.DecimalsQueryAsync();
            var saleInfo = new SaleInfo(saleContractAdress, await tokenName, await tokenSymbol, await tokenDecimals, tokenAdress, saleEnd, isOldContract,
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
