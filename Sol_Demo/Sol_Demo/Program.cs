using AsyncLazy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Download following package from Nuget
// AsyncLazy

namespace Sol_Demo
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            #region Sample

            Console.WriteLine("Hello World!");

            var asyncLazy = new AsyncLazy<int>(() => Task.Run(() => 11));

            int value = await asyncLazy.GetValueAsync();
            Console.WriteLine(value);

            #endregion Sample

            #region Singleton

            AsyncLazySingleton asyncLazySingleton = await AsyncLazySingleton.CreateInstance.GetValueAsync();
            await asyncLazySingleton.TestMethodAsync();

            #endregion Singleton

            #region Lazy On Demand

            LazyOnDemand lazyOnDemand = new LazyOnDemand();

            var listName = await lazyOnDemand.ListName.GetValueAsync();

            foreach (var name in listName)
            {
                Console.WriteLine(name);
            }

            #endregion Lazy On Demand
        }
    }

    public class AsyncLazySingleton
    {
        private static AsyncLazy<AsyncLazySingleton> asyncLazySingleton = null;

        private AsyncLazySingleton()
        {
        }

        public static AsyncLazy<AsyncLazySingleton> CreateInstance
        {
            get =>
                    asyncLazySingleton
                        ??
                            (asyncLazySingleton = new AsyncLazy<AsyncLazySingleton>(() => Task.Run(() => new AsyncLazySingleton())));
        }

        public Task TestMethodAsync()
        {
            return Task.Run(() => Console.WriteLine("Test Method"));
        }
    }

    public class LazyOnDemand
    {
        public LazyOnDemand()
        {
            ListName = new AsyncLazy<List<string>>(() => GetListNamesAsync());
        }

        #region Property

        public AsyncLazy<List<String>> ListName { get; set; }

        #endregion Property

        #region Private Method

        private Task<List<String>> GetListNamesAsync()
        {
            return Task.Run(() =>
            {
                var listName = new List<String>();
                listName.Add("Sharmila");
                listName.Add("Mahesh");
                listName.Add("Kishor");

                return listName;
            });
        }

        #endregion Private Method
    }
}