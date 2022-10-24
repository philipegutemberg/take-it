using NBitcoin;

namespace Ethereum.Nethereum.Services
{
    internal class MnemonicService
    {
        public string GetWordsForSeed()
        {
            Mnemonic mnemo = new(Wordlist.English, WordCount.Fifteen);
            return mnemo.ToString();
        }
    }
}