using System;
using System.Collections.Generic;
using System.Text;

namespace penCsharpener.DotnetUtils {
    public static class RandomExtensions {

        /// <summary>
        /// source https://stackoverflow.com/a/271884/6454517
        /// </summary>
        /// <returns></returns>
        public static bool CoinToss(this Random rng) {
            return rng.Next(2) == 0;
        }

        /// <summary>
        /// source https://stackoverflow.com/a/271884/6454517
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="things"></param>
        /// <returns></returns>
        public static T OneOf<T>(this Random rng, params T[] things) {
            return rng.OneOf(thingies: things);
        }

        public static T OneOf<T>(this Random rng, IList<T> thingies) {
            return thingies[rng.Next(thingies.Count)];
        }
    }
}
