using DecodeBytes.Common.Contracts;
using DecodeBytes.Provider;
using System.Reflection;

namespace DecodeBytes.Common.Implementations
{
    public class ProviderService : IProviderService
    {
        // Libs 資料夾存放各銀行的動態函式庫(dll)
        private const string FolderPath = "libs";
        private const string Extension = "*.dll";
        private readonly string _libsPath;

        public ProviderService()
        {
            _libsPath = ApplicationPath.PathTo(FolderPath);
        }

        public IEnumerable<IBankProvider> GetProviders()
        {
            // 掃描 Libs 資料夾下所有的 .dll 檔案
            string[] providers = Directory.GetFiles(_libsPath, Extension);
            foreach (string provider in providers)
            {
                // 載入組件後，建立銀行實例(通過反射 Reflection)
                Assembly assembly = Assembly.LoadFile(provider);
                Type[] assemblyTypes = assembly.GetTypes();
                IEnumerable<Type> providerTypes = assemblyTypes.Where(t => t.GetInterface(nameof(IBankProvider), true) != null);
                foreach (Type providerType in providerTypes)
                {
                    object? instance = Activator.CreateInstance(providerType);
                    if (instance is IBankProvider bankProvider)
                    {
                        // 先回傳一個，等下次有人要再繼續跑
                        yield return bankProvider;
                    }
                }
            }
        }
    }
}
