# Avalaunch Dashboard

This is an experimental dashboard to present [Avalaunch](https://avalaunch.app/) sales data in an original way.

Tech stack :

- [Blazor wasm](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-sdk-templates#blazorwasm) app with asp.net backend (`dotnet new blazorwasm --hosted --pwa`)
- [Nethereum](https://github.com/Nethereum/Nethereum) : To query smart contracts
- [MudBlazor](https://github.com/MudBlazor/MudBlazor) : Material ui components for blazor
- [Firebase](https://firebase.google.com) : To cache the data and have a faster user experience

## Extract sales data

We start with the [SalesFactory](https://github.com/avalaunch-app/xava-protocol/blob/master/contracts/sales/SalesFactory.sol) contracts (addresses are hardcoded in the app). I found the addresses by digging around using [snowtrace](https://snowtrace.io).

SalesFactory deploys [AvalaunchSales](https://github.com/avalaunch-app/xava-protocol/blob/master/contracts/sales/AvalaunchSale.sol) contracts. You can get the list by querying the `getNumberOfSalesDeployed` and `getAllSales` methods.

We can then get the information we need from each sale by querying the contract:

- Sales (property) : Sale's end time and erc20 token address.
- getVestingInfo : The vesting portions and unlock time
- portionVestingPrecision (property) : Precision of the vesting portions obtained with getVestionInfo method.

We then query the Erc20 contract associated with this sale to get the token name, symbol and number of decimals.

It takes about 5 seconds to query all this data from the blockchain. For a better UX, it's cached in a firestore database for instant access.

## User info dashboard

When you go to the dashboard and enter a wallet address, we query the contract by calling the `getParticipation` method. It contains infos about your participation such as the total amount of tokens bought and info about the portions (claimed or not). We can then calculate the amount of tokens for each portion.

By querying [CoinGecko apis](https://www.coingecko.com/en/api), we can then calculate and display interesting information about the current value of the tokens available to claim (in avax and usd).
